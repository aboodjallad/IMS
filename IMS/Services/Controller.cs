﻿using IMS.Applecation;
using IMS.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Services
{
    internal class Controller
    {
        static ItemService items = new ItemService("Host=localhost; Port=5432; Database=ims; Username=postgres; Password=123");

        public void Add(HttpListenerContext context)
        {
            try
            {
                var request = context.Request;
                Good item = DeserializeJson<Good>(request.InputStream);

                items.AddItem(item.Name, item.Quantity, item.Price, item.category);

                context.Response.StatusCode = 201;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 400;
                WriteResponse(context, ex.Message);
            }
        }

        public void Get(HttpListenerContext context)
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
        public void Update(HttpListenerContext context)
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
                Console.WriteLine(context.ToString(), ex.Message);
            }
        }
        public  int ExtractIdFromUrl(string url)
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


        public void Delete(HttpListenerContext context)
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


        public T DeserializeJson<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string requestBody = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(requestBody);
            }
        }

        public void WriteResponse(HttpListenerContext context, string responseString)
        {
            var buffer = Encoding.UTF8.GetBytes(responseString);
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
        }
    }
}
