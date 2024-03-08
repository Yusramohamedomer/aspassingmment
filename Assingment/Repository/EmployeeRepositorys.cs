using Assingment.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Assingment.Repository
{
    public class EmployeeRepository
    {
        private readonly SqlConnection _connection;
        private SqlCommand _command;

        public EmployeeRepository()
        {
            _connection = new SqlConnection("server=DESKTOP-4M61261\\MSSQL; database=yusra; user id=sa; password=yusra@@; TrustServerCertificate=true;");
        }

        public List<Employee> GetAll()
        {
            List<Employee> list = new List<Employee>();
            using (_connection)
            {
                _connection.Open();
                string query = "SELECT * FROM Employee";
                _command = new SqlCommand(query, _connection);

                SqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Employee
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Department = reader["Department"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"])
                    });
                }
            }
            return list;
        }

        public Employee GetById(int id)
        {
            Employee data = null;
            using (_connection)
            {
                _connection.Open();
                string query = "SELECT * FROM Employee WHERE Id = @Id";
                _command = new SqlCommand(query, _connection);
                _command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = _command.ExecuteReader();
                if (reader.Read())
                {
                    data = new Employee
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Department = reader["Department"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"])
                    };
                }
            }
            return data;
        }

        public bool Create(Employee employee)
        {
            using (_connection)
            {
                _connection.Open();
                string query = "INSERT INTO Employee (FirstName, LastName, Email, Department, Salary) VALUES (@FirstName, @LastName, @Email, @Department, @Salary); SELECT SCOPE_IDENTITY();";
                _command = new SqlCommand(query, _connection);
                _command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                _command.Parameters.AddWithValue("@LastName", employee.LastName);
                _command.Parameters.AddWithValue("@Email", employee.Email);
                _command.Parameters.AddWithValue("@Department", employee.Department);
                _command.Parameters.AddWithValue("@Salary", employee.Salary);

                int newId = Convert.ToInt32(_command.ExecuteScalar());
                employee.Id = newId;
                return true;
            }
        }

        public bool Update(Employee employee)
        {
            using (_connection)
            {
                _connection.Open();
                string query = "UPDATE Employee SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Department = @Department, Salary = @Salary WHERE Id = @Id";
                _command = new SqlCommand(query, _connection);
                _command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                _command.Parameters.AddWithValue("@LastName", employee.LastName);
                _command.Parameters.AddWithValue("@Email", employee.Email);
                _command.Parameters.AddWithValue("@Department", employee.Department);
                _command.Parameters.AddWithValue("@Salary", employee.Salary);
                _command.Parameters.AddWithValue("@Id", employee.Id);

                int count = _command.ExecuteNonQuery();
                return count > 0;
            }
        }

        public bool Delete(int id)
        {
            using (_connection)
            {
                _connection.Open();
                string query = "DELETE FROM Employee WHERE Id = @Id";
                _command = new SqlCommand(query, _connection);
                _command.Parameters.AddWithValue("@Id", id);

                int count = _command.ExecuteNonQuery();
                return count > 0;
            }
        }
    }
}