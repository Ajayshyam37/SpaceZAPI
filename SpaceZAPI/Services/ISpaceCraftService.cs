using System;
using SpaceZAPI.Models;

namespace SpaceZAPI.Services
{
	public interface ISpaceCraftService
	{
		List<SpaceCraft> Get();
		SpaceCraft Get(string id);
		long GetCount();
        SpaceCraft Create(SpaceCraft spaceCraft);
		void Update(string id, SpaceCraft spaceCraft,bool state);
		void UpdateSpaceCraft(string id, SpaceCraft spaceCraft);
        void Remove(string spaceCraft_ID);
    }
}

