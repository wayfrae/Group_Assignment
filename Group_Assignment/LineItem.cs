using System;

=======
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace Group_Assignment
{
    /// <summary>
    /// Class that represents a line on the invoice
    /// </summary>
    class LineItem : INotifyPropertyChanged
    {
        /// <summary>
        /// Line position value
        /// </summary>
        private int position;

        /// <summary>
        /// The item being purchased value
        /// </summary>
        private Item itemOnLine;

        /// <summary>
        /// The line position of the item
        /// </summary>
        public int Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The item being purchased.
        /// </summary>

        public Item ItemOnLine { get; set; }


=======
        public Item ItemOnLine
        {
            get
            {
                return this.itemOnLine;
            }
            set
            {
                this.itemOnLine = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Event to notify a property has changed
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
