using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using SpaceZAPI.Services;
using SpaceZAPI.Models;
using MongoDB.Bson;
using System.Xml.Linq;
using SpaceZAPI.Helper;

namespace SpaceZAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LaunchController : Controller
{
    private readonly ISpaceCraftService spaceCraftService;
    private readonly IPayLoadService payLoadService;


    public LaunchController(ISpaceCraftService spaceCraftService,IPayLoadService payLoadService)
    {
        this.spaceCraftService = spaceCraftService;
        this.payLoadService = payLoadService;
    }
    /// <summary>
    /// Initiates the Launch Sequence, updates the spacecraft state from Waiting to Active and sets its propertys and creates a ppaylooad.
    /// </summary>
    /// <param name="spacecraftId"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Launch")]
    public async Task<IActionResult> LaunchSpaceCraft([FromQuery] string spacecraftId, [FromForm] IFormFile file)
    {
        string lvName = null;
        double lvOrbit = 0;
        string plInfo = null;

        var Cal = new Calculate();
        var oldSpaceCraft = spaceCraftService.Get(spacecraftId);
        if (oldSpaceCraft == null)
        {
            return NotFound($"SpaceCraft not found");
        }

        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            var content = await reader.ReadToEndAsync();
            var lines = content.Split('\n');

            foreach (var line in lines)
            {
                if (line.StartsWith("lvName:"))
                {
                    lvName = line.Substring(7).Trim().Replace("\"", "");
                }
                else if (line.StartsWith("lvOrbit:"))
                {
                    double orbit;
                    if (double.TryParse(line.Substring(8).Trim(), out orbit))
                    {
                        lvOrbit = orbit;
                    }
                    else
                    {
                        return BadRequest("Invalid lvOrbit format");
                    }
                }
                else if (line.StartsWith("plInfo:"))
                {
                    plInfo = line.Substring(7).Trim();
                }
            }
        }
        var plInfoExists = System.IO.File.Exists(plInfo);

        if (!plInfoExists)
        {
            return BadRequest("Invalid plInfo file path");
        }

        var payloadContent = await System.IO.File.ReadAllTextAsync(plInfo);

        string plName = null;
        string plTypeString = null;

        var payloadLines = payloadContent.Split('\n');
        foreach (var line in payloadLines)
        {
            if (line.StartsWith("plName:"))
            {
                plName = line.Substring(7).Trim().Replace("\"", "");
            }
            else if (line.StartsWith("plType:"))
            {
                plTypeString = line.Substring(7).Trim().Replace("\"", "");
            }
        }

        if (lvName.Length == 0 || plName.Length == 0 || lvOrbit == 0)
        {
            return NotFound("Config file incomplete unable to parse required information");
        }

        PayloadType payloadType;
        if (!(Enum.TryParse(plTypeString, out payloadType)))
        {
            return NotFound("Invalid Payload Type");
        }

        oldSpaceCraft.state = State.Active;
        oldSpaceCraft.name = lvName;
        oldSpaceCraft.spaceCraftTelemetery = true;
        oldSpaceCraft.payloadname = plName;
        oldSpaceCraft.payloadid = ObjectId.GenerateNewId().ToString();
        oldSpaceCraft.orbitRadius = lvOrbit;
        oldSpaceCraft.launchtime = DateTime.UtcNow;
        oldSpaceCraft.totalTimeToOrbit = Cal.CalculateTimeToOrbit(lvOrbit,oldSpaceCraft.launchtime);
        spaceCraftService.UpdateSpaceCraft(spacecraftId, oldSpaceCraft);


        var payLoad = new PayLoad {
            payloadid = oldSpaceCraft.payloadid,
            payloadname = plName,
            payloadstate = State.Waiting,
            payLoadType = payloadType,
            payLoadData = false,
        };
        payLoadService.Create(payLoad);

        return Ok();
    }

}