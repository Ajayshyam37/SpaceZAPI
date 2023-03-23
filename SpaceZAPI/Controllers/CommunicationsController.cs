using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SpaceZAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommunicationsController : ControllerBase
{
    private readonly List<Communication> _communications = new List<Communication>
    {
      new Communication()
      {
        spaceCraft_ID = 1, name = "Falcon 9", state = State.Active,
        commType = CommunicationType.Rocket,
        payloadType = PayloadType.Communication
      },
      new Communication()
      {
        spaceCraft_ID = 2, name = "Dragon Crew", state = State.Active,
        commType = CommunicationType.Rocket,
        payloadType = PayloadType.Scientific
      },
      new Communication()
      {
        spaceCraft_ID = 3, name = "Starship", state = State.DeOrbited,
        commType = CommunicationType.Rocket,
        payloadType = PayloadType.Scientific
      },
      new Communication()
      {
        spaceCraft_ID = 4, name = "Saturn V", state = State.Waiting,
        commType = CommunicationType.Rocket,
        payloadType = PayloadType.Scientific
      },
      new Communication()
      {
        spaceCraft_ID = 5, name = "Vostok 1", state = State.Waiting,
        commType = CommunicationType.Rocket,
        payloadType = PayloadType.Scientific
      }

};

    private List<Telemetry> GenerateTelemetry()
    {
       
        var random = new Random();
        var id = Guid.NewGuid();
        var altitude = $"{random.Next(1, 10000)} km";
        var longitude = $"{random.Next(180)} {(random.Next(2) == 0 ? "N" : "S")}";
        var latitude = $"{random.Next(180)} {(random.Next(2) == 0 ? "E" : "W")}";
        var temperature = $"{random.Next(-100, 100)} C";
        var timeToOrbit = $"{random.Next(1, 24)} hours";
        List<Telemetry> temp = new List<Telemetry>() {
        new Telemetry
        {
            id = id,
            altitude = altitude,
            longitude = longitude,
            latitude = latitude,
            temperature = temperature,
            timeToOrbit = timeToOrbit
        } };
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
    public List<Telemetry> GetTelemetry(int spaceCraftId)
    {
        return GenerateTelemetry();
    }

}

