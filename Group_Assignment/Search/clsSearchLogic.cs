using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Group_Assignment.Search
{
    class clsSearchLogic
    {
        /// <summary>
        /// Class to access database
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// Class to get SQL queries 
        /// </summary>
        clsSearchSQL sql;

        /// <summary>
        /// Constructor for the logic class.
        /// </summary>
        public clsSearchLogic()
        {
            db = new clsDataAccess();
            sql = new clsSearchSQL();
        }

        /// <summary>
        /// List of all items
        /// </summary>
        public List<Item> AllItems()
        {
            List<Item> list = new List<Item>();
            int numRows = 0;
            DataSet data = new DataSet();
            data = this.db.ExecuteSQLStatement(sql.SelectAll(), ref numRows);
                for (int i = 0; i<numRows; i++)
                {
                    list.Add(new Item
                    {
                        Code = data.Tables[0].Rows[i][0].ToString(),
                        Description = data.Tables[0].Rows[i][1].ToString(),
                        Price = (decimal) data.Tables[0].Rows[i][2]
                    });
                }
            return list;
        }

        /// <summary>
        /// This returns a list of all InvoiceIds
        /// </summary>
        /// <returns></returns>
        public List<Item> allId()
        {
            List<Item> list = new List<Item>();
            int numRows = 0;
            DataSet data = new DataSet();
            data = this.db.ExecuteSQLStatement(sql.SelectAllId(), ref numRows);
            for (int i = 0; i < numRows; i++)
            {
                list.Add(new Item
                {
                    Code = data.Tables[0].Rows[i][0].ToString()

                });
            }
            return list;
        }

        //These properties will be used to transfer data
        public String getInvoiceId { get; set; }
        public String getInvoiceDate { get; set; }
        public String GetId { get; set; }
    }
}