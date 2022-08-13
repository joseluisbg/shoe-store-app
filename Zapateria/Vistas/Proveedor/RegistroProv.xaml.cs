using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using Zapateria.Tablas;
using Zapateria.Datos;

namespace Zapateria.Vistas.Proveedor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroProv : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public RegistroProv()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnGuardar.Clicked += BtnGuardar_Clicked;
        }

        private void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            {
                var DatosProveedor = new T_Proveedor
                {
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    Direccion = txtDireccion.Text,
                    Telefono = txtTelefono.Text
                };
                if (String.IsNullOrWhiteSpace(txtNombre.Text)&&String.IsNullOrWhiteSpace(txtDescripcion.Text)
                    && String.IsNullOrWhiteSpace(txtDireccion.Text)&& String.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    DisplayAlert("ADVERTENCIA", "Ingrese por favor todos los datos del proveedor", "Aceptar");
                }
                else if (String.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    DisplayAlert("ADVERTENCIA", "Ingrese el nombre del proveedor", "Aceptar");
                }
                else if (String.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    DisplayAlert("ADVERTENCIA", "Ingrese la descripción del proveedor", "Aceptar");
                }else if (String.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    DisplayAlert("ADVERTENCIA", "Ingrese la dirección del proveedor", "Aceptar");
                }else if (String.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    DisplayAlert("ADVERTENCIA", "Ingrese el teléfono del proveedor", "Aceptar");
                }
                else { 
                conexion.InsertAsync(DatosProveedor);
                
                DisplayAlert("CONFIRMACIÓN", "El Proveedor:" + txtNombre.Text + "\n\n ¡Se registro correctamente!", "Aceptar");
                limpiarFormulario();

                }
                void limpiarFormulario()
                {
                    txtNombre.Text = "";
                    txtDescripcion.Text = "";
                    txtDireccion.Text = "";
                    txtTelefono.Text = "";
                }
            }
        }
    }
}