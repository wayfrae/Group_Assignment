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


namespace Group_Assignment.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        public wndItems()
        {
            InitializeComponent();
            Onload();
        }

        void Onload()
        {
            ItemDataGrid.ItemsSource = clsItemsLogic.SelectItem();
        }

        void Clear()
        {
            ItemDataGrid.ItemsSource = null;
            TextBoxCode.IsReadOnly = false;
            TextBoxCode.Text = "";
            TextBoxCost.Text = "";
            TextBoxDesc.Text = "";
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            Onload();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            clsItemsLogic it = new clsItemsLogic();
            it.ItemCode = TextBoxCode.Text;
            it.ItemDesc = TextBoxDesc.Text;
            it.ItemPrice = Convert.ToDecimal(TextBoxCost.Text);
            if (clsItemsLogic.UpdateItem(it))
            {
                MessageBox.Show("item Updated");
                Clear();
                Onload();
            }
            else
                MessageBox.Show("Item not updated");
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ItemDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemDataGrid.SelectedValue == null) return;
            clsItemsLogic it = (clsItemsLogic)ItemDataGrid.SelectedValue;
            TextBoxCode.IsReadOnly = true;
            TextBoxCode.Text = it.ItemCode;
            TextBoxDesc.Text = it.ItemDesc;
            TextBoxCost.Text = it.ItemPrice.ToString();
        }
    }
}
