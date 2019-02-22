using Amazonweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amazonweb.Controllers
{
    public class HeaderPartialController : Controller
    {
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        // GET: HeaderPartial
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult HeaderValue()
        {
            HeaderPartialModel objModel = new HeaderPartialModel();
            UserModel MdUser  = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            objModel.UserName = MdUser.UserName;
            objModel.UserId = MdUser.Id;
            return PartialView("~/Views/Shared/PartialView/_HeaderPartial.cshtml", objModel);
        }
    }
}