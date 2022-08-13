using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Zapateria.Tablas;
using Zapateria.Datos;
using SQLite;
using System.IO;

namespace Zapateria.Vistas.Proveedor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PVDetalle : ContentPage
    {
        public int IdSeleccionado;
        public string NomSeleccionado, DesSeleccionado, DirSeleccionado, TelSeleccionado;
        private SQLiteAsyncConnection conexion;
        IEnumerable<T_Proveedor> ResultadoDelete;
        IEnumerable<T_Proveedor> ResultadoUpdate;
        public PVDetalle(int iD, string nom, string des, string dir, string tel)
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            IdSeleccionado = iD;
            NomSeleccionado = nom;
            DesSeleccionado = des;
            DirSeleccionado = dir;
            TelSeleccionado = tel;
            btnActualizar.Clicked += BtnActualizar_Clicked;
            btnEliminar.Clicked += BtnEliminar_Clicked;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            lblMensaje.Text = "ID" + IdSeleccionado;
            txtNombre.Text = NomSeleccionado;
            txtDescripcion.Text = DesSeleccionado;
            txtDireccion.Text = DirSeleccionado;
            txtTelefono.Text = TelSeleccionado;
        }
        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {

            var ans = await DisplayAlert("ADVERTENCIA", "¿Esta seguro de eliminar al proveedor: "+txtNombre.Text+"?", "Si", "No");
            if (ans == true)
            {
                var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ZapateriaSQLite.db3");
                var db = new SQLiteConnection(rutaDB);
                ResultadoDelete = Delete(db, IdSeleccionado);
                DisplayAlert("CONFIRMACIÓN", "El proveedor: "+txtNombre.Text+"\n\n¡Se elimino correctamente!", "Aceptar");
                Navigation.PopAsync();
            }
            else
            {
                //Se mantiene en la vista
            }


           
        }
        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {

            var ans = await DisplayAlert("ADVERTENCIA", "¿Desea actualizar los datos del proveedor: "+txtNombre.Text+"?", "Si", "No");
            if (ans == true)
            {
                
                var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ZapateriaSQLite.db3");
                var db = new SQLiteConnection(rutaDB);
                ResultadoUpdate = Update(db, txtNombre.Text, txtDescripcion.Text, txtDireccion.Text, txtTelefono.Text, IdSeleccionado);
                DisplayAlert("CONFIRMACIÓN", "El proveedor: "+txtNombre.Text+"\n\n¡Se actualizó correctamente!", "Aceptar");
            }
            else
            {
                //Se mantiene en la vista
            }
        }
        public IEnumerable<T_Proveedor> Delete(SQLiteConnection db, int idSeleccionado)
        {
            return db.Query<T_Proveedor>("DELETE FROM T_Proveedor WHERE Id = ?", idSeleccionado);
        }
        public IEnumerable<T_Proveedor> Update(SQLiteConnection db, string Nombre, string Descripcion, string Direccion, string Telefono, int idSeleccionado)
        {
            return db.Query<T_Proveedor>("UPDATE T_Proveedor SET Nombre = ?, Descripcion = ?, Direccion = ?, Telefono = ? WHERE Id = ?", 
                Nombre, Descripcion, Direccion, Telefono, idSeleccionado);
        }
    }
}