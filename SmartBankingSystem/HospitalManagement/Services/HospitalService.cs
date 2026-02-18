using HospitalManagement.Models;
using HospitalManagement.Exceptions;

namespace HospitalManagement.Services
{
    public class HospitalService
    {
        private List<Doctor> doctors = new List<Doctor>();
        private List<Patient> patients = new List<Patient>();
        private List<Appointment> appointments = new List<Appointment>();
        private Dictionary<int, MedicalRecord> medicalRecords = new Dictionary<int, MedicalRecord>();
        private int appointmentIdCounter = 1;
        private int recordIdCounter = 1;

        // Doctor Management
        public void AddDoctor(Doctor doctor)
        {
            doctor.Id = doctors.Count + 1;
            doctors.Add(doctor);
            Console.WriteLine($"Doctor {doctor.Name} added successfully with ID: {doctor.Id}");
        }

        public List<Doctor> GetAllDoctors()
        {
            return doctors;
        }

        public Doctor? GetDoctorById(int id)
        {
            return doctors.FirstOrDefault(d => d.Id == id);
        }

        // Patient Management
        public void AddPatient(Patient patient)
        {
            patient.Id = patients.Count + 1;
            patient.AdmissionDate = DateTime.Now;
            patients.Add(patient);
            Console.WriteLine($"Patient {patient.Name} added successfully with ID: {patient.Id}");
        }

        public List<Patient> GetAllPatients()
        {
            return patients;
        }

        public Patient? GetPatientById(int id)
        {
            var patient = patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                throw new PatientNotFoundException($"Patient with ID {id} not found.");
            }
            return patient;
        }

        // Appointment Management with overlap checking
        public void ScheduleAppointment(int doctorId, int patientId, DateTime appointmentDateTime, string notes = "")
        {
            var doctor = GetDoctorById(doctorId);
            if (doctor == null)
            {
                throw new InvalidAppointmentException($"Doctor with ID {doctorId} not found.");
            }

            var patient = GetPatientById(patientId);
            if (patient == null)
            {
                throw new PatientNotFoundException($"Patient with ID {patientId} not found.");
            }

            if (!doctor.IsAvailable)
            {
                throw new DoctorNotAvailableException($"Dr. {doctor.Name} is not available.");
            }

            // Check for overlapping appointments (within 1 hour window)
            var overlappingAppointment = appointments.FirstOrDefault(a =>
                a.Doctor?.Id == doctorId &&
                a.Status == "Scheduled" &&
                Math.Abs((a.AppointmentDateTime - appointmentDateTime).TotalMinutes) < 60);

            if (overlappingAppointment != null)
            {
                throw new DoctorNotAvailableException(
                    $"Dr. {doctor.Name} already has an appointment at {overlappingAppointment.AppointmentDateTime}. " +
                    $"Please choose a different time slot.");
            }

            var appointment = new Appointment
            {
                AppointmentId = appointmentIdCounter++,
                Doctor = doctor,
                Patient = patient,
                AppointmentDateTime = appointmentDateTime,
                Status = "Scheduled",
                Notes = notes
            };

            appointments.Add(appointment);
            doctor.AddEarnings(doctor.ConsultationFee);
            Console.WriteLine($"Appointment scheduled successfully! Appointment ID: {appointment.AppointmentId}");
        }

        public void CompleteAppointment(int appointmentId, double additionalCharges = 0)
        {
            var appointment = appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);
            if (appointment == null)
            {
                throw new InvalidAppointmentException($"Appointment with ID {appointmentId} not found.");
            }

            appointment.Status = "Completed";
            appointment.AdditionalCharges = additionalCharges;
            Console.WriteLine($"Appointment {appointmentId} marked as completed.");
            appointment.DisplayBill();
        }

        public List<Appointment> GetAllAppointments()
        {
            return appointments;
        }

        // Medical Record Management
        public void AddMedicalRecord(int patientId, string diagnosis, string treatment, string prescriptions)
        {
            if (medicalRecords.ContainsKey(patientId))
            {
                throw new DuplicateMedicalRecordException($"Medical record for patient ID {patientId} already exists.");
            }

            var patient = GetPatientById(patientId);
            
            var record = new MedicalRecord
            {
                RecordId = recordIdCounter++,
                PatientId = patientId,
                Diagnosis = diagnosis,
                Treatment = treatment,
                Prescriptions = prescriptions,
                RecordDate = DateTime.Now
            };

            medicalRecords[patientId] = record;
            Console.WriteLine($"Medical record created successfully with ID: {record.RecordId}");
        }

        public MedicalRecord? GetMedicalRecord(int patientId)
        {
            return medicalRecords.TryGetValue(patientId, out var record) ? record : null;
        }

        // LINQ Query 1: Get doctors with more than 10 appointments
        public List<Doctor> GetDoctorsWithMoreThan10Appointments()
        {
            var result = doctors.Where(d => d.AppointmentCount > 10).ToList();
            
            Console.WriteLine("\n--- Doctors with more than 10 appointments ---");
            if (result.Any())
            {
                foreach (var doctor in result)
                {
                    Console.WriteLine($"Dr. {doctor.Name} - {doctor.AppointmentCount} appointments");
                }
            }
            else
            {
                Console.WriteLine("No doctors found with more than 10 appointments.");
            }
            
            return result;
        }

        // LINQ Query 2: Get patients treated in last 30 days
        public List<Patient> GetPatientsTreatedInLast30Days()
        {
            var thirtyDaysAgo = DateTime.Now.AddDays(-30);
            
            var recentPatientIds = appointments
                .Where(a => a.AppointmentDateTime >= thirtyDaysAgo && a.Status == "Completed")
                .Select(a => a.Patient?.Id)
                .Distinct()
                .ToList();

            var result = patients.Where(p => recentPatientIds.Contains(p.Id)).ToList();

            Console.WriteLine("\n--- Patients treated in last 30 days ---");
            if (result.Any())
            {
                foreach (var patient in result)
                {
                    Console.WriteLine($"{patient.Name} (ID: {patient.Id}) - Disease: {patient.Disease}");
                }
            }
            else
            {
                Console.WriteLine("No patients treated in the last 30 days.");
            }

            return result;
        }

        // LINQ Query 3: Group appointments by doctor
        public void GroupAppointmentsByDoctor()
        {
            var groupedAppointments = appointments
                .Where(a => a.Doctor != null)
                .GroupBy(a => a.Doctor!.Name)
                .Select(g => new
                {
                    DoctorName = g.Key,
                    AppointmentCount = g.Count(),
                    Appointments = g.ToList()
                })
                .ToList();

            Console.WriteLine("\n--- Appointments Grouped by Doctor ---");
            foreach (var group in groupedAppointments)
            {
                Console.WriteLine($"\nDr. {group.DoctorName} - Total: {group.AppointmentCount} appointments");
                foreach (var appointment in group.Appointments)
                {
                    Console.WriteLine($"  - Appointment ID: {appointment.AppointmentId}, " +
                                    $"Patient: {appointment.Patient?.Name}, " +
                                    $"Date: {appointment.AppointmentDateTime}, " +
                                    $"Status: {appointment.Status}");
                }
            }
        }

        // LINQ Query 4: Find top 3 highest earning doctors
        public List<Doctor> GetTop3HighestEarningDoctors()
        {
            var result = doctors
                .OrderByDescending(d => d.TotalEarnings)
                .Take(3)
                .ToList();

            Console.WriteLine("\n--- Top 3 Highest Earning Doctors ---");
            int rank = 1;
            foreach (var doctor in result)
            {
                Console.WriteLine($"{rank}. Dr. {doctor.Name} - Rs.{doctor.TotalEarnings} " +
                                $"({doctor.AppointmentCount} appointments)");
                rank++;
            }

            return result;
        }

        // LINQ Query 5: Get patients by disease
        public List<Patient> GetPatientsByDisease(string disease)
        {
            var result = patients
                .Where(p => p.Disease.Equals(disease, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Console.WriteLine($"\n--- Patients with {disease} ---");
            if (result.Any())
            {
                foreach (var patient in result)
                {
                    Console.WriteLine($"{patient.Name} (ID: {patient.Id}) - Age: {patient.Age}");
                }
            }
            else
            {
                Console.WriteLine($"No patients found with disease: {disease}");
            }

            return result;
        }

        // LINQ Query 6: Calculate total revenue generated
        public double CalculateTotalRevenue()
        {
            var totalRevenue = appointments
                .Where(a => a.Status == "Completed")
                .Sum(a => a.CalculateBill());

            Console.WriteLine($"\n--- Total Revenue Generated ---");
            Console.WriteLine($"Total Hospital Revenue: Rs.{totalRevenue}");

            return totalRevenue;
        }

        // Export appointment report to console
        public void ExportAppointmentReport()
        {
            var report = appointments
                .Select(a => new
                {
                    AppointmentId = a.AppointmentId,
                    DoctorName = a.Doctor?.Name ?? "N/A",
                    PatientName = a.Patient?.Name ?? "N/A",
                    DateTime = a.AppointmentDateTime.ToString("yyyy-MM-dd HH:mm"),
                    Status = a.Status,
                    Bill = a.CalculateBill()
                })
                .ToList();

            Console.WriteLine("\n╔════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                         APPOINTMENT REPORT                                     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine($"{"ID",-5} {"Doctor",-20} {"Patient",-20} {"Date & Time",-18} {"Status",-12} {"Bill",-10}");
            Console.WriteLine(new string('-', 90));

            foreach (var item in report)
            {
                Console.WriteLine($"{item.AppointmentId,-5} {item.DoctorName,-20} {item.PatientName,-20} " +
                                $"{item.DateTime,-18} {item.Status,-12} Rs.{item.Bill,-10}");
            }
            Console.WriteLine(new string('-', 90));
            Console.WriteLine($"Total Appointments: {report.Count}");
            Console.WriteLine($"Total Revenue: Rs.{report.Where(r => r.Status == "Completed").Sum(r => r.Bill)}");
        }

        // Seed sample data for testing
        public void SeedSampleData()
        {
            // Add Doctors
            AddDoctor(new Doctor { Name = "Rajesh Kumar", Age = 45, Specialization = "Cardiologist", ConsultationFee = 1500, ContactNumber = "9876543210" });
            AddDoctor(new Doctor { Name = "Priya Sharma", Age = 38, Specialization = "Pediatrician", ConsultationFee = 1000, ContactNumber = "9876543211" });
            AddDoctor(new Doctor { Name = "Amit Patel", Age = 50, Specialization = "Orthopedic", ConsultationFee = 1800, ContactNumber = "9876543212" });
            AddDoctor(new Doctor { Name = "Sneha Reddy", Age = 42, Specialization = "Dermatologist", ConsultationFee = 1200, ContactNumber = "9876543213" });

            // Add Patients
            AddPatient(new Patient { Name = "Arjun Singh", Age = 35, Disease = "Heart Disease", ContactNumber = "9123456789", Address = "Mumbai" });
            AddPatient(new Patient { Name = "Meera Desai", Age = 28, Disease = "Diabetes", ContactNumber = "9123456790", Address = "Delhi" });
            AddPatient(new Patient { Name = "Rohan Verma", Age = 42, Disease = "Arthritis", ContactNumber = "9123456791", Address = "Bangalore" });
            AddPatient(new Patient { Name = "Ananya Joshi", Age = 8, Disease = "Fever", ContactNumber = "9123456792", Address = "Pune" });
            AddPatient(new Patient { Name = "Vikram Rao", Age = 55, Disease = "Heart Disease", ContactNumber = "9123456793", Address = "Chennai" });

            // Schedule appointments
            try
            {
                ScheduleAppointment(1, 1, DateTime.Now.AddDays(-25), "Regular checkup");
                ScheduleAppointment(2, 4, DateTime.Now.AddDays(-20), "Child vaccination");
                ScheduleAppointment(3, 3, DateTime.Now.AddDays(-15), "Knee pain treatment");
                ScheduleAppointment(1, 5, DateTime.Now.AddDays(-10), "Heart consultation");
                ScheduleAppointment(4, 2, DateTime.Now.AddDays(-5), "Skin allergy");
                ScheduleAppointment(2, 4, DateTime.Now.AddDays(2), "Follow-up visit");
                ScheduleAppointment(3, 3, DateTime.Now.AddDays(5), "Physiotherapy session");

                // Complete some appointments
                CompleteAppointment(1, 200);
                CompleteAppointment(2, 150);
                CompleteAppointment(3, 300);
                CompleteAppointment(4, 100);
                CompleteAppointment(5, 0);

                // Add medical records
                AddMedicalRecord(1, "Coronary Artery Disease", "Medication and lifestyle changes", "Aspirin, Beta-blockers");
                AddMedicalRecord(4, "Viral Fever", "Rest and hydration", "Paracetamol");
                AddMedicalRecord(3, "Osteoarthritis", "Pain management and physiotherapy", "Ibuprofen, Calcium supplements");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during seeding: {ex.Message}");
            }

            Console.WriteLine("\n✅ Sample data loaded successfully!");
        }
    }
}
