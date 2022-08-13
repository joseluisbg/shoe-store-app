using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Zapateria.Tablas
{
    public class T_Proveedor
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Nombre { get; set; }
        [MaxLength(100)]
        public string Descripcion { get; set; }
        [MaxLength(255)]
        public string Direccion { get; set; }
        [MaxLength(255)]
        public string Telefono { get; set; }
    }
}
