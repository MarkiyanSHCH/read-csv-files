﻿namespace HubSpot
{
    public interface IHubSpotApiSettings
    {
        public string BaseUrl { get; set; }
        public string AccessToken { get; set; }
    }
}