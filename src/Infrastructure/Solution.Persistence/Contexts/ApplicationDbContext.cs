using Microsoft.EntityFrameworkCore;
using Solution.Domain.Common;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        ////public DbSet<UserProfile> UserProfiles { get; set; }
        //public DbSet<Settings> Settings { get; set; }
        //public DbSet<TypeOfBusiness> TypeOfBusinesses { get; set; }
        //public DbSet<VenueAccountInformation> VenueAccountInformations { get; set; }
        //public DbSet<AddOnCategory> AddOnCategory { get; set; }
        //public DbSet<AddOn> AddOn { get; set; }
        //public DbSet<Document> Documents { get; set; }
        //public DbSet<PackageCategory> PackageCategory { get; set; }
        //public DbSet<Package> Package { get; set; }
        //public DbSet<WorkingHour> WorkingHour { get; set; }
        //public DbSet<VenuePublicProfile> VenuePublicProfile { get; set; }
        //public DbSet<SocialProfile> SocialProfile { get; set; }
        ////public DbSet<UserRole> UserRole { get; set; }
        //public DbSet<UploadDocument> UploadDocuments { get; set; }
        ////public DbSet<Client> Clients { get; set; }
        //public DbSet<Room> Rooms { get; set; }
        //public DbSet<Event> Events { get; set; }
        //public DbSet<Notification> Notifications { get; set; }
        //public DbSet<EventAndRoom> EventAndRoom { get; set; }
        //public DbSet<EventClient> EventClients { get; set; }
        //public DbSet<EventFinance> EventFinances { get; set; }
        //public DbSet<EventFinanceAddOn> EventFinanceAddOns { get; set; }
        //public DbSet<EventPayment> EventPayments { get; set; }
        //public DbSet<EventFinancePaymentSchedule> EventFinancePaymentSchedules { get; set; }
        //public DbSet<EventDocument> EventDocuments { get; set; }
        //public DbSet<EventDocumentSigner> EventDocumentSigners { get; set; }
        ////public DbSet<Member> Members { get; set; }
        //public DbSet<EventAndMember> EventAndMembers { get; set; }
        //public DbSet<EventTimeline> EventTimelines { get; set; }
        //public DbSet<PaymentStripe> PaymentStripe { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //SeedAddOnCategory(modelBuilder);
            //SeedPackageCategory(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof
                (ApplicationDbContext).Assembly);

            //InitialSetting(modelBuilder);
            //InitialRates(modelBuilder);
            //SeedTypesOfBusiness(modelBuilder);
            // possible to add here seeded data through migration
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken
            = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        //private static void InitialSetting(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Settings>().HasData(new Settings() { Id = new Guid("91e90ced-b5d8-4a8e-afcd-4a48c11fc25c"), SettingName = "AllowedNumberOfUsersPerVenue", SettingValue = "5" });
        //}

        //private static void InitialRates(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<RateStructure>().HasData(new RateStructure() { Id = new Guid("307da32a-61b1-44b6-86d5-a90ab1f01d22"), Name = "Hourly", Description = "Hourly" });
        //    modelBuilder.Entity<RateStructure>().HasData(new RateStructure() { Id = new Guid("6efd1866-56b2-486e-b8ae-b55b7fe795d1"), Name = "Flat", Description = "Flat rate" });
        //    modelBuilder.Entity<RateStructure>().HasData(new RateStructure() { Id = new Guid("3ca6e0bb-3202-4fc6-b517-2d244f2e806c"), Name = "Per Head", Description = "Per head" });
        //}

        //private static void SeedAddOnCategory(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AddOnCategory>().HasData(new AddOnCategory() { Id = new Guid("027b4ed4-649e-4ff9-8c85-4cdc4dfada5a"), CategoryName = "Wedding" });
        //    modelBuilder.Entity<AddOnCategory>().HasData(new AddOnCategory() { Id = new Guid("b76ddb5a-c1bf-4559-9e97-0e3bdc77e4cb"), CategoryName = "Funeral" });
        //    modelBuilder.Entity<AddOnCategory>().HasData(new AddOnCategory() { Id = new Guid("bcad8b6e-03f7-4a41-9e4d-5dd215f87019"), CategoryName = "Conference" });
        //}

        //private static void SeedPackageCategory(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<PackageCategory>().HasData(new PackageCategory() { Id = new Guid("66ff0605-ac87-4c37-a05b-63730356644f"), CategoryName = "Wedding" });
        //    modelBuilder.Entity<PackageCategory>().HasData(new PackageCategory() { Id = new Guid("dbfc3614-6947-4ae9-acfc-4511adc978ba"), CategoryName = "Funeral" });
        //    modelBuilder.Entity<PackageCategory>().HasData(new PackageCategory() { Id = new Guid("0510a5c7-11ff-44c9-9bae-9f609594c64f"), CategoryName = "Conference" });
        //}

        //private static void SeedTypesOfBusiness(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<TypeOfBusiness>().HasData(new TypeOfBusiness() { Id = new Guid("2e6bbb95-2afa-48a9-bc99-fefba9b75859"), BusinessName = "Banquet Hall" });
        //    modelBuilder.Entity<TypeOfBusiness>().HasData(new TypeOfBusiness() { Id = new Guid("78398392-0279-4805-bdd8-438bbb6d6324"), BusinessName = "Conference Center" });
        //    modelBuilder.Entity<TypeOfBusiness>().HasData(new TypeOfBusiness() { Id = new Guid("8647df86-dfc0-42c7-b103-d68843e046e3"), BusinessName = "Hotel" });
        //}
    }
}
