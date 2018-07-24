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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Group_Assignment.Items;
using Group_Assignment.Main;
using Group_Assignment.Search;

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
        /// Constructor for main window
        /// </summary>
        public wndMain()
        {
            InitializeComponent();

            windowItems = new wndItems();
            windowSearch = new wndSearch();
        }
    }
}
