using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using SQLite;
using Xamarin.Forms;
using System.IO;
using BarCode.Datos;
using BarCode.iOS;
[assembly: Dependency(typeof(SQLiteDB))]

namespace BarCode.iOS
{
    public class SQLiteDB : ISQLiteDB
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //Se crea la base de datos
            var path = Path.Combine(ruta, "Almacen.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}