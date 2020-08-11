using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitSqlLib;
using TaskFront1.Entity;

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
            string str = "'" + string.Join("','", jh.ToArray()) + "'";
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
            string sql = "select * from ts_j_basicinfo where BZJH in (" + str + ") ";
            DataTable dt = access.GetDataTable(sql);
            si.data = dt;
            return si;
        }

        [HttpPost("testpost")]
        public StateInfo testpost([FromForm] PageHelper ph)
        {
            StateInfo si = new StateInfo();
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
            string sql = "select * from ( " +
            "select row_limit.*, rownum rownum_ from(" +
            "select count_num.*, count(1)over() totalnum_ from(" +
            "select * from ts_j_basicinfo  order by bzjh, tcrq" +
            ") count_num" +
            ") row_limit where rownum <= " + ph.page * ph.limit + "" +
            ")where rownum_ > " + ph.page + "";
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
