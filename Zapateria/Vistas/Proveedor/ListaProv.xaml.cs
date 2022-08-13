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

namespace Zapateria.Vistas.Proveedor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaProv : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<T_Proveedor> TablaProveedor;
        public ListaProv()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            ListaProveedores.ItemSelected += ListaProveedores_ItemSelected;

        }

        private void ListaProveedores_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Proveedor)e.SelectedItem;
            var item = Obj.Id.ToString();
            var nom = Obj.Nombre;
            var des = Obj.Descripcion;
            var dir = Obj.Direccion;
            var tel = Obj.Telefono;
            int ID = Convert.ToInt32(item);
            try
            {
                Navigation.PushAsync(new PVDetalle(ID, nom, des, dir, tel));
            }
            catch (Exception)
            {   
                throw;
            }
        }
        protected async override void OnAppearing()
        {
            var ResulRegistros = await conexion.Table<T_Proveedor>().ToListAsync();
            TablaProveedor = new ObservableCollection<T_Proveedor>(ResulRegistros);
            ListaProveedores.ItemsSource = TablaProveedor;
            base.OnAppearing();
        }
    }
}