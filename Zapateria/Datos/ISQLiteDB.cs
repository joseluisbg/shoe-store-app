using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace Zapateria.Datos
{
    public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
