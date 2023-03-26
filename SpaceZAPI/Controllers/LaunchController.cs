using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;


namespace SpaceZAPI.Controllers
{
    public class LaunchController : Controller
    {
        [HttpPost]
        [Route("LaunchSpaceCraft/{spacecraftId}")]
        public async Task<IActionResult> LaunchSpaceCraft(int spacecraftId, [FromForm] IFormFile file)
        {
            string lvName = null;
            int? lvOrbit = null;
            string plInfo = null;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var content = await reader.ReadToEndAsync();
                var lines = content.Split('\n');

                foreach (var line in lines)
                {
                    if (line.StartsWith("lvName:"))
                    {
                        lvName = line.Substring(7).Trim();
                    }
                    else if (line.StartsWith("lvOrbit:"))
                    {
                        int orbit;
                        if (int.TryParse(line.Substring(8).Trim(), out orbit))
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
            string plType = null;

            var payloadLines = payloadContent.Split('\n');
            foreach (var line in payloadLines)
            {
                if (line.StartsWith("plName:"))
                {
                    plName = line.Substring(7).Trim();
                }
                else if (line.StartsWith("plType:"))
                {
                    plType = line.Substring(7).Trim();
                }
            }



            // perform other actions here...

            return Ok();
        }

    }
}