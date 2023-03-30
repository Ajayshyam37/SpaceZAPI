using System;
namespace SpaceZAPI.Models
{
	public class SpaceZDatabase : ISpaceZSpaceCraftDatabase
	{
        public string SpaceCraftsCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}

