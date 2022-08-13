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
using System.IO;
using Zapateria.Droid;
using Zapateria.Datos;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDB))]    
namespace Zapateria.Droid
{
    public class SQLiteDB : ISQLiteDB
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(ruta, "ZapateriaSQLite.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}