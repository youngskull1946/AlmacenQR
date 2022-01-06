using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Xamarin.Forms;
using System.IO;
using BarCode.Datos;
using BarCode.Droid;
[assembly:Dependency(typeof(SQLiteDB))]

namespace BarCode.Droid
{
    public class SQLiteDB: ISQLiteDB
    {
        public SQLiteAsyncConnection GetConnection(){
            var ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //Se crea la base de datos
            var path = Path.Combine(ruta, "Almacen.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}