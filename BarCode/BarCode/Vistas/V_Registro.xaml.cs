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
    public partial class V_Registro : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public V_Registro()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            BtnRegistrar.Clicked += BtnRegistrar_Clicked; 
        }

        private void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            var DatosRefaccion = new T_Refacciones
            {
                Maquina = TxtMaquina.Text,
                Nombre = TxtNombre.Text,
                Cantidad = TxtCantidad.Text    
            };
            conexion.InsertAsync(DatosRefaccion);
            limpiarFormulario();
            DisplayAlert("Alerta", "¡Refacción ingresada satisfactoriamente!", "OK");

        }

        private void limpiarFormulario()
        {
            TxtID.Text = "";
            TxtNombre.Text = "";
            TxtMaquina.Text = "";
            TxtCantidad.Text = "";
        }
    }
}