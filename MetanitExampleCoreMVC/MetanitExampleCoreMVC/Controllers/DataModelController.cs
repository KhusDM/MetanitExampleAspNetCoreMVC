using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetanitExampleCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MetanitExampleCoreMVC.Controllers
{
    public class DataModelController : Controller
    {
        private MobileContext db;

        public DataModelController(MobileContext context)
        {
            db = context;
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
    }
}