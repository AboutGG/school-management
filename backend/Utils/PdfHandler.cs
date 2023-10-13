using System.Reflection;
using System.Text;
using backend.Models;
using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;

namespace backend.Utils;

public class PdfHandler
{
    public static byte[] GeneratePdf<T>(object data, Student? student, string? schoolYear, bool? quarter)
        where T : class
    {
        string htmlContent = string.Empty;
        switch (typeof(T).Name.Trim().ToLower())
        {
            case "subjectgrade":
                var list = data as List<T>;
                htmlContent = GenerateReport("Assets/Report.html", list, student, schoolYear, quarter);
                break;
            case "circular":
                var circular = data as Circular;
                htmlContent = GenerateCircular("Assets/Circular.html", circular);
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

    #region GenerateCircular

    private static string GenerateCircular(string path, Circular circular)
    {
        var htmlContent = File.ReadAllText(path);
        //Sostituisci i segnaposto con i dati dinamici
        htmlContent = htmlContent
            .Replace("{{body}}", circular.Body)
            .Replace("{{location}}", circular.Location)
            .Replace("{{number}}", circular.CircularNumber.ToString())
            .Replace("{{date}}", circular.UploadDate.ToString())
            .Replace("{{header}}", circular.Header)
            .Replace("{{object}}", circular.Object)
            .Replace("{{sign}}", circular.Sign);
        return htmlContent;
    }

    #endregion

    #region GenerateTable

    private static string GenerateTable<T>(string path, List<T> table)
    {
        string htmlPath, htmlContent;
        StringBuilder tableHtml = new StringBuilder();
        htmlPath = "Assets/Table.html";
        htmlContent = File.ReadAllText(htmlPath);

        var propertyNames = table.First().GetType().GetProperties().Select(p => p.Name);
        tableHtml.Append("<table>");

        tableHtml.AppendLine("<tr>");
        foreach (string dummy in propertyNames)
            tableHtml.AppendLine("<th>").AppendLine(dummy).AppendLine("</th>");
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

    #endregion

    #region GenerateReport

    /// <summary> Method used to generate a student report </summary>
    /// <param name="subjectGrade">is the list of the Exams which the student did in a subject</param>
    /// <param name="student">the student's instance used to take all the registry</param>
    /// <param name="schoolYear">the school year which we want to see the quarter</param>
    /// <param name="quarter">indicate which quarter we want to see</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>A PDF file which contains the report of a student</returns>
    private static string GenerateReport<T>(string path, List<T> subjectGrade, Student? student, string? schoolYear,
        bool? quarter)
    {
        string htmlContent;
        StringBuilder tableHtml = new StringBuilder();
        htmlContent = File.ReadAllText(path);

        //Prendo il nome degli attributi all'interno di subjectGrade
        var propertyNames = subjectGrade.First().GetType().GetProperties().Select(p => p.Name);

        //Questa parte di codice serve per creare l'intestazione della tabella
        tableHtml.Append("<table>");
        tableHtml.AppendLine("<tr>");
        foreach (string el in propertyNames)
            tableHtml.AppendLine("<th>").AppendLine(el).AppendLine("</th>");
        tableHtml.AppendLine("</tr>");

        //In questa parte di codice invece estraggo i valori degli attributi in modo da compilare la tabella
        foreach (var item in subjectGrade)
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

        //Questi metodi vengono utilizzati per sostituire i campi con le deternimate diciture all'interno del primo argomento delle funzioni
        //in modo da manipolare i dati del file pdf e da renderlo dinamico
        htmlContent = htmlContent.Replace("{{tabelle}}", tableHtml.ToString())
            .Replace("{{quarter}}", quarter != null && quarter == true ? "primo quadrimestre" : "finale")
            .Replace("{{name}}", student.Registry.Name)
            .Replace("{{surname}}", student.Registry.Surname)
            .Replace("{{birth}}", student.Registry.Birth.ToString())
            .Replace("{{gender}}", student.Registry.Gender)
            .Replace("{{schoolYear}}", schoolYear)
            .Replace("{{relaseDate}}", DateOnly.FromDateTime(DateTime.UtcNow).ToString())
            .Replace("{{classroom}}", student.Classroom.Name);

        return htmlContent;
    }

    #endregion

}

