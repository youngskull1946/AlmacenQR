using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using BarCode.Tablas;
using System.IO;
using BarCode.Datos;
using BarCode.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace BarCode.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class V_Consulta : ContentPage
    {
        
        private SQLiteAsyncConnection conexion;

        public V_Consulta()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            
        }

        IList<OCode> people = new ObservableCollection<OCode>();
        

        private void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_Registro());
        }

        private void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            try 
            {
                var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Almacen.db3");
                var db = new SQLiteConnection(rutaDB);
                db.CreateTable<T_Refacciones>();
                IEnumerable<T_Refacciones> resultado = SELECT_WHERE(db, TxtidR.Text);

                int id = 0, cant=0;
                string maq = "", nom = "", barr="";
                
                if (resultado.Count() > 0)
                {
                    foreach (var s in resultado)
                    {
                        id = s.Id;
                        maq = s.Maquina;
                        nom = s.Nombre;
                        cant = s.Cantidad;
                        barr = s.Barras;
                    }
                    Navigation.PushAsync(new DetalleFinal(id,maq,nom,cant,barr));
                    DisplayAlert("Alerta", "Existe Refacción", "OK");
                }
                else
                {
                    DisplayAlert("Alerta", "No existen Refacciones con ese código", "Ok");
                }
            }
            catch (Exception)
            { 
                throw;
            }

        }

        private IEnumerable<T_Refacciones> SELECT_WHERE(SQLiteConnection db, string barras)
        {
            return db.Query<T_Refacciones>("SELECT * FROM T_Refacciones WHERE Barras=?",barras);
        }

        private async void BtnEscaner_Clicked(object sender, EventArgs e)
        {
            var scanner = DependencyService.Get<ScanningService>();

            var result = await scanner.ScanAsync();
            if (result != null)
            {
                TxtidR.Text = result;
            }
        }
    }
}