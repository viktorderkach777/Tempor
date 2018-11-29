using Backend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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

            var request = HttpContext.Current.Request;
            var url = request.Url.GetLeftPart(UriPartial.Authority) + 
                request.ApplicationPath + "/Token";
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("username", model.Identifier);
            p.Add("password", model.Password);
            p.Add("grant_type", "password");
            string token = "HelloToken";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync(url,
                    new FormUrlEncodedContent(p)).Result;
                var json = response.Content.ReadAsStringAsync().Result;
                TokenModel Token = JsonConvert.DeserializeObject<TokenModel>(json);
                //JsonConvert.DeserializeAnonymousType(json,
                //    new { access_token = "", token_type = "" });
                //MessageBox.Show(json);
                //MessageBox.Show(tokenModel.access_token);
                //this.DialogResult = true;
                //this.Close();
                token = Token.access_token;
            }
            
            return Content(HttpStatusCode.OK, new { token });
        }
    }
}
