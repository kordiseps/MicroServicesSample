using Library.MongoDocumentModel;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Library.MongoRepository.Base
{
    public abstract class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : BaseDocumentModel
    {
        protected readonly IGridFSBucket gridFs;
        protected readonly IMongoCollection<TModel> mongoCollection;

        public GenericRepository(IMongoClient mongoClient, string dbName, string collectionName)
        {
            var mongoDatabase = mongoClient.GetDatabase(dbName);
            mongoCollection = mongoDatabase.GetCollection<TModel>(collectionName);
            gridFs = new GridFSBucket(mongoDatabase, new GridFSBucketOptions()
            {
                BucketName = collectionName + "FS",
            });
        }

        public long Count(Expression<Func<TModel, bool>> filter)
        {
            return mongoCollection.CountDocuments(filter);
        }

        public void Create(TModel model, byte[] file)
        {
            ObjectId fileInfo = gridFs.UploadFromBytes(model.FileName, file);
            model.FileId = fileInfo;
            model.InsertTime = DateTime.Now;
            mongoCollection.InsertOne(model);
        }

        public void Delete(TModel model)
        {
            gridFs.Delete(model.FileId);
            mongoCollection.DeleteOne(m => m.Id == model.Id);
        }

        public List<TModel> Filter(Expression<Func<TModel, bool>> filter)
        {
            return filterBase(filter).ToList();
        }

        private IFindFluent<TModel, TModel> filterBase(Expression<Func<TModel, bool>> filter)
        {
            return mongoCollection.Find(filter);
        }
        public List<TModel> Get()
        {
            return Filter(x => true);
        }

        public TModel Get(string id)
        {
            var docId = new ObjectId(id);
            return GetFirstOrDefault(m => m.Id == docId);
        }

        public byte[] GetFile(ObjectId fileId)
        {
            return gridFs.DownloadAsBytes(fileId);
        }

        public TModel GetFirstOrDefault(Expression<Func<TModel, bool>> filter)
        {
            return filterBase(filter).FirstOrDefault();
        }

        public TModel GetSingleOrDefault(Expression<Func<TModel, bool>> filter)
        {
            return filterBase(filter).SingleOrDefault();
        }

        public void Update(TModel model)
        {
            mongoCollection.ReplaceOne(m => m.Id == model.Id, model);
        }

        public void UpdateFile(TModel model, byte[] file)
        {
            gridFs.Delete(model.FileId);
            ObjectId fileInfo = gridFs.UploadFromBytes(model.FileName, file);
            model.FileId = fileInfo;
            model.InsertTime = DateTime.Now;
            Update(model);
        }
    }

}
