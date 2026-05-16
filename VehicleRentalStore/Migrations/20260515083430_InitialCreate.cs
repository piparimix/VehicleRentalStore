using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleRentalStore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsurancePlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DailyCost = table.Column<decimal>(type: "TEXT", nullable: false),
                    Deductible = table.Column<decimal>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PersonType = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: true),
                    LicenseCategories = table.Column<string>(type: "TEXT", nullable: true),
                    DriversLicenseNumber = table.Column<string>(type: "TEXT", nullable: true),
                    LicenseExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsAnonymized = table.Column<bool>(type: "INTEGER", nullable: true),
                    Role = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DailyRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemType = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Addon_Description = table.Column<string>(type: "TEXT", nullable: true),
                    BillingType = table.Column<int>(type: "INTEGER", nullable: true),
                    FlatFee = table.Column<decimal>(type: "TEXT", nullable: true),
                    Brand = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    ManufactureYear = table.Column<int>(type: "INTEGER", nullable: true),
                    LicensePlate = table.Column<string>(type: "TEXT", nullable: true),
                    VIN = table.Column<string>(type: "TEXT", nullable: true),
                    FuelType = table.Column<int>(type: "INTEGER", nullable: true),
                    RefuelingPremiumPerUnit = table.Column<decimal>(type: "TEXT", nullable: true),
                    FuelTankCapacityLiters = table.Column<double>(type: "REAL", nullable: true),
                    BatteryCapacityKWh = table.Column<double>(type: "REAL", nullable: true),
                    CurrentTires = table.Column<int>(type: "INTEGER", nullable: true),
                    CurrentOdometerKm = table.Column<int>(type: "INTEGER", nullable: true),
                    NextMaintenanceOdometerKm = table.Column<int>(type: "INTEGER", nullable: true),
                    NextInspectionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Transmission = table.Column<int>(type: "INTEGER", nullable: true),
                    PrimaryColor = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IncludedKilometersPerDay = table.Column<int>(type: "INTEGER", nullable: true),
                    ExtraKilometerRate = table.Column<decimal>(type: "TEXT", nullable: true),
                    NumberOfDoors = table.Column<int>(type: "INTEGER", nullable: true),
                    PassengerCapacity = table.Column<int>(type: "INTEGER", nullable: true),
                    CargoCapacityLiters = table.Column<int>(type: "INTEGER", nullable: true),
                    EngineCapacityCc = table.Column<int>(type: "INTEGER", nullable: true),
                    RequiresSpecialLicense = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    ExpectedEndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ActualReturnDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StartOdometerKm = table.Column<int>(type: "INTEGER", nullable: false),
                    EndOdometerKm = table.Column<int>(type: "INTEGER", nullable: true),
                    StartFuelPercentage = table.Column<int>(type: "INTEGER", nullable: false),
                    EndFuelPercentage = table.Column<int>(type: "INTEGER", nullable: true),
                    PickupLocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    DropoffLocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    InsurancePlanId = table.Column<int>(type: "INTEGER", nullable: true),
                    SecurityDepositAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsSecurityDepositReleased = table.Column<bool>(type: "INTEGER", nullable: false),
                    FuelPolicy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_InsurancePlans_InsurancePlanId",
                        column: x => x.InsurancePlanId,
                        principalTable: "InsurancePlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rentals_Locations_DropoffLocationId",
                        column: x => x.DropoffLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rentals_Locations_PickupLocationId",
                        column: x => x.PickupLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rentals_Persons_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rentals_Persons_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VehicleId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Cost = table.Column<decimal>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceLogs_Persons_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintenanceLogs_RentalItems_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "RentalItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConditionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VehicleId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    RentalId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateReported = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Severity = table.Column<int>(type: "INTEGER", nullable: false),
                    LocationOnVehicle = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IsRepaired = table.Column<bool>(type: "INTEGER", nullable: false),
                    EstimatedRepairCost = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConditionLogs_Persons_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConditionLogs_RentalItems_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "RentalItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConditionLogs_Rentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "Rentals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerRental",
                columns: table => new
                {
                    AdditionalDriversId = table.Column<int>(type: "INTEGER", nullable: false),
                    RentalId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRental", x => new { x.AdditionalDriversId, x.RentalId });
                    table.ForeignKey(
                        name: "FK_CustomerRental_Persons_AdditionalDriversId",
                        column: x => x.AdditionalDriversId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerRental_Rentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "Rentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncidentCharges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RentalId = table.Column<int>(type: "INTEGER", nullable: false),
                    OffenseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    FineAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    AdminFee = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsBilledToCustomer = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncidentCharges_Persons_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncidentCharges_Rentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "Rentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RentalId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    TaxRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    DateIssued = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Rentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "Rentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentalRentalItem",
                columns: table => new
                {
                    RentalId = table.Column<int>(type: "INTEGER", nullable: false),
                    RentedItemsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalRentalItem", x => new { x.RentalId, x.RentedItemsId });
                    table.ForeignKey(
                        name: "FK_RentalRentalItem_RentalItems_RentedItemsId",
                        column: x => x.RentedItemsId,
                        principalTable: "RentalItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalRentalItem_Rentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "Rentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConditionLogs_EmployeeId",
                table: "ConditionLogs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionLogs_RentalId",
                table: "ConditionLogs",
                column: "RentalId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionLogs_VehicleId",
                table: "ConditionLogs",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRental_RentalId",
                table: "CustomerRental",
                column: "RentalId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentCharges_CustomerId",
                table: "IncidentCharges",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentCharges_RentalId",
                table: "IncidentCharges",
                column: "RentalId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_RentalId",
                table: "Invoices",
                column: "RentalId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceLogs_EmployeeId",
                table: "MaintenanceLogs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceLogs_VehicleId",
                table: "MaintenanceLogs",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRentalItem_RentedItemsId",
                table: "RentalRentalItem",
                column: "RentedItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CustomerId",
                table: "Rentals",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_DropoffLocationId",
                table: "Rentals",
                column: "DropoffLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_EmployeeId",
                table: "Rentals",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_InsurancePlanId",
                table: "Rentals",
                column: "InsurancePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_PickupLocationId",
                table: "Rentals",
                column: "PickupLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConditionLogs");

            migrationBuilder.DropTable(
                name: "CustomerRental");

            migrationBuilder.DropTable(
                name: "IncidentCharges");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "MaintenanceLogs");

            migrationBuilder.DropTable(
                name: "RentalRentalItem");

            migrationBuilder.DropTable(
                name: "RentalItems");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "InsurancePlans");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
