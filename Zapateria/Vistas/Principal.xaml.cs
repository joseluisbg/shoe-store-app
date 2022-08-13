using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zapateria.Vistas
{
    
    public partial class Principal : MasterDetailPage
    {
        public Principal()
        {
            InitializeComponent();
            this.Master = new master();
            this.Detail = new NavigationPage(new detail());

            App.MasterD = this;
        }
    }
}