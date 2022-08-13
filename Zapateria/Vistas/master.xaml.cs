using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zapateria.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class master : ContentPage
    {
        public master()
        {
            InitializeComponent();
        }
        private async void btnProveedor_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            await App.MasterD.Detail.Navigation.PushAsync(new Vistas.Proveedor.PProveedor());
        }

        private async void btnPedido_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            await App.MasterD.Detail.Navigation.PushAsync(new Vistas.Pedido.PPedido());
        }

        private async void btnCliente_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            await App.MasterD.Detail.Navigation.PushAsync(new Vistas.Cliente.PCliente());
        }
    }
}