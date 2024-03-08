using Assingment.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
namespace Assingment.Repository
{
    public class AppointmentRepository
    {
        private readonly SqlConnection _connection;
        private SqlCommand _command;

        public AppointmentRepository()
        {
            _connection = new SqlConnection("server=DESKTOP-4M61261\\MSSQL; database=yusra; user id=sa; password=yusra@@; TrustServerCertificate=true;");
        }

        public List<Appointment> GetAll()
        {
            List<Appointment> list = new List<Appointment>();
            using (_connection)
            {
                _connection.Open();
                string query = "SELECT * FROM Appointment";
                _command = new SqlCommand(query, _connection);

                SqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Appointment
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                        AppointmentTime = TimeSpan.Parse(reader["AppointmentTime"].ToString()),
                        Description = reader["Description"].ToString()
                    });
                }
            }
            return list;
        }

        public Appointment GetById(int id)
        {
            Appointment data = null;
            using (_connection)
            {
                _connection.Open();
                string query = "SELECT * FROM Appointment WHERE Id = @Id";
                _command = new SqlCommand(query, _connection);
                _command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = _command.ExecuteReader();
                if (reader.Read())
                {
                    data = new Appointment
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                        AppointmentTime = TimeSpan.Parse(reader["AppointmentTime"].ToString()),
                        Description = reader["Description"].ToString()
                    };
                }
            }
            return data;
        }

        public bool Create(Appointment appointment)
        {
            using (_connection)
            {
                _connection.Open();
                string query = "INSERT INTO Appointment (PatientId, AppointmentDate, AppointmentTime, Description) VALUES (@PatientId, @AppointmentDate, @AppointmentTime, @Description)";
                _command = new SqlCommand(query, _connection);
                _command.Parameters.AddWithValue("@PatientId", appointment.PatientId);
                _command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                _command.Parameters.AddWithValue("@AppointmentTime", appointment.AppointmentTime);
                _command.Parameters.AddWithValue("@Description", appointment.Description);

                int count = _command.ExecuteNonQuery();
                return count > 0;
            }
        }

        public bool Update(Appointment appointment)
        {
            using (_connection)
            {
                _connection.Open();
                string query = "UPDATE Appointment SET PatientId = @PatientId, AppointmentDate = @AppointmentDate, AppointmentTime = @AppointmentTime, Description = @Description WHERE Id = @Id";
                _command = new SqlCommand(query, _connection);
                _command.Parameters.AddWithValue("@PatientId", appointment.PatientId);
                _command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                _command.Parameters.AddWithValue("@AppointmentTime", appointment.AppointmentTime);
                _command.Parameters.AddWithValue("@Description", appointment.Description);
                _command.Parameters.AddWithValue("@Id", appointment.Id);

                int count = _command.ExecuteNonQuery();
                return count > 0;
            }
        }

        public bool Delete(int id)
        {
            using (_connection)
            {
                _connection.Open();
                string query = "DELETE FROM Appointment WHERE Id = @Id";
                _command = new SqlCommand(query, _connection);
                _command.Parameters.AddWithValue("@Id", id);

                int count = _command.ExecuteNonQuery();
                return count > 0;
            }
        }
    }
}