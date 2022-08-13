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

namespace Zapateria.Vistas.Cliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaCliente : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public ConsultaCliente()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnBuscar.Clicked += BtnBuscar_Clicked;
        }

        private void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                DisplayAlert("ADVERTENCIA", "Por favor ingrese el nombre del cliente previamente registrado registrado.", "Aceptar");
            }
            else
            {
                try
                {
                    var rutaBD =
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ZapateriaSQLite.db3");
                    var db = new SQLiteConnection(rutaBD);
                    db.CreateTable<T_Cliente>();
                    IEnumerable<T_Cliente> resultado = SELECT_WHERE(db, txtNombre.Text);
                    if (resultado.Count() > 0)
                    {
                        DisplayAlert("RESULTADO DE BUSQUEDA:", "El cliente: " + txtNombre.Text + "\n\n¡Esta registrado!", "Aceptar");
                        limpiar();
                    }
                    else
                    {
                        DisplayAlert("¡Ups!", "El cliente que usted esta buscando no se encuentra registrado.", "Aceptar");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        private IEnumerable<T_Cliente> SELECT_WHERE(SQLiteConnection db, string Nombre)
        {
            return db.Query<T_Cliente>("SELECT * from T_Cliente WHERE Nombre=?", Nombre);
        }
        private void limpiar()
        {
            txtNombre.Text = "";
        }
    }
}