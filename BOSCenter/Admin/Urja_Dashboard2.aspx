<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Urja_Dashboard2.aspx.vb" Inherits="BOSCenter.Urja_Dashboard2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<style>
        .container{
            background-color:#0c1250;
        }
    #front{
        
        color: white;
        height: 500px;
        font-size: small;
        margin: 0px;
    }
  

    #logo{
        background-color: white;
        padding: 5px;
        color: #0c1250;
        font-size: 5px;
        
        
    }
  
    #box{
        background-color: white;
        color: #0c1250;
        padding-top: 10px;
        
    }
   
    #foot{
        font-size: x-small;
        padding: 15px;
    }
   span{
    font-size: x-small;
    color: red;
   }
    .request{
       
        padding-bottom: 20px;
    }
    p{
    padding-left: 60px;
    text-decoration:underline;
   }
   .footer{
    background-color: white;
    color: #0c1250;
    font-size: xx-small;
    padding:5px;
    
    
    
    
}
.charge{
 
    font-size:x-small;
    padding: 15px;
}
td{
    
    padding-left: 25px;
}
td:hover {
            transform: scale(1.1);
            background-color: lightblue;
            transition-duration: 2s;

        }  
        #bar{
            margin-left: 200px;
            margin-top: 10px;
        } 
</style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">

         <div class="menu-bar" style=" display: flex; align-items: flex-start; justify-content: space-between; margin-top: 50px; padding: 8px;">
                    <img src="../Images/Urjamitra/urja mitraaa.png" width="40px">
                    <img src="../Images/Urjamitra/menu.png" width="40px">
                </div>
          <div class="row" style="background-color:#0c1250;">
            <center>
            <div class="row" id="front" style="  color: white; height: 500px; font-size: small;">
   
           
                <div class="row" id="charge" style=" font-size:xx-small; padding: 15px;">
                <div class="col-md-12">
                    <div class="form-group">
                    <lable><b> <u>CHARGES </u></b></lable><br />
           <lable>Visa, Master, Rupay, Corporate Cards, Business Card,  will be same charged as shown below... <br/>
            International Cards like Amex and Diners club will be charged @ 4%</lable> 
                        </div>
               </div> 
          </div> 
                <div class="row" id="box" style="  background-color: white; color: #0c1250; padding-top: 10px;">
                    
                    <div class="request" style="padding-bottom: 20px; position: relative;">
                       <p style=" padding-left:10px;text-decoration:underline;"><b>Credit Card To Bank TRF</b></p>
                  <img src="../images/Urjamitra/request.png" height="40px" width="40px">
                    Request a Payment Link
                    <span style=" font-size: small;
                         color: red;">Charge @ 2.95%</span><br/><br/>
                    <img src="../images/Urjamitra/customer.png" height="40px" width="40px" id="er" onclick="clickOpen()">
                    Swipe on Call
                    <span style=" font-size: small;
    color: red;">Charge @ 2.66%</span><br/><br/>


                    <div id="popup" style="width: 60%; height: 30vh; position: absolute; background-color: whitesmoke; border-radius: 10px; bottom: 5%; left: 20%; box-shadow: 0 0 5px rgba(0,0,0,0.8); visibility: hidden;">  
                      <div class="col-md-12" style="text-align: end;">
                    <img onclick="clickOff()" src="../images/Ozzype/call on/cross.png" width="30px" style="box-shadow: 0 0 4px rgba(0,0,0,0.4); position: relative; top: 8px; right: 6px; border-radius: 5px; background-color: red;">
                        </div>
                    <br />
                    <h3 style="color: rgb   (12,18,80);"><b>Call On</b></h3>

                    <p style="color: rgb(12,18,80);">1800</p>
                </div>



                    <img src="../images/Urjamitra/recharge.png" height="40px" width="40px">
                    Pay Utility Bill of Others
                    <span style=" font-size: small;
    color: red;">Charge @ 2.36%</span><br/>
</div>
                </div>
                <div class="row" id="foot" style=" font-size:xx-small; padding: 8px">
               <b> <u>PLEASE READ CAREFULLY</u></b><br />
                1.We Never Store/share any of your card related info.<br/>
                2.A/c Details are kept only for Money TRF in Your A/c.<br/>
                3.Please alway follow Govt. of India Advisory while using your credit card.<br/>
                4.We never ask for OTP or Card Related information without your consent.

          </div>
                <div class="row" style="  background-color: white; color: #0c1250; font-size: xx-small; padding:5px;">
             <div class="col-md-12">
                  <div class="form-group">
                <label style="padding:10px;"> 
                   <a href="#"><img src="../images/Urjamitra/ac.png" height="40px" width="40px"><br/>

                 <p style="font-size: small;">Beneficiery A/c</p> </a> 
                  </label>
                  <label style="padding:10px;">
                   <a href="#"><img src="../images/Urjamitra/transaction.png" height="40px" width="40px"><br/>
                 <p style="font-size: small;">Transaction </p> </a> 
                  </label>
                  <label style="padding:10px;">
                     <a href="../Admin/Frm_Support.aspx"> <img src="../images/Urjamitra/customer.png" height="40px" width="40px"><br/> 
                    <p style="font-size: small;">Supoort</p> </a>
                  </label>
                  </div>
                   
                </div>  
                   <%-- <div class="col-md-4">
                        <center>
                            <asp:ImageButton ID="lnkbtn_Beneficiery" runat="server"  ImageUrl="../Images/Urjamitra/ac.png" width="30px" /><br />
                            <p style="font-size: small;">Beneficiery A/c</p>
                        </center>
                    </div>
                    <div class="col-md-4">
                     <center>
                          <asp:ImageButton ID="lnkbtn_Transaction" runat="server"  ImageUrl="../Images/Urjamitra/transaction.png" width="30px" /><br />
                         <p style="font-size: small;">Transaction </p> 
                     </center>
                    </div>
                    <div class="col-md-4">
                      <center>
                           <asp:ImageButton ID="lnkbtn_Supoort" runat="server"  ImageUrl="../Images/Urjamitra/customer.png" width="30px" /><br />
                          <p style="font-size: small;">Supoort</p>
                      </center>
                    </div>--%>
               </div>
                </div>
     </center>      
          </div>
      </div>



    <script>

        let popUp = document.getElementById("popup");
        function clickOpen() {
            popUp.style.visibility = "visible";
        }
        function clickOff() {
            popUp.style.visibility = "hidden";
        }
    </script>

</asp:Content>
