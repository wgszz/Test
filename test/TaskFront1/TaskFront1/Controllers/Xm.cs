using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitSqlLib;
using TaskFront1.statusInfo;

namespace TaskFront1.Controllers
{
    [Microsoft.AspNetCore.Cors.EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class XmController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpGet("testget")]
        public StateInfo testget([FromQuery] List<string> jh)
        {
            StateInfo si = new StateInfo();
            string strr = "'" + string.Join("','", jh.ToArray()) + "'";
            RabbitSqlLib.DBEntity dBEntity = new DBEntity();
            dBEntity.DBType = "oracle";
            dBEntity.DBPort = "1521";
            dBEntity.DBServer = "132.232.16.136";
            dBEntity.DBName = "orcl";
            dBEntity.DBUser = "dqts";
            dBEntity.DBPwd = "dqts";
            string dBType = "";
            string connstr = dBEntity.GetConnStr(out dBType);
            RabbitAccess access = new RabbitAccess(dBType, connstr);
            string sql = "select * from ts_j_basicinfo where BZJH in (" + strr + ") ";
            DataTable dt = access.GetDataTable(sql);
            si.data = dt;
            return si;
        }

        [HttpPost("testpost")]
        public StateInfo testpost([FromForm] List<string> jh)
        {
            StateInfo si = new StateInfo();
            string strr = "'" + string.Join("','", jh.ToArray()) + "'";
            RabbitSqlLib.DBEntity dBEntity = new DBEntity();
            dBEntity.DBType = "oracle";
            dBEntity.DBPort = "1521";
            dBEntity.DBServer = "132.232.16.136";
            dBEntity.DBName = "orcl";
            dBEntity.DBUser = "dqts";
            dBEntity.DBPwd = "dqts";
            string dBType = "";
            string connstr = dBEntity.GetConnStr(out dBType);
            RabbitAccess access = new RabbitAccess(dBType, connstr);
            string sql = "select * from ts_j_basicinfo where BZJH in (" + strr + ") ";
            DataTable dt = access.GetDataTable(sql);
            si.data = dt;
            return si;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
