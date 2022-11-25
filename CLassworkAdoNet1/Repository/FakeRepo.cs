using CLassworkAdoNet1.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace CLassworkAdoNet1.Repository
{
    public class FakeRepo
    {
        public List<BookRowsName> GetAllRowName()
        {
            string id = string.Empty;
            string firstName = string.Empty;
            string lastName = string.Empty;
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=corporatesql.database.windows.net;Initial Catalog=Library;User ID=corporatesqladmin;Password=6244736637Jc_OJ;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlDataReader reader = null;
                conn.Open();

                string queery = "select * from Authors";
                using (SqlCommand cmd = new SqlCommand(queery, conn))
                {
                    reader = cmd.ExecuteReader();

                    id = reader.GetName(0).ToString();
                    firstName = reader.GetName(1).ToString();
                    lastName = reader.GetName(2).ToString();
                }
            }
            return new List<BookRowsName>
            {
                new BookRowsName
                {
                    Id = id,
                    Firstname = firstName,
                    Lastname = lastName
                }
            };
        }

        public List<Authors> GetAllAuthors()
        {
            var authors = new List<Authors>();
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=corporatesql.database.windows.net;Initial Catalog=Library;User ID=corporatesqladmin;Password=6244736637Jc_OJ;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlDataReader reader = null;
                conn.Open();

                string queery = "select * from Authors";
                using (SqlCommand command = new SqlCommand(queery, conn))
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Authors a = new Authors()
                        {
                            Id = Convert.ToInt32(reader[0]),
                            Firstname = reader[1].ToString(),
                            Lastname = reader[2].ToString()
                        };
                        authors.Add(a);
                    }
                }
            }
            return authors;
        }
    }
}
