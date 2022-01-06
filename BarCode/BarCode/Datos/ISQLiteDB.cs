using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BarCode.Datos
{
    public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
