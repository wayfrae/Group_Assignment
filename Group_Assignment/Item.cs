using System;
using System.Reflection;

namespace Group_Assignment
{
    /// <summary>
    /// Class that represents an inventory item.
    /// </summary>
    class Item
    {    

        /// <summary>
        /// The code associated with the item
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The description of the item
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The price of the item.
        /// </summary>
        public decimal Price { get; set; }
        

        /// <summary>
        /// The price of the item formatted as $0.00 
        /// </summary>
        public string PriceString
        {
            get
            {
                return this.Price.ToString("N");
            }
            set
            {
                try
                {
                    this.PriceString = value;
                    value.Trim('$');
                    value.Trim(',');
                    this.Price = decimal.Parse(value);
                }
                catch (Exception ex)
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                        + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Returns the description of the item.
        /// </summary>
        /// <returns>The description of the item.</returns>
        public override string ToString()
        {
            return this.Description;
        }
    }
}
