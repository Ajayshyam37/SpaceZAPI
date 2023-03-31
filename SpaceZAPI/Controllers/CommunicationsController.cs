using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Newtonsoft.Json;
using SpaceZAPI.Models;
using SpaceZAPI.Services;
using SpaceZAPI.Helper;
using MongoDB.Bson;

namespace SpaceZAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommunicationsController : ControllerBase
{

    private readonly ISpaceCraftService spaceCraftService;
    private readonly IPayLoadService payLoadService;
    private readonly ITelemetryCommunicationService telemetryCommunicationService;


    public CommunicationsController(ISpaceCraftService spaceCraftService, IPayLoadService payLoadService, ITelemetryCommunicationService telemetryCommunicationService)
    {
        this.spaceCraftService = spaceCraftService;
        this.payLoadService = payLoadService;
        this.telemetryCommunicationService = telemetryCommunicationService;
    }
    /// <summary>
    /// Generates Random Telemetry information and accurate time to orbit in secs
    /// </summary>
    /// <param name="orbitRadius"></param>
    /// <param name="totalTime"></param>
    /// <param name="launchtime"></param>
    /// <param name="spaceCraft_ID"></param>
    /// <returns></returns>
    private List<Telemetry> GenerateTelemetry(double orbitRadius, double totalTime, DateTime launchtime, string spaceCraft_ID)
    {
        Calculate cal = new Calculate();
        var timeToOrbit = cal.CalculateTimeToOrbit(orbitRadius, launchtime);
        var random = new Random();
        var altitude = "";
        if (timeToOrbit <= 0)
        {
            altitude = $"{orbitRadius} KM";
            timeToOrbit = 0;
        }
        else
        {
            altitude = $"{Math.Round(orbitRadius / timeToOrbit)} KM";
        }
        List<Telemetry> temp = new List<Telemetry>() {
        new Telemetry
        {
            id = ObjectId.GenerateNewId().ToString(),
            altitude = altitude.ToString(),
            longitude = $"{random.Next(180)} {(random.Next(2) == 0 ? "N" : "S")}",
            latitude = $"{random.Next(180)} {(random.Next(2) == 0 ? "E" : "W")}",
            temperature = $"{random.Next(-100, 100)} C",
            timeToOrbit = timeToOrbit,

            sourceid = spaceCraft_ID,
            sourcetype = SourceType.LaunchVehicle,
            createdat = DateTime.UtcNow
        }
        };

        if (temp != null)
        {
            var telemetry = telemetryCommunicationService.Create(temp[0]);
        }

        return temp;
    }

    [HttpGet]
    [Route("GetTelemetry")]
    public List<Telemetry> GetTelemetry([FromQuery] string id)
    {
        var spaceCraft = spaceCraftService.Get(id);

        if (spaceCraft == null)
        {
            return new List<Telemetry>();
        }

        return GenerateTelemetry(spaceCraft.orbitRadius, spaceCraft.totalTimeToOrbit, spaceCraft.launchtime, spaceCraft.spaceCraft_ID);
    }
    /// <summary>
    /// Generates random payload data based on the payload type
    /// </summary>
    /// <param name="payloadtype"></param>
    /// <param name="payloadid"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetPayLoadData")]
    public IActionResult GetPayLoadData([FromQuery] int payloadtype, string payloadid)
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
    /// <summary>
    /// Retreives the telemetry information for the spacecraft
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetTelemetryById")]
    public List<Telemetry> GetTelemetryById([FromQuery] string id)
    {
        var spacecraft = spaceCraftService.Get(id);
        if(spacecraft == null)
        {
            return new List<Telemetry>();
        }

        var telemetry = telemetryCommunicationService.Get(id);

        return telemetry;
    }
}

