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

namespace Zapateria.Vistas.Cliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListadoCliente : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<T_Cliente> TablaCliente;
        public ListadoCliente()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            ListaCliente.ItemSelected += ListaCliente_ItemSelected;
        }

        private void ListaCliente_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Cliente)e.SelectedItem;
            var item = Obj.IdCliente.ToString();
            var nom = Obj.Nombre;
            var ape = Obj.Apellidos;
            var dir = Obj.Direccion;
            var cor = Obj.Correo;
            var tel = Obj.Telefono;
            int ID = Convert.ToInt32(item);
            try
            {
                Navigation.PushAsync(new CVDetalle(ID, nom, ape, dir, cor, tel));
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected async override void OnAppearing()
        {
            var ResulRegistros = await conexion.Table<T_Cliente>().ToListAsync();
            TablaCliente = new ObservableCollection<T_Cliente>(ResulRegistros);
            ListaCliente.ItemsSource = TablaCliente;
            base.OnAppearing();
        }
    }
}