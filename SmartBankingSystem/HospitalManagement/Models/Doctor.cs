using HospitalManagement.Interfaces;

namespace HospitalManagement.Models
{
    public class Doctor : Person, IBillable
    {
        public string Specialization { get; set; } = string.Empty;
        public double ConsultationFee { get; set; }
        public bool IsAvailable { get; set; } = true;
        public double TotalEarnings { get; set; } = 0;
        public int AppointmentCount { get; set; } = 0;

        public override void DisplayInfo()
        {
            Console.WriteLine($"Doctor ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Specialization: {Specialization}");
            Console.WriteLine($"Consultation Fee: Rs.{ConsultationFee}");
            Console.WriteLine($"Contact: {ContactNumber}");
            Console.WriteLine($"Available: {(IsAvailable ? "Yes" : "No")}");
        }

        public double CalculateBill()
        {
            return ConsultationFee;
        }

        public void DisplayBill()
        {
            Console.WriteLine($"Consultation Fee for Dr. {Name}: Rs.{CalculateBill()}");
        }

        public void AddEarnings(double amount)
        {
            TotalEarnings += amount;
            AppointmentCount++;
        }
    }
}
