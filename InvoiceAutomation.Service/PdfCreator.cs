namespace InvoiceAutomation.Service;

using System.Collections.Generic;
using System.IO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using InvoiceAutomation.Service.Interfaces;
using InvoiceAutomation.Service.Models;

public class PdfCreator : IPdfCreator
{
    public PdfCreator()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public Stream CreateInvoice(IEnumerable<InvoiceLine> invoiceLines)
    {
        var outputStream = new MemoryStream();

        Document.Create(container =>
        {
            container.Page(page => 
            {
                page.Size(PageSizes.A4);
                page.Margin(15, Unit.Millimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(ts => ts.FontSize(12));

                page.Header()
                    .Text("Hello PDF!")
                    .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                page.Content()
                    .PaddingVertical(10, Unit.Millimetre)
                    .Column(x =>
                    {
                        x.Spacing(20);
                        x.Item().Text(Placeholders.LoremIpsum());
                        x.Item().Image(Placeholders.Image(200, 100));
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
            });
        })
        .GeneratePdf(outputStream);

        outputStream.Seek(0, SeekOrigin.Begin);
        return outputStream;
    }
}
