using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using HtmlAgilityPack;
using static CourseCalendarApp.ViewModels.CalendarViewModel;

namespace CourseCalendarApp.FEU;

public class PortalClient
{
    private const string? UserAgent
        = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/127.0.0.0 Safari/537.36 Edg/127.0.0.0";

    public IEnumerable<CORScheduleItem> ParseScheduleHtml(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var rows = doc.DocumentNode.SelectNodes("//table[@class='assessment_schedule']//tr[not(@class='header')]");

        if (rows == null)
            return [];

        return rows
           .Select(row => row.SelectNodes("td"))
           .Where(cells => cells is { Count: 7 })
           .Select(cells => new CORScheduleItem(
                cells[0].InnerText.Trim(),
                cells[1].InnerText.Trim(),
                cells[2].InnerText.Trim(),
                int.Parse(cells[3].InnerText.Trim()),
                cells[4].InnerText.Trim(),
                cells[5].InnerText.Trim(),
                cells[6].InnerText.Trim()));
    }

    public async Task<HttpClient> LoginAsync(string studentNumber, string password)
    {
        var client = new HttpClient();
        var loginData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("student_number", studentNumber),
            new KeyValuePair<string, string>("password", password),
            new KeyValuePair<string, string>("sign_in", "Sign In")
        });

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "https://students.feutech.edu.ph/auth/login")
        {
            Content = loginData
        };

        loginRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
        loginRequest.Headers.Add("User-Agent", UserAgent);

        var response = await client.SendAsync(loginRequest);

        if (response.StatusCode != HttpStatusCode.OK) throw new Exception("Login failed");

        return client;
    }

    public async Task<IEnumerable<(string yearClass, string yearValue)>> GetTermsAndYearsAsync(HttpClient client)
    {
        var corPageHtml = await GetScheduleAsync(client);

        var doc = new HtmlDocument();
        doc.LoadHtml(corPageHtml);

        var yearNodes = doc.DocumentNode.SelectNodes("//select[@id='school_year']/option");
        return yearNodes
           .Select(node => new
                { Term = node.GetAttributeValue("value", null), Year = node.GetAttributeValue("class", null) })
           .Where(node => !string.IsNullOrEmpty(node.Term) && !string.IsNullOrEmpty(node.Year))
           .Select(node => (yearClass: node.Year, yearValue: node.Term));
    }

    public async Task<IEnumerable<CORScheduleItem>> GetStudentScheduleAsync(
        string studentNumber, string password,
        string term, string schoolYear)
    {
        var client = await LoginAsync(studentNumber, password);
        var termsAndYears = await GetTermsAndYearsAsync(client);

        // Optionally, you can print out the terms and years for selection purposes
        foreach (var (Term, SchoolYear) in termsAndYears)
        {
            Console.WriteLine($"Term: {Term}, School Year: {SchoolYear}");
        }

        var termPageHtml = await GetScheduleAsync(client, term, schoolYear);
        return ParseScheduleHtml(termPageHtml);
    }

    public async Task<string> GetScheduleAsync(HttpClient client)
    {
        var corPageRequest = new HttpRequestMessage(HttpMethod.Get, "https://students.feutech.edu.ph/accounts/saf");
        corPageRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
        corPageRequest.Headers.Add("User-Agent", UserAgent);

        var response = await client.SendAsync(corPageRequest);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetScheduleAsync(HttpClient client, string term, string schoolYear)
    {
        var termData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("term", term),
            new KeyValuePair<string, string>("school_year", schoolYear),
            new KeyValuePair<string, string>("submit", "Submit")
        });

        var termRequest = new HttpRequestMessage(HttpMethod.Post, "https://students.feutech.edu.ph/accounts/saf")
        {
            Content = termData,
            Method = HttpMethod.Post
        };

        termRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
        termRequest.Headers.Add("User-Agent", UserAgent);

        var response = await client.SendAsync(termRequest);
        return await response.Content.ReadAsStringAsync();
    }
}