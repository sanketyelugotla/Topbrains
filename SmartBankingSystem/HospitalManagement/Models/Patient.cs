namespace HospitalManagement.Models
{
    public class Patient : Person
    {
        public string Disease { get; set; } = string.Empty;
        public DateTime AdmissionDate { get; set; }
        public bool IsAdmitted { get; set; } = false;

        public override void DisplayInfo()
        {
            Console.WriteLine($"Patient ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Disease: {Disease}");
            Console.WriteLine($"Contact: {ContactNumber}");
            Console.WriteLine($"Admission Date: {AdmissionDate.ToShortDateString()}");
            Console.WriteLine($"Currently Admitted: {(IsAdmitted ? "Yes" : "No")}");
        }
    }
}