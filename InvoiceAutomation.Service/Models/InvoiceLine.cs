namespace InvoiceAutomation.Service.Models;

using System;

public class InvoiceLine
{
    public DateOnly Date { get; set; }
    public required string Description { get; set; }
    public required decimal Quantity { get; set; }
    public required decimal UnitPrice { get; set; }
    public required decimal TaxRate { get; set; }

    public decimal TotalExcludingTax => Quantity * UnitPrice;
    public decimal TotalIncludingTax => TotalExcludingTax * (1 + TaxRate);
}
