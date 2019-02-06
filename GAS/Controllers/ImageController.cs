using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using GAS.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web.Http.Cors;

namespace GAS.Controllers
{
     [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImageController : ApiController
    {
        // GET: api/Image
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // Get Image by User ID
        public Image GetByUserID(int id)
        {
            try
            {
                var context = new GASEntities();
                var Image = (from res in context.UserImages
                             where (res.UserID == id)
                             select new Image() { UserID = res.UserID, ImageByte = res.Profile_image }).First();
                return Image;
            }
            catch (Exception ex)
            {
                return new Image() { UserID = id, ImageByte = new byte[0] };
            }
        }




        // Add User Image
        public HttpResponseMessage Post([FromBody]Image value)
        {
            String resp;
            try
            {

                using (var context = new GASEntities())
                {

                    List<UserImage> users = (from u in context.UserImages
                                                 where u.UserID == value.UserID
                                                 select u).ToList();
                    if (users.Count == 0)
                    {

                       
                        context.UserImages.Add(new UserImage
                        {
                            UserID = value.UserID,
                            Profile_image = Convert.FromBase64String(value.ImageString),

                        });

                    }
                    else
                    {
                       
                        foreach (UserImage user in users)
                        {
                            user.Profile_image = Convert.FromBase64String(value.ImageString);

                        }
                    }

                    context.SaveChanges();
                    resp = "{\"Response\":\"OK\"}";
                }

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
                return response;
            }

            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        Utility.log("api/Image Failed to Update : Property-" + validationError.PropertyName + "  Error- " + validationError.ErrorMessage + "  At " + DateTime.Now.ToString());


                    }
                }
                resp = "{\"Response\":\"Fail\"}";
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
                return response;
            }
        }

        // PUT: api/Image/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Image/5
        public void Delete(int id)
        {
        }
    }
}
