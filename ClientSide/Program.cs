HttpClient client = new HttpClient();
HttpResponseMessage response = await client.GetAsync("http://localhost:27001/");

var text = await response.Content.ReadAsStringAsync();
Console.WriteLine(text);