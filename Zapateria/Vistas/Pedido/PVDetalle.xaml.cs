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

namespace Zapateria.Vistas.Pedido
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PVDetalle : ContentPage
    {
        public int IdSeleccionado;
        public string NomSeleccionado, DesSeleccionado, AdpSeleccionado, TolSeleccionado;
        private SQLiteAsyncConnection conexion;
        IEnumerable<T_Pedido> ResultadoDelete;
        IEnumerable<T_Pedido> ResultadoUpdate;
        public PVDetalle(int iD, string nom, string des, string adp, string tol)
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            IdSeleccionado = iD;
            NomSeleccionado = nom;
            DesSeleccionado = des;
            AdpSeleccionado = adp;
            TolSeleccionado = tol;
            btnActualizar.Clicked += BtnActualizar_Clicked;
            btnEliminar.Clicked += BtnEliminar_Clicked;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            lblMensaje.Text = "ID" + IdSeleccionado;
            txtNombreCliente.Text = NomSeleccionado;
            txtDescripcion.Text = DesSeleccionado;
            txtAdelantoPago.Text = AdpSeleccionado;
            txtTotalPagar.Text = TolSeleccionado;
        }
        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            var ans = await DisplayAlert("ADVERTENCIA", "¿Esta seguro de eliminar el pedido de: " + txtNombreCliente.Text + "?", "Si", "No");
            if (ans == true)
            {
                var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ZapateriaSQLite.db3");
                var db = new SQLiteConnection(rutaDB);
                ResultadoDelete = Delete(db, IdSeleccionado);
                DisplayAlert("CONFIRMACIÓN", "El contacto se elimino correctamente", "Actualizar");
                Navigation.PopAsync();
            }
            else
            {
                //Se mantiene en la vista
            }
        }

        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {

            var ans = await DisplayAlert("ADVERTENCIA", "¿Desea actualizar los datos del pedido de: " + txtNombreCliente.Text+"?", "Si", "No");
            if (ans == true)
            {
                var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ZapateriaSQLite.db3");
                var db = new SQLiteConnection(rutaDB);
                ResultadoUpdate = Update(db, txtDescripcion.Text, txtNombreCliente.Text, txtAdelantoPago.Text, txtTotalPagar.Text, IdSeleccionado);
                DisplayAlert("CONFIRMACIÓN", "Los datos del pedido se actualizaron correctamente", "Aceptar");
            }
            else
            {
                //se mantiene en la vista
            }
        }
        private IEnumerable<T_Pedido> Delete(SQLiteConnection db, int idSeleccionado)
        {
            return db.Query<T_Pedido>("DELETE FROM T_Pedido WHERE IdPedido = ?", idSeleccionado);
        }

        private IEnumerable<T_Pedido> Update(SQLiteConnection db, string Descripcion, string NombreCliente, string AdelantoPago, string TotalPagar, int idSeleccionado)
        {
            return db.Query<T_Pedido>("UPDATE T_Pedido SET Descripcion = ?, NombreCliente = ?, AdelantoPago = ?, TotalPagar = ? WHERE IdPedido = ?", Descripcion, NombreCliente, AdelantoPago, TotalPagar, idSeleccionado);
        }
    }
}