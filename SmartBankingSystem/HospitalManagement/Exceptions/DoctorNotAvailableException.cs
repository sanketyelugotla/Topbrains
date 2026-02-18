namespace HospitalManagement.Exceptions
{
    public class DoctorNotAvailableException : Exception
    {
        public DoctorNotAvailableException() : base("Doctor is not available at the requested time.")
        {
        }

        public DoctorNotAvailableException(string message) : base(message)
        {
        }

        public DoctorNotAvailableException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
