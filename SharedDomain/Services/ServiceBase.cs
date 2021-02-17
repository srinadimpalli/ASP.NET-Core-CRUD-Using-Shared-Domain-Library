using SharedDomain.Contracts;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Services
{
    public abstract class ServiceBase<T> : IServiceBase<T> where T : class
    {
        private readonly CustOrdProdContext _custOrdProdContext;

        public ServiceBase(CustOrdProdContext custOrdProdContext)
        {
            _custOrdProdContext = custOrdProdContext;
        }
        public void Create(T entity) => _custOrdProdContext.Set<T>().Add(entity);
        public async Task CreateAsync(T entity) => await _custOrdProdContext.Set<T>().AddAsync(entity);
        public void Update(T entity) => _custOrdProdContext.Set<T>().Update(entity);
        public void Delete(T entity) => _custOrdProdContext.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ? _custOrdProdContext.Set<T>().AsNoTracking() : _custOrdProdContext.Set<T>();


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>

            !trackChanges ? _custOrdProdContext.Set<T>()
                            .Where(expression)
                            .AsNoTracking() : _custOrdProdContext.Set<T>()
                            .Where(expression);
    }
}
