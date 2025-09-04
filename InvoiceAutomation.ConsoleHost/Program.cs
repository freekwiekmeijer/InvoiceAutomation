namespace InvoiceAutomation.ConsoleHost;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InvoiceAutomation.Service;
using InvoiceAutomation.Service.Interfaces;
using InvoiceAutomation.Service.Models;

public class Program
{
    private static IHost AppHost { get; set; }

    static void Main(string[] args)
    {
        SetupDependencyInjection();
        CreateInvoice();
    }

    private static void SetupDependencyInjection()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddTransient<IPdfCreator, PdfCreator>();
            })
            .Build();
    }

    private static void CreateInvoice()
    {
        var pdfCreator = AppHost.Services.GetRequiredService<IPdfCreator>();

        using var documentStream = pdfCreator.CreateInvoice(new List<InvoiceLine>());

        using var fileStream = new FileStream(@"d:\Temp\invoice.pdf", FileMode.Create);
        documentStream.CopyTo(fileStream);
        fileStream.Flush();
    }
}