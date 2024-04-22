using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;
using GAS.Models;
using GAS.Attributes;
using System.Runtime.InteropServices.ComTypes;

namespace GAS.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Project")]
    public class ProjectController : ApiController
    {
        // Get List of all projects

        [APIAuthorizeAttribute]
        public IEnumerable<Project> GetAll()
        {
            try
            {
                var ctx = new XPenEntities();
                var projectData = (from o in ctx.Projects
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
        public IEnumerable<ProjectDTO> GetProject(int OrgID, int ProjectID)
        {
            try
            {
                var ctx = new XPenEntities();

                var status = ctx.ProjectStatus.AsEnumerable()
                    .OrderByDescending(x => x.UpdateDate).GroupBy(x => x.ProjectID).Select(x => x.First()).ToList();
                var project = (from prj in ctx.Projects
                                   where prj.OrgID == OrgID && prj.ProjectID == ProjectID
                                   select prj).ToList();
                var projectData = (from p in project
                             join s in status.DefaultIfEmpty()
                             on p.ProjectID equals s.ProjectID
                             select new ProjectDTO {
                                 ProjectName = p.ProjectName,
                                 ProjectNumber = p.ProjectNumber,
                                 ClientName = p.ClientName,
                                 CreatedBy = p.CreatedBy,
                                 ProjectValue = p.ProjectValue,
                                 Status = s.Status,
                                 WorkCompletion = (int)s.WorkCompletion
                             }).ToList();
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
        public IEnumerable<ProjectDTO> GetByOrg(int OrgID, String status)
        {
            try
            {
                //DateTime lastSyncTime = DateTime.Parse(synctime);
                var ctx = new XPenEntities();
                var statusData = ctx.ProjectStatus.AsEnumerable()
                    .OrderByDescending(x => x.UpdateDate).GroupBy(x => x.ProjectID).Select(x => x.First()).ToList();
                var salesData = ctx.SalesInvoices.AsEnumerable()
                    .GroupBy(x => x.ProjectId).Select(x => new { ProjectID = x.Key, Received =x.Sum(v => v.ServiceCost) }).ToList();

                var purchaseData = ctx.PurchaseInvoices.AsEnumerable()
                    .GroupBy(x => x.ProjectId).Select(x => new { ProjectID = x.Key, Paid = x.Sum(v => v.ServiceCost) }).ToList();

                var orderData = (from o in ctx.Projects
                                 where o.OrgID == OrgID
                                 select o).ToList();

                if (status == "Open")
                {
                    String[] ints1 = new String[5];
                    ints1[0] = "Ongoing";

                    orderData = (from o in ctx.Projects
                                     where o.OrgID == OrgID && ints1.Contains(o.Status)
                                     select o).ToList();

                }
                else if (status == "Closed")
                {
                    String[] ints1 = new String[5];
                    ints1[0] = "Completed";
                    ints1[0] = "OnHold";

                    orderData = (from o in ctx.Projects
                                     where o.OrgID == OrgID && ints1.Contains(o.Status)
                                     select o).ToList();

                }
                else
                {
  
                    orderData = (from o in ctx.Projects
                                     where o.OrgID == OrgID
                                     select o).ToList();
                }
                var projectData = (from p in orderData
                                  join s in statusData
                                  on p.ProjectID equals s.ProjectID
                                  join si in salesData
                                  on p.ProjectID equals si.ProjectID
                                  into salesGroup
                                  from sales in salesGroup.DefaultIfEmpty(new {ProjectID = p.ProjectID , Received= 0.00})
                                  join purchase in purchaseData
                                  on p.ProjectID equals purchase.ProjectID
                                  into purchaseGroup
                                  from purchase in purchaseGroup.DefaultIfEmpty(new { ProjectID = p.ProjectID, Paid = 0.00 })
                                  select new ProjectDTO
                                  {
                                      ProjectID = p.ProjectID,
                                      ProjectName = p.ProjectName,
                                      ProjectNumber = p.ProjectNumber,
                                      ClientName = p.ClientName,
                                      CreatedBy = p.CreatedBy,
                                      ProjectValue = p.ProjectValue,
                                      Status = s.Status,
                                      WorkCompletion = (int)s.WorkCompletion,
                                      Received = sales.Received,
                                      Spent = purchase.Paid
                                  }).ToList();

                return projectData;

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
        public IEnumerable<Project> GetByManager(int OrgID, int id, String status)
        {
            try
            {
                //DateTime lastSyncTime = DateTime.Parse(synctime);
              
                if (status == "Open")
                {
                    String[] ints1 = new String[5];
                    ints1[0] = "Ongoing";
                    var ctx = new XPenEntities();
                    var orderData = (from o in ctx.Projects
                                     where o.OrgID == OrgID && o.CreatedBy == id && ints1.Contains(o.Status)
                                     select o);
                    return orderData;
                }
                else if (status == "Closed")
                {
                    String[] ints1 = new String[5];
                    ints1[0] = "Completed";
                    ints1[0] = "OnHold";
                    var ctx = new XPenEntities();
                    var orderData = (from o in ctx.Projects
                                     where o.OrgID == OrgID && o.CreatedBy == id && ints1.Contains(o.Status)
                                     select o);
                    return orderData;

                }
                else
                {
                    var ctx = new XPenEntities();
                    var orderData = (from o in ctx.Projects
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
                var ctx = new XPenEntities();

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
                var ctx = new XPenEntities();

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
