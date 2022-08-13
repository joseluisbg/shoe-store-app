using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Zapateria.Tablas
{
    class T_Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int IdCliente { get; set; }
        [MaxLength(255)]
        public string Nombre { get; set; }
        [MaxLength(20)]
        public string Apellidos { get; set; }
        [MaxLength(50)]
        public string Direccion { get; set; }
        [MaxLength(255)]
        public string Correo { get; set; }
        [MaxLength(50)]
        public string Telefono { get; set; }
    }
}