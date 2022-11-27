using CLassworkAdoNet1.Command;
using CLassworkAdoNet1.Model;
using CLassworkAdoNet1.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace CLassworkAdoNet1.ViewModel
{
    public class AppViewModel : BaseViewModel
    {
        SqlConnection conn;
        string cs = "";
        DataTable table;
        SqlDataReader reader;
        DataSet set;
        SqlDataAdapter da;

        public FakeRepo DataBase { get; set; }
        public ObservableCollection<BookRowsName> booksRowNames { get; set; }
        public ObservableCollection<Authors> authors { get; set; }
        public BookRowsName booksRowName { get; set; }
        public RelayCommand InsertCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand UpdateCommnad { get; set; }
        public AppViewModel()
        {
            conn = new SqlConnection();
            cs = ConfigurationManager.ConnectionStrings["MyconnString"].ConnectionString;

            DataBase = new FakeRepo();
            booksRowNames = new ObservableCollection<BookRowsName>(DataBase.GetAllRowName());
            authors = new ObservableCollection<Authors>(DataBase.GetAllAuthors());
            foreach (var item in booksRowNames)
            {
                booksRowName = item;
            }
            Insert();
            Deleted();
            Update();
        }

        public void CheckTextboxs()
        {
            if (App.IdTexBox.Text == string.Empty)
            {
                App.IdTexBox.Text = "Empty";
                MessageBox.Show("Id cant be empty");
                return;
            }
            else if (App.FirstnameTextBox.Text == string.Empty)
            {
                App.FirstnameTextBox.Text = "Empty";
                MessageBox.Show("Firstname cant be Empty");
                return;
            }
            else if (App.LastnameTextBox.Text == string.Empty)
            {
                App.LastnameTextBox.Text = "Empty";
                MessageBox.Show("Lastname cant be Empty");
                return;
            }
        }
        public void Insert()
        {
            InsertCommand = new RelayCommand((e) =>
            {
                CheckTextboxs();

                using (var conn = new SqlConnection())
                {
                    set = new DataSet();
                    da = new SqlDataAdapter();
                    conn.ConnectionString = cs;
                    conn.Open();

                    SqlCommand command2 = new SqlCommand("Select *from Authors", conn);
                    da.SelectCommand = command2;
                    da.Fill(set, "authors");

                    string queery = @"insert into Authors(Id,FirstName,LastName)
                                      Values(@id,@firstname,@lastname)";

                    SqlCommand command = new SqlCommand(queery, conn);
                    command.Parameters.Add(new SqlParameter
                    {
                        SqlDbType = SqlDbType.Int,
                        ParameterName = "@id",
                        Value = Convert.ToInt32(App.IdTexBox.Text)
                    });
                    command.Parameters.Add(new SqlParameter
                    {
                        SqlDbType = SqlDbType.NVarChar,
                        ParameterName = "@firstname",
                        Value = App.FirstnameTextBox.Text.ToString()
                    });
                    command.Parameters.Add(new SqlParameter
                    {
                        SqlDbType = SqlDbType.NVarChar,
                        ParameterName = "@lastname",
                        Value = App.LastnameTextBox.Text.ToString()
                    });

                    da.InsertCommand = command;
                    da.InsertCommand.ExecuteNonQuery();
                    set.Clear();
                    da.Fill(set, "authors");
                    App.MyDataGrid.ItemsSource = set.Tables[0].DefaultView;
                }
                MessageBox.Show("Inserted operation Successfully");
            });
        }
        public void Deleted()
        {
            DeleteCommand = new RelayCommand((e) =>
            {
                CheckTextboxs();
                string queery = @"Delete From Authors
                                Where Id=@id";
             
                MessageBox.Show("Deleted operation Successfully");
            });
        }

        public void Update()
        {
            UpdateCommnad = new RelayCommand((e) =>
            {
                CheckTextboxs();

                using (var conn = new SqlConnection())
                {
                    set = new DataSet();
                    da = new SqlDataAdapter();
                    conn.ConnectionString = cs;
                    conn.Open();

                    SqlCommand command2 = new SqlCommand("Select *from Authors", conn);
                    da.SelectCommand = command2;
                    da.Fill(set, "authors");

                    string queery = @"Update Authors
                                Set FirstName=@firstname,LastName=@lastname
                                Where Id=@id";

                    SqlCommand command = new SqlCommand(queery, conn);
                    command.Parameters.Add(new SqlParameter
                    {
                        SqlDbType = SqlDbType.Int,
                        ParameterName = "@id",
                        Value = Convert.ToInt32(App.IdTexBox.Text)
                    });
                    command.Parameters.Add(new SqlParameter
                    {
                        SqlDbType = SqlDbType.NVarChar,
                        ParameterName = "@firstname",
                        Value = App.FirstnameTextBox.Text.ToString()
                    });
                    command.Parameters.Add(new SqlParameter
                    {
                        SqlDbType = SqlDbType.NVarChar,
                        ParameterName = "@lastname",
                        Value = App.LastnameTextBox.Text.ToString()
                    });

                    da.UpdateCommand = command;
                    da.UpdateCommand.ExecuteNonQuery();
                    da.Update(set, "authors");
                    set.Clear();
                    da.Fill(set, "authors");
                    App.MyDataGrid.ItemsSource = set.Tables[0].DefaultView;
                }

                MessageBox.Show("Update operation Successfully");
            });
        }
    }
}
