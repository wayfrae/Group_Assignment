using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using System.Reflection;
namespace Group_Assignment.Items
{
    class clsItemsSQL

    {
        public static OleDbConnection GetConnection()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
            return conn;
        }

        public string select()
        {
            return "Select* from ItemDesc";
        }

        public string deletefrom()
        {
            return "Delete From ItemDesc Where ItemCode = @ItemCode ";
        }

        public string saveitem()
        {
            return "Insert into ItemDesc(ItemCode, ItemDesc, Cost) Values(@ItemCode, @ItemDesc, @Cost) ";
        }

        public string update()
        {
            return "Update ItemDesc Set Items = @Items, Cost = @Cost Where ItemCode = @ItemCode ";
        }

    }
}