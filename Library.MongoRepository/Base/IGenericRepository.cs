using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Library.MongoRepository.Base
{
    public interface IGenericRepository<TModel>
    {
        List<TModel> Get();

        TModel Get(string id);

        byte[] GetFile(ObjectId fileId);

        void Create(TModel model, byte[] file);

        void Update(TModel model);

        void UpdateFile(TModel model, byte[] file);

        void Delete(TModel model);

        List<TModel> Filter(Expression<Func<TModel, bool>> filter);

        TModel GetFirstOrDefault(Expression<Func<TModel, bool>> filter);

        TModel GetSingleOrDefault(Expression<Func<TModel, bool>> filter);

        long Count(Expression<Func<TModel, bool>> filter);
    }

}
