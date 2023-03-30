using System;
using SpaceZAPI.Models;

namespace SpaceZAPI.Services
{
	public interface ISpaceCraftService
	{
		List<SpaceCraft> Get();
		SpaceCraft Get(string id);
		SpaceCraft Create(SpaceCraft spaceCraft);
		void UpdateState(string id, SpaceCraft spaceCraft);
		void UpdateSpaceCraft(string id, SpaceCraft spaceCraft);
        void Remove(string spaceCraft_ID);
    }
}

