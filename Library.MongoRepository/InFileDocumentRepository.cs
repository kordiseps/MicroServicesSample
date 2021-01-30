using Library.MongoDocumentModel;
using Library.MongoRepository.Base;
using Library.MongoRepository.Interface;
using MongoDB.Driver;

namespace Library.MongoRepository
{
    public class InFileDocumentRepository : GenericRepository<InFileDocumentModel>, IInFileDocumentRepository
    {
        private const string DB_COLLECTION = "InFileDocument";
        public InFileDocumentRepository(IMongoClient mongoClient, string dbName) : base(mongoClient, dbName, DB_COLLECTION) { }
    }
}
