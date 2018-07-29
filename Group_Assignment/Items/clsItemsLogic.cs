using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Assignment.Items
{
    class clsItemsLogic
    {


    }
    class Item

    {
        /// <summary>
        /// boolean value to store if item is being used in any invoice
        /// </summary>
        public bool isUsed { get; set; }

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

        /// Returns the description of the item.

        /// </summary>

        /// <returns>The description of the item.</returns>

        public override string ToString()

        {

            return this.Description;

        }

    }

    class LineItem

    {

        /// <summary>

        /// The line position of the item

        /// </summary>

        public int Position { get; set; }



        /// <summary>

        /// The quantity of the item

        /// </summary>

        public int Quantity { get; set; }



        /// <summary>

        /// The item being purchased.

        /// </summary>

        public Item Description { get; set; }



        /// <summary>

        /// The price of the item.

        /// </summary>

        public decimal Price { get; set; }



        /// <summary>

        /// The total of the line. (Quantity * Price)

        /// </summary>

        public decimal Total

        {

            get

            {

                return Total;

            }

            set

            {

                this.Total = this.Quantity * this.Price;

            }

        }

    }
}
