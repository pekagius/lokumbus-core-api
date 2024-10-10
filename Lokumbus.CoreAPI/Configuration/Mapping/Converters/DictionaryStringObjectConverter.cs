using System.Text.Json;
using Mapster;
using MongoDB.Bson;

namespace Lokumbus.CoreAPI.Configuration.Mapping.Converters
{
    public class DictionaryStringObjectConverter : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Dictionary<string, object>, Dictionary<string, object>>()
                .MapWith(source => ConvertMetadata(source));

            config.NewConfig<BsonDocument, Dictionary<string, object>>()
                .MapWith(source => ConvertMetadata(source.ToDictionary()));

            config.NewConfig<Dictionary<string, object>, BsonDocument>()
                .MapWith(source => ConvertToBsonDocument(source));
        }

        private static Dictionary<string, object> ConvertMetadata(Dictionary<string, object> source)
        {
            return source.ToDictionary(
                keyValuePair => keyValuePair.Key,
                keyValuePair => ConvertValue(keyValuePair.Value));
        }

        private static object ConvertValue(object value)
        {
            return value switch
            {
                JsonElement jsonElement => ConvertValue(jsonElement),
                IDictionary<string, object> dictionary => dictionary.ToDictionary(kvp => kvp.Key, kvp => ConvertValue(kvp.Value)),
                IEnumerable<object> list => list.Select(ConvertValue).ToList(),
                _ => value
            };
        }

        private static object ConvertValue(JsonElement jsonElement)
        {
            return (jsonElement.ValueKind switch
            {
                JsonValueKind.Object => jsonElement.EnumerateObject().ToDictionary(e => e.Name, e => ConvertValue(e.Value)),
                JsonValueKind.Array => jsonElement.EnumerateArray().Select(ConvertValue).ToList(),
                JsonValueKind.String => jsonElement.GetString(),
                JsonValueKind.Number => jsonElement.TryGetInt32(out int intValue) ? intValue :
                    jsonElement.TryGetInt64(out long longValue) ? longValue :
                    jsonElement.GetDouble(),
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                JsonValueKind.Null => null,
                _ => jsonElement.ToString()
            })!;
        }

        private static BsonDocument ConvertToBsonDocument(Dictionary<string, object> source)
        {
            return new BsonDocument(source.ToDictionary(kvp => kvp.Key, kvp => ConvertToBsonValue(kvp.Value)));
        }

        private static BsonValue ConvertToBsonValue(object value)
        {
            return value switch
            {
                Dictionary<string, object> dictionary => ConvertToBsonDocument(dictionary),
                List<object> list => new BsonArray(list.Select(ConvertToBsonValue)),
                string str => new BsonString(str),
                int intVal => new BsonInt32(intVal),
                long longVal => new BsonInt64(longVal),
                double doubleVal => new BsonDouble(doubleVal),
                bool boolVal => new BsonBoolean(boolVal),
                null => BsonNull.Value,
                _ => new BsonString(value.ToString())
            };
        }
    }
}