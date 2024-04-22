using GAS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using GAPResources;

namespace GAS.Attributes
{
    public class ClosingProjectFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            bool isValidAction = true;
            foreach (var paramValue in actionContext.ActionArguments.Values)
            {
                if (paramValue is GAS.ProjectStatu)
                {
                    //validate project if request is to close it.
                    var stats = paramValue as ProjectStatu;
                   int projID = stats.ProjectID;
                    using(var ctx = new XPenEntities() )
                    {
                        var activities = ctx.NewViewActivities.Where(c => c.ProjectID == projID).ToList();
                        //if (activities != null && activities.All(a => a.ActivityStatus.ToLower() != "initiated" && a.ActivityStatus.ToLower() != "added" && (a.ActivityStatus.ToLower() == "approved" && a.Settlement!=0)))
                        //{
                        //    isValidAction = true;
                           
                        //}

                        if ((activities != null && activities.Count() != 0) && activities.All(a => a.ActivityStatus.ToLower() == "initiated" || a.ActivityStatus.ToLower() == "added" && (a.ActivityStatus.ToLower() == "approved" && a.Settlement== 0)))
                        {
                            isValidAction = false;

                        }

                    }

                    break;
                  
                }
            }
            if (!isValidAction)
            {
                var errorMsg = GAPResources.GAResourceManager.GetResourceString("ProjectClosureError_ActiveActivity");
                throw new InvalidOperationException(errorMsg);
            }
            base.OnActionExecuting(actionContext);
        }
    }
}