using System.Net;

namespace AdventOfCodeLogic;

public interface ITestDataProvider{
    public Task<string[]> GetLinesAsync();
}

public class StringTestDataProvider : ITestDataProvider
{
    public StringTestDataProvider(string testData)
    {
        TestData = testData;
    }

    public string TestData { get; }

    public async Task<string[]> GetLinesAsync()
    {
        var reader = new StringReader(TestData);
        var result = new List<string>();
        string line;
        while ((line = await reader.ReadLineAsync()) != null){
            result.Add(line);
        }

        return result.ToArray();
    }
}

public class FileTestDataProvider : ITestDataProvider
{
    string _filePath;

    public FileTestDataProvider(string root, int day)
    {
        _filePath = Path.Combine(root, day.ToString(), $"input.txt");
    }

    public async Task<string[]> GetLinesAsync()
    {
        var reader = File.OpenText(_filePath);
        var result = new List<string>();
        string line;
        while ((line = await reader.ReadLineAsync()) != null){
            result.Add(line);
        }

        return result.ToArray();
    }
}

public class SessionDataProvider : ITestDataProvider
{
    static HttpClient _httpClient;
    static SessionDataProvider(){
       
        var handler = new HttpClientHandler();
        handler.CookieContainer = new CookieContainer();
        AddCookieToHandler(handler);
         _httpClient = new HttpClient(handler);

    }

    static void AddCookieToHandler(HttpClientHandler handler)
    {
        // Specify the cookie parameters
        var cookie = new Cookie
        {
            Name = "session",
            Value = "", // Insert your session from website cookie here
            Domain = ".adventofcode.com", 
            Path = "/",             
            HttpOnly = true,       
            Secure = true,        
            Expires = DateTime.Now.AddMonths(1), 
        };

        // Add the cookie to the handler's CookieContainer
        handler.CookieContainer.Add(cookie);
    }

    private string _url;
    
    public SessionDataProvider(int day) {
        _url = $"https://adventofcode.com/2023/day/{day}/input";
    }
    public async Task<string[]> GetLinesAsync()
    {
        var response = await _httpClient.GetAsync(_url);

        var stringResponse = await response.Content.ReadAsStringAsync();

        return await new StringTestDataProvider(stringResponse).GetLinesAsync();
    }
}
