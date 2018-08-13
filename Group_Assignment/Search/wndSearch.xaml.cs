using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Group_Assignment.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        clsSearchLogic logic;
        /// <summary>
        /// Holds the invoice selected from this window to be passed into the main window.
        /// </summary>
        public string InvoiceNumber { get; set; }

        public wndSearch()
        {
            InitializeComponent();
            logic = new clsSearchLogic();
            this.DataContext = logic;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Hides the window instead of closing it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        
        /// <summary>
        /// Updates the datagrid when selection is made
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InvoiceCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            displayGrid.ItemsSource = logic.AllItems();
        }
    }
}
