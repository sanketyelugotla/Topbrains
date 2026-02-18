using System;
using System.Collections.Generic;
using FlexibleInventorySystem.Domain;
using FlexibleInventorySystem.Repositories;

namespace FlexibleInventorySystem.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        private IRepository<T> GetRepository<T>() where T : Product
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                _repositories[typeof(T)] = new InMemoryRepository<T>();
            }
            return (IRepository<T>)_repositories[typeof(T)];
        }

        public void AddProduct<T>(T product) where T : Product
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            product.Validate();
            GetRepository<T>().Add(product);
        }

        public void UpdateProduct<T>(T product) where T : Product
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            product.Validate();
            GetRepository<T>().Update(product);
        }

        public void RemoveProduct<T>(Guid id) where T : Product
        {
            GetRepository<T>().Remove(id);
        }

        public T GetProductById<T>(Guid id) where T : Product
        {
            return GetRepository<T>().GetById(id);
        }

        public IEnumerable<T> GetProductsByCategory<T>() where T : Product
        {
            return GetRepository<T>().GetAll();
        }
    }
}