using System;
namespace SpaceZAPI.Models
{
	public class SpaceZDatabase : ISpaceZDatabaseSettings
	{
        public string SpaceCraftsCollectionName { get; set; } = String.Empty;
        public string PayLoadCollectionName { get; set; } = string.Empty;
        public string TelemetryCommunicationCollectionName { get; set; } = string.Empty;
        public string PayLoadDataCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}

