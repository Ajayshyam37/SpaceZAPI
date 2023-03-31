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
public class PayLoadController : ControllerBase
{
    private readonly IPayLoadService payLoadService;

    public PayLoadController(IPayLoadService payLoadService)
    {
        this.payLoadService = payLoadService;
    }
    /// <summary>
    /// Retrieves a list of PayLoads.
    /// </summary>
    /// <returns></returns>

    [HttpGet]
    public ActionResult<List<PayLoad>> Get()
    {
        return payLoadService.Get();
    }

    /// <summary>
    /// Retrieves a list of PayLoad based on the Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetPayLoadById")]
    public ActionResult<PayLoad> Get([FromQuery] string id)
    {
        var payLoad = payLoadService.Get(id);

        if (payLoad == null)
        {
            return NotFound($"PayLoad {id} not found");
        }
        return payLoad;
    }

    /// <summary>
    /// Creates a new PayLoad with its name, type and id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="payloadType"></param>
    /// <returns></returns>

    [HttpPost]
    [Route("NewPayLoad")]
    public ActionResult<PayLoad> NewPayLoad(string id, string name, PayloadType payloadType)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest("Spacecraft name cannot be empty");
        }

        var newPayLoad = new PayLoad
        {
            payloadid = id,
            payloadname = name,
            payloadstate = State.Waiting,
            payLoadType = payloadType,
            payLoadData = false,
        };

        payLoadService.Create(newPayLoad);

        return CreatedAtAction(nameof(Get), new { id = newPayLoad.payloadid }, newPayLoad);
    }

    /// <summary>
    /// Updates the PayLoad
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>

    [HttpPut]
    public ActionResult Put(string id, string name)
    {
        var oldPayLoad = payLoadService.Get(id);
        if (oldPayLoad == null)
        {
            return NotFound($"PayLoad not found");
        }
        oldPayLoad.payloadname = name;
        payLoadService.UpdatePayLoad(id, oldPayLoad);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        var oldPayLoad = payLoadService.Get(id);
        if (oldPayLoad == null)
        {
            return NotFound($"PayLoad {id} not found");
        }
        payLoadService.Remove(oldPayLoad.payloadid);

        return Ok($"PayLoad {oldPayLoad.payloadname} deleted");
    }
    /// <summary>
    /// Updates the PayLoad PayLoadData parameter
    /// </summary>
    /// <param name="id"></param>
    /// <param name="data"></param>
    /// <returns></returns>

    [HttpPut]
    [Route("UpdatePayLoadData")]
    public ActionResult UpdatePayLoadData(string id, bool data)
    {
        var oldPayLoad = payLoadService.Get(id);
        if (oldPayLoad == null)
        {
            return NotFound($"PayLoad not found");
        }
        oldPayLoad.payLoadData = data;
        payLoadService.UpdatePayLoad(id, oldPayLoad);
        return NoContent();
    }

    /// <summary>
    /// Updates the state of a PayLoad to DeOrbited
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("DeOrbitPayLoad")]
    public ActionResult DeOrbitPayLoad([FromQuery]string id)
    {
        var oldPayLoad = payLoadService.Get(id);
        if (oldPayLoad == null)
        {
            return NotFound($"PayLoad not found");
        }
        oldPayLoad.payloadstate = State.DeOrbited;
        payLoadService.UpdatePayLoad(id, oldPayLoad);
        return NoContent();
    }

    /// <summary>
    /// Updates a PayLoad from Waiting state to Active
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("LaunchPayLoad")]
    public ActionResult LaunchPayLoad([FromQuery] string id)
    {
        var oldPayLoad = payLoadService.Get(id);
        if (oldPayLoad == null)
        {
            return NotFound($"PayLoad not found");
        }
        oldPayLoad.payloadstate = State.Active;
        payLoadService.UpdatePayLoad(id, oldPayLoad);
        return NoContent();
    }


}
