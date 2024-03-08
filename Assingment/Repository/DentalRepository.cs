using Assingment.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assingment.Repository
{
    public class DentalRepository
    {
        private readonly SqlConnection _connection;
        private SqlCommand _command;

        public DentalRepository()
        {
            _connection = new SqlConnection("Server=DESKTOP-4M61261\\MSSQL;Database=yusra;User Id=sa;Password=yusra@@;TrustServerCertificate=true;");
        }

        public List<Patient> GetAll()
        {
            List<Patient> list = new List<Patient>();
            using (_connection)
            {
                _connection.Open();
                string query = "SELECT * FROM Patient";
                _command = new SqlCommand(query, _connection);

                SqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Patient
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        Email = reader["Email"].ToString()
                    });
                }
            }
            return list;
        }

        public Patient GetById(int id)
        {
            Patient data = null;
            using (_connection)
            {
                _connection.Open();
                string query = "SELECT * FROM Patient WHERE Id = @Id";
                _command = new SqlCommand(query, _connection);
                _command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    data = new Patient
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        Email = reader["Email"].ToString()
                    };
                }
            }
            return data;
        }

        public bool Create(Patient patient)
        {
            using (_connection)
            {
                _connection.Open();
                string query = "INSERT INTO Patient (Name, Age, Email) VALUES (@Name, @Age, @Email)";
                _command = new SqlCommand(query, _connection);
                _command.Parameters.AddWithValue("@Name", patient.Name);
                _command.Parameters.AddWithValue("@Age", patient.Age);
                _command.Parameters.AddWithValue("@Email", patient.Email);

                int count = _command.ExecuteNonQuery();
                return count > 0;
            }
        }

        public bool Update(Patient patient)
        {
            using (_connection)
            {
                _connection.Open();
                string query = "UPDATE Patient SET Name = @Name, Age = @Age, Email = @Email WHERE Id = @Id";
                _command = new SqlCommand(query, _connection);
                _command.Parameters.AddWithValue("@Name", patient.Name);
                _command.Parameters.AddWithValue("@Age", patient.Age);
                _command.Parameters.AddWithValue("@Email", patient.Email);
                _command.Parameters.AddWithValue("@Id", patient.Id);

                int count = _command.ExecuteNonQuery();
                return count > 0;
            }
        }

        public bool Delete(int id)
        {
            using (_connection)
            {
                _connection.Open();
                string query = "DELETE FROM Patient WHERE Id = @Id";
                _command = new SqlCommand(query, _connection);
                _command.Parameters.AddWithValue("@Id", id);

                int count = _command.ExecuteNonQuery();
                return count > 0;
            }
        }

        public List<Patient> GetLastTop50()
        {
            List<Patient> list = new List<Patient>();
            using (_connection)
            {
                _connection.Open();
                string query = "SELECT TOP 50 * FROM Patient ORDER BY Id DESC";
                _command = new SqlCommand(query, _connection);

                SqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Patient
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        Email = reader["Email"].ToString()
                    });
                }
            }
            return list;
        }
    }
}