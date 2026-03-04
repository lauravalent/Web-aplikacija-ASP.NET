using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rad.DAL;
using Rad.Model;
using System.Security.Claims;

namespace KanducarValent_Laura_0246111632.Controllers
{
    [Authorize]
    public class ReviewController(
        GuestManagerDbContext _dbContext) : Controller
    {
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reviews = _dbContext.Reviews
                .Where(r => r.UserId == userId)
                .Include(r => r.Accomodation)
                .ToList();

            return View(reviews);
        }
        public IActionResult Create()
        {
            this.FillDropdownValues();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Review model)
        {
            Console.WriteLine($"Otvorena je Create metodaaaaa: {model}");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.UserId = userId;
            ModelState.Remove("UserId");

            foreach (var modelState in ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    System.Diagnostics.Debug.WriteLine($"Polje: {modelState.Key}, Greška: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                _dbContext.Reviews.Add(model);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
 

            FillDropdownValues();
            return View(model);
        }
        private void FillDropdownValues()
        {
            var selectItems = new List<SelectListItem>();

            var listItem = new SelectListItem();
            listItem.Text = "odaberite";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var category in _dbContext.Accomodations)
            {
                listItem = new SelectListItem(category.Name, category.ID.ToString());
                selectItems.Add(listItem);
            }

            ViewBag.PossibleAccomodations = selectItems;
        }

        public IActionResult ByAccomodation(int id)
        {
            var reviews = _dbContext.Reviews
                .Where(r => r.AccomodationID == id)
                .Include(r => r.User)
                .ToList();

            return PartialView("_ReviewsPartial", reviews);
        }

        [ActionName(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            var model = _dbContext.Reviews
                .FirstOrDefault(c => c.ID == id);
            this.FillDropdownValues();
            return View(model);
        }
        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(int id)
        {
            var res = _dbContext.Reviews
                .Single(c => c.ID == id);
            var ok = await this.TryUpdateModelAsync(res);

            if (ok && this.ModelState.IsValid)
            {
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            this.FillDropdownValues();
            return View();
        }
    }
}
