using System;
using System.IO;
using System.Net;
using System.Text;
using IMS.Applecation;
using IMS.Services;
using Newtonsoft.Json; 
/*
class Controller
{
    static HttpListener listener;
    static ItemService items = new  ItemService("Host=localhost; Port=5432; Database=ims; Username=postgres; Password=123");

    static void Main(string[] args)
    {
        listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:5000/");
        listener.Start();
        Console.WriteLine("Listening...");

        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            string path = request.Url.AbsolutePath.ToString();
            if (path.Contains("/items/") && request.HttpMethod == "PUT")
            {
                Update(context);
            }
            else if (path.Contains("/items/") && request.HttpMethod == "DELETE")
            {
                Delete(context);
            }
            else if (path.Contains("/item") && request.HttpMethod == "POST")
            {
                Add(context);
            }
            else if (path.Contains("/item") && request.HttpMethod == "GET")
            {
                Get(context);
            }
            else if (path.Contains("/item/") && request.HttpMethod == "GET")
            {
                Get(context);
            }
            else
            {
                response.StatusCode = 404;

            }

            response.OutputStream.Close();
        }
    }

    static void Add(HttpListenerContext context)
    {
        try
        {
            var request = context.Request;
            Good item = DeserializeJson<Good>(request.InputStream);

            items.AddItem(item.Name,item.Quantity,item.Price);

            context.Response.StatusCode = 201; 
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 400; 
            WriteResponse(context, ex.Message);
        }
    }

    static void Get(HttpListenerContext context)
    {
        try
        {
            var itemsList = items.GetItems(); 
            WriteResponse(context, JsonConvert.SerializeObject(items));
            context.Response.StatusCode = 200;
            foreach (var item in itemsList)
            {

                Console.WriteLine(item.ToString());
            }
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500; 
            WriteResponse(context, ex.Message);
        }
    }
    static void Update(HttpListenerContext context)
    {
        try
        {
           
            string url = context.Request.RawUrl;
            int itemId = ExtractIdFromUrl(url); 

            Good itemToUpdate = DeserializeJson<Good>(context.Request.InputStream);
            bool updateSuccess = items.UpdateItem(itemId, itemToUpdate.Name, itemToUpdate.Quantity, itemToUpdate.Price); 

            if (updateSuccess)
            {
                context.Response.StatusCode = 204; 
            }
            else
            {
                Console.WriteLine(context.ToString(), "Item not found");
                context.Response.StatusCode = 404;
                
            }
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 400; 
            Console.WriteLine( context.ToString(), ex.Message);
        }
    }
    private static int ExtractIdFromUrl(string url)
    {
        var segments = url.Split('/');
        if (segments.Length >= 3)
        {
            var idSegment = segments[segments.Length - 1];
            if (int.TryParse(idSegment, out int itemId))
            {
                return itemId;
            }
        }
        throw new ArgumentException("Invalid URL format or missing item ID.");
    }


    static void Delete(HttpListenerContext context)
    {
        try
        {
            string url = context.Request.RawUrl;
            int itemId = ExtractIdFromUrl(url); 

            bool deleteSuccess = items.DeleteItem(itemId);

            if (deleteSuccess)
            {
                context.Response.StatusCode = 204; 
            }
            else
            {
                context.Response.StatusCode = 404; 
                WriteResponse(context, "Item not found.");
            }
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 400; 
            WriteResponse(context, ex.Message);
        }
    }


    static T DeserializeJson<T>(Stream stream)
    {
        using (var reader = new StreamReader(stream, Encoding.UTF8))
        {
            string requestBody = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(requestBody);
        }
    }

    static void WriteResponse(HttpListenerContext context, string responseString)
    {
        var buffer = Encoding.UTF8.GetBytes(responseString);
        context.Response.ContentLength64 = buffer.Length;
        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
    }
}

//*/


