using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace BaseballQuery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string lastNameToSearch;

        public string LastNameToSearch
        {
            get
            {
                return lastNameToSearch;
            }
            set
            {
                lastNameToSearch = value;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            
        }
        // Entity Framework DbContext
        private BaseballLibrary.BaseballEntities dbcontext =
           new BaseballLibrary.BaseballEntities();

        // load data from database into DataGridView
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            dbcontext.Players.Load();
            DisplayAllPlayers();
            // Load data by setting the CollectionViewSource.Source property:
            // playerViewSource.Source = [generic data source]
        }

        public void DisplayAllPlayers()
        {
            System.Windows.Data.CollectionViewSource playerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("playerViewSource")));

            playerViewSource.Source = dbcontext.Players.Local.OrderBy(lastName => lastName.LastName);

        }

        private void resetLastNames_Click(object sender, RoutedEventArgs e)
        {
            DisplayAllPlayers();
        }

        private void lastNameQueryTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            LastNameToSearch = lastNameQueryTB.Text;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource playerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("playerViewSource")));

            playerViewSource.Source = dbcontext.Players.Local
                .Where(lastName => lastName.LastName == lastNameToSearch);
        }
    }
}
