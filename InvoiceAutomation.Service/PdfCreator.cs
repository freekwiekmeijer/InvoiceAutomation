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
    public Stream CreateInvoice(IEnumerable<InvoiceLine> invoiceLines)
    {
        var outputStream = new MemoryStream();

        Document.Create(container =>
        {
            container.Page(page => 
            {
                page.Size(PageSizes.A4);
                page.Margin(20, Unit.Millimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(ts => ts.FontSize(20));

                page.Header()
                    .Text("Hello PDF!")
                    .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
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
