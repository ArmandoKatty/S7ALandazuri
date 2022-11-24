using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace S7ALandazuri
{
    public interface Database
    {
        SQLiteAsyncConnection GetConnection();
    }
}
