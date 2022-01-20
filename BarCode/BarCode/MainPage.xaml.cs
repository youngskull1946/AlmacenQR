﻿using BarCode.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using SQLite;
using BarCode.Tablas;
using System.IO;
using BarCode.Datos;
using ZXing.Net.Mobile.Forms;

namespace BarCode
{
    
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        IList<Codigo> people = new ObservableCollection<Codigo>();
        private SQLiteAsyncConnection _conn;
        IEnumerable<T_Refacciones> ResultadoUpdate;

        public MainPage()
        {
            BindingContext = people;
            _conn = DependencyService.Get<ISQLiteDB>().GetConnection();
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        


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
                            var print = "";
                            await DisplayAlert("Alerta", "Existe Refacción", "OK");  
                            var stocksStartingWithA = db.Query<T_Refacciones>("SELECT * FROM T_Refacciones WHERE Barras = ?", result);
                            foreach (var s in stocksStartingWithA)
                            {
                                print = s.Cantidad.ToString();
                            }
                            people.Add(new Codigo(result,print));
                            
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
            foreach(var s in people)
            {
                var code = s.Code;
                var quant = Convert.ToInt32(s.Quantity);
                var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Almacen.db3");
                var db = new SQLiteConnection(rutaDB);
                ResultadoUpdate = Update(db, code, quant);
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

        private IEnumerable<T_Refacciones> Update(SQLiteConnection db, string barras, int cantidad)
        {
            return db.Query<T_Refacciones>("UPDATE T_Refacciones SET Cantidad = ? WHERE Barras = ? ", cantidad,barras);
        }

    }
}
