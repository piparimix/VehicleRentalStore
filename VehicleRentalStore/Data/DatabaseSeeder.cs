using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bogus;
using VehicleRentalStore.Models;

namespace VehicleRentalStore.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed(VehicleRentalDbContext context)
        {
            // If we already have items in the database, don't duplicate the seed rows
            if (context.RentalItems.Any()) return;

            // Set the locale to Finnish for authentic names and regional addresses
            var locale = "fi";

            // Fixed seed ensures that identical deterministic random data generates on every database rebuild
            Randomizer.Seed = new Random(8675309);

            // 1. Generate Locations (Featuring major Finnish hubs and regional branches)
            var locations = new List<Location>
            {
                new Location { Name = "Joensuu Keskusta", Address = "Tikkarinne 9", City = "Joensuu", Latitude = 62.5978, Longitude = 29.7426 },
                new Location { Name = "Helsinki-Vantaa Lentoasema", Address = "Lentäjäntie 1", City = "Vantaa", Latitude = 60.3172, Longitude = 24.9633 },
                new Location { Name = "Tampere Rautatieasema", Address = "Rautatienkatu 21", City = "Tampere", Latitude = 61.4981, Longitude = 23.7725 },
                new Location { Name = "Rovaniemi Napapiiri", Address = "Joulupukintie 1", City = "Rovaniemi", Latitude = 66.5436, Longitude = 25.8472 }
            };
            context.Locations.AddRange(locations);

            // 2. Hardcode realistic Insurance Plans
            var plans = new List<InsurancePlan>
            {
                new InsurancePlan { Name = "Basic (Perus)", DailyCost = 0, Deductible = 1500, Description = "Standard liability protection with a high deductible limit." },
                new InsurancePlan { Name = "Premium (Kasko)", DailyCost = 25, Deductible = 400, Description = "Collision damage waiver coverage with a lowered deductible." },
                new InsurancePlan { Name = "Zero Risk (Täyskasko)", DailyCost = 45, Deductible = 0, Description = "Complete coverage with zero deductible and glass damage protection." }
            };
            context.InsurancePlans.AddRange(plans);

            // 3. Generate Customers with valid Finnish HETU structures
            var customerFaker = new Faker<Customer>(locale)
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
                .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("040 #######"))
                .RuleFor(c => c.Address, f => $"{f.Address.StreetAddress()}, {f.Address.ZipCode()} {f.Address.City()}")
                .RuleFor(c => c.Type, f => f.PickRandom<CustomerType>())
                .RuleFor(c => c.LicenseCategories, f => f.Make(1, () => f.PickRandom("B", "A", "C1")).ToList())
                .RuleFor(c => c.DriversLicenseNumber, f => f.Random.Replace("FI########"))
                .RuleFor(c => c.LicenseExpiryDate, f => f.Date.Future(10))
                .RuleFor(c => c.DateOfBirth, f => f.Date.Past(40, DateTime.Now.AddYears(-19))) // Ensures legal adult status (19-59)
                .RuleFor(c => c.Ssn, (f, c) =>
                {
                    // Generate mathematically valid Henkilötunnus (HETU) validation check-mark matching birthday
                    string dd = c.DateOfBirth.ToString("dd");
                    string mm = c.DateOfBirth.ToString("MM");
                    string yy = c.DateOfBirth.ToString("yy");

                    char centurySign = c.DateOfBirth.Year >= 2000 ? 'A' : '-';
                    int individualNumber = f.Random.Number(002, 899); // Real identification tracking range

                    string fullNineDigits = $"{dd}{mm}{yy}{individualNumber:D3}";
                    long parseNumber = long.Parse(fullNineDigits);

                    string checksumTable = "0123456789ABCDEFHJKLMNPRSTUVWXY";
                    char controlChar = checksumTable[(int)(parseNumber % 31)];

                    return $"{dd}{mm}{yy}{centurySign}{individualNumber:D3}{controlChar}";
                })
                .RuleFor(c => c.DateCreated, f => f.Date.Past(2));

            var customers = customerFaker.Generate(30);
            context.Customers.AddRange(customers);

            // 4. Generate Employees
            var employeeFaker = new Faker<Employee>(locale)
                .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                .RuleFor(e => e.LastName, f => f.Name.LastName())
                .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FirstName, e.LastName, "vuokraamo.fi"))
                .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber("050 #######"))
                .RuleFor(e => e.Address, f => $"{f.Address.StreetAddress()}, {f.Address.ZipCode()} {f.Address.City()}")
                .RuleFor(e => e.Role, f => f.PickRandom<EmployeeRole>())
                .RuleFor(e => e.DateCreated, f => f.Date.Past(3));

            var employees = employeeFaker.Generate(8);
            context.Employees.AddRange(employees);

            // 5. Generate an Accurate Fleet of Cars
            var carModels = new[]
            {
                new { Brand = "Toyota", Model = "Corolla Hybrid", Type = FuelType.Hybrid, Rate = 55m },
                new { Brand = "Volkswagen", Model = "Golf TSi", Type = FuelType.Gasoline, Rate = 50m },
                new { Brand = "Tesla", Model = "Model 3 Long Range", Type = FuelType.Electric, Rate = 95m },
                new { Brand = "Volvo", Model = "XC60 D5", Type = FuelType.Diesel, Rate = 110m },
                new { Brand = "Skoda", Model = "Octavia Combi", Type = FuelType.Hybrid, Rate = 65m },
                new { Brand = "BMW", Model = "320i xDrive", Type = FuelType.Gasoline, Rate = 85m },
                new { Brand = "Mercedes-Benz", Model = "E220d", Type = FuelType.Diesel, Rate = 120m },
                new { Brand = "Nissan", Model = "Leaf EV", Type = FuelType.Electric, Rate = 45m }
            };

            var carFaker = new Faker<Car>(locale)
                .RuleFor(c => c.ManufactureYear, f => f.Random.Number(2021, 2026))
                // Clean Finnish standard car license plate structure: e.g. ABC-123
                .RuleFor(c => c.LicensePlate, f => $"{f.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")}-{f.Random.Number(100, 999)}")
                .RuleFor(c => c.VIN, f => f.Vehicle.Vin())
                .RuleFor(c => c.RefuelingPremiumPerUnit, 3.50m)
                .RuleFor(c => c.FuelTankCapacityLiters, (f, c) => c.FuelType == FuelType.Electric ? 0 : f.Random.Double(45, 65))
                .RuleFor(c => c.BatteryCapacityKWh, (f, c) => c.FuelType == FuelType.Electric ? 75 : (c.FuelType == FuelType.Hybrid ? 12 : 0))
                .RuleFor(c => c.CurrentTires, f => f.PickRandom<TireType>(TireType.Summer, TireType.Winter))
                .RuleFor(c => c.CurrentOdometerKm, f => f.Random.Number(5000, 120000))
                .RuleFor(c => c.NextMaintenanceOdometerKm, (f, c) => c.CurrentOdometerKm + f.Random.Number(3000, 15000))
                .RuleFor(c => c.Transmission, f => f.PickRandom<TransmissionType>())
                .RuleFor(c => c.PrimaryColor, f => f.PickRandom("Musta", "Valkoinen", "Harmaa", "Sininen", "Punainen"))
                .RuleFor(c => c.Description, "Nykyaikainen ja taloudellinen henkilöauto.")
                .RuleFor(c => c.NumberOfDoors, 5)
                .RuleFor(c => c.PassengerCapacity, 5)
                .RuleFor(c => c.CargoCapacityLiters, f => f.Random.Number(380, 580))
                .RuleFor(c => c.Status, ItemStatus.Available)
                .RuleFor(c => c.IncludedKilometersPerDay, 250)
                .RuleFor(c => c.ExtraKilometerRate, 0.30m);

            var cars = new List<Car>();
            for (int i = 0; i < 16; i++)
            {
                var meta = carModels[i % carModels.Length];
                var car = carFaker.Generate();
                car.Brand = meta.Brand;
                car.Model = meta.Model;
                car.FuelType = meta.Type;
                car.DailyRate = meta.Rate;
                car.HourlyRate = Math.Round(meta.Rate / 6, 2);
                cars.Add(car);
            }
            context.Cars.AddRange(cars);

            // 6. Generate a Fleet of Motorcycles
            var mcModels = new[]
            {
                new { Brand = "Yamaha", Model = "MT-07", CC = 689, Rate = 60m, License = false },
                new { Brand = "KTM", Model = "1290 Super Adventure", CC = 1301, Rate = 95m, License = true },
                new { Brand = "BMW", Model = "R 1250 GS", CC = 1254, Rate = 110m, License = true },
                new { Brand = "Harley-Davidson", Model = "Sportster S", CC = 1252, Rate = 120m, License = true },
                new { Brand = "Honda", Model = "CB500X", CC = 471, Rate = 50m, License = false }
            };

            var mcFaker = new Faker<Motorcycle>(locale)
                .RuleFor(m => m.ManufactureYear, f => f.Random.Number(2022, 2026))
                // Clean Finnish motorcycle registration scheme format: e.g. 12-ABC or 99-XYZ
                .RuleFor(m => m.LicensePlate, f => $"{f.Random.Number(10, 99)}-{f.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")}")
                .RuleFor(m => m.VIN, f => f.Vehicle.Vin())
                .RuleFor(m => m.FuelType, FuelType.Gasoline)
                .RuleFor(m => m.RefuelingPremiumPerUnit, 3.50m)
                .RuleFor(m => m.FuelTankCapacityLiters, f => f.Random.Double(14, 23))
                .RuleFor(m => m.BatteryCapacityKWh, 0)
                .RuleFor(m => m.CurrentTires, TireType.Performance)
                .RuleFor(m => m.CurrentOdometerKm, f => f.Random.Number(2000, 40000))
                .RuleFor(m => m.NextMaintenanceOdometerKm, (f, m) => m.CurrentOdometerKm + f.Random.Number(2000, 8000))
                .RuleFor(m => m.Transmission, TransmissionType.Manual)
                .RuleFor(m => m.PrimaryColor, f => f.PickRandom("Musta", "Oranssi", "Punainen", "Sininen"))
                .RuleFor(m => m.Description, "Suorituskykyinen moottoripyörä maantieajoon.")
                .RuleFor(m => m.Status, ItemStatus.Available)
                .RuleFor(m => m.IncludedKilometersPerDay, 300)
                .RuleFor(m => m.ExtraKilometerRate, 0.25m);

            var motorcycles = new List<Motorcycle>();
            for (int i = 0; i < 8; i++)
            {
                var meta = mcModels[i % mcModels.Length];
                var mc = mcFaker.Generate();
                mc.Brand = meta.Brand;
                mc.Model = meta.Model;
                mc.EngineCapacityCc = meta.CC;
                mc.RequiresSpecialLicense = meta.License;
                mc.DailyRate = meta.Rate;
                mc.HourlyRate = Math.Round(meta.Rate / 5, 2);
                motorcycles.Add(mc);
            }
            context.Motorcycles.AddRange(motorcycles);

            // 7. Generate Optional Addons / Store Accessories
            var addons = new List<Addon>
            {
                new Addon { Name = "GPS Navigaattori", Description = "Premium Garmin navigaattori Euroopan koroilla.", BillingType = AddonBillingType.PerDay, DailyRate = 5.00m, HourlyRate = 1.00m, Status = ItemStatus.Available, FlatFee = 0 },
                new Addon { Name = "Lasten turvaistuin", Description = "Isofix-kiinnitteinen turvaistuin 9-36 kg lapsille.", BillingType = AddonBillingType.PerRental, DailyRate = 0, HourlyRate = 0, Status = ItemStatus.Available, FlatFee = 25.00m },
                new Addon { Name = "Suksiboksi", Description = "Thule tilava suksiboksi auton katolle asennettuna.", BillingType = AddonBillingType.PerWeek, DailyRate = 35.00m, HourlyRate = 5.00m, Status = ItemStatus.Available, FlatFee = 0 },
                new Addon { Name = "Lisäkuljettaja", Description = "Oikeuttaa toisen rekisteröidyn kuljettajan ajamaan autoa.", BillingType = AddonBillingType.PerRental, DailyRate = 0, HourlyRate = 0, Status = ItemStatus.Available, FlatFee = 15.00m },
                new Addon { Name = "Perävaunu", Description = "Katettu jarruton perävaunu muuttokuormille.", BillingType = AddonBillingType.PerDay, DailyRate = 25.00m, HourlyRate = 5.00m, Status = ItemStatus.Available, FlatFee = 0 }
            };
            context.Addons.AddRange(addons);

            // First SaveChanges pushes master data entities so they get tracking Primary Keys
            context.SaveChanges();

            // Retrieve saved lists containing database ids for secondary operational items
            var dbLocations = context.Locations.ToList();
            var dbPlans = context.InsurancePlans.ToList();
            var dbCustomers = context.Customers.ToList();
            var dbEmployees = context.Employees.ToList();
            var dbVehicles = context.Vehicles.ToList();
            var dbAddons = context.Addons.ToList();

            var rentals = new List<Rental>();
            var invoices = new List<Invoice>();
            var incidentCharges = new List<IncidentCharge>();
            var conditionLogs = new List<ConditionLog>();
            var maintenanceLogs = new List<MaintenanceLog>();

            var random = new Random(42);

            // Reflection helps populate internal private read-only collections inside Rental entity
            var rentedItemsField = typeof(Rental).GetField("_rentedItems", BindingFlags.NonPublic | BindingFlags.Instance);
            var additionalDriversField = typeof(Rental).GetField("_additionalDrivers", BindingFlags.NonPublic | BindingFlags.Instance);

            // 8. Generate Maintenance Logs
            var maintenanceStaff = dbEmployees.Where(e => e.Role == EmployeeRole.Maintenance).ToList();
            if (!maintenanceStaff.Any()) maintenanceStaff = dbEmployees;

            for (int i = 0; i < 6; i++)
            {
                var vehicle = dbVehicles[i % dbVehicles.Count];
                var emp = maintenanceStaff[i % maintenanceStaff.Count];
                var log = new MaintenanceLog
                {
                    VehicleId = vehicle.Id,
                    Vehicle = vehicle,
                    EmployeeId = emp.Id,
                    Employee = emp,
                    Date = DateTime.UtcNow.AddMonths(-random.Next(1, 12)),
                    Cost = random.Next(120, 850),
                    Description = i % 3 == 0 ? "Kausihuolto ja öljynvaihto" : (i % 3 == 1 ? "Määräaikaistarkastus ja jarrupalojen vaihto" : "Määräaikainen renkaiden kausivaihto (Kesä/Talvi)")
                };
                maintenanceLogs.Add(log);
            }
            context.MaintenanceLogs.AddRange(maintenanceLogs);

            // 9. Generate Rentals, Invoices, Condition Logs & Incident Charges
            for (int i = 0; i < 20; i++)
            {
                var customer = dbCustomers[i % dbCustomers.Count];
                var employee = dbEmployees[i % dbEmployees.Count];
                var vehicle = dbVehicles[(i + 4) % dbVehicles.Count];
                var plan = dbPlans[i % dbPlans.Count];
                var pickupLoc = dbLocations[i % dbLocations.Count];
                var dropoffLoc = dbLocations[(i + 2) % dbLocations.Count];

                var start = DateTime.UtcNow;
                var durationDays = random.Next(2, 7);
                var expectedEnd = start.AddDays(durationDays);
                DateTime? actualEnd = null;
                var status = RentalStatus.Completed;

                if (i < 12)
                {
                    // Historic completed rentals
                    start = DateTime.UtcNow.AddDays(-random.Next(15, 70));
                    expectedEnd = start.AddDays(durationDays);
                    actualEnd = expectedEnd;
                    status = RentalStatus.Completed;
                }
                else if (i >= 12 && i < 16)
                {
                    // Active rentals out on road right now
                    start = DateTime.UtcNow.AddDays(-random.Next(1, 3));
                    expectedEnd = start.AddDays(durationDays);
                    actualEnd = null;
                    status = RentalStatus.Active;
                    vehicle.Status = ItemStatus.OnRent;
                }
                else if (i >= 16 && i < 18)
                {
                    // Overdue rentals (expected return window is in the past)
                    start = DateTime.UtcNow.AddDays(-random.Next(8, 12));
                    expectedEnd = start.AddDays(random.Next(3, 5));
                    actualEnd = null;
                    status = RentalStatus.Active;
                    vehicle.Status = ItemStatus.OnRent;
                }
                else
                {
                    // Cancelled booking logs
                    start = DateTime.UtcNow.AddDays(random.Next(2, 6));
                    expectedEnd = start.AddDays(durationDays);
                    actualEnd = null;
                    status = RentalStatus.Cancelled;
                }

                var startOdo = vehicle.CurrentOdometerKm - random.Next(500, 4000);
                if (startOdo < 0) startOdo = 2000;
                int? endOdo = (status == RentalStatus.Completed) ? (startOdo + random.Next(120, 900)) : null;

                var rental = new Rental
                {
                    CustomerId = customer.Id,
                    Customer = customer,
                    EmployeeId = employee.Id,
                    Employee = employee,
                    StartDate = start,
                    EndDate = expectedEnd,
                    ExpectedEndDate = expectedEnd,
                    ActualReturnDate = actualEnd,
                    Status = status,
                    StartOdometerKm = startOdo,
                    EndOdometerKm = endOdo,
                    StartFuelPercentage = 100,
                    EndFuelPercentage = (status == RentalStatus.Completed) ? random.Next(92, 100) : null,
                    PickupLocationId = pickupLoc.Id,
                    PickupLocation = pickupLoc,
                    DropoffLocationId = dropoffLoc.Id,
                    DropoffLocation = dropoffLoc,
                    InsurancePlanId = plan.Id,
                    InsurancePlan = plan,
                    SecurityDepositAmount = 250.00m,
                    IsSecurityDepositReleased = (status == RentalStatus.Completed),
                    FuelPolicy = FuelPolicy.FullToFull
                };

                // Add selected vehicle into the private list using reflection
                var rentedItemsList = (List<RentalItem>)rentedItemsField.GetValue(rental)!;
                rentedItemsList.Add(vehicle);

                // Add an addon item to roughly 50% of orders
                if (random.NextDouble() > 0.5)
                {
                    var addon = dbAddons[random.Next(dbAddons.Count)];
                    rentedItemsList.Add(addon);
                }

                // Add an additional secondary driver to 30% of records
                if (random.NextDouble() > 0.7)
                {
                    var extraDriver = dbCustomers[(i + 2) % dbCustomers.Count];
                    if (extraDriver.Id != customer.Id)
                    {
                        var driversList = (List<Customer>)additionalDriversField.GetValue(rental)!;
                        driversList.Add(extraDriver);
                    }
                }

                // Calculate subtotal from matching strategies
                TimeSpan rentalDuration = expectedEnd - start;
                decimal vehicleCost = vehicle.CalculateRentalCost(rentalDuration);
                decimal planCost = plan.DailyCost * rentalDuration.Days;
                decimal addonsCost = 0;

                foreach (var item in rentedItemsList)
                {
                    if (item is Addon addon)
                    {
                        addonsCost += addon.CalculateRentalCost(rentalDuration);
                    }
                }

                decimal subTotal = vehicleCost + planCost + addonsCost;
                decimal taxRate = 0.255m; // Finnish standard 25.5% VAT rate
                decimal taxAmount = Math.Round(subTotal * taxRate, 2);
                decimal totalAmount = subTotal + taxAmount;

                rental.TotalAmount = totalAmount;
                rentals.Add(rental);

                // 10. Generate Invoices
                if (status != RentalStatus.Cancelled)
                {
                    var invoiceStatus = InvoiceStatus.Unpaid;
                    if (status == RentalStatus.Completed)
                    {
                        invoiceStatus = InvoiceStatus.Paid;
                    }
                    else if (expectedEnd < DateTime.UtcNow)
                    {
                        invoiceStatus = InvoiceStatus.Overdue;
                    }

                    var invoice = new Invoice
                    {
                        Rental = rental,
                        SubTotal = subTotal,
                        TaxRate = taxRate,
                        TaxAmount = taxAmount,
                        Total = totalAmount,
                        DateIssued = start,
                        DueDate = start.AddDays(14),
                        Status = invoiceStatus
                    };
                    invoices.Add(invoice);
                }

                // 11. Generate Incident Charges for minor traffic violations relative to the actual rental window
                if (status == RentalStatus.Completed && i % 5 == 0)
                {
                    var charge = new IncidentCharge
                    {
                        Rental = rental,
                        OffenseDate = start.AddDays(random.Next(1, durationDays)),
                        Description = "Ylinopeussakko - Automaattikamera",
                        FineAmount = 140.00m,
                        AdminFee = 40.00m,
                        IsBilledToCustomer = true
                    };
                    incidentCharges.Add(charge);
                }

                // 12. Generate Return Quality Condition Logs relative to the actual end of rental
                if (status == RentalStatus.Completed && i % 4 == 0)
                {
                    var dmgLog = new ConditionLog
                    {
                        VehicleId = vehicle.Id,
                        Vehicle = vehicle,
                        EmployeeId = employee.Id,
                        Employee = employee,
                        Rental = rental,
                        DateReported = actualEnd ?? expectedEnd,
                        Type = DamageType.Scratch,
                        Severity = DamageSeverity.Cosmetic,
                        LocationOnVehicle = "Konepelti",
                        Description = "Pieni kiveniskemä keulamaskissa matka-ajon seurauksena.",
                        IsRepaired = true,
                        EstimatedRepairCost = 120.00m
                    };
                    conditionLogs.Add(dmgLog);
                }
            }

            context.Rentals.AddRange(rentals);
            context.Invoices.AddRange(invoices);
            context.IncidentCharges.AddRange(incidentCharges);
            context.ConditionLogs.AddRange(conditionLogs);

            // Final push persists entire linked transaction into SQLite context
            context.SaveChanges();
        }
    }
}