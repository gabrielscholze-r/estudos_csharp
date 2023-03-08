
using System.Data;
using ESTUDO.DTO;
using ESTUDO.Model;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ESTUDO
{


    public class FoodRepository
    {
        private readonly IConfiguration configuration;
        public FoodRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public Food Create(FoodDTO food)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("connectionString")))
            {
                connection.Open();
                string sql = "INSERT INTO Food (Name, Description, Price) OUTPUT INSERTED.Name, INSERTED.Description, INSERTED.Price values (@Name, @Description, @Price)";
                var result = connection.Query<Food>(sql, new { food }).First();
                return result;
            }
        }
        public IEnumerable<Food> getAll()
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("connectionString")))
            {
                connection.Open();
                string sql = "SELECT * FROM Food";
                var result = connection.Query<Food>(sql);
                return result;
            }
        }

        public Food getById(int id)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("connectionString")))
            {
                connection.Open();
                string sql = "SELECT * FROM Food where id=@id";
                var result = connection.QueryFirstOrDefault<Food>(sql, new { id });
                return result;
            }
        }

        public bool delete(int id)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("connectionString")))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM Food where id=@id";
                    connection.QueryFirstOrDefault<Food>(sql, new { id });
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }

        public bool update(FoodDTO foodDTO, int id)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("connectionString")))
            {
                try
                {
                    connection.Open();
                    string sql = "UPDATE Food SET Name=@Name, Description=@Description, Price=@Price where id=@id";
                    connection.Execute(sql, new { foodDTO, id });
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}