using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalStore.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public required Rental Rental { get; set; }
        public decimal SubTotal { get; set; }     // Price before tax
        public decimal TaxRate { get; set; }      // The applicable tax rate at the time of billing
        public decimal TaxAmount { get; set; }    // SubTotal * TaxRate
        public decimal Total { get; set; }        // SubTotal + TaxAmount
        public DateTime DateIssued { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Unpaid;
    }
}
