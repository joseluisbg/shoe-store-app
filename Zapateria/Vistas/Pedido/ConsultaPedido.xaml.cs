using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using Zapateria.Tablas;
using Zapateria.Datos;
using System.IO;

namespace Zapateria.Vistas.Pedido
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaPedido : ContentPage
    {
        private SQLiteAsyncConnection conexion;

        public ConsultaPedido()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnBuscar.Clicked += BtnBuscar_Clicked;
        }

        private void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtNombreCliente.Text))
            {
                DisplayAlert("ADVERTENCIA", "Por favor ingrese el nombre del cliente a quién se le registro el pedido.", "Aceptar");
            }
            else
            {

                try
                {
                    var rutaBD =
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ZapateriaSQLite.db3");
                    var db = new SQLiteConnection(rutaBD);
                    db.CreateTable<T_Pedido>();
                    IEnumerable<T_Pedido> resultado = SELECT_WHERE(db, txtNombreCliente.Text);
                    if (resultado.Count() > 0)
                    {
                        DisplayAlert("RESULTADO DE BUSQUEDA", "El cliente: " + txtNombreCliente.Text + "\n\n¡Tiene un pedido registrado!", "Aceptar");
                        limpiar();
                    }
                    else
                    {
                        DisplayAlert("¡Ups!", "El cliente que usted esta buscando no tiene ningún pedido registrado.", "Aceptar");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private IEnumerable<T_Pedido> SELECT_WHERE(SQLiteConnection db, string NombreCliente)
        {
            return db.Query<T_Pedido>("SELECT * from T_Pedido WHERE NombreCliente=?", NombreCliente);
        }

        private void limpiar()
        {
            txtNombreCliente.Text = "";
        }
    }
}