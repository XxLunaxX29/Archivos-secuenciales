namespace Archivos_secuenciales
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnCrearArchivo = new Button();
            btnGuardarModificacion = new Button();
            brnEliminar = new Button();
            btnAbrir = new Button();
            btnMover = new Button();
            btnRenombrar = new Button();
            btnCopiar = new Button();
            label1 = new Label();
            label2 = new Label();
            txtCelular = new TextBox();
            txtNombre = new TextBox();
            DgvDATAS = new DataGridView();
            dataGridView2 = new DataGridView();
            btnPropiedades = new Button();
            DgvProperties = new DataGridView();
            btnCrearCarpeta = new Button();
            btnAgregarInfromacion = new Button();
            btnBuscar = new Button();
            txtBuscar = new TextBox();
            label3 = new Label();
            btnEliminarFila = new Button();
            ((System.ComponentModel.ISupportInitialize)DgvDATAS).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DgvProperties).BeginInit();
            SuspendLayout();
            // 
            // btnCrearArchivo
            // 
            btnCrearArchivo.Location = new Point(511, 12);
            btnCrearArchivo.Name = "btnCrearArchivo";
            btnCrearArchivo.Size = new Size(127, 29);
            btnCrearArchivo.TabIndex = 0;
            btnCrearArchivo.Text = "Crear archivo";
            btnCrearArchivo.UseVisualStyleBackColor = true;
            btnCrearArchivo.Click += btnCrearArchivo_Click;
            // 
            // btnGuardarModificacion
            // 
            btnGuardarModificacion.Location = new Point(915, 82);
            btnGuardarModificacion.Name = "btnGuardarModificacion";
            btnGuardarModificacion.Size = new Size(127, 29);
            btnGuardarModificacion.TabIndex = 1;
            btnGuardarModificacion.Text = "Modificacion";
            btnGuardarModificacion.UseVisualStyleBackColor = true;
            btnGuardarModificacion.Click += btnGuardar_Click;
            // 
            // brnEliminar
            // 
            brnEliminar.Location = new Point(644, 47);
            brnEliminar.Name = "brnEliminar";
            brnEliminar.Size = new Size(127, 29);
            brnEliminar.TabIndex = 2;
            brnEliminar.Text = "Eliminar";
            brnEliminar.UseVisualStyleBackColor = true;
            brnEliminar.Click += brnEliminar_Click;
            // 
            // btnAbrir
            // 
            btnAbrir.Location = new Point(644, 12);
            btnAbrir.Name = "btnAbrir";
            btnAbrir.Size = new Size(127, 29);
            btnAbrir.TabIndex = 3;
            btnAbrir.Text = "Abrir";
            btnAbrir.UseVisualStyleBackColor = true;
            btnAbrir.Click += btnAbrir_Click;
            // 
            // btnMover
            // 
            btnMover.Location = new Point(777, 12);
            btnMover.Name = "btnMover";
            btnMover.Size = new Size(127, 29);
            btnMover.TabIndex = 4;
            btnMover.Text = "Mover";
            btnMover.UseVisualStyleBackColor = true;
            btnMover.Click += btnMover_Click;
            // 
            // btnRenombrar
            // 
            btnRenombrar.Location = new Point(915, 12);
            btnRenombrar.Name = "btnRenombrar";
            btnRenombrar.Size = new Size(127, 29);
            btnRenombrar.TabIndex = 5;
            btnRenombrar.Text = "Renombrar";
            btnRenombrar.UseVisualStyleBackColor = true;
            btnRenombrar.Click += btnRenombrar_Click;
            // 
            // btnCopiar
            // 
            btnCopiar.Location = new Point(777, 47);
            btnCopiar.Name = "btnCopiar";
            btnCopiar.Size = new Size(127, 29);
            btnCopiar.TabIndex = 6;
            btnCopiar.Text = "Copiar";
            btnCopiar.UseVisualStyleBackColor = true;
            btnCopiar.Click += btnCopiar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 40);
            label1.Name = "label1";
            label1.Size = new Size(64, 20);
            label1.TabIndex = 7;
            label1.Text = "Nombre";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 73);
            label2.Name = "label2";
            label2.Size = new Size(55, 20);
            label2.TabIndex = 8;
            label2.Text = "Celular";
            // 
            // txtCelular
            // 
            txtCelular.Location = new Point(87, 69);
            txtCelular.Name = "txtCelular";
            txtCelular.Size = new Size(125, 27);
            txtCelular.TabIndex = 9;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(87, 36);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(125, 27);
            txtNombre.TabIndex = 10;
            // 
            // DgvDATAS
            // 
            DgvDATAS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvDATAS.Location = new Point(16, 125);
            DgvDATAS.Name = "DgvDATAS";
            DgvDATAS.RowHeadersWidth = 51;
            DgvDATAS.Size = new Size(654, 338);
            DgvDATAS.TabIndex = 11;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(16, 482);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(654, 119);
            dataGridView2.TabIndex = 12;
            // 
            // btnPropiedades
            // 
            btnPropiedades.Location = new Point(915, 47);
            btnPropiedades.Name = "btnPropiedades";
            btnPropiedades.Size = new Size(127, 29);
            btnPropiedades.TabIndex = 13;
            btnPropiedades.Text = "Propiedades";
            btnPropiedades.UseVisualStyleBackColor = true;
            btnPropiedades.Click += btnPropiedades_Click;
            // 
            // DgvProperties
            // 
            DgvProperties.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvProperties.Location = new Point(737, 125);
            DgvProperties.Name = "DgvProperties";
            DgvProperties.RowHeadersWidth = 51;
            DgvProperties.Size = new Size(305, 437);
            DgvProperties.TabIndex = 14;
            // 
            // btnCrearCarpeta
            // 
            btnCrearCarpeta.Location = new Point(378, 12);
            btnCrearCarpeta.Name = "btnCrearCarpeta";
            btnCrearCarpeta.Size = new Size(127, 29);
            btnCrearCarpeta.TabIndex = 15;
            btnCrearCarpeta.Text = "Crear Carpeta";
            btnCrearCarpeta.UseVisualStyleBackColor = true;
            btnCrearCarpeta.Click += btnCrearCarpeta_Click;
            // 
            // btnAgregarInfromacion
            // 
            btnAgregarInfromacion.Location = new Point(511, 47);
            btnAgregarInfromacion.Name = "btnAgregarInfromacion";
            btnAgregarInfromacion.Size = new Size(127, 29);
            btnAgregarInfromacion.TabIndex = 16;
            btnAgregarInfromacion.Text = "Agregar Info";
            btnAgregarInfromacion.UseVisualStyleBackColor = true;
            btnAgregarInfromacion.Click += btnAgregarInfromacion_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(511, 82);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(127, 29);
            btnBuscar.TabIndex = 17;
            btnBuscar.Text = "Buscar Nombre";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(87, 3);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(125, 27);
            txtBuscar.TabIndex = 18;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 6);
            label3.Name = "label3";
            label3.Size = new Size(52, 20);
            label3.TabIndex = 19;
            label3.Text = "Buscar";
            // 
            // btnEliminarFila
            // 
            btnEliminarFila.Location = new Point(644, 82);
            btnEliminarFila.Name = "btnEliminarFila";
            btnEliminarFila.Size = new Size(127, 29);
            btnEliminarFila.TabIndex = 20;
            btnEliminarFila.Text = "Eliminar Info";
            btnEliminarFila.UseVisualStyleBackColor = true;
            btnEliminarFila.Click += btnEliminarFila_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1074, 613);
            Controls.Add(btnEliminarFila);
            Controls.Add(label3);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(btnAgregarInfromacion);
            Controls.Add(btnCrearCarpeta);
            Controls.Add(DgvProperties);
            Controls.Add(btnPropiedades);
            Controls.Add(dataGridView2);
            Controls.Add(DgvDATAS);
            Controls.Add(txtNombre);
            Controls.Add(txtCelular);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCopiar);
            Controls.Add(btnRenombrar);
            Controls.Add(btnMover);
            Controls.Add(btnAbrir);
            Controls.Add(brnEliminar);
            Controls.Add(btnGuardarModificacion);
            Controls.Add(btnCrearArchivo);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)DgvDATAS).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)DgvProperties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCrearArchivo;
        private Button btnGuardarModificacion;
        private Button brnEliminar;
        private Button btnAbrir;
        private Button btnMover;
        private Button btnRenombrar;
        private Button btnCopiar;
        private Label label1;
        private Label label2;
        private TextBox txtCelular;
        private TextBox txtNombre;
        private DataGridView DgvDATAS;
        private DataGridView dataGridView2;
        private Button btnPropiedades;
        private DataGridView DgvProperties;
        private Button btnCrearCarpeta;
        private Button btnAgregarInfromacion;
        private Button btnBuscar;
        private TextBox txtBuscar;
        private Label label3;
        private Button btnEliminarFila;
    }
}
