using CIT.HelpDesk.Application.Interfaces;
using CIT.HelpDesk.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace CIT.HelpDesk.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericrepository<T> where T : class
    {
        private readonly UserContext _userContext;
        private readonly DbSet<T> _entities;

        public GenericRepository(UserContext userContext)
        {
            _userContext = userContext;
            _entities = userContext.Set<T>();
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
            _userContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
            _userContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public T GetById(int id)
        {
            return _entities.Find(id);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
            _userContext.SaveChanges();
        }
    }
}
