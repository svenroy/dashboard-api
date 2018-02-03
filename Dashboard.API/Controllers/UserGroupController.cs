using System.Threading.Tasks;
using Dashboard.API.Models;
using Microsoft.AspNetCore.Mvc;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Diagnostics;

namespace Dashboard.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserGroupController : Controller
    {
        readonly IAmazonCognitoIdentityProvider _identityProvider;

        public UserGroupController(IAmazonCognitoIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        [HttpPost]
        public async Task Post([FromBody]UserGroup value)
        {
            var request = new AdminAddUserToGroupRequest
            {
                GroupName = value.GroupName,
                Username = value.Username,
                UserPoolId = value.UserPoolId
            };

            await _identityProvider.AdminAddUserToGroupAsync(request);
        }
    }
}