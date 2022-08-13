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

namespace Zapateria.Vistas.Proveedor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaProv : ContentPage
    {
        private SQLiteAsyncConnection conexion;

        public ConsultaProv()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnBuscar.Clicked += BtnBuscar_Clicked;
        }

        private void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                DisplayAlert("ADVERTENCIA", "Por favor ingrese el nombre del proveedor.", "Aceptar");
            }
            else
            {
                try
                {
                    var rutaBD =
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ZapateriaSQLite.db3");
                    var db = new SQLiteConnection(rutaBD);
                    db.CreateTable<T_Proveedor>();
                    IEnumerable<T_Proveedor> resultado = SELECT_WHERE(db, txtNombre.Text);
                    if (resultado.Count() > 0)
                    {
                        DisplayAlert("RESULTADO DE BUSQUEDA", "El proveedor: "+txtNombre.Text+ "\n\n¡Se encuentra registrado!", "Aceptar");
                        limpiar();
                    }
                    else
                    {
                        DisplayAlert("¡Ups!", "El proveedor: " + txtNombre.Text + "\n\n¡No esta registrado!", "Aceptar");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public static IEnumerable<T_Proveedor> SELECT_WHERE(SQLiteConnection db, string nombre)
        {
            return db.Query<T_Proveedor>("SELECT * from T_Proveedor WHERE Nombre=?", nombre);
        }
        private void limpiar()
        {
            txtNombre.Text = "";
        }
    }
}