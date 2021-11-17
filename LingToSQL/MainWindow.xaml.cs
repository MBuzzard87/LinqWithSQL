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
using System.Configuration;

namespace LingToSQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        LinqToSQLDataClassesDataContext dataContext;

        public MainWindow()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["LingToSQL.Properties.Settings.BuzzyDBConnectionString"].ConnectionString;
            dataContext = new LinqToSQLDataClassesDataContext(connectionString);

            InsertUniversities();

        }

        public void InsertUniversities()
        {
            dataContext.ExecuteCommand("delete from University");

            University yale = new University() { Name = "Yale" };
            dataContext.Universities.InsertOnSubmit(yale);

            University ccc = new University() { Name = "Camden County College" };
            dataContext.Universities.InsertOnSubmit(ccc);

            dataContext.SubmitChanges();

            MainDataGrid.ItemsSource = dataContext.Universities;
        }


        

    }
}
