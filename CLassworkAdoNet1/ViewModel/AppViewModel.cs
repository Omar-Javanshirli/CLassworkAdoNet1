using CLassworkAdoNet1.Command;
using CLassworkAdoNet1.Model;
using CLassworkAdoNet1.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public FakeRepo DataBase { get; set; }
        public ObservableCollection<BookRowsName> booksRowNames { get; set; }
        public ObservableCollection<Authors> authors { get; set; }
        public BookRowsName booksRowName { get; set; }
        public RelayCommand InsertCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand UpdateCommnad { get; set; }
        public AppViewModel()
        {
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
        public void SqlQueery(string queery)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=corporatesql.database.windows.net;Initial Catalog=Library;User ID=corporatesqladmin;Password=6244736637Jc_OJ;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlDataReader reader = null;
                conn.Open();

                var paramId = new SqlParameter();
                paramId.ParameterName = "@id";
                paramId.SqlDbType = System.Data.SqlDbType.Int;
                paramId.Value = App.IdTexBox.Text;

                var paramFirstName = new SqlParameter();
                paramFirstName.ParameterName = "@firstName";
                paramFirstName.SqlDbType = System.Data.SqlDbType.NVarChar;
                paramFirstName.Value = App.FirstnameTextBox.Text;

                var paramLastName = new SqlParameter();
                paramLastName.ParameterName = "@lastName";
                paramLastName.SqlDbType = System.Data.SqlDbType.NVarChar;
                paramLastName.Value = App.LastnameTextBox.Text;
                using (SqlCommand command = new SqlCommand(queery, conn))
                {
                    command.Parameters.Add(paramId);
                    command.Parameters.Add(paramFirstName);
                    command.Parameters.Add(paramLastName);
                    var result = command.ExecuteNonQuery();
                    Console.WriteLine($"{result} rows affected");
                }
            }
        }
        public void Insert()
        {
            InsertCommand = new RelayCommand((e) =>
            {
                CheckTextboxs();
                string queery = @"insert into Authors(Id,FirstName,LastName)
                                      Values(@id,@firstName,@lastName)";
                SqlQueery(queery);
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
                SqlQueery(queery);
                MessageBox.Show("Deleted operation Successfully");
            });
        }

        public void Update()
        {
            UpdateCommnad = new RelayCommand((e) =>
            {
                CheckTextboxs();
                string queery = @"Update Authors
                                Set Id=@id,FirstName=@firstname,LastName=@lastname
                                Where Id=@id";
                SqlQueery(queery);
                MessageBox.Show("Update operation Successfully");
            });
        }
    }
}
