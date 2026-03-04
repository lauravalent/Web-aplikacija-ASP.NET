using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rad.DAL;
using Rad.Model;
using System.Globalization;
using System.Security.Claims;

namespace KanducarValent_Laura_0246111632.Controllers
{
    [Authorize]
    public class ReservationController(
        GuestManagerDbContext _dbContext) : Controller
    {
        [Route("moje-rezervacije")]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reservations = _dbContext.Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.Accomodation)
                .ToList();

            return View(reservations);
        }
        public IActionResult Create()
        {
            this.FillDropdownValues();
            return View();
        }

        [HttpGet]
        public IActionResult Create(int accomodationId)
        {
            var accomodation = _dbContext.Accomodations.Find(accomodationId);
            if (accomodation == null)
                return NotFound();

            var model = new Reservation
            {
                AccomodationID = accomodationId
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Reservation model)
        {
            Console.WriteLine($"Otvorena je Create metodaaaaa: {model}");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.UserId = userId;
            ModelState.Remove("UserId");

            var accomodation = _dbContext.Accomodations.FirstOrDefault(a => a.ID == model.AccomodationID);
            if (accomodation == null)
                return NotFound();


            if (model.NumberOfGuests > accomodation.Capacity)
            {
                ModelState.AddModelError("NumberOfGuests", $"Broj gostiju ne može biti veći od kapaciteta ({accomodation.Capacity}).");
            }

            var overlappingReservation = _dbContext.Reservations
                .Where(r => r.AccomodationID == model.AccomodationID)
                .Any(r =>
                    model.StartDate < r.EndDate &&
                    model.EndDate > r.StartDate
                );

            if (overlappingReservation)
            {
                ModelState.AddModelError("", "Odabrani termin je zauzet. Odaberite drugi.");
            }

            if (ModelState.IsValid)
            {
                _dbContext.Reservations.Add(model);
                _dbContext.SaveChanges();

                var acc = _dbContext.Accomodations.FirstOrDefault(a => a.ID == model.AccomodationID);

                if (acc != null)
                {
                    var days = (model.EndDate - model.StartDate).Days;
                    var total = days * acc.PricePerNight;

                    TempData["ReservationTotal"] = total.ToString("F2", CultureInfo.InvariantCulture);
                    TempData["ReservationDays"] = days.ToString();
                }

                return RedirectToAction("Confirmation");
            }


            FillDropdownValues();
            return View(model);
        }

        public IActionResult Confirmation()
        {
            return View();
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

        [ActionName(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            var model = _dbContext.Reservations
                .FirstOrDefault(c => c.ID == id);
            return View(model);
        }
        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(int id)
        {

            var res = _dbContext.Reservations
                .Single(c => c.ID == id);
            var ok = await this.TryUpdateModelAsync(res);

            var accomodation = _dbContext.Accomodations.FirstOrDefault(a => a.ID == res.AccomodationID);
            if (accomodation == null)
                return NotFound();


            if (res.NumberOfGuests > accomodation.Capacity)
            {
                ModelState.AddModelError("NumberOfGuests", $"Broj gostiju ne može biti veći od kapaciteta ({accomodation.Capacity}).");
            }

            if (ok && this.ModelState.IsValid)
            {
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
   
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = _dbContext.Reservations.FirstOrDefault(c => c.ID == id);
            if (reservation != null)
            {
                _dbContext.Reservations.Remove(reservation);
                _dbContext.SaveChanges();
            }

            return RedirectToAction(nameof(Index));

        }
    }
    }
