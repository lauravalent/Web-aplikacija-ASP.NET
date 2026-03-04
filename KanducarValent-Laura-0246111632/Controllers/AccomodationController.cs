using KanducarValent_Laura_0246111632.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Rad.DAL;
using Rad.Model;

namespace KanducarValent_Laura_0246111632.Controllers
{
    public class AccomodationController(GuestManagerDbContext _dbContext) : Controller
    {
        public IActionResult Index(AccomodationFilterModel filter = null)
        {
            filter ??= new AccomodationFilterModel();

            var accomodationQuery = _dbContext.Accomodations.Include(c => c.City).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.City))
                accomodationQuery = accomodationQuery.Where(p => p.CityID != null && p.City.Name.ToLower().Contains(filter.City.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                accomodationQuery = accomodationQuery.Where(a => a.Name != null && a.Name.ToLower().Contains(filter.Name.ToLower()));
            }
            if (filter.Capacity > 0)
            {
                accomodationQuery = accomodationQuery.Where(a => a.Capacity >= filter.Capacity);
            }
            if (filter.Size > 0)
            {
                accomodationQuery = accomodationQuery.Where(a => a.Size >= filter.Size);
            }

            var accomodations = accomodationQuery.ToList();
            return View(accomodations);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Accomodation model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Accomodations.Add(model);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            else
            {
                return View();
            }


        }
        [ActionName(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            var model = _dbContext.Accomodations
                .FirstOrDefault(c => c.ID == id);
            return View(model);
        }
        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(int id)
        {
            var acco = _dbContext.Accomodations
                .Single(c => c.ID == id);
            var ok = await this.TryUpdateModelAsync(acco);

            if (ok && this.ModelState.IsValid)
            {
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Details(int id)
        {
            var accomodation = _dbContext.Accomodations
                .Include(a => a.Reviews)
                .Include(a => a.City)
                .FirstOrDefault(a => a.ID == id);

            if (accomodation == null)
                return NotFound();

            return View(accomodation);
        }


    }

}
