using System;
using System.Collections.Generic;
using FlexibleInventorySystem.Domain;

namespace FlexibleInventorySystem.Repositories
{
    public interface IRepository<T> where T : Product
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(Guid id);
        T GetById(Guid id);
        IEnumerable<T> GetAll();
    }
}