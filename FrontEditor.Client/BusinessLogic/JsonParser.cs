using Newtonsoft.Json;

namespace FrontEditor.Client.BusinessLogic
{
    public class JsonParser<T>
    {
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public string SerializeData(T data)
        {
            return JsonConvert.SerializeObject(data, settings);
        }
        public T DeserializeData(string json)
        {
            T objectData = JsonConvert.DeserializeObject<T>(json, settings);
            return objectData;
        }
    }
}