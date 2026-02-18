namespace HospitalManagement.Exceptions
{
    public class DuplicateMedicalRecordException : Exception
    {
        public DuplicateMedicalRecordException() : base("Medical record already exists for this patient.")
        {
        }

        public DuplicateMedicalRecordException(string message) : base(message)
        {
        }

        public DuplicateMedicalRecordException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
