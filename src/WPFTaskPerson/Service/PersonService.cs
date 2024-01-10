using System.Data.SqlClient;
using WPFTaskPerson.Modals;

namespace WPFTaskPerson.Service
{
    public class PersonService
    {
        //This is will go in config. Because of time crunch took it here...
        readonly string connectionString = @"Data Source=DESKTOP-OUBJB3E;Initial Catalog=PersonInformation;Integrated Security=True;TrustServerCertificate=true;";
        public List<Person> GetAllPerson()
        {
                        List<Person> persons = new();

            try
            {
                using SqlConnection sqlConnection = new(connectionString);
                string query = "SELECT * FROM Person";
                using SqlCommand cmd = new(query, sqlConnection);
                sqlConnection.Open();
                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Person person = new()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = Convert.ToString(reader["FirstName"]),
                        LastName = Convert.ToString(reader["LastName"]),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        Email = Convert.ToString(reader["Email"]),
                        Gender = Convert.ToString(reader["Gender"]),
                        Phone = Convert.ToString(reader["Phone"]),
                        Language = Convert.ToString(reader["Language"]),
                    };

                    persons.Add(person);
                }
            }
            catch (Exception ex)
            {
                //Log error here
                throw ex.InnerException;
            }


            return persons;
        }
        public bool AddNewPerson(Person obj)
        {
            try
            {
                using SqlConnection sqlConnection = new(connectionString);
                string query = "INSERT INTO Person (FirstName, LastName, DOB, Email, Gender, Phone, Language) " +
                               "VALUES (@FirstName, @LastName, @DOB, @Email, @Gender, @Phone, @Language)";

                using SqlCommand cmd = new(query, sqlConnection);
                cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
                cmd.Parameters.AddWithValue("@LastName", obj.LastName);

                if (obj.DOB.HasValue)
                {
                    cmd.Parameters.AddWithValue("@DOB", obj.DOB.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DOB", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("@Email", obj.Email);
                cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                cmd.Parameters.AddWithValue("@Phone", obj.Phone);
                cmd.Parameters.AddWithValue("@Language", obj.Language);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Log error here
                throw ex.InnerException;
            }

            return true;
        }
        public bool UpdatePerson(Person obj)
        {
            bool IsUpdated = false;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string updateQuery = "UPDATE Person SET FirstName = @FirstName,LastName=@LastName,DOB=@DOB,Email=@Email,Gender=@Gender,Phone=@Phone,Language=@Language WHERE Id = @Id";
            using (SqlCommand command = new(updateQuery, sqlConnection))
            {
                command.Parameters.AddWithValue("@FirstName", obj.FirstName);
                command.Parameters.AddWithValue("@LastName", obj.LastName);
                command.Parameters.AddWithValue("@DOB", Convert.ToDateTime(obj.DOB));
                command.Parameters.AddWithValue("@Email", obj.Email);
                command.Parameters.AddWithValue("@Gender", obj.Gender);
                command.Parameters.AddWithValue("@Phone", obj.Phone);
                command.Parameters.AddWithValue("@Language", obj.Language);
                command.Parameters.AddWithValue("@Id", obj.Id);
                command.ExecuteNonQuery();
                IsUpdated = true;
            }
            return IsUpdated;
        }
        public bool DeletePerson(int id)
        {
            bool IsDeleted = false;
            SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            try
            {
                string query = "delete from Person where Id=" + id;
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                IsDeleted = true;
            }
            catch (SqlException ex)
            {
                //Log error here
                throw ex.InnerException;
            }
            finally
            {
                sqlConnection.Close();
            }
            return IsDeleted;
        }
    }
}
