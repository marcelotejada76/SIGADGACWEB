using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace SistemaIntegradoGestion.Reporte
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string reportFolder = Session["UrlReporte"].ToString();
                rvSiteMapping.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rvSiteMapping.ServerReport.ReportServerUrl = new Uri(System.Configuration.ConfigurationManager.AppSettings["SSRSRReportsUrl"].ToUpper()); // Add the Reporting Server URL  
                rvSiteMapping.ServerReport.ReportPath = reportFolder; //"/Report Project1/ReporteSIGPOA";//String.Format("{0}{1}", reportFolder, Request["ReportName"].ToString());
                rvSiteMapping.SizeToReportContent = true;
                rvSiteMapping.ShowPrintButton = false;
                rvSiteMapping.ShowDocumentMapButton = true;
                rvSiteMapping.Width = Unit.Percentage(100);
                rvSiteMapping.Height = Unit.Percentage(100);
                rvSiteMapping.ShowBackButton = false;
                rvSiteMapping.ZoomMode = ZoomMode.Percent;
                rvSiteMapping.ZoomPercent = 100;
                rvSiteMapping.ServerReport.Refresh();
            }
        }
    }
}