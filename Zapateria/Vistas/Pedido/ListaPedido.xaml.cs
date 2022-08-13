using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using Zapateria.Tablas;
using System.Collections.ObjectModel;
using System.IO;
using Zapateria.Datos;

namespace Zapateria.Vistas.Pedido
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaPedido : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<T_Pedido> TablaPedido;
        public ListaPedido()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            ListasPedido.ItemSelected += ListasPedido_ItemSelected;

        }

        private void ListasPedido_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Pedido)e.SelectedItem;
            var item = Obj.IdPedido.ToString();
            var des = Obj.Descripcion;
            var nom = Obj.NombreCliente;
            var adp = Obj.AdelantoPago;
            var tol = Obj.TotalPagar;
            int ID = Convert.ToInt32(item);
            try
            {
                Navigation.PushAsync(new PVDetalle(ID, nom, des, adp, tol));
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected async override void OnAppearing()
        {
            var ResulRegistros = await conexion.Table<T_Pedido>().ToListAsync();
            TablaPedido = new ObservableCollection<T_Pedido>(ResulRegistros);
            ListasPedido.ItemsSource = TablaPedido;
            base.OnAppearing();
        }
    }
}