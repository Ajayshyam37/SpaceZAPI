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
    /// <summary>
    /// Retrieves a list of space crafts.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<List<SpaceCraft>> Get()
    {
        return spaceCraftService.Get();
    }

    /// <summary>
    /// Retrieves a single SpaceCraft object by its unique identifier (id) from the server using the SpaceCraftService.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    [HttpGet]
    [Route("GetSpaceCraftById")]
    public ActionResult<SpaceCraft> GetSpaceCraftById([FromQuery] string id)
    {
        var spacecraft = spaceCraftService.Get(id);

        if (spacecraft == null)
        {
            return NotFound($"SpaceCraft {id} not found");
        }
        return spacecraft;
    }

    /// <summary>
    /// Creates a new SpaceCraft object with various properties such as name and state.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("NewSpaceCraft")]
    public ActionResult<SpaceCraft> NewSpaceCraft()
    {
        long spacecraftscount = spaceCraftService.GetCount();
        var newSpaceCraft = new SpaceCraft
        {
            spaceCraft_ID = ObjectId.GenerateNewId().ToString(),
            name = "LV " + (spacecraftscount + 1),
            state = State.Waiting,
            spaceCraftTelemetery = false,
            payloadname = String.Empty,
            payloadid = ObjectId.GenerateNewId().ToString(),
            orbitRadius = 0,
            totalTimeToOrbit = 0,
            createdtime = DateTime.UtcNow

        };

        spaceCraftService.Create(newSpaceCraft);

        return CreatedAtAction(nameof(Get), new { id = newSpaceCraft.spaceCraft_ID }, newSpaceCraft);
    }

    /// <summary>
    /// Updates the state of a SpaceCraft to DeOrbited by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

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
        spaceCraftService.Update(id, oldSpaceCraft, false);
        return NoContent();
    }

    /// <summary>
    /// Updates an exisiting SpaceCraft
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>

    [HttpPut]
    public ActionResult Put(string id, string name)
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

    /// <summary>
    /// Deletes a SpaceCraft
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        var oldSpaceCraft = spaceCraftService.Get(id);
        if (oldSpaceCraft == null)
        {
            return NotFound($"SpaceCraft {id} not found");
        }
        spaceCraftService.Remove(id.Trim());

        return Ok($"SpaceCraft {oldSpaceCraft.name} deleted");
    }

    /// <summary>
    /// Updates the telemetry state of a space craft identified by an id parameter.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="state"></param>
    /// <returns></returns>

    [HttpPut]
    [Route("SpaceCraftTelemetry")]
    public SpaceCraft SpaceCraftTelemetry([FromQuery] string id, [FromQuery] bool state)
    {
        var oldSpaceCraft = spaceCraftService.Get(id);
        if (oldSpaceCraft == null)
        {
            return new SpaceCraft();
        }
        spaceCraftService.Update(id, oldSpaceCraft, state);
        oldSpaceCraft = spaceCraftService.Get(id);
        return oldSpaceCraft;
    }

}
