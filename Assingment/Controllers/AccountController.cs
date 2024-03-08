using Assingment.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using Assingment.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assingment.Controllers
{
    public class AccountController : Controller
    {
            public IActionResult Login()
            {
                ViewData["error"] = "";
                return View();
            }

            [HttpPost]
            public IActionResult Login(LoginViewModel model)
            {
                //find the user credentials from db
                string connString = "server=DESKTOP-4M61261\\MSSQL; database=yusra; user id=sa; password=yusra@@; TrustServerCertificate=true;";
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    string stmt = $"select count(*) total from users where username='{model.Username}' and password='{model.Password}'";
                    SqlCommand cmd = new SqlCommand(stmt, con);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        //user is valid
                        HttpContext.Session.SetString("Username", model.Username);

                        //create authentication cookie
                        var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, model.Username),
                        new Claim(ClaimTypes.Role, "Admin")
                    }.ToArray();

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["error"] = "Invalid Credentials";
                        //user is invalid
                        return View(model);
                    }
                }
            }

            public IActionResult Logout()
            {
                HttpContext.Session.Remove("Username");
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login");
            }
        }
    }