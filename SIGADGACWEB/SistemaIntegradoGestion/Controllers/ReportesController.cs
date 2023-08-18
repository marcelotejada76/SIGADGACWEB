using CapaModelo;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SistemaIntegradoGestion.Controllers
{
    public class ReportesController : Controller
    {
        private static tbUsuario SesionUsuario;
        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult reporte(string reporteurl, string nombreReporte)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            string ssrsurl = ConfigurationManager.AppSettings["SSRSRReportsUrl"].ToString();
            ViewBag.ReportViewer = null;
            ViewBag.NombreViewer = null;
            if (reporteurl != null)
            {
                SesionUsuario = (tbUsuario)Session["Usuario"];

                string url = Uri.EscapeUriString("./.." + System.Configuration.ConfigurationManager.AppSettings.Get("ReportViewerURL"));
                ViewData["reportURL"] = Uri.EscapeUriString("~/../../" + System.Configuration.ConfigurationManager.AppSettings.Get("ReportViewerURL"));
                Session["UrlReporte"] = reporteurl;               
                ViewBag.NombreViewer = nombreReporte;
            }


            return View();
        }

        public ActionResult ReporteSIPOADirecciones(string reporteurl, string nombreReporte)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            string ssrsurl = ConfigurationManager.AppSettings["SSRSRReportsUrl"].ToString();
            ViewBag.ReportViewer = null;
            ViewBag.NombreViewer = null;
            string _eod = string.Empty;
            string _anio = "2023";
            if (Session["Usuario"] != null)
                SesionUsuario = (tbUsuario)Session["Usuario"];

            if (reporteurl != null)
            {
                var SesionUsuario = (tbUsuario)Session["Usuario"];
                if (SesionUsuario.CodigoCiudad.Equals("SEQM"))
                {
                    _eod = "PLANTA CENTRAL";
                }
                else
                {
                    _eod = "ZONAL";
                }

                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Remote;
                viewer.ZoomMode = ZoomMode.PageWidth;
                viewer.SizeToReportContent = true;
                viewer.Width = Unit.Percentage(100);
                viewer.Height = Unit.Pixel(100);
                viewer.AsyncRendering = true;
                viewer.ServerReport.ReportServerUrl = new Uri(ssrsurl);
                viewer.ServerReport.ReportPath = reporteurl;
                ReportParameter[] reportParameter = new ReportParameter[3];
                reportParameter[0] = new ReportParameter("anio", _anio);
                reportParameter[1] = new ReportParameter("eod", _eod);
                reportParameter[1].Visible = false;
                reportParameter[2] = new ReportParameter("direcciones", SesionUsuario.CodigoSubsistema);
                reportParameter[2].Visible = false;

                viewer.ServerReport.SetParameters(reportParameter);

                //Direccion por usuario
                viewer.ServerReport.Refresh();
                ViewBag.ReportViewer = viewer;
                ViewBag.NombreViewer = nombreReporte;
            }


            return View();
        }

    }
}