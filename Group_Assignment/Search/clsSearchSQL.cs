using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Assignment.Search
{
    class clsSearchSQL
    {
        /// <summary>
        /// This SQL gets all data on an invoice for all invoices.
        /// </summary>
        /// <returns>All data for the invoices.</returns>
        
        public string SelectAllInvoiceData()
        {
            string sSQL = "SELECT * FROM Invoices";

            return sSQL;
        }
        /// <summary>
        /// This SQL gets all data on an invoice for a given InvoiceID.
        /// </summary>
        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>

        public string SelectIdInvoiceData(string sInvoiceID)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID;

            return sSQL;
        }
        /// <summary>
        /// This SQL gets all data on an invoice for all invoices on a given InvoiceDate.
        /// </summary>
        /// <param name="sInvoiceDate">The InvoiceDate for the invoice to retrieve all data.</param>
        /// <returns>All data for the given InvoiceDate.</returns>

        public string SelectDateInvoiceData(string sInvoiceDate)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = " + sInvoiceDate;

            return sSQL;
        }
        /// <summary>
        /// This SQL gets all data on an invoice for all invoices with an ItemCode.
        /// </summary>
        /// <param name="sItemCode">The ItemCode for the invoice to retrieve all data.</param>
        /// <returns>All data for the given ItemCode.</returns>

        public string SelectCodeInvoiceData(string sItemCode)
        {
            string sSQL = "SELECT * FROM Invoices i JOIN LineItems l on i.InvoiceNum = l.InvoiceNum WHERE ItemCode = " + sItemCode;

            return sSQL;
        }
    }
}
