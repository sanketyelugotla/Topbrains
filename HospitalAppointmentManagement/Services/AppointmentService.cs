using Microsoft.Data.SqlClient;
using HospitalAppointmentManagement.Models;

namespace HospitalAppointmentManagement.Services
{
    public class AppointmentService
    {
        private string connectionString;

        public AppointmentService(string conn)
        {
            connectionString = conn;
        }

        public void AddAppointment(string patientName, string doctorName, DateTime date)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Appointments (PatientName, DoctorName, AppointmentDate, Status) VALUES (@p, @d, @ad, @s)";
            cmd.Parameters.AddWithValue("@p", patientName);
            cmd.Parameters.AddWithValue("@d", doctorName);
            cmd.Parameters.AddWithValue("@ad", date);
            cmd.Parameters.AddWithValue("@s", "Scheduled");
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public List<Appointment> GetAllAppointments()
        {
            List<Appointment> list = new List<Appointment>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Appointments", con);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Appointment app = new Appointment();
                app.Id = (int)reader["Id"];
                app.PatientName = (string)reader["PatientName"];
                app.DoctorName = (string)reader["DoctorName"];
                app.AppointmentDate = (DateTime)reader["AppointmentDate"];
                app.Status = (string)reader["Status"];
                list.Add(app);
            }

            con.Close();
            return list;
        }

        public Appointment GetAppointmentById(int id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Appointments WHERE Id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Appointment app = new Appointment();
                app.Id = (int)reader["Id"];
                app.PatientName = (string)reader["PatientName"];
                app.DoctorName = (string)reader["DoctorName"];
                app.AppointmentDate = (DateTime)reader["AppointmentDate"];
                app.Status = (string)reader["Status"];
                con.Close();
                return app;
            }

            con.Close();
            return null;
        }

        public void UpdateAppointment(int id, string patientName, string doctorName, string status)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Appointments SET PatientName = @p, DoctorName = @d, Status = @s WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@p", patientName);
            cmd.Parameters.AddWithValue("@d", doctorName);
            cmd.Parameters.AddWithValue("@s", status);
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteAppointment(int id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Appointments WHERE Id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
