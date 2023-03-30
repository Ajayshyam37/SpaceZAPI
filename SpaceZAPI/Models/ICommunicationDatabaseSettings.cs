namespace SpaceZAPI.Models
{
	public interface ISpaceZDatabase
	{
		string SpaceCraftsCollectionName { get; set; }
		string ConnectionString { get; set; }
		string DatabaseName { get; set; }

    }
}

