using Acr.UserDialogs;
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

        async void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new V_Registro());
        }

        async void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new V_Consulta());
        }

        async void BtnTomar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        async void BtnResurtir_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Ingresar());
        }

        async void BtnSustraer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Sustraer());
        }

        async void BtnInventario_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new V_Detalle());
        }


    }


   
}