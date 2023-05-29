using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Core.Integrations.HubSpot;
using Core.Integrations.HubSpot.Models;

using Domain.Constants;
using Domain.Extensions;
using Domain.Result;

using HubSpot.Models;

namespace HubSpot
{
    public class HubSpotApiClient : IHubSpotApiClient
    {
        private readonly HttpClient _httpClient;

        public HubSpotApiClient(IHubSpotApiSettings settings, HttpClient httpClient)
        {
            this._httpClient = httpClient;
            this._httpClient.BaseAddress = new Uri(settings.BaseUrl);
            this._httpClient.DefaultRequestHeaders.Accept.Clear();
            this._httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(ContentTypes.Json));
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", settings.AccessToken);
        }

        public async Task<IResult<HubSpotContact>> GetContactById(long contactId)
        {
            ApiContactResponse contact = await this.GetContactModelByIdAsync(contactId);
            if (contact == null || contact.Properties == null)
                return Result.Failed<HubSpotContact>($"HubSpot contact {contactId} was not found.");

            return Result.Success<HubSpotContact>(contact.ToCoreModel());
        }

        private async Task<ApiContactResponse> GetContactModelByIdAsync(long contactId)
        {
            try
            {
                string resourceUrl = $"crm/v3/objects/contacts/{contactId}" +
                    $"?properties=internal_user_id" +
                    $"&properties=email" +
                    $"&properties=firstname" +
                    $"&properties=lastname" +
                    $"&properties=date_of_birth";
                HttpResponseMessage response = await this._httpClient.GetAsync(resourceUrl);
                if (response == null || !response.IsSuccessStatusCode || response.Content == null) return null;
                return await response.Content.ReadAsJsonAsync<ApiContactResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}