using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using BarCode.Tablas;
using System.Collections.ObjectModel;
using System.IO;
using BarCode.Datos;

namespace BarCode.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Detalle : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<T_Refacciones> TablaRefacciones;
        public V_Detalle()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            ListaRefacciones.ItemSelected += ListaRefacciones_ItemSelected;
        }

        private void ListaRefacciones_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Refacciones)e.SelectedItem;
            var item = Obj.Id.ToString();
            var maquina = Obj.Maquina;
            var nom = Obj.Nombre;
            var cantidad = Obj.Cantidad;
            var cantidad2 = Convert.ToDouble(cantidad);
            var ID = Convert.ToInt32(item);
            try 
            {
                Navigation.PushAsync(new DetalleFinal(ID,maquina,nom,cantidad2));
            }
            catch (Exception) { throw; }
        }

        protected async override void OnAppearing()
        {
            var ResulRegistros = await conexion.Table<T_Refacciones>().ToListAsync();
            TablaRefacciones = new ObservableCollection<T_Refacciones>(ResulRegistros);
            ListaRefacciones.ItemsSource = TablaRefacciones;
            base.OnAppearing();
        }
    }
}