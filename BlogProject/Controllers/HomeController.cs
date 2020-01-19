using BlogProject.Helpers;
using BlogProject.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? page, string searchStr)
        {
            ViewBag.Search = searchStr;
            var blogList = TextSearch.IndexSearch(searchStr);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(blogList.Where(b => b.Published).OrderByDescending(b => b.Created).ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model) 
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    await EmailHelper.ComposeEmailAsync(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            EmailModel model = new EmailModel();
            return View(model);
        }
    }
}