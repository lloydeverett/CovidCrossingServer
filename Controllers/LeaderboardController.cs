using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CovidCrossingServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaderboardController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody] Record record, [FromServices] LeaderboardContext db)
        {
            if (record.Nickname == null || record.Nickname == "") { return BadRequest("Invalid nickname"); }
            if (record.Guid == null) { return BadRequest("Invalid GUID"); }
            var found = db.Records.SingleOrDefault(r => r.Guid == record.Guid);
            if (found != default)
            {
                found.Nickname = record.Nickname;
                found.HighScore = record.HighScore;
            }
            else
            {
                db.Records.Add(record);
            }
            db.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public IEnumerable<Record> Get([FromServices] LeaderboardContext db)
        {
            return db.Records.OrderByDescending(r => r.HighScore);
        }
    }
}
