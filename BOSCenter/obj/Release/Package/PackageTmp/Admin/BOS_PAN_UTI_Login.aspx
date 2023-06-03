<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_PAN_UTI_Login.aspx.vb" Inherits="BOSCenter.BOS_PAN_UTI_Login" %>
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
       <asp:HyperLink ID="ForwardLink" Target="_blank"  NavigateUrl="https://www.psaonline.utiitsl.com/psaonline/"   cssclass="btn btn-danger mar_top10" runat="server">PSA ONLINE LOGIN</asp:HyperLink>
      </div>
       <div class="clearfix" style="margin-bottom:10px;"></div>
     </center>
</div>
 </div>
 </div>



	
</div> 


</asp:Content>
