using HospitalManagement.Interfaces;

namespace HospitalManagement.Models
{
    public class Appointment : IBillable
    {
        public int AppointmentId { get; set; }
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Status { get; set; } = "Scheduled"; // Scheduled, Completed, Cancelled
        public double AdditionalCharges { get; set; } = 0;
        public string Notes { get; set; } = string.Empty;

        public double CalculateBill()
        {
            double totalBill = Doctor?.ConsultationFee ?? 0;
            totalBill += AdditionalCharges;
            return totalBill;
        }

        public void DisplayBill()
        {
            Console.WriteLine($"\n--- Appointment Bill ---");
            Console.WriteLine($"Appointment ID: {AppointmentId}");
            Console.WriteLine($"Patient: {Patient?.Name}");
            Console.WriteLine($"Doctor: {Doctor?.Name}");
            Console.WriteLine($"Consultation Fee: Rs.{Doctor?.ConsultationFee ?? 0}");
            Console.WriteLine($"Additional Charges: Rs.{AdditionalCharges}");
            Console.WriteLine($"Total Bill: Rs.{CalculateBill()}");
            Console.WriteLine($"------------------------");
        }

        public void DisplayAppointmentInfo()
        {
            Console.WriteLine($"Appointment ID: {AppointmentId}");
            Console.WriteLine($"Patient: {Patient?.Name} (ID: {Patient?.Id})");
            Console.WriteLine($"Doctor: Dr. {Doctor?.Name} ({Doctor?.Specialization})");
            Console.WriteLine($"Date & Time: {AppointmentDateTime}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"Notes: {Notes}");
        }
    }
}
