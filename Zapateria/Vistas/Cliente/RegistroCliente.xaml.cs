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

namespace Zapateria.Vistas.Cliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroCliente : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public RegistroCliente()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnGuardar.Clicked += BtnGuardar_Clicked;
        }

        private void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            {
                var DatosCliente = new T_Cliente
                {
                    Nombre = txtNombre.Text,
                    Apellidos = txtApellidos.Text,
                    Direccion = txtDireccion.Text,
                    Correo = txtCorreo.Text,
                    Telefono=txtTelefono.Text
                };
                if (String.IsNullOrWhiteSpace(txtNombre.Text)&& String.IsNullOrWhiteSpace(txtApellidos.Text)&&
                    String.IsNullOrWhiteSpace(txtDireccion.Text)&& String.IsNullOrWhiteSpace(txtCorreo.Text)&&
                    String.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    DisplayAlert("ADVERTENCIA", "Por favor ingrese todos los datos del cliente", "Aceptar");
                }
                else if (String.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    DisplayAlert("ADVERTENCIA", "Ingrese el nombre del cliente", "Aceptar");
                }
                else if (String.IsNullOrWhiteSpace(txtApellidos.Text))
                {
                    DisplayAlert("ADVERTENCIA", "Ingrese el apellido del cliente", "Aceptar");
                }
                else if (String.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    DisplayAlert("ADVERTENCIA", "Ingrese la dirección del cliente", "Aceptar");
                }
                else if (String.IsNullOrWhiteSpace(txtCorreo.Text))
                {
                    DisplayAlert("ADVERTENCIA", "Ingrese el correo electrónico del cliente", "Aceptar");
                }
                else if (String.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    DisplayAlert("ADVERTENCIA", "Ingrese teléfono del cliente", "Aceptar");
                }
                else
                {
                    conexion.InsertAsync(DatosCliente);
                    limpiarFormulario();
                    DisplayAlert("CONFIRMACIÓN", "El cliente se registro correctamente", "Aceptar");
                }
            }
            void limpiarFormulario()
            {
                txtNombre.Text = "";
                txtApellidos.Text = "";
                txtDireccion.Text = "";
                txtCorreo.Text = "";
                txtTelefono.Text = "";
            }
        }
    }
}