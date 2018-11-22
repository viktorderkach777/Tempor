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
        public SignupResultModel Post(SignupBindingModel model)
        {
            ErrorsSignupBindingModel errors = new ErrorsSignupBindingModel();
            if(string.IsNullOrEmpty(model.UserName))
                errors.username = "This field is required";

            if (string.IsNullOrEmpty(model.Email))
                errors.email = "This field is required";
            try
            {
                MailAddress emailAddress = new MailAddress(model.Email);
            }
            catch
            {
                errors.email = "Email is invalid";
            }
            if (string.IsNullOrEmpty(model.Password))
                errors.password = "This field is required";
            if (!model.Password.Equals(model.PasswordConfirmation))
                errors.passwordConfirmation = "Passwords must match";
            if (string.IsNullOrEmpty(model.Timezone))
                errors.timezone = "This field is required";
            SignupResultModel result = new SignupResultModel
            {
                errors = errors,
                isValid = false
            };
            if (!ModelState.IsValid)
            {
                return result;
            }
            return result;
        }
    }
}
