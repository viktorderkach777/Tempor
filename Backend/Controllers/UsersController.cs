using Backend.Models;
using Newtonsoft.Json;
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
            else
                return Content(HttpStatusCode.OK, new { success = true });
            //return Request.CreateResponse(HttpStatusCode.OK, new { success=true });
        }
    }
}
