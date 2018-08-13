using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using System.Reflection;
namespace WpfApp3
{
    class Database
    {
        public static OleDbConnection GetConnection()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
            return conn;
        }

        public string select() {
            return "Select* from ItemDesc";
        }

    }
}