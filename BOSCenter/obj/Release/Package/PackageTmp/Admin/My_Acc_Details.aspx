<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master"  CodeBehind="My_Acc_Details.aspx.vb" Inherits="BOSCenter.My_Acc_Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        window.onload = function ()
        {
            var seconds = 905;
            setTimeout(function () {
                document.getElementById("<%=lblError.ClientID %>").style.display = "none";
        }, seconds * 1000);
    };
    </script>

    <asp:UpdatePanel runat='server' ID='updatepanel1'>
    <ContentTemplate>
    <div class="container">
	<div class="row">

        <div class="col-sm-9 col-sm-offset-1">
        
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	My Account Details
                    </div>
                </div>
            </div>

              
        	<div class="log_form1">
    
                      
         <div class="row">

    	<div  runat="server" id="Div_Member_details">
        
        	
        
            	
                    <form class="form-horizontal">
                      <%--<div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Enter Member Id</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txt_member_id"  cssclass="form-control" runat="server" AutoPostBack="true" ></asp:TextBox>
                        </div>
                      </div>--%>
                        
                        
                         <div class="form-group">
                      <label for="inputEmail3" class="col-sm-4 control-label">Account No </label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txt_account_no" cssclass="form-control" runat="server" ></asp:TextBox>
                        </div>
                      </div>
                           
                       <div class="clearfix" style="margin-bottom:20px;"></div>



                         <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">IFSC</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txt_ifsc" cssclass="form-control" runat="server"></asp:TextBox>
                        </div>
                      </div>
                       <div class="clearfix" style="margin-bottom:20px;"></div>


                         <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">UPI ID</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txt_upi_id"   cssclass="form-control" runat="server"></asp:TextBox>
                        </div>
                      </div>
                       <div class="clearfix" style="margin-bottom:20px;"></div>

                       
<div class="form-group">
<label class="col-md-4">UPI QR Code</label>
<div class="col-md-8">

    <asp:Image ID="Image_UPIQRCode_url" runat="server" BorderStyle="None" CssClass="" 
        Height="250px" ImageUrl="~/images/uploadimage.png" Width="250px" /></div>
    </div>
   
<div class="clearfix" style="margin-bottom:5px;"></div>

                 <div class="clearfix" style="margin-bottom:5px;"></div>       
            <div class="row">
<div class="col-md-12">
<center>
<asp:Label ID="lblError" runat="server" Text="" Visible ="false"></asp:Label>
    <asp:Label ID="lbl_result" runat="server" Text="" Visible ="false"></asp:Label>
</center>
</div>
</div>      

     </div>
  </div>
</div>








    </div>
</div>
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
