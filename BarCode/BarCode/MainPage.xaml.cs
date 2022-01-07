using BarCode.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

    namespace BarCode
{
    
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        IList<Codigo> people = new ObservableCollection<Codigo>();
        
        public MainPage()
        {
            BindingContext = people;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        


        private async void BtnScan_Clicked(object sender, EventArgs e)
        {
            try{
                var scanner = DependencyService.Get<ScanningService>();
                var result = await scanner.ScanAsync();
                if (result != null) 
                {
                    TxtBarcode.Text = result;
                    people.Add(new Codigo(result));
                }
            } catch (Exception) { throw; }
        }

        void BtnEnviar_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.DisplayAlert("", "Objetos Retirados del Inventario", "Ok");
        }

        void BtnBorrar_Clicked(object sender, EventArgs e)
        {
            people.Clear();
        }



    }
}
