using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SpaceZAPI.Models;

namespace SpaceZAPI.Controllers;

[ApiController]
public class MissonControlController : ControllerBase
{
    private readonly IMongoCollection<SpaceCraft> _spaceCrafts;

    public MissonControlController(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("yourDatabaseName");
        _spaceCrafts = database.GetCollection<SpaceCraft>("SpaceCrafts");
    }

    private readonly ILogger<MissonControlController> _logger;

    public MissonControlController(ILogger<MissonControlController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("GetSpaceCrafts")]
    public IEnumerable<object> Get()
    {
        var spaceCrafts = new List<object>();
        foreach (var spaceCraft in _spaceCrafts.FindSync<SpaceCraft>(Builders<SpaceCraft>.Filter.Empty).ToList())
        {
            spaceCrafts.Add(new { spaceCraft.spaceCraft_ID, spaceCraft.name, spaceCraft.state });
        }
        return spaceCrafts;
    }

    [HttpGet]
    [Route("NewSpaceCraft")]
    public IActionResult GenerateGuid([FromQuery] string name)
    {
        // Generate a new GUID based on the name parameter
        Guid guid = Guid.NewGuid();

        return Ok(guid);
    }
}
