using System;
using SpaceZAPI.Models;

namespace SpaceZAPI.Services
{
	public interface ITelemetryCommunicationService
	{
		List<Telemetry> Get(string id);
		Telemetry GetTelemetry(string id);
		Telemetry Create(Telemetry telemetry);
        void Remove(string id);
    }
}

