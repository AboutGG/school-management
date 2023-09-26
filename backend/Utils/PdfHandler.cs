using System.Reflection;
using System.Text;
using backend.Models;
using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using Path = iText.Kernel.Geom.Path;

namespace backend.Utils;

public class PdfHandler
{
    public static byte[] GeneratePdf(string type, List<object>? table, Circular? data)
    {
        string htmlPath, htmlContent = string.Empty;
        // StringBuilder tableHtml = new StringBuilder();
        // if (type == "table")
        // {
        //     htmlPath = "Assets/Table.html";
        //     htmlContent = File.ReadAllText(htmlPath);
        //     
        //     
        //     var propertyNames = table.First().GetType().GetProperties().Select(p => p.Name);
        //     tableHtml.Append("<table>");
        //     
        //     tableHtml.AppendLine("<tr>");
        //     foreach (string dummy in propertyNames)
        //         tableHtml.AppendLine("<td>").AppendLine(dummy).AppendLine("</td>");
        //     tableHtml.AppendLine("</tr>");
        //
        //
        //     foreach (var item in table)
        //     {
        //         tableHtml.AppendLine("<tr>");
        //         foreach (PropertyInfo property in item.GetType().GetProperties()) //PropertyInfo represents the information of a property of a class.
        //         {
        //             if(property != null)
        //                 tableHtml.AppendLine("<td>").AppendLine(property.GetValue(item).ToString()).AppendLine("</td>"); //takes the property's value of a specific object in this case: property
        //         }
        //         tableHtml.AppendLine("</tr>");
        //     }
        //     tableHtml.Append("</table>");
        //     htmlContent = htmlContent.Replace("{{tabelle}}", tableHtml.ToString());
        // }
        // else
        // {
        //     htmlPath = "Assets/Circular.html";
        //     htmlContent = File.ReadAllText(htmlPath);
        //     // // Sostituisci i segnaposto con i dati dinamici
        //     // htmlContent = htmlContent.Replace("{{Title}}", data.Title)
        //     //     .Replace("{{SchoolName}}", data.SchoolName)
        //     //     .Replace("{{body}}", data.Body);
        //     // //.Replace("{{eta}}", "30");
        // }
        
        switch (type.Trim().ToLower())
        {
            case "report":
               htmlContent = GenerateCircular("Assets/Circular.html", data);
              break;  
            case "circular":
                htmlContent = GenerateTable("Assets/Table.html",table);
                break;
            default:
                throw new Exception("INVALID_PDF_TYPE");
        }
        
        using var memoryStream = new MemoryStream();
        {
            var writer = new PdfWriter(memoryStream);
            var pdf = new PdfDocument(writer);
            pdf.SetDefaultPageSize(PageSize.A4);
            var document = new Document(pdf);
            HtmlConverter.ConvertToPdf(htmlContent, pdf, new ConverterProperties());
            document.Close();
            return memoryStream.ToArray();
        }
        
    }
    

    private static string GenerateCircular(string path, Circular circular)
    {
        var htmlContent = File.ReadAllText(path);
        //Sostituisci i segnaposto con i dati dinamici
        htmlContent = htmlContent
            .Replace("{{body}}", circular.Body)
            .Replace("{{location}}", circular.Location)
            .Replace("{{number}}", circular.CircularNumber.ToString())
            .Replace("{{date}}", circular.UploadDate.ToString())
            .Replace("{{header}}", circular.Header);
        return htmlContent;
    }

    private static string GenerateTable(string path, List<object> table)
    {
        string htmlPath, htmlContent;
        StringBuilder tableHtml = new StringBuilder();
        htmlPath = "Assets/Table.html";
        htmlContent = File.ReadAllText(htmlPath);


        var propertyNames = table.First().GetType().GetProperties().Select(p => p.Name);
        tableHtml.Append("<table>");

        tableHtml.AppendLine("<tr>");
        foreach (string dummy in propertyNames)
            tableHtml.AppendLine("<td>").AppendLine(dummy).AppendLine("</td>");
        tableHtml.AppendLine("</tr>");


        foreach (var item in table)
        {
            tableHtml.AppendLine("<tr>");
            foreach (PropertyInfo property in
                     item.GetType().GetProperties()) //PropertyInfo represents the information of a property of a class.
            {
                if (property != null)
                    tableHtml.AppendLine("<td>").AppendLine(property.GetValue(item).ToString())
                        .AppendLine("</td>"); //takes the property's value of a specific object in this case: property
            }

            tableHtml.AppendLine("</tr>");
        }

        tableHtml.Append("</table>");
        htmlContent = htmlContent.Replace("{{tabelle}}", tableHtml.ToString());

        return htmlContent;
    }

    private static void GenerateReport(string path)
    {
        
    }
}

