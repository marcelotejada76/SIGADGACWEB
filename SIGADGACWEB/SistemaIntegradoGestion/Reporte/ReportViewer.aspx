<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="SistemaIntegradoGestion.Reporte.ReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%; margin: 0; padding: 0;">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        html, body, form, #form1 {
            height: 100%;
            margin: 0;
            padding: 0;
            overflow: hidden;
        }
    </style>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="form1" runat="server" style="height: 100%; margin: 0; padding: 0;">
        <div style="height: 100%; margin: 0; padding: 0;">
             <asp:ScriptManager ID="ScriptManager1" runat="server">                
            </asp:ScriptManager>
            <rsweb:ReportViewer id="rvSiteMapping" runat ="server" BackColor="White"
            Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos=" (Collection)"
            ProcessingMode="Remote" ShowBackButton="False" ShowFindControls="False"
            ShowPageNavigationControls="true" SizeToReportContent="False"
            ToolBarItemBorderColor="White" ToolBarItemHoverBackColor="White"
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%">
            </rsweb:ReportViewer>  
        </div>
    </form>
</body>
</html>
