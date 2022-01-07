using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BarCode.Datos;
using BarCode.Tablas;
using SQLite;
using System.IO;

namespace BarCode.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleFinal : ContentPage
    {
        public int idseleccionado;
        public string maqseleccionada, nombreseleccionado;
        public double cantseleccionada;
        private SQLiteAsyncConnection conexion;
        IEnumerable<T_Refacciones> ResultadoDelete;
        IEnumerable<T_Refacciones> ResultadoUpdate;

        public DetalleFinal(int id, string maquina, string nombre, double cantidad)
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            idseleccionado = id;
            maqseleccionada = maquina;
            nombreseleccionado = nombre;
            cantseleccionada = cantidad;
            BtnActualizar.Clicked += BtnActualizar_Clicked;
            BtnBorrar.Clicked += BtnBorrar_Clicked;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LblMensaje.Text = " ID " + idseleccionado;
            TxtMaquina.Text = maqseleccionada;
            TxtNombre.Text = nombreseleccionado;
            TxtCantidad.Text = cantseleccionada.ToString();
        }

        private void BtnBorrar_Clicked(object sender, EventArgs e)
        {
            var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Almacen.db3");
            var db = new SQLiteConnection(rutaDB);
            ResultadoDelete = Delete(db, idseleccionado);
            DisplayAlert("","La refacción ha sido eliminada satisfactoriamente", "ok");
            Limpiar();
        }

        private void Limpiar()
        {
            TxtNombre.Text = "";
            TxtCantidad.Text = "";
            TxtMaquina.Text = "";
        }

        private IEnumerable<T_Refacciones> Delete(SQLiteConnection db, int id)
        {
            return db.Query<T_Refacciones>("DELETE FROM T_Refacciones WHERE Id=?", id);
        }

        private void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Almacen.db3");
            var db = new SQLiteConnection(rutaDB);
            ResultadoUpdate = Update(db, TxtMaquina.Text, TxtNombre.Text , TxtCantidad.Text, idseleccionado);
            DisplayAlert("", "La refacción ha sido actualizada satisfactoriamente", "ok");
        }

        private IEnumerable<T_Refacciones> Update(SQLiteConnection db, string maquina, string nombre, string cantidad,int id)
        {
            return db.Query<T_Refacciones>("UPDATE T_Refacciones SET Maquina = ? , Nombre = ? , Cantidad = ? WHERE Id = ? ", maquina,nombre,cantidad,id);
        }
    }
}