<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="ProfileInfo.aspx.vb" Inherits="BOSCenter.ProfileInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
    $(document).ready(function () {
        $(".inputtext").change(function (event) {
            var obj = $(this);
            //alert(obj.val());
        });
    });
</script>
<br />

 <asp:UpdatePanel runat='server' ID='updatepanel1'>
<ContentTemplate>
 <div class="container">
	<div class="row">
    	<div class="col-sm-8 col-sm-offset-2">
        
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	My Account Details
                    </div>
                </div>
            </div>
        	<div class="log_form1">
            
                    <form class="form-horizontal">
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Name</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtUser_Name" cssclass="form-control inputtext" 
                                runat="server"  ClientIDMode="Static" ReadOnly="True" ></asp:TextBox>
                   
                        </div>
                      </div>
                      <div class="clearfix" style="margin-bottom:5px;"></div>
                   <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Login ID
                           </label>&nbsp;<div class="col-sm-6">
                            <asp:TextBox ID="txtUser_ID" cssclass="form-control inputtext" runat="server"  
                                ClientIDMode="Static" AutoPostBack="True" ReadOnly="True" ></asp:TextBox>
                      
                        </div>
                       
                         <asp:Label ID="lblloginIdError"  runat="server" cssclass="col-sm-3"  ></asp:Label>
                       
                       
                      </div>
                     

                         <div class="clearfix" style="margin-bottom:5px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Email ID</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtEmailID" cssclass="form-control inputtext" runat="server"  ReadOnly="true" ClientIDMode="Static" ></asp:TextBox>
                        </div>
                      </div>


                      <div class="clearfix" style="margin-bottom:5px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Mobile NO</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtMobileNo" cssclass="form-control inputtext" runat="server" ReadOnly="true" ClientIDMode="Static" ></asp:TextBox>
                        </div>
                      </div>




                      <div class="clearfix" style="margin-bottom:5px;"></div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Group</label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="ddlUser_Type" runat="server" 
                                cssclass="form-control inputtext" Enabled="False">
                            </asp:DropDownList>
                        </div>
                      </div>
                    
                       <div class="clearfix" style="margin-bottom:5px;"></div>

                     <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Upload Image</label>
                        <div class="col-sm-6 inline ">

                            <asp:FileUpload ID="FileUpload1"  runat="server" Enabled="False" />  <asp:Button ID="btnUpload" runat="server" cssclass="btn btn-primary mar_top10" Text="Upload" />&nbsp;<asp:Button 
                                ID="btnDeleteUpload" runat="server" cssclass="btn btn-primary mar_top10" 
                                Text="Remove" />
                          
                        </div>

                        <div class="col-sm-3">
                          <asp:Image ID="Image1" CssClass="" runat="server" Height="125px" Width="143px" 
                                ImageUrl="~/images/logo_login2.png" 
                                BorderStyle="None" />
                                <asp:Label ID="lblErrorImageError" runat="server" Text=""></asp:Label>
                                <br />
                        </div>
                        
                    </div>


                      <div class="clearfix" style="margin-bottom:5px;"></div>
          
       
                  <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                             <asp:Label ID="lblError" ClientIDMode="Static" runat="server"  ></asp:Label>
                   
                          
                            <asp:Label ID="lblUpadate" runat="server" Visible="False"></asp:Label>
                        </div>
                      </div>




                      <div class="clearfix"></div>

                  <div class="form-group">
                  	<div class="col-sm-12">
                    <div class="col-sm-3">
                    </div> 
                          <div class="col-sm-9">
                           <asp:Button ID="btnSave" runat="server" Text="Close" 
                             cssclass="btn btn-primary mar_top10" ValidationGroup="a" />&nbsp; &nbsp;  
                         


                        
                          </div> 
                        
                    </div>
                  </div>

        <div class="clearfix"></div>
                  </form>
                 
                
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
            <asp:Label ID="lblDialogMsg" runat="server" Text=""></asp:Label>  </td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnok" runat="server" Text="Yes" Width="80px" CssClass="btn btn-primary" OnClick="btnDeleteRow_Click" />
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
    <asp:PostBackTrigger ControlID="btnUpload" />
    </Triggers>
</asp:UpdatePanel>

</asp:Content>
