using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Group_Assignment.Items;
using Group_Assignment.Main;
using Group_Assignment.Search;
using MaterialDesignThemes.Wpf;

namespace Group_Assignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        /// <summary>
        /// Class to hold window for items
        /// </summary>
        wndItems windowItems;

        /// <summary>
        /// Class to hold window for search
        /// </summary>
        wndSearch windowSearch;

        /// <summary>
        /// Class to hold all business logic
        /// </summary>
        clsMainLogic mainLogic;

        /// <summary>
        /// Constructor for main window
        /// </summary>
        public wndMain()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                windowItems = new wndItems();
                windowSearch = new wndSearch();
                mainLogic = new clsMainLogic();
                StackPanelMain.DataContext = mainLogic;
                ComboBoxItems.ItemsSource = mainLogic.Items;
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Opens Items Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemUpdateItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                windowItems.Left = this.Left;
                windowItems.Top = this.Top;
                this.Hide();
                windowItems.ShowDialog();
                //TODO: Check the flag for an update and update the items dropdown if there was a change
                //check if there was a change to the items, if there was a change update the items dropdown
                //if(windowItems.HasChanged == true)
                //{
                //    mainLogic.GetItems();
                //}
                this.Left = windowItems.Left;
                this.Top = windowItems.Top;
                this.Show();
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        
        /// <summary>
        /// Opens Search Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                windowSearch.Left = this.Left;
                windowSearch.Top = this.Top;
                this.Hide();
                windowSearch.ShowDialog();
                //TODO: Get the selected invoice and display it
                //get the invoice selected by the search
                //mainLogic.CurrentInvoice = mainLogic.GetInvoice(windowSearch.InvoiceNumber);
                this.Left = windowSearch.Left;
                this.Top = windowSearch.Top;
                this.Show();
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Allows the datagrid to be edited
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EditInvoice.IsChecked == true)
                {
                    DataGridOrderSummary.CanUserAddRows = true;
                    ButtonEditInvoice.ToolTip = "Save Invoice";
                }
                else
                {
                    DataGridOrderSummary.CanUserAddRows = false;
                    ButtonEditInvoice.ToolTip = "Edit Invoice";
                }
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handles the exception by showing a message box with user friendly stack trace. Will write to Console.Error if message box fails.
        /// </summary>
        /// <param name="className">The name of the calling class.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="message">The message associated with the exception.</param>
        private void HandleException(string className, string methodName, string message)
        {
            try
            {
                MessageBox.Show(className + "." + methodName + " -> " + message);
            }
            catch (Exception ex)
            {
                TextWriter errorWriter = Console.Error;
                errorWriter.WriteLine("HandleError Exception: " + ex.Message);
            }
        }

        
    }
}
