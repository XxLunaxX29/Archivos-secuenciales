using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Archivos_secuenciales
{
    public partial class Form1 : Form
    {
        private List<string[]> Datas;
        private string[] headers;
        private int selectedRowIndex = -1;
        private string rutaArchivoActual;
        public Form1()
        {
            InitializeComponent();
            ConfigurarDataGridView();

            DgvDATAS.SelectionChanged += DataGridView1_SelectionChanged;

        }

        private void ConfigurarDataGridView()
        {
            // Habilitar modo virtual para mejor rendimiento
            DgvDATAS.VirtualMode = true;
            DgvDATAS.ReadOnly = true;
            DgvDATAS.AllowUserToAddRows = false;
            DgvDATAS.AllowUserToDeleteRows = false;

            // Selección por fila completa para facilitar la edición
            DgvDATAS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvDATAS.MultiSelect = false;

            // Optimizaciones de rendimiento
            DgvDATAS.RowHeadersVisible = false;
            DgvDATAS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // Eventos del modo virtual
            DgvDATAS.CellValueNeeded += DataGridView1_CellValueNeeded;

            // Configuración del grid de edición (una sola fila)
            dataGridView2.ReadOnly = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.CellSelect;

            DgvProperties.AllowUserToAddRows = false;
            DgvProperties.AllowUserToDeleteRows = false;
            DgvProperties.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (DgvProperties.Columns.Count == 0)
            {
                DgvProperties.Columns.Add("Propiedad", "Propiedad");
                DgvProperties.Columns.Add("Valor", "Valor");
            }
        }

        private void CargarArchivo(string rutaArchivo)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                //Leer la primera línea 
                string? firstLine = File.ReadLines(rutaArchivo).FirstOrDefault();

                // Validar si el archivo está vacío
                if (string.IsNullOrEmpty(firstLine))
                {
                    MessageBox.Show("El archivo está vacío.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                headers = firstLine.Split(',');

                // Leer todas las líneas del CSV
                Datas = File.ReadAllLines(rutaArchivo)
                              .Skip(1) // Saltar encabezado
                              .Select(line => line.Split(','))
                              .ToList();

                // Configurar columnas (ajustar según tu CSV)
                DgvDATAS.Columns.Clear();

                for (int i = 0; i < headers.Length; i++)
                {
                    DgvDATAS.Columns.Add($"Column{i}", headers[i]);
                }

                // Establecer número de filas
                DgvDATAS.RowCount = Datas.Count;

                // Preparar grid de edición con las mismas columnas
                dataGridView2.Columns.Clear();
                for (int i = 0; i < headers.Length; i++)
                {
                    dataGridView2.Columns.Add($"EditCol{i}", headers[i]);
                }
                dataGridView2.RowCount = 1; // una fila para editar

                selectedRowIndex = -1;
                DgvDATAS.ClearSelection();

                MessageBox.Show($"Se cargaron {Datas.Count:N0} registros exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el archivo: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void DataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            // Solo proporcionar datos cuando se necesiten (virtualización)
            if (Datas != null && e.RowIndex < Datas.Count)
            {
                var row = Datas[e.RowIndex];
                if (e.ColumnIndex < row.Length)
                {
                    e.Value = row[e.ColumnIndex];
                }
            }
        }

        private void DataGridView1_SelectionChanged(object? sender, EventArgs e)
        {
            if (Datas == null)
            {
                selectedRowIndex = -1;
                return;
            }

            // Guardar edición pendiente de la fila previamente seleccionada
            if (selectedRowIndex >= 0 && selectedRowIndex < Datas.Count)
            {
                SaveEditorToCsvData(selectedRowIndex);
            }

            if (DgvDATAS.SelectedRows.Count == 0)
            {
                selectedRowIndex = -1;
                return;
            }

            var sel = DgvDATAS.SelectedRows[0];
            int rowIndex = sel.Index;
            if (rowIndex < 0 || rowIndex >= Datas.Count)
            {
                selectedRowIndex = -1;
                return;
            }

            selectedRowIndex = rowIndex;
            LoadRowIntoEditor(Datas[rowIndex]);
        }

        private void LoadRowIntoEditor(string[] row)
        {
            // Asegurar columnas coinciden
            int colCount = Math.Min(row.Length, dataGridView2.ColumnCount);

            // Limpiar celdas restantes si hay menos columnas
            for (int i = 0; i < dataGridView2.ColumnCount; i++)
            {
                dataGridView2.Rows[0].Cells[i].Value = i < colCount ? row[i] : string.Empty;
            }
        }

        private void SaveEditorToCsvData(int rowIndex)
        {
            if (Datas == null || headers == null) return;
            if (rowIndex < 0 || rowIndex >= Datas.Count) return;

            // Asegurar que se confirmen los cambios en el DataGridView de edición
            try
            {
                // Finaliza edición en curso y forzar commit
                dataGridView2.EndEdit();
                dataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch
            {
                // Ignorar errores de commit; se seguirá intentando guardar valores actuales de celdas
            }

            var newRow = new string[headers.Length];
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = dataGridView2.Rows[0].Cells[i];
                newRow[i] = cell?.Value?.ToString() ?? string.Empty;
            }

            Datas[rowIndex] = newRow;

            // Refrescar fila en el DataGridView virtual
            DgvDATAS.InvalidateRow(rowIndex);
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            BuscarArchivo();
        }
        private void BuscarArchivo()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos CSV|*.csv| Archivos JSON|*.json| Archivos TXT|*.txt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    rutaArchivoActual = ofd.FileName;
                    CargarArchivo(ofd.FileName);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Datas == null || Datas.Count == 0)
            {
                MessageBox.Show("No hay datos para guardar.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(rutaArchivoActual))
            {
                MessageBox.Show("No se ha cargado ningún archivo para guardar.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Guardar edición actual de la fila seleccionada (si hay)
            if (selectedRowIndex >= 0 && selectedRowIndex < Datas.Count)
            {
                SaveEditorToCsvData(selectedRowIndex);
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                // Construir líneas (primera línea: encabezados)
                var lines = new List<string>
                {
                     string.Join(",", headers)
                };

                // Agregar todas las filas
                lines.AddRange(Datas.Select(r => string.Join(",", r)));

                // Sobrescribir el archivo original
                File.WriteAllLines(rutaArchivoActual, lines);

                MessageBox.Show("Archivo actualizado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el archivo: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }
        private void Propiedades(Propiedades propiedades)
        {
            DgvDATAS.Rows.Clear();

            FileInfo info = new FileInfo(propiedades.Ruta);

            // Propiedades del archivo
            string nombre = info.Name;
            string tipo = info.Extension;
            string ubicacion = info.FullName;
            string carpeta = info.DirectoryName;
            string tamano = (info.Length / 1024.0).ToString("F2") + " KB";
            string fechaCreacion = info.CreationTime.ToString();
            string ultimoAcceso = info.LastAccessTime.ToString();
            string extension = info.Extension;
            DgvProperties.Rows.Add("Nombre", nombre);
            DgvProperties.Rows.Add("Tipo", tipo);
            DgvProperties.Rows.Add("Ubicación", ubicacion);
            DgvProperties.Rows.Add("Carpeta contenedora", carpeta);
            DgvProperties.Rows.Add("Tamaño", tamano);
            DgvProperties.Rows.Add("Extensión", extension);

            DgvProperties.Rows.Add("Fecha de creación", fechaCreacion);
            DgvProperties.Rows.Add("Último acceso", ultimoAcceso);

        }
        private void btnPropiedades_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Archivos CSV|*.csv| Archivos JSON|*.json| Archivos TXT|*.txt"; ;
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string rutaArchivo in openFileDialog1.FileNames)
                {
                    // Crear objeto Cancion
                    Propiedades propiedades = new Propiedades
                    {
                        Ruta = rutaArchivo,
                    };

                    // Llamar al método
                    Propiedades(propiedades);
                }
            }
        }
        private void CopiarArchivo()
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Archivos de texto (*.txt)|*.txt|Archivos CSV (*.csv)|*.csv|Archivos de datos (*.dat)|*.dat|Todos los archivos (*.*)|*.*";
                openFileDialog1.Title = "Seleccionar archivo a copiar";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string rutaOrigen = openFileDialog1.FileName;

                    FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                    folderBrowserDialog1.Description = "Seleccione la carpeta de destino";
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string carpetaDestino = folderBrowserDialog1.SelectedPath;
                        string nombreArchivo = Path.GetFileName(rutaOrigen);
                        string rutaDestino = Path.Combine(carpetaDestino, nombreArchivo);

                        if (File.Exists(rutaDestino))
                        {
                            DialogResult resultado = MessageBox.Show("El archivo ya existe en la carpeta de destino. ¿Desea reemplazarlo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (resultado == DialogResult.No)
                            {
                                return;
                            }
                            File.Delete(rutaDestino);
                        }

                        File.Copy(rutaOrigen, rutaDestino);
                        MessageBox.Show("Archivo copiado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al copiar el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            CopiarArchivo();
        }
        private static bool Eliminar()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filepath = ofd.FileName;
                DialogResult resultado = MessageBox.Show("¿Desea eliminarlo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                {
                    return false;
                }

                File.Delete(filepath);
            }

            return true;
        }

        private void brnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void MoverArchivo()
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Archivos de texto (*.txt)|*.txt|Archivos CSV (*.csv)|*.csv|Archivos de datos (*.dat)|*.dat|Archivos JSON (*.json)|*.json|Todos los archivos (*.*)|*.*";
                openFileDialog1.Title = "Seleccionar archivo a mover";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string rutaOrigen = openFileDialog1.FileName;

                    FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                    folderBrowserDialog1.Description = "Seleccione la carpeta de destino";
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string carpetaDestino = folderBrowserDialog1.SelectedPath;
                        string nombreArchivo = Path.GetFileName(rutaOrigen);
                        string rutaDestino = Path.Combine(carpetaDestino, nombreArchivo);

                        if (File.Exists(rutaDestino))
                        {
                            DialogResult resultado = MessageBox.Show("El archivo ya existe en la carpeta de destino. ¿Desea reemplazarlo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (resultado == DialogResult.No)
                            {
                                return;
                            }
                            File.Delete(rutaDestino);
                        }

                        File.Move(rutaOrigen, rutaDestino);
                        MessageBox.Show("Archivo movido exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mover el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMover_Click(object sender, EventArgs e)
        {
            MoverArchivo();
        }

        private void CrearArchivo()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtCelular.Text))
            {
                MessageBox.Show("Debe escribir información en ambos TextBox.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivos CSV (*.csv)|*.csv|Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            sfd.Title = "Guardar archivo";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string ruta = sfd.FileName;

                    // Primera línea: encabezado
                    string encabezado = "Nombre,Celular";

                    // Segunda línea: datos
                    string datos = txtNombre.Text + "," + txtCelular.Text;

                    // Escribir ambas líneas
                    File.WriteAllLines(ruta, new string[]
                    {
                encabezado,
                datos
                    });

                    MessageBox.Show("Archivo creado correctamente con encabezados.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtNombre.Clear();
                    txtCelular.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al crear el archivo: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCrearArchivo_Click(object sender, EventArgs e)
        {
            CrearArchivo();
        }

        private void btnRenombrar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Todos los archivos (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string rutaOriginal = ofd.FileName;
                string carpeta = Path.GetDirectoryName(rutaOriginal);
                string extension = Path.GetExtension(rutaOriginal);

                string nuevoNombre = Microsoft.VisualBasic.Interaction.InputBox(
                    "Ingrese el nuevo nombre del archivo (sin extensión):",
                    "Renombrar archivo");

                if (string.IsNullOrWhiteSpace(nuevoNombre))
                {
                    MessageBox.Show("Nombre no válido.");
                    return;
                }

                string nuevaRuta = Path.Combine(carpeta, nuevoNombre + extension);

                if (File.Exists(nuevaRuta))
                {
                    MessageBox.Show("Ya existe un archivo con ese nombre.");
                    return;
                }

                try
                {
                    File.Move(rutaOriginal, nuevaRuta);
                    MessageBox.Show("Archivo renombrado correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al renombrar: " + ex.Message);
                }
            }
        }
       
        private void btnCrearCarpeta_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "Seleccione la ubicación donde se creará la carpeta";

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string rutaBase = fbd.SelectedPath;

                    string nombreCarpeta = Microsoft.VisualBasic.Interaction.InputBox(
                        "Ingrese el nombre de la nueva carpeta:",
                        "Crear carpeta");

                    if (string.IsNullOrWhiteSpace(nombreCarpeta))
                    {
                        MessageBox.Show("Debe ingresar un nombre válido.",
                            "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string rutaNuevaCarpeta = Path.Combine(rutaBase, nombreCarpeta);

                    if (Directory.Exists(rutaNuevaCarpeta))
                    {
                        MessageBox.Show("La carpeta ya existe.",
                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Directory.CreateDirectory(rutaNuevaCarpeta);

                    MessageBox.Show("Carpeta creada correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear la carpeta: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarInfromacion_Click(object sender, EventArgs e)
        {

            // Validar que haya archivo cargado
            if (string.IsNullOrEmpty(rutaArchivoActual))
            {
                MessageBox.Show("No hay ningún archivo cargado en el DataGrid.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar TextBox
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtCelular.Text))
            {
                MessageBox.Show("Debe escribir información en ambos TextBox.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Crear la línea (misma línea)
                string linea = txtNombre.Text + "," + txtCelular.Text;

                // Guardar en el archivo cargado
                File.AppendAllText(rutaArchivoActual, linea + Environment.NewLine);

                // Agregar a la lista en memoria
                string[] nuevaFila = linea.Split(',');
                Datas.Add(nuevaFila);

                // Actualizar grid
                DgvDATAS.RowCount = Datas.Count;
                DgvDATAS.Refresh();

                //  Moverse a la última fila
                int lastRow = DgvDATAS.RowCount - 1;
                if (lastRow >= 0)
                {
                    DgvDATAS.ClearSelection();
                    DgvDATAS.Rows[lastRow].Selected = true;
                    DgvDATAS.FirstDisplayedScrollingRowIndex = lastRow;
                }

                MessageBox.Show("Datos agregados correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNombre.Clear();
                txtCelular.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar datos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }



        private void btnBuscar_Click(object sender, EventArgs e)
        { if (DgvDATAS.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos cargados");
                return;
            }

            string textoBuscar = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(textoBuscar))
            {
                MessageBox.Show("Escribe un texto para buscar");
                return;
            }

            textoBuscar = textoBuscar.ToLower();

            foreach (DataGridViewRow fila in DgvDATAS.Rows)
            {
                foreach (DataGridViewCell celda in fila.Cells)
                {
                    if (celda.Value != null &&
                        celda.Value.ToString().ToLower().Contains(textoBuscar))
                    {
                        // Selecciona la fila
                        fila.Selected = true;

                        // Lleva el scroll a esa fila
                        DgvDATAS.FirstDisplayedScrollingRowIndex = fila.Index;

                        MessageBox.Show($"Texto encontrado en la fila {fila.Index + 1}");
                        return; // termina al encontrar la primera coincidencia
                    }
                }
            }

            MessageBox.Show("Texto no encontrado");
        }


     

        private void btnEliminarFila_Click(object sender, EventArgs e)
        {
            if (DgvDATAS.CurrentCell == null)
            {
                MessageBox.Show("No hay ninguna fila seleccionada.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int rowIndex = DgvDATAS.CurrentCell.RowIndex;

            if (Datas == null || rowIndex < 0 || rowIndex >= Datas.Count)
            {
                MessageBox.Show("No hay datos para eliminar.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("¿Estás seguro de eliminar este registro?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            selectedRowIndex = -1; // muy importante

            Datas.RemoveAt(rowIndex);

            DgvDATAS.RowCount = Datas.Count;

            if (Datas.Count > 0)
            {
                int newIndex = Math.Min(rowIndex, Datas.Count - 1);
                DgvDATAS.CurrentCell = DgvDATAS.Rows[newIndex].Cells[0];
            }

            DgvDATAS.Invalidate();

            MessageBox.Show("Registro eliminado.", "Listo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    
}
