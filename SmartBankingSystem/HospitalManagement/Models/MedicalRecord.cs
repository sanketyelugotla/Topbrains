namespace HospitalManagement.Models
{
    public class MedicalRecord
    {
        private int recordId;
        private int patientId;
        private string diagnosis = string.Empty;
        private string treatment = string.Empty;
        private DateTime recordDate;
        private string prescriptions = string.Empty;

        // Encapsulation with properties
        public int RecordId
        {
            get { return recordId; }
            set { recordId = value; }
        }

        public int PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        public string Diagnosis
        {
            get { return diagnosis; }
            set { diagnosis = value; }
        }

        public string Treatment
        {
            get { return treatment; }
            set { treatment = value; }
        }

        public DateTime RecordDate
        {
            get { return recordDate; }
            set { recordDate = value; }
        }

        public string Prescriptions
        {
            get { return prescriptions; }
            set { prescriptions = value; }
        }

        public void DisplayRecord()
        {
            Console.WriteLine($"\n--- Medical Record ---");
            Console.WriteLine($"Record ID: {RecordId}");
            Console.WriteLine($"Patient ID: {PatientId}");
            Console.WriteLine($"Diagnosis: {Diagnosis}");
            Console.WriteLine($"Treatment: {Treatment}");
            Console.WriteLine($"Prescriptions: {Prescriptions}");
            Console.WriteLine($"Record Date: {RecordDate.ToShortDateString()}");
            Console.WriteLine($"---------------------");
        }
    }
}
