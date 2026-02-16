using System;

namespace Exceptions
{
    public class DuplicateMedicineException : Exception
    {
        public DuplicateMedicineException(string id) : base($"Duplicate medicine with id {id}") { }
    }
}
