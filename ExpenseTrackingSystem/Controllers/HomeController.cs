using ExpenseTrackingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using ExpenseTrackingSystem.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTrackingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var id = user.Id;
            if (user != null)
            {
                var user_id = user.Id;

                var chart1 = _context.Expense.Where(expense => expense.IsDeleted == 0)
                          .Where(expense => expense.CreatedBy == user_id)
                          .GroupBy(e => e.CategoryId)
                          .Select(group => new
                            {
                                Category = group.Key,
                                TotalAmount = group.Sum(e => e.Amount)
                            })
                          .ToList();

                var chart2 = _context.Expense.Where(expense => expense.IsDeleted == 0)
                          .Where(expense => expense.CreatedBy == user_id)
                          .GroupBy(e => e.AccountId)
                          .Select(group => new
                          {
                              Account = group.Key,
                              TotalAmount = group.Sum(e => e.Amount)
                          })
                          .ToList();

                var categories = _context.Category
                        .Where(category => category.IsDeleted == 0)
                        .ToList();

                var accounts = _context.Account
                        .Where(account => account.IsDeleted == 0)
                        .ToList();

                ViewBag.chart1 = chart1;
                ViewBag.chart2 = chart2;
                ViewBag.Category = categories;
                ViewBag.Account = accounts;

            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            // Clear the existing external cookie
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return RedirectToAction("Index", "Home"); // Replace with the appropriate action and controller
        }
    }
}