namespace SpaceZAPI.Models
{
	public interface ISpaceZSpaceCraftDatabase
	{
		string SpaceCraftsCollectionName { get; set; }
		string ConnectionString { get; set; }
		string DatabaseName { get; set; }

    }
}

