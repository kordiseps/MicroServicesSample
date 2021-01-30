using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Library.MsSqlRepository.Base
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        List<TModel> GetList();

        TModel GetById(object id);

        void Create(TModel model);

        void Update(TModel model);

        void Delete(TModel model);

        void Delete(object id);

        IEnumerable<TModel> Filter(Expression<Func<TModel, bool>> filter);

        TModel GetFirstOrDefault(Expression<Func<TModel, bool>> filter);

        TModel GetSingleOrDefault(Expression<Func<TModel, bool>> filter);

        long Count(Expression<Func<TModel, bool>> filter);
    }
}
