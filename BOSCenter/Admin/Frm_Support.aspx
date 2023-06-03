<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Frm_Support.aspx.vb" Inherits="BOSCenter.Frm_Support" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="conatiner">
      <div class="col-md-10 col-md-offset-1">
        <div class="menu-bar" style=" display: flex; align-items: flex-start; justify-content: space-between; margin-top: 50px; padding: 8px;">
                    <img src="../Images/Urjamitra/urja mitraaa.png" width="40px">
                    <img src="../Images/Urjamitra/menu.png" width="40px">
                </div>
          <center>
               <div class="row" style=" display: flex; flex-direction: column; gap: 15px;">
            <div class="col-md-12">
                    <hr>
                    <h5 style="  margin: -14px;  text-align: center;  color: #0C1250;  font-size: 16px;  font-weight:610;">Support </h5>
                    <hr>
                </div>
            </div>
        <div class="row" style="display: flex; align-items: center; justify-content: center; margin-top: 25px; text-align: center   ;">
          <div class="col-md-4">
               <asp:ImageButton ID="lnkbtn_RaiseRequest" runat="server"  ImageUrl="../Images/Urjamitra/RaiseComplaint.png" width="30px" /><br />
         <%--<a href="../Admin/BOS_Raise_Request_Complaint.aspx"> <img src="../Images/Urjamitra/RaiseComplaint.png" width="30px" >--%>
             <p>Raise Complaint</p><%--</a>  --%>
          </div>
          <div class="col-md-4">
               <asp:ImageButton ID="lnkbtn_PendingReport" runat="server"  ImageUrl="../Images/Urjamitra/PendingCompalint.png" width="30px" /><br />
            <%--<a href="../Admin/BOS_PendingForm_Report.aspx"><img src="../Images/Urjamitra/PendingCompalint.png" width="30px">--%>
                <p>Pending Complaint</p><%--</a>   --%>
          </div>
          <div class="col-md-4">
               <asp:ImageButton ID="lnkbtn_CloseReport" runat="server"  ImageUrl="../Images/Urjamitra/CloseComplaint.png" width="30px" /><br />
            <%--<a href="../Admin/BOS_ClosedComplaint_Report.aspx"><img src="../Images/Urjamitra/CloseComplaint.png" width="30px">--%>
                <p>Close Compalaint</p><%--</a> --%>  
          </div>
        </div>
           <div class="row" id="foot" style=" font-size:small; padding: 8px; margin-top:25px; background-color:rgb(12,18,80);">
               <center>
             <class="col-md-12">
                   <b style="color:white;">Toll Free: 1800 309 2030</b><br />
                <b style="color:white;">An initiative by : Power Payment Services Pvt. Ltd.</b>

             </class>
                   </center>
          </div>
              </center>
      </div>
    </div>
</asp:Content>
