using Library.MongoDocumentModel;
using Library.MongoRepository.Base;
using Library.MongoRepository.Interface;
using MongoDB.Driver;

namespace Library.MongoRepository
{
    public class OutFileDocumentRepository : GenericRepository<OutFileDocumentModel>, IOutFileDocumentRepository
    {
        private const string DB_COLLECTION = "OutFileDocument";
        public OutFileDocumentRepository(IMongoClient mongoClient, string dbName) : base(mongoClient, dbName, DB_COLLECTION) { }
    }
}
