using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetanitExampleCoreMVC.Models;
using MetanitExampleCoreMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace MetanitExampleCoreMVC.Controllers
{
    public class DataModelController : Controller
    {
        private MobileContext db;
        private UsersContext dbUsers;

        public DataModelController(MobileContext context, UsersContext usersContext)
        {
            db = context;
            dbUsers = usersContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Phones.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Phone phone)
        {
            db.Phones.Add(phone);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Phone phone = await db.Phones.FirstOrDefaultAsync(p => p.Id == id);

                if (phone != null)
                {
                    return View(phone);
                }
            }

            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Phone phone = await db.Phones.FirstOrDefaultAsync(p => p.Id == id);

                if (phone != null)
                {
                    return View(phone);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Phone phone)
        {
            db.Phones.Update(phone);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Phone phone = await db.Phones.FirstOrDefaultAsync(p => p.Id == id);

                if (phone != null)
                {
                    return View(phone);
                }
            }

            return NotFound();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {

                //Phone phone = new Phone { Id = id.Value };
                //db.Entry(phone).State = EntityState.Deleted;
                //await db.SaveChangesAsync();
                //return RedirectToAction("Index");

                Phone phone = await db.Phones.FirstOrDefaultAsync(p => p.Id == id);

                if (phone != null)
                {
                    db.Phones.Remove(phone);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return NotFound();
        }

        public async Task<IActionResult> Sort(SortState sortOrder = SortState.NameAsc)
        {
            IIncludableQueryable<MetanitExampleCoreMVC.Models.User, Company> users = dbUsers.Users.Include(x => x.Company);
            IQueryable<MetanitExampleCoreMVC.Models.User> usersResult;

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["AgeSort"] = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
            ViewData["CompanySort"] = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    usersResult = users.OrderByDescending(s => s.Name);
                    break;
                case SortState.AgeAsc:
                    usersResult = users.OrderBy(s => s.Name);
                    break;
                case SortState.AgeDesc:
                    usersResult = users.OrderByDescending(s => s.Name);
                    break;
                case SortState.CompanyAsc:
                    usersResult = users.OrderBy(s => s.Name);
                    break;
                case SortState.CompanyDesc:
                    usersResult = users.OrderByDescending(s => s.Company.Name);
                    break;
                default:
                    usersResult = users.OrderBy(s => s.Name);
                    break;
            }

            IndexSortViewModel viewModel = new IndexSortViewModel
            {
                Users = await usersResult.AsNoTracking().ToListAsync(),
                SortViewModel = new SortViewModel(sortOrder)
            };

            return View(viewModel);
        }
    }
}