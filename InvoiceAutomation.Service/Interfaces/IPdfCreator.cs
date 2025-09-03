namespace InvoiceAutomation.Service.Interfaces;

using System.Collections.Generic;
using System.IO;
using InvoiceAutomation.Service.Models;

public interface IPdfCreator
{
    Stream CreateInvoice(IEnumerable<InvoiceLine> invoiceLines);
}
