using FrontEditor.Client.Models.EditorModels;
using Newtonsoft.Json;

namespace FrontEditor.Client.BusinessLogic
{
    public class JsonParser<T>
    {
        public string SerializeData(T data)
        { 
            return JsonConvert.SerializeObject(data);
        }
        public T DeserializeData(string json) 
        {
            T objectData = JsonConvert.DeserializeObject<T>(json);
            return objectData;            
        }
    }
}