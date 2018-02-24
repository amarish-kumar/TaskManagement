using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManagement.Core.Entities;
using TaskManagement.Repository;
using TaskManagment.Web.Models;

namespace TaskManagment.Web.Controllers
{
    [RoutePrefix("api/Auth")]
    public class AuthController : ApiController
    {
        public ICurrentUserRepository CurrentUserRepository { get; }

        public AuthController(ICurrentUserRepository currentUserRepository)
        {
            CurrentUserRepository = currentUserRepository;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(LoginViewModel loginInfo)
        {
            try
            {
                var userManager = Request.GetOwinContext().Get<UserManager<ApplicationUser>>();
                var signManager = Request.GetOwinContext().Get<ApplicationSignInManager>();
                var result = await signManager.PasswordSignInAsync(loginInfo.UserName, loginInfo.Password, true, false);
                // var tUser = await userManager.FindByNameAsync(loginInfo.Email);

                switch (result)
                {
                    case SignInStatus.Failure:
                        return Unauthorized();

                    case SignInStatus.Success:
                        return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("getmyinfo")]
        public IHttpActionResult GetMyInfo()
        {
            try
            {
                return Ok(new { UserId = CurrentUserRepository.GetCurrentUserId(),IsAdmin = CurrentUserRepository.IsAdmin() , UserName = CurrentUserRepository.GetCurrentUserName() });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

    }
}
