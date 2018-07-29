using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Group_Assignment.Main
{
    class clsMainLogic
    {
        /// <summary>
        /// Class to access database
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// Class to get SQL queries 
        /// </summary>
        clsMainSQL sql;

        /// <summary>
        /// List of all items
        /// </summary>
        public List<Item> Items { get; set; }

        /// <summary>
        /// The current invoice being displayed
        /// </summary>
        public Invoice CurrentInvoice { get; set; }

        /// <summary>
        /// Constructor for the logic class. Sets the current invoice to the most recent invoice. 
        /// </summary>
        public clsMainLogic()
        {
            db = new clsDataAccess();
            sql = new clsMainSQL();
            this.CurrentInvoice = this.GetInvoice(sql.SelectMostRecentInvoice());
            this.Items = this.GetItems();
        }

        /// <summary>
        /// Gets a list of all the inventory items to populate the combobox
        /// </summary>
        /// <returns>A list of all inventory items</returns>
        public List<Item> GetItems()
        {
            List<Item> list = new List<Item>();
            int numRows = 0;
            DataSet data = new DataSet();
            data = this.db.ExecuteSQLStatement(sql.SelectItems(), ref numRows);
            for (int i = 0; i < numRows; i++)
            {
                list.Add(new Item
                {                    
                Code = data.Tables[0].Rows[i][0].ToString(),
                Description = data.Tables[0].Rows[i][1].ToString(),
                Price = (decimal)data.Tables[0].Rows[i][2]
                });
            }
            return list;
        }        

        /// <summary>
        /// Returns the invoice with the given invoice number
        /// </summary>
        /// <param name="invoiceNumber">The invoice number of the desired invoice</param>
        /// <returns>The invoice with the given invoice</returns>
        public Invoice GetInvoice(string invoiceNumber)
        {
            try
            {
                Invoice inv = new Invoice();
                DataSet data = new DataSet();
                ObservableCollection<LineItem> lines = new ObservableCollection<LineItem>();
                int numRows = 0;
                data = this.db.ExecuteSQLStatement(sql.SelectAllInvoiceData(invoiceNumber), ref numRows);
                inv.Date = (DateTime)data.Tables[0].Rows[0][0];
                inv.Number = data.Tables[0].Rows[0][5].ToString();
                for (int i = 0; i < numRows; i++)
                {
                    lines.Add(new LineItem
                    {
                        ItemOnLine = new Item
                        {
                            Code = (string)data.Tables[0].Rows[i][1],
                            Description = (string)data.Tables[0].Rows[i][2],
                            Price = decimal.Round((decimal)data.Tables[0].Rows[i][3] * 1.00M, 2, MidpointRounding.AwayFromZero)
                        },
                        Position = (int)data.Tables[0].Rows[i][4]
                    });
                }
                inv.LineItems = lines;
                return inv;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
