using Microsoft.VisualStudio.TestTools.UnitTesting;
using HospitalAppointmentManagement.Services;

namespace HospitalAppointmentManagement.Tests
{
    [TestClass]
    public class AppointmentServiceTests
    {
        private AppointmentService service;
        private string connectionString = "Server=DESKTOP-LAPTOP\\SQLEXPRESS;Database=HospitalAppointmentDB;Trusted_Connection=True;Encrypt=False;";

        [TestInitialize]
        public void Setup()
        {
            service = new AppointmentService(connectionString);
        }

        [TestMethod]
        public void TestGetAllAppointments()
        {
            var appointments = service.GetAllAppointments();
            Assert.IsNotNull(appointments);
            Assert.IsTrue(appointments.Count >= 0);
        }

        [TestMethod]
        public void TestAddAppointment()
        {
            service.AddAppointment("John Doe", "Dr. Smith", DateTime.Now.AddDays(5));
            var appointments = service.GetAllAppointments();
            Assert.IsTrue(appointments.Count > 0);
        }

        [TestMethod]
        public void TestGetAppointmentById()
        {
            var appointments = service.GetAllAppointments();
            if (appointments.Count > 0)
            {
                var app = service.GetAppointmentById(appointments[0].Id);
                Assert.IsNotNull(app);
                Assert.AreEqual(appointments[0].Id, app.Id);
            }
        }

        [TestMethod]
        public void TestUpdateAppointment()
        {
            var appointments = service.GetAllAppointments();
            if (appointments.Count > 0)
            {
                int id = appointments[0].Id;
                service.UpdateAppointment(id, "Updated Name", "Dr. Brown", "Scheduled");
                var app = service.GetAppointmentById(id);
                Assert.AreEqual("Updated Name", app.PatientName);
            }
        }

        [TestMethod]
        public void TestDeleteAppointment()
        {
            service.AddAppointment("To Delete", "Dr. Test", DateTime.Now.AddDays(7));
            var appointments = service.GetAllAppointments();
            int lastId = appointments[appointments.Count - 1].Id;

            service.DeleteAppointment(lastId);
            var app = service.GetAppointmentById(lastId);
            Assert.IsNull(app);
        }
    }
}
