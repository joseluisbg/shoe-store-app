using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Zapateria.Tablas
{
    class T_Pedido
    {
        [PrimaryKey, AutoIncrement]
        public int IdPedido { get; set; }
        [MaxLength(255)]
        public string Descripcion { get; set; }
        [MaxLength(255)]
        public String NombreCliente { get; set; }
        [MaxLength(30)]
        public string AdelantoPago { get; set; }
        [MaxLength(100)]
        public string TotalPagar { get; set; }
    }
}
