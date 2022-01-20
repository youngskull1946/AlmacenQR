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
        public string maqseleccionada, nombreseleccionado,barrasseleccionado;
        public double cantseleccionada;
        private SQLiteAsyncConnection conexion;
        IEnumerable<T_Refacciones> ResultadoDelete;
        IEnumerable<T_Refacciones> ResultadoUpdate;

        public DetalleFinal(int id, string maquina, string nombre, int cantidad,string barras)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            idseleccionado = id;
            maqseleccionada = maquina;
            nombreseleccionado = nombre;
            cantseleccionada = cantidad;
            barrasseleccionado = barras; 
            BtnActualizar.Clicked += BtnActualizar_Clicked;
            BtnBorrar.Clicked += BtnBorrar_Clicked;
            BtnBorrarTodo.Clicked += BtnBorrarTodo_Clicked;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LblMensaje.Text = " Código de Barras: " + barrasseleccionado;
            TxtMaquina.Text = maqseleccionada;
            TxtNombre.Text = nombreseleccionado;
            TxtCantidad.Text = cantseleccionada.ToString();
            TxtBarras.Text = barrasseleccionado;
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
            TxtBarras.Text = "";
        }

        
        private void BtnBorrarTodo_Clicked(object sender, EventArgs e)
        {
            var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Almacen.db3");
            var db = new SQLiteConnection(rutaDB);
            ResultadoDelete = DeleteAll(db);
            DisplayAlert("", "Todas las refacciones han sido borradas satisfactoriamente", "ok");
            Limpiar();
        }
        private void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Almacen.db3");
            var db = new SQLiteConnection(rutaDB);
            ResultadoUpdate = Update(db, TxtMaquina.Text, TxtNombre.Text , TxtCantidad.Text, TxtBarras.Text, idseleccionado);
            DisplayAlert("", "La refacción ha sido actualizada satisfactoriamente", "ok");
        }

        private IEnumerable<T_Refacciones> Update(SQLiteConnection db, string maquina, string nombre, string cantidad, string barras,int id)
        {
            return db.Query<T_Refacciones>("UPDATE T_Refacciones SET Maquina = ? , Nombre = ? , Cantidad = ? , Barras = ? WHERE Id = ? ", maquina,nombre,cantidad,barras,id);
        }

        private IEnumerable<T_Refacciones> Delete(SQLiteConnection db, int id)
        {
            return db.Query<T_Refacciones>("DELETE FROM T_Refacciones WHERE Id=?", id);
        }


        private IEnumerable<T_Refacciones> DeleteAll(SQLiteConnection db)
        {
            return db.Query<T_Refacciones>("DELETE FROM T_Refacciones");
        }

    }
}