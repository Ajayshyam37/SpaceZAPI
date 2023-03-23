using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SpaceZAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MissonControlController : ControllerBase
{
    private readonly List<SpaceCraft> _spaceCrafts = new List<SpaceCraft>
     {
            new SpaceCraft { spaceCraft_ID = 1, name = "Falcon 9", state = State.Active },
            new SpaceCraft { spaceCraft_ID = 2, name = "Dragon Crew", state = State.Active },
            new SpaceCraft { spaceCraft_ID = 3, name = "Starship", state = State.DeOrbited },
            new SpaceCraft { spaceCraft_ID = 4, name = "Saturn V", state = State.Waiting },
            new SpaceCraft { spaceCraft_ID = 5, name = "Vostok 1", state = State.Waiting }
     };

    private readonly ILogger<MissonControlController> _logger;

    public MissonControlController(ILogger<MissonControlController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<object> Get()
    {
        var spaceCrafts = new List<object>();
        foreach (var spaceCraft in _spaceCrafts)
        {
            spaceCrafts.Add(new { spaceCraft.spaceCraft_ID, spaceCraft.name, spaceCraft.state });
        }
        return spaceCrafts;
    }
}

