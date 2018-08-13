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

        private List<Item> hiddenList;

        public List<Item> displayList
        {
            get
            {
                return this.hiddenList;
            }
            set
            {
                this.hiddenList = value;
            }
        }

        private List<string> hiddenNum;

        public List<string> displayNum
        {
            get
            {
                return this.hiddenNum;
            }
            set
            {
                this.hiddenNum = value;
            }
        }


        private List<string> hiddenDate;

        public List<string> displayDate
        {
            get
            {
                return this.hiddenDate;
            }
            set
            {
                this.hiddenDate = value;
            }
        }

        private List<string> hiddenCost;

        public List<string> displayCost
        {
            get
            {
                return this.hiddenCost;
            }
            set
            {
                this.hiddenCost = value;
            }
        }

        /// <summary>
        /// Constructor for the logic class.
        /// </summary>
        public clsSearchLogic()
        {
            db = new clsDataAccess();
            sql = new clsSearchSQL();
            this.displayList = AllItems();
            this.displayNum = allInvoiceNum();
            this.displayDate = allInvoiceDate();
            this.displayCost = allInvoiceCost();
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
        /// This returns a list of all InvoiceNums
        /// </summary>
        /// <returns></returns>
        public List<string> allInvoiceNum()
        {
            List<string> list = new List<string>();
            int numRows = 0;
            DataSet data = new DataSet();
            data = this.db.ExecuteSQLStatement(sql.SelectAllNum(), ref numRows);
            for (int i = 0; i < numRows; i++)
            {
                list.Add(data.Tables[0].Rows[i][0].ToString());
            }
            return list;
        }



        /// <summary>
        /// This returns a list of invoice dates
        /// </summary>
        /// <returns></returns>
        public List<string> allInvoiceDate()
        {
            List<string> list = new List<string>();
            int numRows = 0;
            DataSet data = new DataSet();
            data = this.db.ExecuteSQLStatement(sql.SelectAllDate(), ref numRows);
            for (int i = 0; i < numRows; i++)
            {
                list.Add(data.Tables[0].Rows[i][0].ToString());
            }
            return list;
        }

        public List<string> allInvoiceCost()
        {
            List<string> list = new List<string>();
            int numRows = 0;
            DataSet data = new DataSet();
            data = this.db.ExecuteSQLStatement(sql.SelectAllCost(), ref numRows);
            for (int i = 0; i < numRows; i++)
            {
                list.Add(data.Tables[0].Rows[i][0].ToString());
            }
            return list;
        }
    }
}