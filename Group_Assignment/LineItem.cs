namespace Group_Assignment
{
    /// <summary>
    /// Class that represents a line on the invoice
    /// </summary>
    class LineItem
    {
        /// <summary>
        /// The line position of the item
        /// </summary>
        public int Position { get; set; }        

        /// <summary>
        /// The item being purchased.
        /// </summary>
        public Item ItemOnLine { get; set; }        
        
    }
}
