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
    public partial class detail : ContentPage
    {
        public detail()
        {
            InitializeComponent();
            btnSalir.Clicked += BtnSalir_Clicked;
        }

        private void BtnSalir_Clicked(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}