using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetanitExampleCoreMVC.Models;
using MetanitExampleCoreMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Filter(int? company, string name)
        {
            IIncludableQueryable<MetanitExampleCoreMVC.Models.User, Company> users = dbUsers.Users.Include(p => p.Company);
            IQueryable<MetanitExampleCoreMVC.Models.User> usersResult = null;
            if (company != null && company != 0)
            {
                usersResult = users.Where(p => p.CompanyId == company);
            }
            else
            {
                usersResult = users;
            }

            if (!String.IsNullOrEmpty(name))
            {
                usersResult = usersResult.Where(p => p.Name.Contains(name));
            }

            List<Company> companies = dbUsers.Companies.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            companies.Insert(0, new Company { Name = "Все", Id = 0 });

            UsersListViewModel viewModel = new UsersListViewModel
            {
                Users = usersResult?.ToList(),
                Companies = new SelectList(companies, "Id", "Name"),
                Name = name
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Pagination(int page = 1)
        {
            int pageSize = 1;
            IIncludableQueryable<MetanitExampleCoreMVC.Models.User, Company> source = dbUsers.Users.Include(x => x.Company);
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexPageViewModel viewModel = new IndexPageViewModel()
            {
                PageViewModel = pageViewModel,
                Users = items
            };

            return View(viewModel);
        }

        public async Task<IActionResult> FilterSortPaging(int? company, string name, int page = 1,
           SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 1;

            //фильтрация
            IQueryable<MetanitExampleCoreMVC.Models.User> users = dbUsers.Users.Include(x => x.Company);

            if (company != null && company != 0)
            {
                users = users.Where(p => p.CompanyId == company);
            }
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    users = users.OrderByDescending(s => s.Name);
                    break;
                case SortState.AgeAsc:
                    users = users.OrderBy(s => s.Age);
                    break;
                case SortState.AgeDesc:
                    users = users.OrderByDescending(s => s.Age);
                    break;
                case SortState.CompanyAsc:
                    users = users.OrderBy(s => s.Company.Name);
                    break;
                case SortState.CompanyDesc:
                    users = users.OrderByDescending(s => s.Company.Name);
                    break;
                default:
                    users = users.OrderBy(s => s.Name);
                    break;
            }

            // пагинация
            var count = await users.CountAsync();
            var items = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            FilterSortPagingViewModel viewModel = new FilterSortPagingViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortSecondViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(dbUsers.Companies.ToList(), company, name),
                Users = items
            };

            return View(viewModel);
        }
    }
}