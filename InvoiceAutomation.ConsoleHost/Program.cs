namespace InvoiceAutomation.ConsoleHost;

using InvoiceAutomation.Service;
using InvoiceAutomation.Service.Models;



public class Program
{
    static void Main(string[] args)
    {
        CreateInvoice();
    }

    private static void CreateInvoice()
    {
        var pdfCreator = new PdfCreator();      // TODO : Use DI

        using var documentStream = pdfCreator.CreateInvoice(new List<InvoiceLine>());

        using var fileStream = new FileStream(@"d:\Temp\invoice.pdf", FileMode.Create);
        documentStream.CopyTo(fileStream);
        fileStream.Flush();
    }
}