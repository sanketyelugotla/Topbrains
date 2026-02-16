using System.Collections.Generic;
using Domain;
using Exceptions;

namespace Services
{
    public class ManagementService
    {
        private SortedDictionary<int, List<BaseEntity>> _data = new SortedDictionary<int, List<BaseEntity>>();

        public void AddEntity(int key, BaseEntity entity)
        {
            // TODO: Validate entity
            // TODO: Handle duplicate entries
            // TODO: Add entity to SortedDictionary
            if (!_data.ContainsKey(key))_data[key] = new List<BaseEntity>();
            _data[key].Add(entity);
        }

        public void UpdateEntity(int key)
        {
            // TODO: Update entity logic
        }

        public void RemoveEntity(int key)
        {
            // TODO: Remove entity logic
            if (_data.ContainsKey(key))_data.Remove(key);
            else throw new InvalidOperationException($"Entity with key {key} not found.");
        }

        public IEnumerable<BaseEntity> GetAll()
        {
            // TODO: Return sorted entities
            List<BaseEntity> allEntities = new List<BaseEntity>();
            foreach (var kvp in _data)
            {
                allEntities.AddRange(kvp.Value);
            }
            return allEntities;
        }
    }
}
