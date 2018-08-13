using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Assignment.Items
{
    class clsItemsLogic
    {
        /// <summary>
        /// boolean value that checks if an item is in an invoice
        /// </summary>
        public bool isininvoice;

        public String ItemCode { get; set; }
        public String ItemDesc { get; set; }
        public decimal? ItemPrice { get; set; }
        /// <summary>
        /// method to pull data from database
        /// </summary>
        /// <returns></returns>
        public static List<clsItemsLogic> SelectItem()
        {
            List<clsItemsLogic> items = new List<clsItemsLogic>();
            using (OleDbConnection db = clsItemsSQL.GetConnection())
            {
                db.Open();
                clsItemsSQL sql = new clsItemsSQL();
                OleDbCommand command = new OleDbCommand(sql.select(), db);
                OleDbDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    items.Add(new clsItemsLogic() { ItemCode = read.GetString(0) ?? "", ItemDesc = read.GetString(1) ?? "", ItemPrice = read.GetDecimal(2) });
                }
                db.Close();
            }
            return items;
        }
        /// <summary>
        /// overloaded delete item method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool DeleteItem(clsItemsLogic item)
        {
            return DeleteItem(item.ItemCode);
        }
        /// <summary>
        /// method to delete item from datebase
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        public static bool DeleteItem(String ItemCode)
        {
            using (OleDbConnection db = clsItemsSQL.GetConnection())
            {
                clsItemsSQL sql = new clsItemsSQL();
                db.Open();
                OleDbCommand command = new OleDbCommand(sql.deletefrom(), db);
                command.Parameters.Add(ItemCode);
                return (command.ExecuteNonQuery() == 0) ? false : true;
            }
        }
        /// <summary>
        /// method to save items to the databse
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool SaveToDatabase(clsItemsLogic item)
        {
            using (OleDbConnection db = clsItemsSQL.GetConnection())
            {
                db.Open();
                clsItemsSQL sql = new clsItemsSQL();
                OleDbCommand command = new OleDbCommand(sql.saveitem(), db);
                command.Parameters.Add(new OleDbParameter("@ItemCode", item.ItemCode));
                command.Parameters.Add(new OleDbParameter("@ItemDesc", item.ItemDesc));
                command.Parameters.Add(new OleDbParameter("@Cost", item.ItemPrice));
                return (command.ExecuteNonQuery() == 0) ? false : true;
            }
        }
        /// <summary>
        /// overloaded save method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool SaveToDatabase(List<clsItemsLogic> items)
        {
            bool flag = true;
            foreach (clsItemsLogic item in items)
            {
                flag = SaveToDatabase(item);
            }
            return flag;
        }
        /// <summary>
        /// method to update datebase items
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool UpdateItem(clsItemsLogic item)
        {
            if (item.ItemCode == null) return false;
            using (OleDbConnection db = clsItemsSQL.GetConnection())
            {
                db.Open();
                clsItemsSQL sql = new clsItemsSQL();
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
