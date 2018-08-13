using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3
{
    class Item
    {
         

        public String ItemCode { get; set; }
        public String ItemDesc { get; set; }
        public decimal? ItemPrice { get; set; }

        public static List<Item> SelectItem()
        {
            List<Item> items = new List<Item>();
            using (OleDbConnection db = Database.GetConnection())
            {
                db.Open();
                Database sql = new Database();
                OleDbCommand command = new OleDbCommand(sql.select(), db);
                OleDbDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    items.Add(new Item() { ItemCode = read.GetString(0) ?? "", ItemDesc = read.GetString(1) ?? "", ItemPrice = read.GetDecimal(2) });
                }
                db.Close();
            }
            return items;
        }

        public static bool DeleteItem(Item item)
        {
            return DeleteItem(item.ItemCode);
        }

        public static bool DeleteItem(String ItemCode)
        {
            using (OleDbConnection db = Database.GetConnection())
            {
                Database sql = new Database();
                db.Open();
                OleDbCommand command = new OleDbCommand(sql.deletefrom(), db);
                command.Parameters.Add(ItemCode);
                return (command.ExecuteNonQuery() == 0) ? false : true;
            }
        }

        public static bool SaveToDatabase(Item item)
        {
            using (OleDbConnection db = Database.GetConnection())
            {
                db.Open();
                Database sql = new Database();
                OleDbCommand command = new OleDbCommand(sql.saveitem(), db);
                command.Parameters.Add(new OleDbParameter("@ItemCode", item.ItemCode));
                command.Parameters.Add(new OleDbParameter("@ItemDesc", item.ItemDesc));
                command.Parameters.Add(new OleDbParameter("@Cost", item.ItemPrice));
                return (command.ExecuteNonQuery() == 0) ? false : true;
            }
        }

        public static bool SaveToDatabase(List<Item> items)
        {
            bool flag = true;
            foreach (Item item in items)
            {
                flag = SaveToDatabase(item);
            }
            return flag;
        }

        public static bool UpdateItem(Item item)
        {
            if (item.ItemCode == null) return false;
            using (OleDbConnection db = Database.GetConnection())
            {
                db.Open();
                Database sql = new Database();
                OleDbCommand command = new OleDbCommand(sql.update(), db);
                command.Parameters.AddRange(new OleDbParameter[] {
                new OleDbParameter("@Items", item.ItemDesc),
                new OleDbParameter("@Cost", item.ItemPrice),
                new OleDbParameter("@ItemCode", item.ItemCode) });
                return (command.ExecuteNonQuery() == 0) ? false : true;
            }
        }
    }
}
