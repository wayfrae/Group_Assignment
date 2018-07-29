using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Assignment.Items
{
    class clsItemsLogic
    {
        /// <summary>
        /// boolean value to store if item is being used in any invoice
        /// </summary>
        public bool isUsed { get; set; }

        /// <summary>
        /// boolean value to store if an item has changed and needs to be updated in the database
        /// </summary>
        public bool HasChanged { get; set; }
    }
    
}