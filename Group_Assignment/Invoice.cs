using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Group_Assignment
{
    /// <summary>
    /// Class that represents an invoice
    /// </summary>
    class Invoice : INotifyPropertyChanged
    {
        /// <summary>
        /// Holds line items
        /// </summary>
        private ObservableCollection<LineItem> lineItems;

        /// <summary>
        /// The the total as a string
        /// </summary>
        private string total;

        /// <summary>
        /// the value of Number
        /// </summary>
        private string number;

        /// <summary>
        /// The invoice number
        /// </summary>
        public string Number
        {
            get
            {
                return this.number;
            }
            set
            {
                this.number = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The invoice date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// A collection of all the lines on the invoice
        /// </summary>
        public ObservableCollection<LineItem> LineItems
        {
            get
            {
                return this.lineItems;
            }
            set
            {
                this.lineItems = value;      
            }
        }

        /// <summary>
        /// The total due for all items.
        /// </summary>
        public string Total
        {
            get
            {                
                return this.total;
            }
            set
            {
                this.total = value;
            }
        }

        /// <summary>
        /// Constructor for Invoice
        /// </summary>
        public Invoice()
        {
            this.LineItems = new ObservableCollection<LineItem>();
            this.GetTotal();
        }


        /// <summary>
        /// Calculates the total of all lines
        /// </summary>
        public void GetTotal()
        {
            try
            {
                decimal count = 0;
                foreach (LineItem line in LineItems)
                {
                    if(line.ItemOnLine != null)
                    {
                        count += line.ItemOnLine.Price;
                    }
                }
                this.total = string.Format("Total Due: ${0}", (count * 1.00M).ToString("F"));
                NotifyPropertyChanged("Total");

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Event for when a property has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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
    }
}
