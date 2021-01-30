using MongoDB.Bson;
using System;

namespace Library.MongoDocumentModel
{
    public abstract class BaseDocumentModel
    {
        //tüm mongodb modelleri için zorunlu
        public ObjectId Id { get; set; }
        public DateTime InsertTime { get; set; }

        //tüm mongodb.gridfs modelleri için zorunlu
        public string FileName { get; set; }
        public ObjectId FileId { get; set; }
        public long FileSize { get; set; }
    }
}
