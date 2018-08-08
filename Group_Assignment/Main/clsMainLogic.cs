﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Group_Assignment.Main
{
    class clsMainLogic : INotifyPropertyChanged
    {
        private Invoice currentInvoice;
        /// <summary>
        /// Class to access database
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// Class to get SQL queries 
        /// </summary>
        clsMainSQL sql;

        /// <summary>
        /// Event for when a property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// List of all items
        /// </summary>
        public List<Item> Items { get; set; }

        /// <summary>
        /// The current invoice being displayed
        /// </summary>
        public Invoice CurrentInvoice
        {
            get
            {
                return this.currentInvoice;
            }
            set
            {
                this.currentInvoice = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Constructor for the logic class. Sets the current invoice to the most recent invoice. 
        /// </summary>
        public clsMainLogic()
        {
            db = new clsDataAccess();
            sql = new clsMainSQL();

            //get invoice using a sql subquery
            this.CurrentInvoice = this.GetInvoice(sql.SelectMostRecentInvoice());

            this.CurrentInvoice.GetTotal();
            this.Items = this.GetItems();
        }        

        /// <summary>
        /// Update current invoice to database
        /// </summary>
        public void UpdateDatabase()
        {
            db.ExecuteNonQuery(sql.UpdateInvoices(CurrentInvoice.Number, CurrentInvoice.Date));
            foreach(LineItem line in CurrentInvoice.LineItems)
            {
                db.ExecuteNonQuery(sql.UpdateLineItems(CurrentInvoice.Number, line.Position, line.ItemOnLine.Code));
            }
            CurrentInvoice = GetInvoice(sql.SelectMostRecentInvoice());
        }

        /// <summary>
        /// Save current invoice to database
        /// </summary>
        public void SaveToDatabase()
        {
            db.ExecuteNonQuery(sql.InsertToInvoices(CurrentInvoice.Date));

            CurrentInvoice.Number = db.ExecuteScalarSQL(sql.SelectMostRecentInvoice());
            foreach (LineItem line in CurrentInvoice.LineItems)
            {
                Console.WriteLine(sql.InsertToLineItems(CurrentInvoice.Number, line.Position, line.ItemOnLine.Code));
                db.ExecuteNonQuery(sql.InsertToLineItems(CurrentInvoice.Number, line.Position, line.ItemOnLine.Code));
            }
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
        /// Sends notification that the property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
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
                if(numRows > 0)
                {
                    inv.Date = (DateTime)data.Tables[0].Rows[0][0];
                    inv.Number = data.Tables[0].Rows[0][5].ToString();
                }
                
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
