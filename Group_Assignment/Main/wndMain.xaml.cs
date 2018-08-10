using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                GridMain.DataContext = mainLogic;
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
                MenuToggleButton.IsChecked = false; //close drawer
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
                MenuToggleButton.IsChecked = false; //close drawer
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
                    DataGridOrderSummary.IsEnabled = true;
                    ButtonEditInvoice.ToolTip = "Save Invoice";
                    ButtonNewInvoice.IsEnabled = false;
                    
                }
                else
                {
                    //save is clicked
                    if (mainLogic.CurrentInvoice.Number.Equals("TBD"))
                    {
                        mainLogic.SaveToDatabase();
                    }
                    else
                    {
                        mainLogic.UpdateDatabase();
                    }
                    DataGridOrderSummary.IsEnabled = false;
                    ButtonEditInvoice.ToolTip = "Edit Invoice";
                    ButtonNewInvoice.IsEnabled = true;
                    DataGridOrderSummary.SelectedIndex = -1;
                    


                }
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Creates a new invoice when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                btn.IsEnabled = false;
                mainLogic.CurrentInvoice = new Invoice
                {
                    Number = "TBD",
                    LineItems = new ObservableCollection<LineItem>(),
                    Date = DateTime.Now,
                    Total = "Total Due: $0.00"
                };

                EditInvoice.IsChecked = true;
                DataGridOrderSummary.IsEnabled = true;
                ButtonEditInvoice.ToolTip = "Save Invoice";
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Updates the total when the cell is done being edited
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridOrderSummary_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                mainLogic.CurrentInvoice.GetTotal();
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Adds a new line item with the proper position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridOrderSummary_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {

            try
            {
                e.NewItem = new LineItem
                {
                    Position = DataGridOrderSummary.Items.Count,
                    ItemOnLine = new Item()
                };
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

        /// <summary>
        /// When user Deletes row with the DELETE key, this method recalculates line positions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridOrderSummary_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Delete)
                {
                    mainLogic.RecalculateLinePositions();
                    mainLogic.CurrentInvoice.GetTotal();
                }
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Deletes item from the list when the user uses the context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainLogic.DeleteLineItem(DataGridOrderSummary);
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Delete invoice when delete button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                mainLogic.DeleteInvoice();
                DataGridOrderSummary.IsEnabled = false;
                EditInvoice.IsChecked = false;
                ButtonNewInvoice.IsEnabled = true;
                ButtonEditInvoice.ToolTip = "Edit Invoice";

            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Scrolls the DataGrid even when mouse is not over it. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {
                if (Scroller.IsMouseOver == false)
                {
                    if (e.Delta < 0)
                    {
                        Scroller.LineDown();
                        Scroller.LineDown();
                        Scroller.LineDown();
                    }

                    if (e.Delta > 0)
                    {
                        Scroller.LineUp();
                        Scroller.LineUp();
                        Scroller.LineUp();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Close the 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SnackbarMessage.IsActive = false;
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
