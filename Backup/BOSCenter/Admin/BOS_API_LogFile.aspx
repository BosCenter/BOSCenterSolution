<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_API_LogFile.aspx.vb" Inherits="BOSCenter.BOS_API_LogFile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>
    <div class="container">
	<div class="row">
    	<div class="col-sm-10 col-sm-offset-1">
        
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	API Log Details</div>
                </div>
            </div>
        	<div class="log_form1">
            	 <div class="form-group">
                       
                        <div class="col-sm-8 mar_top5">
                           
                            <asp:DropDownList ID="ddlAPILogs" runat="server" class='form-control' >
 
                                <asp:ListItem>:::: Select API Logs ::::</asp:ListItem>
                                <asp:ListItem>Recharge API</asp:ListItem>
                                <asp:ListItem>Money Transfer API</asp:ListItem>
                                <asp:ListItem>PAN Card API</asp:ListItem>
                                <asp:ListItem>AEPS API</asp:ListItem>
 
    </asp:DropDownList>
   
                        </div>
                        <div class="col-sm-4 mar_top5 ">
                         <asp:Button ID="btnGo" runat="server" Text="Go" 
                             cssclass="btn btn-primary" ValidationGroup="a" />
                             &nbsp;
                         <asp:Button ID="btnClearLogs" runat="server" Text="Clear Logs" 
                             cssclass="btn btn-danger" ValidationGroup="a" />
                             &nbsp;
                              <asp:Button ID="btnReset" runat="server" Text="Reset" 
                             cssclass="btn btn-primary" ValidationGroup="a" />
                        </div>
                       
                      </div>
                       <div class="clearfix" style="margin-bottom:5px;"></div>

                    <form class="form-horizontal">
                      <div class="form-group">
                         <div class="col-sm-12">
                            <asp:TextBox ID="txtLogDetails" cssclass="form-control"  Height="500px" 
                                 TextMode="MultiLine"  runat="server"></asp:TextBox>
                        </div>
                      </div>
                       <div class="clearfix" style="margin-bottom:5px;"></div>
                                      <div class="form-group">
                       <div class="col-sm-12">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                         <asp:Label ID="lblError" runat="server"></asp:Label>    
                            <asp:Label ID="lblUpadate" runat="server" Visible="False"></asp:Label>
                              <asp:Label ID="lblSessionFlag" runat="server" Visible="False"></asp:Label>
                              <asp:Label ID="lblComplaintID" runat="server" Visible="False"></asp:Label>
                        </div>
                      </div>


                  </form>
                    <div class="clearfix" style="margin-bottom:5px;"></div>
                

            </div>
            
            
        </div>
    </div>
</div>



 <asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="DeletePopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="DeletePopup" runat="server" Width="350px" style="display:none;"  >

<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td align="center" bgcolor="Silver"   >&nbsp;</td>
</tr>
    <tr>
        <td align="center" bgcolor="Silver">
            <strong>Confirmation Dialog</strong>
            <br />
        </td>
    </tr>
    <tr>
        <td align="center" bgcolor="Silver">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
           <asp:Label ID="lblDialogMsg" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnok" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn btn-primary" />
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>

    </asp:Panel>


          <asp:Button ID="Button2" runat="server" Text="Button" style="display:none;"/>
<asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="Button2" PopupControlID="DeleteDocumentPopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancelDeleteDocument" >
</asp:ModalPopupExtender>

<asp:Panel ID="DeleteDocumentPopup" runat="server" Width="400px" style="display:none;" >
<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td align="center" bgcolor="Silver"   >&nbsp;</td>
</tr>
    <tr>
        <td align="center" bgcolor="Silver">
            <strong>::: Alert Dialog :::</strong>
            <br />
        </td>
    </tr>
    <tr>
        <td align="center" bgcolor="Silver">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
           <asp:Label ID="lblDeleteInfo" runat="server" Text="This Action will Remove the document Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"></asp:Label></td>
           <asp:Label ID="lblDeleteDocumentInfo" runat="server" Text=""></asp:Label>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnDelete_Document" runat="server" Text="Yes" Width="80px" CssClass="btn btn-primary"/>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancelDeleteDocument" runat="server" Text="No" Width="80px" CssClass="btn btn-primary" />
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>
    </asp:Panel>


    </ContentTemplate>


</asp:UpdatePanel> 

</asp:Content>
