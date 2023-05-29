using Core.Integrations.HubSpot.Models;

namespace HubSpot.Models
{
    internal class ApiContactResponse
    {
        public string Id { get; set; }
        public ApiContactProperties Properties { get; set; }
        public bool Archived { get; set; }

        public HubSpotContact ToCoreModel()
            => this.Properties == null
                ? null
                : new HubSpotContact
                {
                    UserId = this.Properties.InternalUserId,
                    Email = this.Properties.Email,
                    FirstName = this.Properties.FirstName,
                    LastName = this.Properties.LastName,
                    BirthDate = this.Properties.BirthDate
                };
    }
}