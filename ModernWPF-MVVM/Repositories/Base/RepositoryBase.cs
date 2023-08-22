using System;
using System.Data.SqlClient;

namespace ModernWPF_MVVM.Repositories.Base
{
    internal abstract class RepositoryBase
    {
        private readonly string _ConnectionString;

        public RepositoryBase() => _ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\кирилл\labaRez\12\ModernWPF-MVVM\ModernWPF-MVVM\Data\Persons1.mdf;Integrated Security=True";

        protected SqlConnection GetConnection()
        {
            try
            {
                return new SqlConnection(_ConnectionString);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message, "Ошибка при подключении к базе данных!");
            }
        }
    }
}
