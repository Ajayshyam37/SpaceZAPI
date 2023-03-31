using System;
using SpaceZAPI.Models;

namespace SpaceZAPI.Services
{
	public interface IPayLoadService
	{
		List<PayLoad> Get();
        PayLoad Get(string id);
        PayLoad Create(PayLoad payLoad);
		void UpdateState(string id, PayLoad payLoad);
        void UpdatePayLoad(string id, PayLoad payLoad);
        void Remove(string payloadid);
    }
}

