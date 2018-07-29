using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Assignment.Main
{
    /// <summary>
    /// Class that holds SQL queries to run on the database
    /// </summary>
    class clsMainSQL
    {
        /// <summary>
        /// Returns SQL query string for most recent invoice
        /// </summary>
        /// <returns>SQL query string for most recent invoice</returns>
        public string SelectMostRecentInvoice()
        {
            return "SELECT MAX(InvoiceNum) FROM Invoices";
        }

        /// <summary>
        /// Returns a SQL query string that selects all invoice data for the given invoice number
        /// </summary>
        /// <param name="invoiceNumber">The invoice number for the desired invoice</param>
        /// <returns>A query string to retrieve all data for the given invoice</returns>
        public string SelectAllInvoiceData(string invoiceNumber)
        {
            return "SELECT Invoices.InvoiceDate, ItemDesc.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost, LineItems.LineItemNum, Invoices.InvoiceNum " +
                "FROM ItemDesc INNER JOIN(Invoices INNER JOIN LineItems ON Invoices.[InvoiceNum] = LineItems.[InvoiceNum])" +
                " ON ItemDesc.[ItemCode] = LineItems.[ItemCode] WHERE Invoices.InvoiceNum = (" + invoiceNumber + ") ORDER BY LineItems.LineItemNum";
        }

        /// <summary>
        /// Returns a SQL query string to select all items from the database
        /// </summary>
        /// <returns>SQL query string to select all items from the database</returns>
        public string SelectItems()
        {
            return "SELECT * From ItemDesc";
        }

        /// <summary>
        /// Returns an SQL insert string to add an entry to the Invoices table
        /// </summary>
        /// <param name="invoiceNumber">invoice number to add</param>
        /// <param name="date">date to add</param>
        /// <returns>SQL insert string to add an entry to the Invoices table</returns>
        public string InsertToInvoices(string invoiceNumber, DateTime date)
        {
            return "INSERT INTO Invoices(InvoiceNum, Date) VALUES(" + invoiceNumber + ", " + date.Date.ToOADate() + ")";
        }

        /// <summary>
        /// Returns an SQL insert string to add an entry to the LineItems table
        /// </summary>
        /// <param name="invoiceNumber">the invoice number to insert</param>
        /// <param name="position">the line position to insert</param>
        /// <param name="itemCode">the item code to insert</param>
        /// <returns></returns>
        public string InsertToLineItems(string invoiceNumber, int position, string itemCode)
        {
            return "INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) VALUES(" + invoiceNumber + ", " + position + ", " + itemCode + ")";
        }

         /// <summary>
         /// Returns an SQL update string to edit an entry to the LineItems table
         /// </summary>
         /// <param name="invoiceNumber">the invoice number to update</param>
         /// <param name="position">the line position to update</param>
         /// <param name="itemCode">the item code to update</param>
         /// <returns></returns>
        public string UpdateLineItems(string invoiceNumber, int position, string itemCode)
        {
            return "UPDATE LineItems SET LineItemNum=" + position + ", ItemCode=" + itemCode + "WHERE InvoiceNum=" + invoiceNumber;
        }

        /// <summary>
        /// Returns an SQL update string to edit an entry to the Invoices table
        /// </summary>
        /// <param name="invoiceNumber">invoice number to edit</param>
        /// <param name="date">date to edit</param>
        /// <returns>SQL update string to add an entry to the Invoices table</returns>
        public string UpdateInvoices(string invoiceNumber, DateTime date)
        {
            return "UPDATE Invoices SET Date=" + date.Date.ToOADate() + " WHERE InvoiceNum=" + invoiceNumber;
        }


    }
}
