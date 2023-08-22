using ModernWPF_MVVM.Models;
using ModernWPF_MVVM.Repositories.Base;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace ModernWPF_MVVM.Repositories
{
    internal class UserRepository : RepositoryBase
    {

        public ObservableCollection<Person> GetAllUsers()
        {
            var users = new ObservableCollection<Person>();

            //using (var connection = GetConnection())
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
                            Number = reader.GetString(3)
                        });
                    }
                }

            }
            return users;
        }
    }
}
