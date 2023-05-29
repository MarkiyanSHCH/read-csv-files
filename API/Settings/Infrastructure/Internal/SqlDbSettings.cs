using Sql.DataAccess;

namespace API.Settings.Infrastructure.Internal
{
    public class SqlDbSettings : ISqlDbSettings
    {
        public string ConnectionString { get; set; }
    }
}