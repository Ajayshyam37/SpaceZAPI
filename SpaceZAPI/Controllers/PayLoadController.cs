using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SpaceZAPI.Helper;
using SpaceZAPI.Models;
using SpaceZAPI.Services;

namespace SpaceZAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpaceCraftsController : ControllerBase
{
    private readonly ISpaceCraftService spaceCraftService;

    public SpaceCraftsController(ISpaceCraftService spaceCraftService)
    {
        this.spaceCraftService = spaceCraftService;
    }

    [HttpGet]
    public ActionResult<List<SpaceCraft>> Get()
    {
        return spaceCraftService.Get();
    }

    [HttpGet("{id}")]
    public ActionResult<SpaceCraft> Get(string id)
    {
        var spacecraft = spaceCraftService.Get(id);

        if (spacecraft == null)
        {
            return NotFound($"SpaceCraft {id} not found");
        }
        return spacecraft;
    }

    [HttpPost]
    [Route("NewSpaceCraft")]
    public ActionResult<SpaceCraft> NewSpaceCraft(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest("Spacecraft name cannot be empty");
        }

        var newSpaceCraft = new SpaceCraft
        {
            spaceCraft_ID = ObjectId.GenerateNewId().ToString(),
            name = name,
            state = State.Waiting,
            spaceCraftTelemetery = false,
            payloadname = String.Empty,
            payloadid = ObjectId.GenerateNewId().ToString(),
            orbitRadius = 0,
            totalTimeToOrbit = 0
        };

        spaceCraftService.Create(newSpaceCraft);

        return CreatedAtAction(nameof(Get), new { id = newSpaceCraft.spaceCraft_ID }, newSpaceCraft);
    }

    [HttpPut]
    [Route("DeOrbit")]
    public ActionResult DeOrbit(string id)
    {
        var oldSpaceCraft = spaceCraftService.Get(id);
        if (oldSpaceCraft == null)
        {
            return NotFound($"SpaceCraft not found");
        }
        oldSpaceCraft.state = State.DeOrbited;
        spaceCraftService.Update(id, oldSpaceCraft);
        return NoContent();
    }

    [HttpPut]
    public ActionResult Put(string id,string name)
    {
        var oldSpaceCraft = spaceCraftService.Get(id);
        if (oldSpaceCraft == null)
        {
            return NotFound($"SpaceCraft not found");
        }
        oldSpaceCraft.payloadname = name;
        spaceCraftService.UpdateSpaceCraft(id, oldSpaceCraft);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        var oldSpaceCraft = spaceCraftService.Get(id);
        if (oldSpaceCraft == null)
        {
            return NotFound($"SpaceCraft {id} not found");
        }
        spaceCraftService.Remove(oldSpaceCraft.spaceCraft_ID);

        return Ok($"SpaceCraft {oldSpaceCraft.name} deleted");
    }

}
