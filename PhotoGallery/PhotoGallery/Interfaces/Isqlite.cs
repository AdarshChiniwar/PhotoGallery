using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoGallery.Interfaces
{
    public interface Isqlite
    {
        SQLiteConnection GetConnection();
    }
}
