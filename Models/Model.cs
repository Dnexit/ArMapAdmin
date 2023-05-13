using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArMapAdmin.Models
{
    public class Model
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        //public object coordinates { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public string color { get; set; }
        public string modelURL { get; set; }
        public string imageURL { get; set; }
    }
}
