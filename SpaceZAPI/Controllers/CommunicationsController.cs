using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SpaceZAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommunicationsController : ControllerBase
{
    int telemetryCount = 0;
    private readonly List<Communication> _communications = new List<Communication>
    {
      new Communication()
      {
        spaceCraft_ID = 1, name = "Falcon 9", state = State.Active,payloadState =State.Active,
        commType = CommunicationType.Rocket,
        payloadType = PayloadType.Communication,
        orbitRadius = 30000,
        totalTimeToOrbit = (30000 / 3600 + 10)
      },
      new Communication()
      {
        spaceCraft_ID = 2, name = "Dragon Crew", state = State.Active,payloadState =State.Waiting,
        commType = CommunicationType.Rocket,
        payloadType = PayloadType.Scientific,
        orbitRadius = 400000,
        totalTimeToOrbit = (400000 / 3600 + 10)
      },
      new Communication()
      {
        spaceCraft_ID = 3, name = "Starship", state = State.DeOrbited,payloadState =State.Waiting,
        commType = CommunicationType.Rocket,
        payloadType = PayloadType.Scientific,
        orbitRadius = 40000,
        totalTimeToOrbit = (4000 / 3600 + 10)
      },
      new Communication()
      {
        spaceCraft_ID = 4, name = "Saturn V", state = State.Waiting,payloadState =State.Waiting,
        commType = CommunicationType.Rocket,
        payloadType = PayloadType.Scientific,
        orbitRadius = 500000,
        totalTimeToOrbit = (500000 / 3600 + 10)
      },
      new Communication()
      {
        spaceCraft_ID = 5, name = "Vostok 1", state = State.Waiting,payloadState =State.Waiting,
        commType = CommunicationType.Rocket,
        payloadType = PayloadType.Scientific,
        orbitRadius = 6000,
        totalTimeToOrbit = (600000 / 3600 + 10)
      }

};

    private List<Telemetry> GenerateTelemetry(double orbitRadius, double totalTime,int telemetryCount)
    {
        var random = new Random();
        var timeToOrbit = totalTime - (telemetryCount * 5);
        var altitude = "";
        if (timeToOrbit <= 0)
        {
            altitude = $"{orbitRadius}";
            timeToOrbit = 0;
        }
        else
        { 
          altitude = $"{Math.Round(telemetryCount * (orbitRadius / totalTime))} KM";
        }
        List<Telemetry> temp = new List<Telemetry>() {
        new Telemetry
        {
            id = new Guid(),
            altitude = altitude.ToString(),
            longitude = $"{random.Next(180)} {(random.Next(2) == 0 ? "N" : "S")}",
            latitude = $"{random.Next(180)} {(random.Next(2) == 0 ? "E" : "W")}",
            temperature = $"{random.Next(-100, 100)} C",
            timeToOrbit = timeToOrbit.ToString()
        }
    };
        return temp;
    }


    private readonly ILogger<CommunicationsController> _logger;

    public CommunicationsController(ILogger<CommunicationsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<object> Get()
    {
        var communications = new List<object>();
        foreach (var communication in _communications)
        {
            communications.Add(new { communication.spaceCraft_ID, communication.name, communication.state, communication.commType, communication.payloadType });
        }
        return communications;
    }
    [HttpGet("{spaceCraftId}")]
    public Communication GetBySpaceCraftId(int spaceCraftId)
    {
        var communications = new Communication();
        foreach (var communication in _communications)
        {
            if (communication.spaceCraft_ID == spaceCraftId)
            {
                communications = communication;
                break;
            }
        }
        return communications;
    }
    [HttpGet("GetTelmentry/{spaceCraftId}")]
    public List<Telemetry> GetTelemetry(int spaceCraftId, int count)
    {
        var communication = new Communication();
        communication = GetBySpaceCraftId(spaceCraftId);
        return GenerateTelemetry(communication.orbitRadius,communication.totalTimeToOrbit,count);
    }

}

