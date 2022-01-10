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

namespace BarCode.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Consulta : ContentPage
    {
        private SQLiteAsyncConnection conexion;

        public V_Consulta()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            BtnBuscar.Clicked += BtnBuscar_Clicked;
            BtnRegistrar.Clicked += BtnRegistrar_Clicked;
        }

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
                if(resultado.Count() > 0) 
                {
                    Navigation.PushAsync(new V_Detalle());
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
            return db.Query<T_Refacciones>("SELECT Id,Nombre FROM T_Refacciones WHERE Barras=?",barras);
        }
    }
}