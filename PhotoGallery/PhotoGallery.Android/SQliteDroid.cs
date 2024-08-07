using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PhotoGallery.Droid;
using PhotoGallery.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQliteDroid))]

namespace PhotoGallery.Droid
{
    ///data/user/0/com.companyname.photogallery/files/.local/share/photogallery.sql
    public class SQliteDroid : Isqlite
    {
        public static string fullpath;
        public static string dbpath;
        public SQLiteConnection GetConnection()
        {
            var dbase = "photogallery.sql";
            dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            fullpath = Path.Combine(dbpath, dbase);
            var connection = new SQLiteConnection(fullpath);
            return connection;

        }
    }
}