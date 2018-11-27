using Backend.Models;
using Newtonsoft.Json;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Backend.Controllers
{
    [RoutePrefix("api/Users")]
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        public IHttpActionResult /*HttpResponseMessage*/ Post(SignupBindingModel model)
        {
            bool isValid = true;
            ErrorsSignupBindingModel errors = new ErrorsSignupBindingModel();
            if (string.IsNullOrEmpty(model.UserName))
            {
                errors.username = "This field is required";
                isValid = false;
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                errors.email = "This field is required";
                isValid = false;
            }
            try
            {
                MailAddress emailAddress = new MailAddress(model.Email);
            }
            catch
            {
                errors.email = "Email is invalid";
                isValid = false;
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                errors.password = "This field is required";
                isValid = false;
            }
            if (!model.Password.Equals(model.PasswordConfirmation))
            {
                errors.passwordConfirmation = "Passwords must match";
                isValid = false;
            }
            if (string.IsNullOrEmpty(model.Timezone))
            {
                errors.timezone = "This field is required";
                isValid = false;
            }
            SignupResultModel result = new SignupResultModel
            {
                errors = errors,
                isValid = isValid
            };
            if (!isValid)
                return Content(HttpStatusCode.BadRequest, result);
            //return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            ICryptoService cryptoService = new PBKDF2();
            //save this salt to the database
            string salt = cryptoService.GenerateSalt();
            //save this hash to the database
            string hashedPassword = cryptoService.Compute(model.Password);

            SignupUser signup = new SignupUser
            {
                Email = model.Email,
                Password = hashedPassword,
                PasswordSalt = salt,
                Timezone = model.Timezone,
                UserName = model.UserName
            };
            try
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    context.SignupUser.Add(signup);
                    context.SaveChanges();
                    return Content(HttpStatusCode.OK, new { success = true });
                }
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { error = ex });
            }

            
            //return Request.CreateResponse(HttpStatusCode.OK, new { success=true });
        }

        [Route("{identifier}")]
        public IHttpActionResult Get(string identifier)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var user = context.SignupUser.SingleOrDefault(u=>u.Email==identifier || u.UserName==identifier);
                return Content(HttpStatusCode.OK, new { user=user });
            }
        }
    }
}
