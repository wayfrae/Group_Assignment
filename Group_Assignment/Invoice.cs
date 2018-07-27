using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Group_Assignment
{
    /// <summary>
    /// Class that represents an invoice
    /// </summary>
    class Invoice
    {
        /// <summary>
        /// The invoice number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The invoice date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// A collection of all the lines on the invoice
        /// </summary>
        public ObservableCollection<LineItem> LineItems { get; set; }

        /// <summary>
        /// The total due for all items.
        /// </summary>
        public string Total
        {
            get
            {
                try
                {
                    decimal count = 0;
                    foreach (LineItem line in LineItems)
                    {
                        count += line.ItemOnLine.Price;
                    }
                    return string.Format("Total Due: ${0}", (count * 1.00M).ToString("F"));
                }
                catch (Exception ex)
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                        + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }
            }
        }
    }
}
