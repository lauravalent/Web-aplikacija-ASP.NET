using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rad.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Vjezba.Model;

namespace Rad.DAL
{
    public class GuestManagerDbContext : IdentityDbContext<AppUser>
    {
        public GuestManagerDbContext(DbContextOptions<GuestManagerDbContext> options)
        : base(options)
        {
        }
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().HasData(new City { ID = 1, Name = "Sirova Katalena" });
            modelBuilder.Entity<City>().HasData(new City { ID = 2, Name = "Budrovac" });
            modelBuilder.Entity<City>().HasData(new City { ID = 3, Name = "Sveta Ana" });

            modelBuilder.Entity<Accomodation>().HasData(
                new Accomodation
                {
                    ID = 1,
                    Name = "Apartman Sofija",
                    Capacity = 2,
                    Size = 45,
                    ImageUrl = "/images/sofija.jpg",
                    CityID = 1,
                    PricePerNight = 75.00m
                },
                new Accomodation
                {
                    ID = 2,
                    Name = "Apartman Draga",
                    Capacity = 3,
                    Size = 35,
                    ImageUrl = "/images/Janko.jpg",
                    CityID = 2,
                    PricePerNight = 65.00m
                },
                new Accomodation
                {
                    ID = 3,
                    Name = "Kuća Braco",
                    Capacity = 4,
                    Size = 50,
                    ImageUrl = "/images/draga.jpg",
                    CityID = 3,
                    PricePerNight = 85.00m
                },
                new Accomodation
                {
                    ID = 4,
                    Name = "Kuca Ivica",
                    Capacity = 2,
                    Size = 56,
                    ImageUrl = "/images/ivica.jpg",
                    CityID = 1,
                    PricePerNight = 70.00m
                },
                new Accomodation
                {
                    ID = 5,
                    Name = "Apartman More",
                    Capacity = 4,
                    Size = 60,
                    ImageUrl = "/images/more.jpeg",
                    CityID = 2,
                    PricePerNight = 90.00m
                },
                new Accomodation
                {
                    ID = 6,
                    Name = "Vila Planina",
                    Capacity = 6,
                    Size = 120,
                    ImageUrl = "/images/planina.jpeg",
                    CityID = 3,
                    PricePerNight = 150.00m
                }
            );

        }

    }
}
