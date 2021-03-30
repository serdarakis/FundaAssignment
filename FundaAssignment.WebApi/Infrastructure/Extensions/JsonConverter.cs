using Newtonsoft.Json;

namespace FundaAssignment.WebApi.Infrastructure.Extensions
{
    public static class JsonConverter
    {
        public static T ToObject<T>(this string data)
        {
            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };

            return JsonConvert.DeserializeObject<T>(data, microsoftDateFormatSettings);
        }

        public static string ToJson(this object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
