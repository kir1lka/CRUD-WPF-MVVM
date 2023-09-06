using ModernWPF_MVVM.Models;
using ModernWPF_MVVM.Repositories.Base;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace ModernWPF_MVVM.Repositories
{
    internal class UserRepository : RepositoryBase
    {
        public void UpdateUser(Person person)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [Persons] SET Name = @Name, Adress = @Adress, Number = @Number, Description = @Description WHERE Id = @Id";

                command.Parameters.AddWithValue("@Id", person.Id);
                command.Parameters.AddWithValue("@Name", person.Name);
                command.Parameters.AddWithValue("@Adress", person.Adress);
                command.Parameters.AddWithValue("@Number", person.Number);
                command.Parameters.AddWithValue("@Description", person.Description);

                command.ExecuteNonQuery();

            }
        }
        public void DeleteUser(int userId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [Persons] WHERE Id = @Id";

                command.Parameters.AddWithValue("@Id", userId);

                command.ExecuteNonQuery();
            }
        }
        public void AddUser(Person person)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO [Persons] (Name, Adress, Number, Description) VALUES (@Name, @Adress, @Number, @Description)";

                command.Parameters.AddWithValue("@Name", person.Name);
                command.Parameters.AddWithValue("@Adress", person.Adress);
                command.Parameters.AddWithValue("@Number", person.Number);
                command.Parameters.AddWithValue("@Description", person.Description);
                //SqlParameter descriptionParam = command.Parameters.AddWithValue("@Description", person.Description);
                //if (person.Description == null)
                //{
                //    descriptionParam.Value = DBNull.Value;
                //}

                command.ExecuteNonQuery();
            }
        }

        public ObservableCollection<Person> GetAllUsers()
        {
            var users = new ObservableCollection<Person>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [Persons]";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new Person
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Adress = reader.GetString(2),
                            Number = reader.GetString(3),
                            Description = reader.GetString(4)
                        });
                    }
                }

            }
            return users;
        }
    }
}
