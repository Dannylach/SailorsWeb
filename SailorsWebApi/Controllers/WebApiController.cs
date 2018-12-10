using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SailorsWebApi.Models;

namespace SailorsWebApi.Controllers
{
    public class WebApiController : ApiController
    {
        // GET: api/WebApi
        [Route("api/webapi/getall")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var db = new HowDatabaseEntities();
                var users = db.Users.Select(x => x.userName.Replace(" ", "")).ToList();

                var response = new ResponseWrapper<IEnumerable<string>>(users, true);
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new ResponseWrapper<string>(e.Message, false);
                return Content(HttpStatusCode.BadRequest, response);
            }
        }

        [HttpGet]
        [Route("api/webapi/get")]
        public IHttpActionResult Get(int id)
        {
            return Ok();
        }

        // POST: api/WebApi
        [HttpGet]
        [Route("api/webapi/getAllFrom")]
        public IHttpActionResult getAllFrom(string name)
        {
            try
            {
                var db = new HowDatabaseEntities();
                var users = db.Users.Where(x => x.userName == name).Select(x => x).ToList();
                //change to string before sending
                var response = new ResponseWrapper<List<Users>>(users, true);
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new ResponseWrapper<string>(e.Message, false);
                return Content(HttpStatusCode.BadRequest, response);
            }
        }

        // PUT: api/WebApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WebApi/5
        public void Delete(int id)
        {
        }
    }
}
