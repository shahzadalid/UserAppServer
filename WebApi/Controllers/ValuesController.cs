using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        user user = new user();
        Classes.DatabaseOperation op = new Classes.DatabaseOperation();
        public List<user> Get()
        {
            return op.getUserDetails();
        }

        // GET api/values/5
        public List<user> Get(int id)
        {
            return op.getUserDetails();
        }

        // POST api/values
        public user Post([FromBody] user u)
        {
            int result= op.saveUser(u);
            if (result>0)
            {
                u.id = 1;
                return u;
            }
            else
            {
                return new user();
            }
            
        }

        // PUT api/values/5
        public user Put([FromBody] user u)
        {

            int result = op.editUser(u);
            if (result > 0)
            {
                u.id = 1;
                return u;
            }
            else
            {
                return new user();
            }
            
        }

        // DELETE api/values/5
        public int Delete(int id)
        {
            int result=op.deleteUser(id);
            if (result > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
