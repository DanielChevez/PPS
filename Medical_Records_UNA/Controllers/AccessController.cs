using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Medical_Records_UNA.Extensions;
using Medical_Record_Models;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Diagnostics.Eventing.Reader;

using Medical_Records_Data;
using Medical_Record_Data;

namespace Medical_Records_UNA.Controllers
{
    public class AccessController : Controller
    {
        private DB_Logic dbLogic = new DB_Logic();
        // GET: AccessController
        public ActionResult Index()
        {

            var claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }




        [HttpPost]

        public async Task<JsonResult> AccessVerify([FromBody] LoginCredentials pCredentials)
        {
            string result = "0";

            try
            {

                if (pCredentials != null)
                {
                    var VerifyUser = dbLogic.ValidateUser(pCredentials.Email, pCredentials.Password);

                    if (VerifyUser != null)
                    {
                        List<int> actionsAllow = dbLogic.getActionsOfUser();
                        HttpContext.Session.SetObject("ActionsOfUser", actionsAllow);
                        //List<int> actions = HttpContext.Session.GetObject<List<int>>("ActionsOfUser");

                        List<Claim> claims = new List<Claim>() {

                            new Claim(ClaimTypes.Name, VerifyUser.Username),
                            new Claim("Correo",VerifyUser.Email),

                        };
                        foreach (var rol in VerifyUser.Roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, rol));
                        }

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity));
                        ViewData["Username"] = VerifyUser.Username;

                        //return RedirectToAction("Index","Home");
                        //HttpContext.Session.SetObject("User", pCredentials);

                        //var user = HttpContext.Session.Get("Correo");


                        result = "1";
                    }

                }
                ViewData["Validation Message"] = "Usuario no encontrado.";
                //return View();
                return Json(new { result });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(new { result });
            }

        }

        //[Authorize(Roles = ]
        public JsonResult AccessMethod([FromBody] LoginCredentials pCredentials)
        {
            string result = "0", user = "cali", pass = "1234";
            try
            {

                if (pCredentials != null)
                {

                    if (user == pCredentials.Username && pass == pCredentials.Password)
                    {

                        HttpContext.Session.SetObject("User", pCredentials);
                        HttpContext.Session.GetObject<LoginCredentials>("User");
                        //Ht Session["User"] = pCredentials;
                        result = "1";
                    }

                }


                return Json(new { result });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(new { result });
            }
        }


        [ActionsFilter(5)]

        // GET: AccessController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccessController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccessController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccessController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccessController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccessController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccessController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
