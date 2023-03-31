namespace SpaceZAPI.Models
{
	public interface ISpaceZDatabaseSettings
    {
		string SpaceCraftsCollectionName { get; set; }
        string PayLoadCollectionName { get; set; }
        string TelemetryCommunicationCollectionName { get; set; }
        string PayLoadDataCollectionName { get; set; }
        string ConnectionString { get; set; }
		string DatabaseName { get; set; }

    }
}

