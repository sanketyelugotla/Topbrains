using System;

namespace Exceptions
{
    public class MedicineNotFoundException : Exception
    {
        public MedicineNotFoundException(string id) : base($"Medicine with id {id} not found.") { }
    }
}
