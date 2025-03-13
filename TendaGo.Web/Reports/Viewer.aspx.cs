using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using TendaGo.Web.Models;
using System.Net;
using Microsoft.Reporting.WebForms;

namespace TendaGo.Web.Reports
{
    public partial class Viewer : System.Web.UI.Page
    {
        public int IdLocalSelected => int.TryParse(Session["LocalSeleccionado"]?.ToString(), out int result) ? result : 0;
        public string ReportServer => ConfigurationManager.AppSettings["URLServidor"];
        public string ReportUser => ConfigurationManager.AppSettings["UsuarioReporte"];
        public string ReportPass => ConfigurationManager.AppSettings["ContrasenaReporte"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string ReportURL = "";
                    if (Request.QueryString.Count>0)
                    {
                        ReportURL = Request.QueryString[0];
                    }
                    
                    rvSiteMapping.ServerReport.ReportServerCredentials = new ReportCredentials(ReportUser, ReportPass, "");
                    rvSiteMapping.ServerReport.ReportServerUrl = new Uri(ReportServer + "/ReportServer"); // Add the Reporting Server URL  
                    rvSiteMapping.ServerReport.ReportPath = ReportURL;

                    rvSiteMapping.Width = new Unit(100, UnitType.Percentage);
                    rvSiteMapping.Height = new Unit(100, UnitType.Percentage);
                    rvSiteMapping.AsyncRendering = false;
                    rvSiteMapping.SizeToReportContent = true;
                    rvSiteMapping.KeepSessionAlive = true;
                    rvSiteMapping.ProcessingMode = ProcessingMode.Remote;
                    rvSiteMapping.ShowParameterPrompts = false;
                     
                    var parametros = new ReportParameterCollection();
                    
                    for (int i = 0; i < Request.Form.Count; i++)
                    {
                        var parameterName = Request.Form.AllKeys[i];
                        var parameterValue = Request.Form[parameterName];

                        parametros.Add(new ReportParameter(parameterName, parameterValue));
                    }

                    if (parametros.Count > 0)
                    {
                        rvSiteMapping.ServerReport.SetParameters(parametros);
                    }

                    rvSiteMapping.ServerReport.Refresh();
                }
                catch (Exception ex)
                {

                }
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