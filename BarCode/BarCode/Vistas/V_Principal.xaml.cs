using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarCode.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Principal : ContentPage
    {
        
        public V_Principal()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_Registro());
        }

        void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_Consulta());
        }

        void BtnTomar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
        void BtnInventario_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_Detalle());
        }

    }
}