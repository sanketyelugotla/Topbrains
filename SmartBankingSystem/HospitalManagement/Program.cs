using HospitalManagement.Models;
using HospitalManagement.Services;
using HospitalManagement.Exceptions;

namespace HospitalManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║        HOSPITAL MANAGEMENT SYSTEM                          ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            HospitalService hospitalService = new HospitalService();

            // Load sample data
            Console.WriteLine("Loading sample data...");
            hospitalService.SeedSampleData();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n═══════════════════════════════════════════════════════════");
                Console.WriteLine("                    MAIN MENU");
                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.WriteLine("1.  Add Doctor");
                Console.WriteLine("2.  Add Patient");
                Console.WriteLine("3.  Schedule Appointment");
                Console.WriteLine("4.  Complete Appointment");
                Console.WriteLine("5.  View All Doctors");
                Console.WriteLine("6.  View All Patients");
                Console.WriteLine("7.  View All Appointments");
                Console.WriteLine("8.  Add Medical Record");
                Console.WriteLine("9.  View Medical Record");
                Console.WriteLine("10. Doctors with 10+ Appointments (LINQ)");
                Console.WriteLine("11. Patients Treated in Last 30 Days (LINQ)");
                Console.WriteLine("12. Group Appointments by Doctor (LINQ)");
                Console.WriteLine("13. Top 3 Highest Earning Doctors (LINQ)");
                Console.WriteLine("14. Search Patients by Disease (LINQ)");
                Console.WriteLine("15. Calculate Total Revenue (LINQ)");
                Console.WriteLine("16. Export Appointment Report");
                Console.WriteLine("17. Exit");
                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.Write("Enter your choice: ");

                var input = Console.ReadLine();

                try
                {
                    switch (input)
                    {
                        case "1":
                            AddDoctor(hospitalService);
                            break;

                        case "2":
                            AddPatient(hospitalService);
                            break;

                        case "3":
                            ScheduleAppointment(hospitalService);
                            break;

                        case "4":
                            CompleteAppointment(hospitalService);
                            break;

                        case "5":
                            ViewAllDoctors(hospitalService);
                            break;

                        case "6":
                            ViewAllPatients(hospitalService);
                            break;

                        case "7":
                            ViewAllAppointments(hospitalService);
                            break;

                        case "8":
                            AddMedicalRecord(hospitalService);
                            break;

                        case "9":
                            ViewMedicalRecord(hospitalService);
                            break;

                        case "10":
                            hospitalService.GetDoctorsWithMoreThan10Appointments();
                            break;

                        case "11":
                            hospitalService.GetPatientsTreatedInLast30Days();
                            break;

                        case "12":
                            hospitalService.GroupAppointmentsByDoctor();
                            break;

                        case "13":
                            hospitalService.GetTop3HighestEarningDoctors();
                            break;

                        case "14":
                            SearchPatientsByDisease(hospitalService);
                            break;

                        case "15":
                            hospitalService.CalculateTotalRevenue();
                            break;

                        case "16":
                            hospitalService.ExportAppointmentReport();
                            break;

                        case "17":
                            exit = true;
                            Console.WriteLine("\nThank you for using Hospital Management System!");
                            break;

                        default:
                            Console.WriteLine("\n❌ Invalid option. Please try again.");
                            break;
                    }
                }
                catch (DoctorNotAvailableException ex)
                {
                    Console.WriteLine($"\n❌ Error: {ex.Message}");
                }
                catch (InvalidAppointmentException ex)
                {
                    Console.WriteLine($"\n❌ Error: {ex.Message}");
                }
                catch (PatientNotFoundException ex)
                {
                    Console.WriteLine($"\n❌ Error: {ex.Message}");
                }
                catch (DuplicateMedicalRecordException ex)
                {
                    Console.WriteLine($"\n❌ Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Unexpected error: {ex.Message}");
                }
            }
        }

        static void AddDoctor(HospitalService service)
        {
            Console.WriteLine("\n--- Add New Doctor ---");
            Console.Write("Enter name: ");
            var name = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter age: ");
            int.TryParse(Console.ReadLine(), out int age);

            Console.Write("Enter specialization: ");
            var specialization = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter consultation fee: ");
            double.TryParse(Console.ReadLine(), out double fee);

            Console.Write("Enter contact number: ");
            var contact = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter address: ");
            var address = Console.ReadLine() ?? string.Empty;

            var doctor = new Doctor
            {
                Name = name,
                Age = age,
                Specialization = specialization,
                ConsultationFee = fee,
                ContactNumber = contact,
                Address = address
            };

            service.AddDoctor(doctor);
        }

        static void AddPatient(HospitalService service)
        {
            Console.WriteLine("\n--- Add New Patient ---");
            Console.Write("Enter name: ");
            var name = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter age: ");
            int.TryParse(Console.ReadLine(), out int age);

            Console.Write("Enter disease: ");
            var disease = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter contact number: ");
            var contact = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter address: ");
            var address = Console.ReadLine() ?? string.Empty;

            var patient = new Patient
            {
                Name = name,
                Age = age,
                Disease = disease,
                ContactNumber = contact,
                Address = address,
                IsAdmitted = true
            };

            service.AddPatient(patient);
        }

        static void ScheduleAppointment(HospitalService service)
        {
            Console.WriteLine("\n--- Schedule Appointment ---");

            Console.Write("Enter Doctor ID: ");
            int.TryParse(Console.ReadLine(), out int doctorId);

            Console.Write("Enter Patient ID: ");
            int.TryParse(Console.ReadLine(), out int patientId);

            Console.Write("Enter appointment date (yyyy-MM-dd): ");
            var dateStr = Console.ReadLine();

            Console.Write("Enter appointment time (HH:mm): ");
            var timeStr = Console.ReadLine();

            Console.Write("Enter notes (optional): ");
            var notes = Console.ReadLine() ?? string.Empty;

            if (DateTime.TryParse($"{dateStr} {timeStr}", out DateTime appointmentDateTime))
            {
                service.ScheduleAppointment(doctorId, patientId, appointmentDateTime, notes);
            }
            else
            {
                Console.WriteLine("❌ Invalid date/time format.");
            }
        }

        static void CompleteAppointment(HospitalService service)
        {
            Console.WriteLine("\n--- Complete Appointment ---");
            Console.Write("Enter Appointment ID: ");
            int.TryParse(Console.ReadLine(), out int appointmentId);

            Console.Write("Enter additional charges (0 if none): ");
            double.TryParse(Console.ReadLine(), out double charges);

            service.CompleteAppointment(appointmentId, charges);
        }

        static void ViewAllDoctors(HospitalService service)
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    ALL DOCTORS                            ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");

            var doctors = service.GetAllDoctors();
            if (doctors.Any())
            {
                foreach (var doctor in doctors)
                {
                    Console.WriteLine();
                    doctor.DisplayInfo();
                    Console.WriteLine(new string('-', 60));
                }
            }
            else
            {
                Console.WriteLine("No doctors found.");
            }
        }

        static void ViewAllPatients(HospitalService service)
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    ALL PATIENTS                           ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");

            var patients = service.GetAllPatients();
            if (patients.Any())
            {
                foreach (var patient in patients)
                {
                    Console.WriteLine();
                    patient.DisplayInfo();
                    Console.WriteLine(new string('-', 60));
                }
            }
            else
            {
                Console.WriteLine("No patients found.");
            }
        }

        static void ViewAllAppointments(HospitalService service)
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                  ALL APPOINTMENTS                         ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");

            var appointments = service.GetAllAppointments();
            if (appointments.Any())
            {
                foreach (var appointment in appointments)
                {
                    Console.WriteLine();
                    appointment.DisplayAppointmentInfo();
                    Console.WriteLine(new string('-', 60));
                }
            }
            else
            {
                Console.WriteLine("No appointments found.");
            }
        }

        static void AddMedicalRecord(HospitalService service)
        {
            Console.WriteLine("\n--- Add Medical Record ---");
            Console.Write("Enter Patient ID: ");
            int.TryParse(Console.ReadLine(), out int patientId);

            Console.Write("Enter diagnosis: ");
            var diagnosis = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter treatment: ");
            var treatment = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter prescriptions: ");
            var prescriptions = Console.ReadLine() ?? string.Empty;

            service.AddMedicalRecord(patientId, diagnosis, treatment, prescriptions);
        }

        static void ViewMedicalRecord(HospitalService service)
        {
            Console.WriteLine("\n--- View Medical Record ---");
            Console.Write("Enter Patient ID: ");
            int.TryParse(Console.ReadLine(), out int patientId);

            var record = service.GetMedicalRecord(patientId);
            if (record != null)
            {
                record.DisplayRecord();
            }
            else
            {
                Console.WriteLine($"No medical record found for patient ID {patientId}");
            }
        }

        static void SearchPatientsByDisease(HospitalService service)
        {
            Console.Write("\nEnter disease name to search: ");
            var disease = Console.ReadLine() ?? string.Empty;
            service.GetPatientsByDisease(disease);
        }
    }
}