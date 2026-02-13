using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos_secuenciales
{
    internal class Propiedades
    {
        private string nombre;
        private string ruta;
        private string tipo;
        private string ubicacion;
        private string carpetaContenedora;
        private long tamaño;
        private DateTime fechaCreacion;
        private string ultimoAcceso;
        private string extension;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public string Ruta
        {
            get { return ruta; }
            set { ruta = value; }
        }
        public string Ubicacion
        {
            get { return ubicacion; }
            set { ubicacion = value; }
        }
        public string CarpetaContenedora
        {
            get { return carpetaContenedora; }
            set { carpetaContenedora = value; }
        }
        public long Tamaño
        {
            get { return tamaño; }
            set { tamaño = value; }
        }
        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }
        public string UltimoAcceso
        {
            get { return ultimoAcceso; }
            set { ultimoAcceso = value; }
        }
      public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }
    }
}
