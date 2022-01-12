using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using BarCode.Tablas;
using System.Threading.Tasks;

namespace BarCode
{
    
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<T_Refacciones>().Wait();
        }


       

    }

}
