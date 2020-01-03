using System;

namespace CRUD_file.Services
{
    public interface IStorageService<T>
    {
        void AddItem(T item);
        bool EditItem(T item, Func<T, bool> whereClause);
        bool DeleteItem(Func<T, bool> whereClause);
        T GetItemWhere(Func<T, bool> whereClause);
    }
}
