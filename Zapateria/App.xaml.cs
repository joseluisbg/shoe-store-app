using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Zapateria.Vistas;

namespace Zapateria
{
    public partial class App : Application
    {
        public static Principal MasterD { get; internal set; }
        public App()
        {
            InitializeComponent();

            MainPage = new Vistas.Principal();
        }
        protected override void OnStart()
        {
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
    }
}
