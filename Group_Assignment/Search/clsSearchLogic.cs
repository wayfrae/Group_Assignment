using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public bool numSearch;
        public bool dateSearch;
        public bool costSearch;

        private List<Invoice> hiddenList;

        public List<Invoice> displayList
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
            numSearch = false;
            dateSearch = false;
            costSearch = false;
        }

        /// <summary>
        /// List of all items
        /// </summary>
        /// List of all items
        /// </summary>
        public List<Invoice> AllItems()
        {
            List<Invoice> list = new List<Invoice>();
            List<LineItem> lineList = new List<LineItem>();
            int numRows = 0;
            DataSet data = new DataSet();
            data = this.db.ExecuteSQLStatement(sql.SelectAllInvoiceData(), ref numRows);
            for (int i = 0; i < numRows; i++)
            {
                int index = list.FindIndex(x => x.Number == data.Tables[0].Rows[i][5].ToString());
                if (index >= 0)
                {
                    list[index].LineItems.Add(new LineItem
                    {
                        Position = (int)data.Tables[0].Rows[i][4],
                        ItemOnLine = new Item
                        {
                            Code = data.Tables[0].Rows[i][1].ToString(),
                            Description = data.Tables[0].Rows[i][2].ToString(),
                            Price = (decimal)data.Tables[0].Rows[i][3]

                        }
                    });
                }
                else
                {
                    ObservableCollection<LineItem> lineItems = new ObservableCollection<LineItem>();
                    lineItems.Add(new LineItem
                    {
                        Position = (int)data.Tables[0].Rows[i][4],
                        ItemOnLine = new Item
                        {
                            Code = data.Tables[0].Rows[i][1].ToString(),
                            Description = data.Tables[0].Rows[i][2].ToString(),
                            Price = (decimal)data.Tables[0].Rows[i][3]
                        }
                    });

                    list.Add(new Invoice
                    {
                        Number = data.Tables[0].Rows[i][5].ToString(),
                        Date = DateTime.Parse(data.Tables[0].Rows[i][0].ToString()),
                        LineItems = new ObservableCollection<LineItem>(lineItems)
                    });
                }
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


        public List<Invoice> SearchItems(string num, string date, string cost)
        {
            List<Invoice> list = new List<Invoice>();
            List<LineItem> lineList = new List<LineItem>();
            int numRows = 0;
            DataSet data = new DataSet();
            data = this.db.ExecuteSQLStatement(sql.SelectInvoiceData(numSearch, dateSearch, costSearch, num, date, cost), ref numRows);
            for (int i = 0; i < numRows; i++)
            {
                int index = list.FindIndex(x => x.Number == data.Tables[0].Rows[i][5].ToString());
                if (index >= 0)
                {
                    list[index].LineItems.Add(new LineItem
                    {
                        Position = (int)data.Tables[0].Rows[i][4],
                        ItemOnLine = new Item
                        {
                            Code = data.Tables[0].Rows[i][1].ToString(),
                            Description = data.Tables[0].Rows[i][2].ToString(),
                            Price = (decimal)data.Tables[0].Rows[i][3]

                        }
                    });
                }
                else
                {
                    ObservableCollection<LineItem> lineItems = new ObservableCollection<LineItem>();
                    lineItems.Add(new LineItem
                    {
                        Position = (int)data.Tables[0].Rows[i][4],
                        ItemOnLine = new Item
                        {
                            Code = data.Tables[0].Rows[i][1].ToString(),
                            Description = data.Tables[0].Rows[i][2].ToString(),
                            Price = (decimal)data.Tables[0].Rows[i][3]
                        }
                    });

                    list.Add(new Invoice
                    {
                        Number = data.Tables[0].Rows[i][5].ToString(),
                        Date = DateTime.Parse(data.Tables[0].Rows[i][0].ToString()),
                        LineItems = new ObservableCollection<LineItem>(lineItems)
                    });
                }
            }

            return list;
        }
    }
}