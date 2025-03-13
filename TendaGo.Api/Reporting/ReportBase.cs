using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TendaGo.Api.Reporting
{
    public class ReportBuilder
    {
        public ReportBuilder(string reportPath)
        {
            ReportPath = reportPath;
        }

        protected string ReportPath { get; private set; }

        private readonly ReportParameterCollection parameters = new ReportParameterCollection();

        public ReportParameter AddParameter(string key, object value)
        {
            var parameter = new ReportParameter(key, Convert.ToString(value));
            parameters.Add(parameter);
            return parameter;
        }

        public enum ReportSizeEnum
        {
            HORIZONTAL,
            VERTICAL,
            SLIM
        }
        public ReportResult Render(string reportType, ReportSizeEnum size = ReportSizeEnum.VERTICAL)
        {
            var viewer = new ReportViewer { ProcessingMode = ProcessingMode.Remote };
            viewer.ServerReport.ReportServerCredentials = new ReportCredentials(ReportClient.ReportServerUser, ReportClient.ReportServerPass, "");
            viewer.ServerReport.ReportServerUrl = new Uri($"{ReportClient.ReportServerUrl}/ReportServer/");
            viewer.ServerReport.ReportPath = ReportPath;

            if (parameters != null && parameters.Count > 0)
            {
                viewer.ServerReport.SetParameters(parameters);
            }

            viewer.ServerReport.Refresh();
            string width = " <PageWidth>8.5in</PageWidth>";
            string height = " <PageHeight>11in</PageHeight>";
            string left = " <MarginLeft>1cm</MarginLeft>";
            string right = " <MarginRight>1cm</MarginRight>";
            string top = " <MarginTop>1cm</MarginTop>";
            string bot = " <MarginBottom>1cm</MarginBottom>";

            switch (size)
            {
                case ReportSizeEnum.HORIZONTAL:
                    width = " <PageWidth>11.69in</PageWidth>";
                    height = " <PageHeight>8.27in</PageHeight>";
                    break;
                case ReportSizeEnum.SLIM:
                    width = " <PageWidth>1.88976in</PageWidth>";
                    height = " <PageHeight>5in</PageHeight>";
                    left = " <MarginLeft>0cm</MarginLeft>";
                    right = " <MarginRight>0cm</MarginRight>";
                    top = " <MarginTop>0cm</MarginTop>";
                    bot = " <MarginBottom>0cm</MarginBottom>";
                    break;
                default:
                    width = " <PageWidth>8.5in</PageWidth>";
                    height = " <PageHeight>11in</PageHeight>";
                    break;
            }

            string deviceInfo =
                "<DeviceInfo>" +
                " <OutputFormat>" + reportType + "</OutputFormat>" +
                width +
                height +
                left +
                right +
                top +
                bot +


                "</DeviceInfo>";

            string mimeType;
            string encoding;
            string fileNameExt;
            string[] streams;
            Warning[] warnings;


            byte[] content = viewer.ServerReport.Render(reportType, deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExt,
                out streams,
                out warnings);

            var result = new ReportResult(content)
            {
                MimeType = mimeType,
                Enconding = encoding,
                Extension = fileNameExt,
                Streams = streams,
                Warnings = warnings
            };

            return result;
        }

    }

    public enum ReportFormatEnum
    {
        PDF,
        Excel,
        Word
    }

    public class ReportClient
    {
        private readonly string[] ParameterNames;
        public ReportClient(string reportPath, params string[] parameterNames)
        {
            ReportPath = reportPath.Replace("/", "%2f");
            ParameterNames = parameterNames;
        }

        internal static string ReportServerUrl => ConfigurationManager.AppSettings["Reporting:ServerUrl"];
        internal static string DefaultFolder => ConfigurationManager.AppSettings["Reporting:DefaultFolder"];

        internal static string ReportServerUser => ConfigurationManager.AppSettings["Reporting:Username"];
        internal static string ReportServerPass => ConfigurationManager.AppSettings["Reporting:Password"];

        private readonly string ReportPath;
        public async Task<HttpResponseMessage> Render(string format, params object[] parameterValues)
        {
            try
            {
                parameterValues = parameterValues ?? new object[0];

                var url = $"{ReportServerUrl}/{DefaultFolder}?{ReportPath}&amp;rs:Command=Render&amp;rs:Format={format}";

                // Asignacion dinamica de los valores para los parametros.
                var i = 0;
                for (i = 0; i < ParameterNames.Length; i++)
                {
                    url += $"&{ParameterNames[i]}=";

                    if (parameterValues.Length > i)
                    {
                        url += $"{parameterValues[i]}";
                    }
                }

                if (parameterValues.Length > i)
                {
                    for (i = i * 1; i < parameterValues.Length; i++)
                    {
                        url += $"&{parameterValues[i]}";
                    }
                }

                CredentialCache credentialCache = new CredentialCache();
                credentialCache.Add(new Uri(ReportServerUrl), "NTLM", new NetworkCredential(ReportServerUser, ReportServerPass));

                using (var httpClient = new HttpClient(new HttpClientHandler { Credentials = credentialCache }))
                {
                    httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                    return await httpClient.GetAsync(url);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }


    public class ReportCredentials : IReportServerCredentials
    {
        private string _UserName;
        private string _PassWord;
        private string _DomainName;

        public ReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string user,
         out string password, out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }

}
