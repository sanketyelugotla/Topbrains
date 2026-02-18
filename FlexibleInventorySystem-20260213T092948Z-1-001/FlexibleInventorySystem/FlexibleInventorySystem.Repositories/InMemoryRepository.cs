using System;
using System.Collections.Generic;
using System.Linq;
using FlexibleInventorySystem.Domain;

namespace FlexibleInventorySystem.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : Product
    {
        private readonly List<T> _storage = new List<T>();

        public void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _storage.Add(entity);
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var existing = GetById(entity.ProductId);
            if (existing != null)
            {
                _storage.Remove(existing);
                _storage.Add(entity);
            }
            else
            {
                throw new InvalidOperationException("Product not found.");
            }
        }

        public void Remove(Guid id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _storage.Remove(entity);
            }
        }

        public T GetById(Guid id)
        {
            return _storage.FirstOrDefault(e => e.ProductId == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _storage.ToList();
        }
    }
}