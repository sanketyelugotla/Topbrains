namespace HospitalManagement.Exceptions
{
    public class InvalidAppointmentException : Exception
    {
        public InvalidAppointmentException() : base("Invalid appointment details provided.")
        {
        }

        public InvalidAppointmentException(string message) : base(message)
        {
        }

        public InvalidAppointmentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
