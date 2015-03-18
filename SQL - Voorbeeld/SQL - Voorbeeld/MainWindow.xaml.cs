using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace SQL___Voorbeeld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string connectionString = @"Data Source=(localdb\v11.0;" + @"Initial Catalog=MusicSales;Integrated Security=True";

        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable table = new DataTable();

                //set up SQL query
                string command = "SELECT * FROM Artists WHERE " + "Artist = '" + artistTextBox.Text + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(command, connectionString);
                sqlLabel.Content = command;

                //execute query
                table.Clear();
                int recordCount = adapter.Fill(table);

                //display results
                if (recordCount != 0)
                {
                    companyTextBox.Text = Convert.ToString(table.Rows[0][1]);
                    salesTextBox.Text = Convert.ToString(table.Rows[0][2]);
                }
                else
                    MessageBox.Show("Artist not found!");
            }
            catch (Exception obj)
            {
                MessageBox.Show(obj.Message);
            }
        }

        private void instertButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand insertCommand = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();

            try
            {
                connection.Open();
                string command = "INSERT INTO Artists" + "(Artist, Company, Sales) " + "VALUES('" + artistTextBox.Text + "', '" + companyTextBox.Text + "', " + salesTextBox.Text + ")";
                sqlLabel.Content = command;

                //put command in adapter
                insertCommand.Connection = connection;
                insertCommand.CommandText = command;
                adapter.InsertCommand = insertCommand;

                //do the insert
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception obj)
            {
                MessageBox.Show(obj.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            DataTable table = new DataTable();
            SqlCommand updateCommand = new SqlCommand();

            try
            {
                connection.Open();
                SqlDataAdapter adapter : new SqlDataAdapter();

                //set up SQL update
                string command = "UPDATE Artists SET Company = '"+companyTextBox.Text+"', Sales = '"+salesTextBox.Text+"' WHERE Artist = '"+artistTextBox.Text+"'";
                sqlLabel.Content=command;

                //put command in adapter
                updateCommand.CommandText=command;
                updateCommand.Connection=connection;
                adapter.UpdateCommand=updateCommand;

                //execute update
                adapter.UpdateCommand.ExecuteNonQuery();
            }
            catch (Exception obj)
            {
                MessageBox.Show(obj.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand deleteCommand = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();

            try
            {
                connection.Open();
                string command = "DELETE FROM Artists WHERE Artist = '" + artistTextBox.Text + "'";
                sqlLabel.Content = command;

                //put command in adapter
                deleteCommand.Connection = connection;
                deleteCommand.CommandText = command;
                adapter.DeleteCommand = deleteCommand;

                //execute insert
                int rowsAffecting = adapter.DeleteCommand.ExecuteNonQuery();
                if (rowsAffecting == 0)
                {
                    MessageBox.Show("Deletion not executed");
                }
                else
                {
                    MessageBox.Show("Arist deleted");
                }
            }
            catch (Exception obj)
            {
                MessageBox.Show(obj.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
