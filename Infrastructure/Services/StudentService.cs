using System.Diagnostics;
using Dapper;
using Domain.Entites;
using Npgsql;

namespace Infrastructure.Services;

public class StudentService
{
    private string _connectionString = "Server=127.0.0.1;Port=5432;Database=dapper_demo;User Id=postgres;Password=12345;";
    public StudentService()
    {

    }

    public List<Student> GetStudents()
    {
        var sw = new Stopwatch();
        sw.Start();
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM students order by id";
            var result = connection.Query<Student>(sql);

            sw.Stop();
            System.Console.WriteLine($"Elapsed Times with dapper /  {sw.ElapsedMilliseconds}");
            return result.ToList();
        }

    }

    public bool AddStudent(Student student)
    {

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var sql = $"insert into students (firstname, lastname, email,phone)" +
                   $"values ('{student.FirstName}','{student.LastName}', '{student.Email}','{student.Phone}')";
           var inserted =  connection.Execute(sql);
            if (inserted > 0) return true;
            else return false;
        }
    }

    public bool UpdateStudent(Student student)
    {

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var sql = $"update students set firstname = '{student.FirstName}', "+
                $"lastname = '{student.LastName}',email = '{student.Email}',phone = '{student.Phone}' "+
                $"where id = {student.Id}";
            var updated = connection.Execute(sql);
            if (updated > 0) return true;
            else return false;
        }
    }

    public bool DeleteStudent(int  id)
    {

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var sql = $"delete from students where id = {id}";
            var deleted = connection.Execute(sql);
            if (deleted > 0) return true;
            else return false;
        }
    }



    public List<Student> GetStudentsWithoutDapper()
    {
        string sql = "SELECT * FROM students";
        var students = new List<Student>();
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var sw = new Stopwatch();
            sw.Start();
            connection.Open();
            using (var command = new NpgsqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Student()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                            LastName = reader.GetString(reader.GetOrdinal("lastname")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            Phone = reader.GetString(reader.GetOrdinal("phone"))
                        };
                        students.Add(student);
                    }
                }
            }
            sw.Stop();
            System.Console.WriteLine($"Elapsed Times without dapper /  {sw.ElapsedMilliseconds}");
            connection.Close();
        }

        return students;
    }

}