using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BarCode.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SQLite;
using BarCode.Tablas;
using System.IO;
using BarCode.Datos;



namespace BarCode.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class Sustraer : ContentPage
    {
        public Sustraer()
        {
            BindingContext = people;
            _conn = DependencyService.Get<ISQLiteDB>().GetConnection();
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        IList<OCode> people = new ObservableCollection<OCode>();
        private SQLiteAsyncConnection _conn;
        IEnumerable<T_Refacciones> ResultadoUpdate;


        private async void BtnScan_Clicked(object sender, EventArgs e)
        {
            
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
                            string nombre = "", machin="";
                            await DisplayAlert("Alerta", "Existe Refacción", "OK");
                            var stocksStartingWithA = db.Query<T_Refacciones>("SELECT * FROM T_Refacciones WHERE Barras = ?", result);
                            foreach (var s in stocksStartingWithA)
                            {
                                nombre = s.Nombre;
                                machin = s.Maquina;
                            }

                            people.Add(new OCode(result, nombre,machin));

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
                
            
        }

        void BtnEnviar_Clicked(object sender, EventArgs e)
        {
            foreach (var s in people)
            {
                var code = s.Code;
                var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Almacen.db3");
                var db = new SQLiteConnection(rutaDB);
                ResultadoUpdate = Update(db, code);
            }
            people.Clear();
            Application.Current.MainPage.DisplayAlert("", " Objetos Actualizados del Inventario", "ok");

        }

        void BtnBorrar_Clicked(object sender, EventArgs e)
        {
            people.Clear();
        }

        private IEnumerable<T_Refacciones> SELECT_WHERE(SQLiteConnection db, string barras)
        {

            return db.Query<T_Refacciones>("SELECT * FROM T_Refacciones WHERE Barras=?", barras);
        }

        private IEnumerable<T_Refacciones> Update(SQLiteConnection db, string barras)
        {
            return db.Query<T_Refacciones>("UPDATE T_Refacciones SET Cantidad = Cantidad - 1 WHERE Barras = ? ", barras);
        }
    }
}