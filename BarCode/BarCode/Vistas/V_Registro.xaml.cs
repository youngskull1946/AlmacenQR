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
            NavigationPage.SetHasNavigationBar(this, false);

        }

        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            
                if (validarDatos())
                {
                    T_Refacciones refac = new T_Refacciones
                    {
                        Barras = TxtID.Text,
                        Maquina = TxtMaquina.Text,
                        Nombre = TxtNombre.Text,
                        Cantidad = int.Parse(TxtCantidad.Text)
                    };
                try { 
                    await conexion.InsertAsync(refac);
                    limpiarFormulario();
                    await DisplayAlert("Alerta", "¡Refacción ingresada satisfactoriamente!", "OK");
                }
                catch (Exception)
                {
                    throw;
                }
            }
                else
                {
                    await DisplayAlert("Advertencia", "Ingresar todos los datos", "OK");
                }
            

            

            

        }

        public bool validarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(TxtID.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(TxtMaquina.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(TxtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(TxtID.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
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