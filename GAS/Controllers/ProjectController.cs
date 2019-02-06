using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;
using GAS.Models;
using GAS.Attributes;

namespace GAS.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Project")]
    public class ProjectController : ApiController
    {
        // Get List of all projects

        [APIAuthorizeAttribute]
        public IEnumerable<ViewProject> GetAll()
        {
            try
            {
                var ctx = new GASEntities();
                var projectData = (from o in ctx.ViewProjects
                                 select o).Take(20);
                return projectData;
            }
            catch (Exception ex)
            {
                int a = 1;
                return null;
            }
        }

        // Get project details by project id
        [Route("Organization/{OrgID}/Project/{ProjectID}")]
        [HttpGet]
        public IEnumerable<ViewProject> GetProject(int OrgID, int ProjectID)
        {
            try
            {
                var ctx = new GASEntities();
                var projectData = (from prj in ctx.ViewProjects
                                   where prj.OrgID == OrgID && prj.ProjectID == ProjectID
                                   select prj);
                return projectData;
            }
            catch (Exception ex)
            {
                int a = 1;
                return null;
            }
        }


        // Get project in an organization by status
        [Route("Organization/{OrgID}/Status/{status}")]
        [HttpGet]
        [APIAuthorizeAttribute]
        public IEnumerable<ViewProject> GetByOrg(int OrgID, String status)
        {
            try
            {
               
                //DateTime lastSyncTime = DateTime.Parse(synctime);
                if (status == "Open")
                {
                    String[] ints1 = new String[5];
                    ints1[0] = "Ongoing";
                    var ctx = new GASEntities();
                    var orderData = (from o in ctx.ViewProjects
                                     where o.OrgID == OrgID && ints1.Contains(o.Status)
                                     select o);
                    return orderData;
                }
                else if (status == "Closed")
                {
                    String[] ints1 = new String[5];
                    ints1[0] = "Completed";
                    ints1[0] = "OnHold";
                    var ctx = new GASEntities();
                    var orderData = (from o in ctx.ViewProjects
                                     where o.OrgID == OrgID && ints1.Contains(o.Status)
                                     select o);
                    return orderData;
                
                }
                else
                {
                    var ctx = new GASEntities();
                    var orderData = (from o in ctx.ViewProjects
                                     where o.OrgID == OrgID 
                                     select o);
                    return orderData;
                }

                
            }
            catch (Exception ex)
            {
                int a = 1;
                return null;
            }
        }


        // Get project in an organization by status

        [Route("Organization/{OrgID}/Manager/{id}/Status/{status}")]
        [HttpGet]
        [APIAuthorizeAttribute]
        public IEnumerable<ViewProject> GetByManager(int OrgID, int id, String status)
        {
            try
            {
                //DateTime lastSyncTime = DateTime.Parse(synctime);
              
                if (status == "Open")
                {
                    String[] ints1 = new String[5];
                    ints1[0] = "Ongoing";
                    var ctx = new GASEntities();
                    var orderData = (from o in ctx.ViewProjects
                                     where o.OrgID == OrgID && o.CreatedBy == id && ints1.Contains(o.Status)
                                     select o);
                    return orderData;
                }
                else if (status == "Closed")
                {
                    String[] ints1 = new String[5];
                    ints1[0] = "Completed";
                    ints1[0] = "OnHold";
                    var ctx = new GASEntities();
                    var orderData = (from o in ctx.ViewProjects
                                     where o.OrgID == OrgID && o.CreatedBy == id && ints1.Contains(o.Status)
                                     select o);
                    return orderData;

                }
                else
                {
                    var ctx = new GASEntities();
                    var orderData = (from o in ctx.ViewProjects
                                     where o.OrgID == OrgID && o.CreatedBy == id
                                     select o);
                    return orderData;
                }



            }
            catch (Exception ex)
            {
                int a = 1;
                return null;
            }
        }


        // Add new Project
        [Route("New")]
        [HttpPost]
       public HttpResponseMessage PostNew([FromBody]Project prj)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            var response = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                var ctx = new GASEntities();

                prj.ProjectCreationDate = DateTime.UtcNow;
                

                if (prj != null)
                {
                    ctx.Projects.Add(prj);
                    ctx.SaveChanges();
                    ctx.ProjectStatus.Add(
                        new ProjectStatu
                        {
                            ProjectID = prj.ProjectID,
                            Remarks = "Initiated",
                            UpdateDate = DateTime.UtcNow,
                            WorkCompletion = 0,
                            Status= "Ongoing"
                        });

                    ctx.SaveChanges();
                          resp = "{\"Response\":\"OK\"}";
                }
            }
            catch (Exception ex)
            {
                int a = 1;
                resp = "{\"Response\":\"Fail\"}";
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
               
            }

            // var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;
        }

        // Update Project Status
        [Route("Update")]
        [HttpPost]
        [ClosingProjectFilter]
    
        public HttpResponseMessage PostUpdate([FromBody]ProjectStatu prjStatus)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            try
            {
                var ctx = new GASEntities();

                prjStatus.UpdateDate = DateTime.UtcNow;

                if (prjStatus != null)
                {
                    ctx.ProjectStatus.Add(prjStatus);
                    ctx.SaveChanges();
                    resp = "{\"Response\":\"OK\"}";
                }
            }
            catch (Exception ex)
            {
                int a = 1;
                resp = "{\"Response\":\"Fail\"}";
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;
        }



        // PUT: api/Project/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Project/5
        public void Delete(int id)
        {
        }
    }
}
