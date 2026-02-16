using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Exceptions;

namespace Services
{
    public class MedicineUtility
    {
        private readonly SortedDictionary<int, List<Medicine>> _store = new SortedDictionary<int, List<Medicine>>();

        public void AddMedicine(Medicine medicine)
        {
            if (medicine == null) throw new ArgumentNullException(nameof(medicine));
            if (medicine.Price < 0) throw new InvalidPriceException("Price cannot be negative.");
            if (medicine.ExpiryYear < 1900 || medicine.ExpiryYear > DateTime.Now.Year + 100) throw new InvalidExpiryYearException("ExpiryYear is invalid.");

            if (_store.Values.SelectMany(x => x).Any(m => m.Id == medicine.Id)) throw new DuplicateMedicineException(medicine.Id);

            if (!_store.ContainsKey(medicine.ExpiryYear)) _store[medicine.ExpiryYear] = new List<Medicine>();
            _store[medicine.ExpiryYear].Add(medicine);
        }

        public IEnumerable<Medicine> GetAllMedicines()
        {
            var list = new List<Medicine>();
            foreach (var kvp in _store)
            {
                list.AddRange(kvp.Value);
            }
            return list;
        }

        public void UpdateMedicinePrice(string id, int newPrice)
        {
            if (newPrice < 0) throw new InvalidPriceException("Price cannot be negative.");
            var med = _store.Values.SelectMany(x => x).FirstOrDefault(m => m.Id == id);
            if (med == null) throw new MedicineNotFoundException(id);
            med.Price = newPrice;
        }
    }
}
