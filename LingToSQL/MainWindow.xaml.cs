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

            //InsertUniversities();
            InsertStudents();
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

        public void InsertStudents()
        {

            University yale = dataContext.Universities.First(un => un.Name.Equals("Yale"));
            University ccc = dataContext.Universities.First(un => un.Name.Equals("Camden County College"));

            List<Student> students = new List<Student>();
            students.Add(new Student { Name = "Mike", Gender = "Male", UniversityId = ccc.Id });
            students.Add(new Student { Name = "Munir", Gender = "Male", University = yale });
            students.Add(new Student { Name = "Jenn", Gender = "Female", University = ccc });
            students.Add(new Student { Name = "Kate", Gender = "Female", UniversityId = yale.Id });


            dataContext.ExecuteCommand("delete from Student");

            dataContext.Students.InsertAllOnSubmit(students);

            dataContext.SubmitChanges();

            MainDataGrid.ItemsSource = dataContext.Students;
        }


    }
}
