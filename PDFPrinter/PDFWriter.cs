using System.Collections.Generic;
using PdfSharp.Pdf;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using Abstract;
using MigraDoc.DocumentObjectModel.Tables;

namespace PDFPrinter
{
    public class PDFWriter : IPDFWriter
    {
        public void Write(string title, IEnumerable<IRowData> tabledata)
        {
            Document document = new Document();
            Section section = document.AddSection();

            AddParagraph(section, title);

            AddTable(section, tabledata);

            var style = document.Styles[StyleNames.Normal]!;
            style.Font.Name = "Arial";

            var pdfRenderer = new PdfDocumentRenderer
            {
                Document = document,
                PdfDocument =
                {
                    PageLayout = PdfPageLayout.SinglePage,
                    ViewerPreferences =
                    {
                        FitWindow = true
                    }
                }
            };

            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save($"{title}.pdf");
        }

        private void AddParagraph(Section section, string text)
        {
            var paragraph = section.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.SpaceAfter = Unit.FromPoint(6);
            paragraph.AddFormattedText(text, TextFormat.Bold);
        }

        public static void AddTable(Section section, IEnumerable<IRowData> tabledata)
        {
            Table table = section.AddTable();
            table.Borders.Visible = true;
            table.TopPadding = 5;

            table.AddColumn(Unit.FromMillimeter(20));
            table.AddColumn(Unit.FromMillimeter(75));
            table.AddColumn(Unit.FromMillimeter(75));
            table.Rows.Height = 20;

            Row row = table.AddRow();
            row.Shading.Color = Colors.LightGray;
            row.Cells[0].AddParagraph().AddFormattedText("Номер", TextFormat.Bold);
            row.Cells[1].AddParagraph().AddFormattedText("Фамилия", TextFormat.Bold);
            row.Cells[2].AddParagraph().AddFormattedText("Имя", TextFormat.Bold);

            foreach (IRowData item in tabledata)
            {
                row = table.AddRow();
                row.Cells[0].AddParagraph(item.Number.ToString());
                row.Cells[1].AddParagraph(item.LastName);
                row.Cells[2].AddParagraph(item.FirstName);
            }
        }
    }
}
