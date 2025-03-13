using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TendaGo
{
    public static class Tools
    {
        public static byte[] ToByteArray(this Stream input)
        {
            MemoryStream ms = new MemoryStream();
            input.CopyTo(ms);
            return ms.ToArray();
        }

        public static string ConvertirByteArrayAImagen(byte[] imagen)
        {
            try
            {
                if (imagen != null && imagen.Any())
                {
                    string imreBase64Data = Convert.ToBase64String(imagen);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    return imgDataURL;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }         

        public static string HashToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        public static string GetApiUrlBase()
        {
            var url = ConfigurationManager.AppSettings["api:ApiRestUrl"];
            return url;
        }


        public static string GetApiKey()
        {
            var apiKey = ConfigurationManager.AppSettings["api:ApiKey"];
            return apiKey;
        }

        public static string ToHtml(this NameValueCollection collection)
        {
            var sb = new StringBuilder();
            sb.Append("<ul>");

            if (collection?.Keys != null)
                foreach (string key in collection.Keys)
                {
                    sb.Append($"<li>{key}:");

                    var values = collection?.GetValues(key);
                    if (values != null)
                        foreach (var item in values)
                        {
                            sb.Append($"<br>{item}");
                        }

                    sb.Append($"</li>");
                }

            sb.Append($"</ul>");

            return sb.ToString();
        }

        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string Left(this string text, int length)
        {
            return text?.Substring(0, text.Length > length ? length : text.Length);
        }


        public static void SendMail(string recipients, string subject, string html, params Attachment[] attachments)
        {
            SendMailAsync(recipients, subject, html, attachments).RunSynchronously();
        }

        public static async Task SendMailAsync(string recipients, string subject, string html, params Attachment[] attachments)
        {
            var username = ConfigurationManager.AppSettings["Email:Username"];
            var password = ConfigurationManager.AppSettings["Email:Password"];
            var port = Convert.ToInt32(ConfigurationManager.AppSettings["Email:Port"]);
            var host = ConfigurationManager.AppSettings["Email:Host"];
            var enableSsl = ConfigurationManager.AppSettings["Email:EnableSSL"];
            var sender = ConfigurationManager.AppSettings["Email:Sender"];

            var mailMessage = new MailMessage
            {
                Subject = subject,
                Body = html,
                IsBodyHtml = true,
                From = new MailAddress(sender)
            };

            foreach (var recipient in recipients.Split(',', ';'))
            {
                mailMessage.To.Add(new MailAddress(recipient));
            }
             
            foreach (var attachment in attachments)
            {
                mailMessage.Attachments.Add(attachment);
            }

            var client = new SmtpClient
            {
                Credentials = new NetworkCredential(username, password),
                Port = port,
                Host = host,
                EnableSsl = (enableSsl?.ToLower() == "true" || enableSsl == "1")
            };

            await client.SendMailAsync(mailMessage);
        }

        public static string GetHtmlTemplate<T>(string template, T model)
        {
            if (!template.Contains("."))
                template += ".html";

            string path = Path.Combine("Templates", template);
            string html = default;

            if (File.Exists(path))
            {
                html = File.ReadAllText(path);
                var type = typeof(T);
                var props = type.GetProperties();

                foreach (var item in props)
                {
                    try
                    {
                        var name = item.Name.ToUpper();
                        var original = item.GetValue(model);

                        // Si es decimal, lo redondeo a dos decimales
                        if (original is decimal)
                            original = decimal.Round((decimal)(original), 2);

                        var value = WebUtility.HtmlEncode($"{original}");
                        html = html.Replace($"{{{name}}}", $"{value}");
                    }
                    catch
                    {
                        // Manejo de excepciones si ocurre un error en la reflexión
                    }
                }
            }

            return html;
        }

        public static async Task SendMailAsync<T>(T model, string email, string subject, string template, params Attachment[] attachments)
        {
            var html = GetHtmlTemplate(template, model);
            if (!string.IsNullOrEmpty(html))
            { 
                await SendMailAsync($"{email}", subject, html, attachments);
            }

            throw new Exception($"No se encontró la plantilla para el correo {subject} para {email}");
        }

    }
}