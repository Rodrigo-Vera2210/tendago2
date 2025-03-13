<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Viewer.aspx.cs" Inherits="TendaGo.Web.Reports.Viewer" EnableEventValidation="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>  
   
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>  
<!doctype html>  
 <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE11">  
   
<html xmlns="http://www.w3.org/1999/xhtml">  
<head id="Head1" runat="server">  
    <title></title>  
      
</head>  
<body>  
    <div style="Width:auto;"> 

        <form id="form1" runat="server"  style="width:100%; height:100%;">  
        <div>  
            <asp:ScriptManager ID="scriptManagerReport" runat="server">  
             </asp:ScriptManager>  
   
            <rsweb:ReportViewer  runat ="server" Width="100%" Height="100%" 
                    AsyncRendering="False" 
                    SizeToReportContent="true"
                    KeepSessionAlive="True" 
                    ProcessingMode="Remote"  ShowToolBar="False"
                    id="rvSiteMapping" >
                   <ServerReport ReportPath="/tendago/Salida" ReportServerUrl="http://localhost/ReportServer" />
            
            </rsweb:ReportViewer>                    
        </div>  
        </form>
    </div>
</body>  
</html>  