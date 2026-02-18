namespace HospitalManagement.Exceptions
{
    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException() : base("Patient not found in the system.")
        {
        }

        public PatientNotFoundException(string message) : base(message)
        {
        }

        public PatientNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
