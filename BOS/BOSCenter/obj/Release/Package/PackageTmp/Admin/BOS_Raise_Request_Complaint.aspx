<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_Raise_Request_Complaint.aspx.vb" Inherits="BOSCenter.BOS_Raise_Request_Complaint" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>
    <div class="container">
	<div class="row">
    	<div class="col-sm-8 col-sm-offset-2">
        
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	Raise Request Complaint
                    </div>
                </div>
            </div>
        	<div class="log_form1">
            	 <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Product <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-9">
                           
                            <asp:DropDownList ID="ddlProductService" runat="server" class='form-control' 
                                AutoPostBack="True">
 
    </asp:DropDownList>

                        </div>
                      </div>
                       <div class="clearfix" style="margin-bottom:5px;"></div>

                        <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Problem <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-9">
                           
                            <asp:DropDownList ID="ddlProblem" runat="server" class='form-control'></asp:DropDownList>

                        </div>
                      </div>
                       <div class="clearfix" style="margin-bottom:5px;"></div>



                    <form class="form-horizontal">
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Complaint<asp:Label ID="Label1" ForeColor="Red" runat="server" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtComments" cssclass="form-control" TextMode="MultiLine"  runat="server"></asp:TextBox>
                        </div>
                      </div>
                       <div class="clearfix" style="margin-bottom:5px;"></div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Upload Attachments</label>
                        <div class="col-sm-9">
                           
                         <asp:FileUpload ID="FileUpload_AgeProof" runat="server"  />

    <asp:Button ID="btnUpload_AgeProof" runat="server"
        Text="Upload" />
    &nbsp;<asp:Button ID="btnDeleteUpload_AgeProof" runat="server" 
         Text="Remove" Enabled="False" />
    <asp:Image ID="Image_AgeProof" runat="server" BorderStyle="None" CssClass="" 
        Height="61px" ImageUrl="~/images/uploadimage.png" Width="71px" />
    <asp:Label ID="lblErrorImage_PanCard" runat="server"></asp:Label>

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

  <div class="form-group">
                  	<div class="col-sm-12">
                    <div class="col-sm-3">
                    </div> 
                          <div class="col-sm-9">
                           <asp:Button ID="btnSave" runat="server" Text="Submit" 
                             cssclass="btn btn-primary mar_top10" ValidationGroup="a" />&nbsp;  
                         
                          </div> 
                        
                    </div>
                  </div>
                  </form>
                    <div class="clearfix" style="margin-bottom:5px;"></div>
                




            </div>
            
            <div class="col-sm-12 table_head">
                        &nbsp;Request Details ::</div>
            <div class="row">
                 <div class="col-sm-12">
                 <div class="table-responsive">
                    <div class="table_wid">
                   
                    <div class="clearfix"></div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true"  cssclass="grid-view-themeclass"
                            BorderStyle="None" AllowPaging="True">
                          <%--  <AlternatingRowStyle BackColor="#f5f5f5" />--%>
                            <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="grid-pagin" />
               

                      </asp:GridView>
                      </div>
                    </div>
                      
                  </div>
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



    <Triggers>

<asp:PostBackTrigger ControlID="btnUpload_AgeProof" />
<asp:AsyncPostBackTrigger ControlID="btnDeleteUpload_AgeProof" EventName="Click"  />
</Triggers> 

</asp:UpdatePanel> 

</asp:Content>
