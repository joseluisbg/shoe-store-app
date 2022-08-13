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


namespace Zapateria.Vistas.Cliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CVDetalle : ContentPage
    {
        public int IdSeleccionado;
        public string NomSeleccionado, ApeSeleccionado, DirSeleccionado, CorSeleccionado, TelSeleccionado;
        private SQLiteAsyncConnection conexion;
        IEnumerable<T_Cliente> ResultadoDelete;
        IEnumerable<T_Cliente> ResultadoUpdate;
        public CVDetalle(int iD, string nom, string ape, string dir, string cor, string tel)
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            IdSeleccionado = iD;
            NomSeleccionado = nom;
            ApeSeleccionado = ape;
            DirSeleccionado = dir;
            CorSeleccionado = cor;
            TelSeleccionado = tel;
            btnActualizar.Clicked += BtnActualizar_Clicked;
            btnEliminar.Clicked += BtnEliminar_Clicked;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            lblMensaje.Text = "ID" + IdSeleccionado;
            txtNombre.Text = NomSeleccionado;
            txtApellidos.Text = ApeSeleccionado;
            txtDireccion.Text = DirSeleccionado;
            txtCorreo.Text = CorSeleccionado;
            txtTelefono.Text = TelSeleccionado;
        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            var ans = await DisplayAlert("ADVERTENCIA", "¿Esta seguro de eliminar al cliente: " + txtNombre.Text + "?", "Si", "No");
            if (ans == true)
            {
                var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ZapateriaSQLite.db3");
                var db = new SQLiteConnection(rutaDB);
                ResultadoDelete = Delete(db, IdSeleccionado);
                DisplayAlert("CONFIRMACIÓN", "El cliente:"+txtNombre.Text+"\n\n¡Se elimino correctamente!", "Aceptar");
                Navigation.PopAsync();
            }
            else
            {
                //false conditon
            }
        }

        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            var ans = await DisplayAlert("ADVERTENCIA", "¿Desea actualizar los datos del cliente: " + txtNombre.Text+"?", "Si", "No");
            if (ans == true)
            {

                var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ZapateriaSQLite.db3");
                var db = new SQLiteConnection(rutaDB);
                ResultadoUpdate = Update(db, txtNombre.Text, txtApellidos.Text, txtDireccion.Text, txtCorreo.Text, txtTelefono.Text, IdSeleccionado);
                DisplayAlert("CONFIRMACIÓN", "Los datos del cliente se actualizaron correctamente", "Aceptar");
            }
            else
            {
                //se mantiene en la vista
            }
        }

        private IEnumerable<T_Cliente> Update(SQLiteConnection db, string Nombre, string Apellidos, string Direccion, string Correo, string Telefono, int idSeleccionado)
        {
            return db.Query<T_Cliente>("UPDATE T_Cliente SET Nombre = ?, Apellidos = ?, Direccion = ?, Correo = ?, Telefono=? WHERE IdCliente = ?", Nombre, Apellidos, Direccion, Correo, Telefono, idSeleccionado);
        }

        private IEnumerable<T_Cliente> Delete(SQLiteConnection db, int idSeleccionado)
        {
            return db.Query<T_Cliente>("DELETE FROM T_Cliente WHERE IdCliente = ?", idSeleccionado);
        }
    }
}