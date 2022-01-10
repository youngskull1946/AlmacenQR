using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BarCode.Tablas;
using SQLite;

namespace BarCode.Datos
{
    
    public interface ISQLiteDB
    {
        
        SQLiteAsyncConnection GetConnection();
    }
    
}
