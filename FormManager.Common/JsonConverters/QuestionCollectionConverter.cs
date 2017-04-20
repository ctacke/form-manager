using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace OpenNETCF.FormManager
{
    public class QuestionCollectionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(QuestionCollection));
        }

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                         object existingValue,
                                         JsonSerializer serializer)
        {
            var arr = JArray.Load(reader);

            var qc = new QuestionCollection();

            foreach (JObject item in arr)
            {
                qc.Add(item.ToObject<Question>());
            }

            return qc;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }     
    }
}
