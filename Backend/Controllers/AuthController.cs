using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Backend.Controllers
{
    [RoutePrefix("api/Auth")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public IHttpActionResult Login(LoginBindingModel model)
        {
            //bool isValid = true;
            //ErrorsLoginModel errors = new ErrorsLoginModel();
            //if (string.IsNullOrEmpty(model.Identifier))
            //{
            //    errors.identifier = "This field is required";
            //    isValid = false;
            //}
            //if (string.IsNullOrEmpty(model.Password))
            //{
            //    errors.password = "This field is required";
            //    isValid = false;
            //}

            var user = _context.SignupUser.SingleOrDefault(us =>
                  us.UserName == model.Identifier || 
                  us.Email == model.Identifier);

            if (user == null)
                return Content(HttpStatusCode.Unauthorized, new { errors = new { form="Invalid Credentials" } });
            string token = "HelloToken";
            return Content(HttpStatusCode.OK, new { token });
        }
    }
}
