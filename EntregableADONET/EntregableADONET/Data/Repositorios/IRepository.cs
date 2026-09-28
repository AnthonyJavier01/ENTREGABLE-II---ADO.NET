using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Data.Repositorios
{
    public interface IRepository<T>
    {
        void Insert(T entity);

        void Update(T entity);

        void Delete(int id);

        List<T> GetAll();

        T GetById(int id);
    }
}
