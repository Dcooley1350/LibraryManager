using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Library.Controllers
{
    [Authorize]
    public class CopyController : Controller
    {
        private readonly LibraryContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CopyController(UserManager<ApplicationUser> userManager, LibraryContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public ActionResult Create(int id)
        {
            new Copy = 
        }
    }
}
