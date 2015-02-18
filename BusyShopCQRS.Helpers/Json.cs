using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace BusyShopCQRS.Helpers
{
    public class Json
    {
        public static HttpResponseMessage UploadJsonObjectAsync<T>(Uri uri, T data)
        {
            using (var client = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(data);
                return client.PostAsync(uri, new StringContent(content, Encoding.UTF8, "application/json")).Result;
            }
        }
    }
}