using Newtonsoft.Json;
using OnlineStore.BusinessLogic.Contracts.Dtos;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineStore.Api.IntegrationTests.Helpers
{
    public static class ResponseHelper
    {
        public static async Task<ApiResult<T>> GetApiResultAsync<T>(HttpResponseMessage responseMessage)
        {
            return JsonConvert.DeserializeObject<ApiResult<T>>(await responseMessage.Content.ReadAsStringAsync());
        }
    }
}
