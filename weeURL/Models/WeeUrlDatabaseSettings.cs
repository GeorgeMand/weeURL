using System;
namespace weeURL.Models
{
	public class WeeUrlDatabaseSettings
	{
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string WeeCollectionName { get; set; } = null!;
    }
}

