using Dapper;
using Domain.Dtos;
using Domain.Entites;
using Npgsql;

namespace Infrastructure.Services;

public class OrderService
{
   
        private string _connectionString = "Server=127.0.0.1;Port=5432;Database=dapper_demo;User Id=postgres;Password=12345;";
       
        public List<GetOrderDto> Orders()
        {
               
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                        string sql = "select o.id,p.id as ProductId, c.id as CustomerId, o.createddate as CreatedDate, o.productcount as ProductCount, o.price as Price, p.productname as ProductName, c.firstname from orders o join customers c on c.id = o.customerid join products p on o.productid = p.id";
                        var result = connection.Query<GetOrderDto>(sql);
                        return result.ToList();
                }

        }
}