using BarCode.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using SQLite;
using BarCode.Tablas;
using System.IO;
using BarCode.Datos;

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
                    try
                    {
                        var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Almacen.db3");
                        var db = new SQLiteConnection(rutaDB);
                        db.CreateTable<T_Refacciones>();
                        IEnumerable<T_Refacciones> resultado = SELECT_WHERE(db, result);
                        if (resultado.Count() > 0)
                        {
                            
                            await DisplayAlert("Alerta", "Existe Refacción", "OK");
                            
                            TxtBarcode.Text = result;
                            people.Add(new Codigo(result));
                        }
                        else
                        {
                            await DisplayAlert("Alerta", "No existen Refacciones con ese código", "Ok");
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    
                }
            } catch (Exception) { throw; }
        }

        void BtnEnviar_Clicked(object sender, EventArgs e)
        {
            LvElementos.
            Application.Current.MainPage.DisplayAlert("", "", "ok");
        }

        void BtnBorrar_Clicked(object sender, EventArgs e)
        {
            people.Clear();
        }

        private IEnumerable<T_Refacciones> SELECT_WHERE(SQLiteConnection db, string barras)
        {

            return db.Query<T_Refacciones>("SELECT * FROM T_Refacciones WHERE Barras=?", barras);
        }



    }
}
