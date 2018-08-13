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

        public string SelectAll()
        {
            string sSQL = "SELECT * FROM ItemDesc";

            return sSQL;
        }


        /// <summary>
        /// This SQL gets all Nums for all invoices.
        /// </summary>
        /// <returns>All data for the invoices.</returns>

        public string SelectAllNum()
        {
            string sSQL = "SELECT DISTINCT InvoiceNum FROM Invoices";

            return sSQL;
        }

        /// <summary>
        /// This SQL gets all dates for all invoices
        /// </summary>
        /// <returns></returns>
        public string SelectAllDate()
        {
            string sSQL = "SELECT DISTINCT InvoiceDate FROM Invoices";

            return sSQL;
        }


        /// <summary>
        /// This SQL gets all costs for all invoices
        /// </summary>
        /// <returns></returns>
        public string SelectAllCost()
        {
            string sSQL = "SELECT DISTINCT Cost FROM ItemDesc";

            return sSQL;
        }
    }
}