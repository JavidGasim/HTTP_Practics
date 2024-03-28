using System.Net;

using HttpListener listener = new HttpListener();
var count = 0;

listener.Prefixes.Add("http://localhost:27001/");

listener.Start();

while (true)
{
    var context = listener.GetContext();

    _ = Task.Run(() =>
    {
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;

        var url = request.RawUrl;
        Console.WriteLine(url);

        if (url == "/")
        {
            using var writer = new StreamWriter(response.OutputStream);

            var index = File.ReadAllText("MySite/index.html");
            writer.Write(index);
        }

        else
        {
            var urls = url.Split('/').ToList();

            if (urls[1] == "MySite")
            {
                var files = Directory.GetFiles(urls[1]);

                foreach (var file in files)
                {
                    var temp = file.Split("\\").ToList();
                    var temp1 = $"{temp}.html";
                    var temp2 = $"{urls[2]}.html";
                    if (temp[1] == urls[2] || temp1 == temp2)
                    {
                        using StreamWriter writer = new StreamWriter(response.OutputStream);
                        var index = File.ReadAllText($"MySite/{temp[1]}");
                        writer.Write(index);
                        count = 1;
                    }


                }
                if (count == 1)
                {
                    using StreamWriter writer = new StreamWriter(response.OutputStream);
                    var index = File.ReadAllText($"MySite/404.html");
                    writer.Write(index);
                }
            }
        }
    });
}