using CRUD_file.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUD_file.Data
{
    public class StorageService<T> : IStorageService<T> where T : class, new()
    {
        private static HashSet<T> list;

        public StorageService()
        {
            if (list == null)
            {
                list = new HashSet<T>();
            }
        }

        public void AddItem(T item)
        {
            try
            {
                lock (list) list.Add(item);                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EditItem(T item, Func<T, bool> whereClause) 
        {
            bool resultOk = true;
            try
            {               
                var data = list.Where<T>(whereClause).ToList();
                if ((data != null) && (data.Any()))
                {
                    lock (list) list.Remove(data[0]);
                    lock (list) list.Add(item);
                }
                else
                {
                    resultOk = false;
                }
            }
            catch (Exception)
            {
                resultOk = false;
            }
            return resultOk;

        }

        public bool DeleteItem(Func<T, bool> whereClause)
        {
            bool resultOk = true;
            try
            {
                var data = list.Where<T>(whereClause).ToList();
                if ((data != null) && (data.Any()))
                {
                    lock (list) list.Remove(data[0]);
                }
                else
                {
                    resultOk = false;
                }
            }
            catch (Exception)
            {
                resultOk = false;
            }
            return resultOk;
        }

        public T GetItemWhere(Func<T, bool> whereClause)
        {
            try
            {
                var data = list.Where<T>(whereClause).ToList();
                if ((data != null) && (data.Any()))
                    return data[0];
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
