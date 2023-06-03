<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_Axis_Bank.aspx.vb" Inherits="BOSCenter.BOS_Axis_Bank" %>
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
 <div class="container">


 <div class="row">

 <div class="col-sm-1"> 
 &nbsp;
 </div>
 <div class="col-sm-10">
 
  <div class="form-section"> 
   <center>  
   <div class="col-sm-12">
      
        <asp:Label ID="lblStateName" runat="server" Visible="false"  Text=""></asp:Label>
       <asp:Label ID="lblDistrictName" runat="server" Visible="false"  Text=""></asp:Label>
       <asp:Label ID="lblStateID" runat="server" Visible="false"  Text=""></asp:Label>
       <asp:Label ID="lblDistrictID" runat="server" Visible="false"  Text=""></asp:Label>

        <asp:Label ID="lblFirstName" runat="server" Visible="false"  Text=""></asp:Label>
       <asp:Label ID="lblLastName" runat="server" Visible="false"  Text=""></asp:Label>
       <asp:Label ID="lblTransId" runat="server" Visible="false"  Text=""></asp:Label>
       
        <asp:Label ID="lblEmailID" runat="server" Visible="false"  Text=""></asp:Label>
       <asp:Label ID="lblMobileNo" runat="server" Visible="false"  Text=""></asp:Label>
       <asp:Label ID="lblAlternateMobileNo" runat="server" Visible="false"  Text=""></asp:Label>
        <asp:Label ID="lblDOB" runat="server" Visible="false"  Text=""></asp:Label>

        <asp:Label ID="lblPincode" runat="server" Visible="false"  Text=""></asp:Label>
        <asp:Label ID="lblCity" runat="server" Visible="false"  Text=""></asp:Label>
        <asp:Label ID="lblPanCardNumber" runat="server" Visible="false"  Text=""></asp:Label>
        <asp:Label ID="lblAgencyName" runat="server" Visible="false"  Text=""></asp:Label>
        <asp:Label ID="lblOfficeAddress" runat="server" Visible="false"  Text=""></asp:Label>
        <asp:Label ID="lblBcCode" runat="server" Visible="false"  Text=""></asp:Label>

         <asp:Label ID="lblIP" runat="server" Visible="false"  Text=""></asp:Label>
        <asp:Label ID="lblUserid" runat="server" Visible="false"  Text=""></asp:Label>
        <asp:Label ID="lblAgentID" runat="server" Visible="False"></asp:Label>
       <asp:Label ID="lblAccountType" runat="server" Visible="False"></asp:Label>
<asp:Label ID="lblAgentType" runat="server" Visible="False"></asp:Label>

 <asp:LinkButton ID="lnkbtn_StartAEPS" cssclass="btn btn-success  mar_top10"  runat="server">Saving Account</asp:LinkButton>
       <asp:LinkButton ID="lnkbtn_CurrentAccount" cssclass="btn btn-danger mar_top10"  runat="server">Current Account</asp:LinkButton>
 <br /><br />
 <asp:Label ID="lblError" runat="server" Visible="true"  Text=""></asp:Label>

       <%--<asp:HyperLink ID="ForwardLink" Target="_blank"  NavigateUrl="#"   cssclass="btn btn-danger mar_top10" runat="server">START AEPS</asp:HyperLink>--%>
      </div>
       <div class="clearfix" style="margin-bottom:10px;"></div>
     </center>
</div>
 </div>
 </div>



	
</div> 


</asp:Content>
