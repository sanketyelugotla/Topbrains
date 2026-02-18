using System.Collections.Generic;
using Domain;
using Exceptions;

namespace Services
{
    public class ManagementService
    {
        private SortedDictionary<int, List<Student>> _data = new SortedDictionary<int, List<Student>>(Comparer<int>.Create((a, b) => b.CompareTo(a)));

        public void AddEntity(int key, Student student)
        {
            // TODO: Validate entity
            // TODO: Handle duplicate entries
            // TODO: Add entity to SortedDictionary
            if (student == null) throw new ArgumentNullException(nameof(student));
            if (student.GPA < 0 || student.GPA > 10) throw new ArgumentException("GPA must be between 0 and 10.");
            if (!_data.ContainsKey(key)) _data[key] = new List<Student>();
            _data[key].Add(student);
        }

        public void UpdateEntity(int key)
        {
            // TODO: Update entity logic
            if (!_data.ContainsKey(key)) throw new ArgumentException($"Entity with key {key} not found.");
            
        }

        public void RemoveEntity(int key)
        {
            // TODO: Remove entity logic
            if (!_data.ContainsKey(key)) throw new ArgumentException($"Entity with key {key} not found.");
            
        }

        public IEnumerable<Student> GetAll()
        {
            // TODO: Return sorted entities
            return new List<Student>();
        }
    }
}
