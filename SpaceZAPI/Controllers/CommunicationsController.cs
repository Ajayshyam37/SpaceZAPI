using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Newtonsoft.Json;

namespace SpaceZAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommunicationsController : ControllerBase
{
    private readonly List<Communication> _communications = new List<Communication>
    {
      new Communication()
      {
        spaceCraft_ID = 1, name = "Falcon 9",
        state = State.Active,
        commType = CommunicationType.Rocket,
        payLoad_ID = Guid.NewGuid(),
        payloadState =State.Waiting,
        payloadType = PayloadType.Communication,
        telemetry = false,
        payloadData = false,
        orbitRadius = 30000,
        totalTimeToOrbit = (30000 / 3600 + 10)
      },
      new Communication()
      {
        spaceCraft_ID = 2,
        name = "Dragon Crew",
        state = State.Active,
        commType = CommunicationType.Rocket,
        payLoad_ID = Guid.NewGuid(),
        payloadState =State.Active,
        payloadType = PayloadType.Spy,
        telemetry = false,
        payloadData = false,
        orbitRadius = 400000,
        totalTimeToOrbit = (400000 / 3600 + 10)
      },
      new Communication()
      {
        spaceCraft_ID = 3, name = "Starship",
        state = State.Active,
        commType = CommunicationType.Rocket,
        payLoad_ID = Guid.NewGuid(),
        payloadState =State.Waiting,
        payloadType = PayloadType.Scientific,
        telemetry = false,
        payloadData = false,
        orbitRadius = 40000,
        totalTimeToOrbit = (4000 / 3600 + 10)
      },
      new Communication()
      {
        spaceCraft_ID = 4,
        name = "Saturn V",
        state = State.Waiting,
        commType = CommunicationType.Rocket,
        payLoad_ID = Guid.NewGuid(),
        payloadState =State.Waiting,
        payloadType = PayloadType.Scientific,
        telemetry = false,
        payloadData = false,
        orbitRadius = 500000,
        totalTimeToOrbit = (500000 / 3600 + 10)
      },
      new Communication()
      {
        name = "Vostok 1",
        state = State.Waiting,
        commType = CommunicationType.Rocket,
        payloadState =State.Waiting,
        payLoad_ID = Guid.NewGuid(),
        payloadType = PayloadType.Communication,
        telemetry = false,
        payloadData = false,
        orbitRadius = 6000,
        totalTimeToOrbit = (600000 / 3600 + 10)
      }

};

    private List<Telemetry> GenerateTelemetry(double orbitRadius, double totalTime, int telemetryCount)
    {
        var random = new Random();
        var timeToOrbit = totalTime - (telemetryCount * 5);
        var altitude = "";
        var id = Guid.NewGuid();
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
            id = id,
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
    [Route("GetCommunications")]
    public IEnumerable<object> Get()
    {
        var communications = new List<object>();
        foreach (var communication in _communications)
        {
            communications.Add(new { communication.spaceCraft_ID, communication.name, communication.state, communication.commType, communication.payloadType });
        }
        return communications;
    }
    [HttpGet]
    [Route("GetBySpaceCraftId")]
    public Communication GetBySpaceCraftId([FromQuery] int id)
    {
        var communications = new Communication();
        foreach (var communication in _communications)
        {
            if (communication.spaceCraft_ID == id)
            {
                communications = communication;
                break;
            }
        }
        return communications;
    }

    [HttpGet]
    [Route("GetTelemetry")]
    public List<Telemetry> GetTelemetry([FromQuery] int id, [FromQuery] int count)
    {
        var communication = new Communication();
        communication = GetBySpaceCraftId(id);
        return GenerateTelemetry(communication.orbitRadius, communication.totalTimeToOrbit, count);
    }
    [HttpGet]
    [Route("GetPayLoadData")]
    public IActionResult GetPayLoadData([FromQuery] int payloadtype)
    {
        dynamic payloadData = null;

        switch (payloadtype)
        {
           
            case 0: // Communication payload
                payloadData = new
                {
                    CommunicationData = new
                    {
                        Bandwidth = new { Uplink = new Random().Next(100), Downlink = new Random().Next(100) }
                    }
                };
                break;

            case 1: // Spy payload
                string imageUrl = "https://source.unsplash.com/random/?satellite-images";
                byte[] imageData = new System.Net.WebClient().DownloadData(imageUrl);
                string base64Image = Convert.ToBase64String(imageData);
                string dataFormat = "jpeg";
                return Ok(new { ImageData = $"data:image/{dataFormat};base64,{base64Image}" });
            case 2: // Scientific payload
                payloadData = new
                {
                    ScientificData = new
                    {
          
                        Weather = new { Rain = new Random().Next(100), Humidity = new Random().Next(100), Snow = new Random().Next(100) }
                    }
                };
                break;
            default:
                return NotFound();
        }

        string payloadJson = JsonConvert.SerializeObject(payloadData);
        return Ok(payloadJson);
    }
}

