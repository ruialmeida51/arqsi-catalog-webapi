using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Server.Repository.Wrapper;
using Server.Utils;

namespace Server.DataAnnotation
{
    public class  RequireUserRoleFilter : TypeFilterAttribute
    {
        // We need to use a type filter to be able to receive parameters and 
        // add a private constructor to the class, which then allows us to use DI
        public RequireUserRoleFilter(string userID, UserRole[] userRoles) : base(typeof(RequireUserRoleFilterImplementation))
        {
            //These arguments will be passed down to the private class below.
            Arguments = new object[] {userID, userRoles};
        }

        private class RequireUserRoleFilterImplementation : IActionFilter
        {
            // User ID received from the route.
            private string UserId { get; set; }

            // Dependency injection of the repository wrapper.
            private IRepositoryWrapper RepositoryWrapper { get; set; }
            
            // User role to allow
            private UserRole[] UserRoles { get; set; }

            // Constructor with DI that receives arguments from the "Arguments" array defined in the constructor above.
            public RequireUserRoleFilterImplementation(IRepositoryWrapper repositoryWrapper, string userId, UserRole[] userRoles)
            {
                RepositoryWrapper = repositoryWrapper;
                UserId = userId;
                UserRoles = userRoles;
            }

            // Before the execution of the Action..
            public void OnActionExecuting(ActionExecutingContext filterContext)
            {
                // If we can't find the key received from the route in the controller, return a BadRequest.
                if (!filterContext.ActionArguments.ContainsKey(UserId)) { filterContext.Result = new BadRequestObjectResult(new {status = "Failure", message = "No ID for user provided."}); return; }

                // Get the id from the arguments as a long. Return bad request if the ID is not readable.
                if (!(filterContext.ActionArguments[UserId] is long id)) { filterContext.Result = new BadRequestObjectResult(new {status = "Failure", message = "Invalid ID provided for the user."}); return; }

                // Grab the user from the repository. (.Result means it will execute synchronously.
                
                var user = RepositoryWrapper.User.GetDetachedUserById(id).Result;

                // If we can't find the user return a NotFound.
                if (user == null) { filterContext.Result = new NotFoundObjectResult(new {status = "Failure", message = "Could not find user with given ID."}); return; }

                // If the user has permission to view the content, proceed with the request.
                //if (user.Role == UserRole) { return; }

                if (UserRoles.Any(role => user.Role == role))
                {
                    return;
                }

                // If we reach this far it means the user does not have the necessary roles to view the content,
                // so we send an appropriate message informing of the case.
                filterContext.Result = new UnauthorizedObjectResult(new {status = "Failure", message = "User does not have permission to view this content."});
            }

            // Type filter attribute needs this method to be implemented, no code is needed here.
            public void OnActionExecuted(ActionExecutedContext context)
            {
                Console.WriteLine("Finished user role check.");
            }
        }
    }
}