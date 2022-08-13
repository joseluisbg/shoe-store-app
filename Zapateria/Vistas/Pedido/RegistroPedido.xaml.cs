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

namespace Zapateria.Vistas.Pedido
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroPedido : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public RegistroPedido()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnGuardar.Clicked += BtnGuardar_Clicked;
        }

        private void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            {
                var DatosPedido = new T_Pedido
                {
                    Descripcion = txtDescripcion.Text,
                    NombreCliente = txtNombreCliente.Text,
                    AdelantoPago = txtAdelantoPago.Text,
                    TotalPagar = txtTotalPagar.Text
                };
                if (String.IsNullOrWhiteSpace(txtNombreCliente.Text)&& String.IsNullOrWhiteSpace(txtDescripcion.Text)
                    && String.IsNullOrWhiteSpace(txtAdelantoPago.Text)&& String.IsNullOrWhiteSpace(txtTotalPagar.Text))
                {
                    DisplayAlert("ADVERTENCIA", "Ingrese por favor todos los datos del pedido", "Aceptar");
                }
                else if (String.IsNullOrWhiteSpace(txtNombreCliente.Text))
                {
                    DisplayAlert("ADVERTENCIA", "Ingrese el nombre del pedido", "Aceptar");
                } else if (String.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    DisplayAlert("ADVERTENCIA", "Ingrese la descripción del pedido", "Aceptar");
                }
                else if (String.IsNullOrWhiteSpace(txtTotalPagar.Text))
                {
                    DisplayAlert("ADVERTENCIA", "Ingrese el total a pagar del pedido", "Aceptar");
                }
                else {
                    conexion.InsertAsync(DatosPedido);
                    limpiarFormulario();
                    DisplayAlert("CONFIRMACIÓN", "El pedido se registro correctamente", "Aceptar");
                }
            }
            void limpiarFormulario()
            {
                txtDescripcion.Text = "";
                txtNombreCliente.Text = "";
                txtAdelantoPago.Text = "";
                txtTotalPagar.Text = "";
            }
        }
    }
}