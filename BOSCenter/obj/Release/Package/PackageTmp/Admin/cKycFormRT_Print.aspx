
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="cKycFormRT_Print.aspx.vb" Inherits="BOSCenter.cKycFormRT_Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <style type="text/css">
        .style2
        {
            width: 350px;
        }
        .style3
        {
            width: 209px;
        }
    </style>
    
</head>
<body style="background: #fff;margin:0px;">
 <center>
 
   
   <div id="pnlPrint" style="margin:aoto;padding:auto;">
	
    
       <link href="./Welcome_Letter.aspx_files/Print.css" rel="stylesheet" type="text/css">
        <div class="printwrapper">
        
        <div class="printwrapperinner">
        <center>
        <fieldset style="margin:20px">
                      
            <div class="printworkwrapper">
                <div class="printworkwrapperinner" >
                    <center><h3> <u>CARD KYC FORM </u></h3></center>
                  <table cellpadding="0" cellspacing="10" align="center" width="100%">
                    <tbody>
                  <tr>
                        <td align="left" valign="top">
                            Request ID : <br /><strong><asp:Label ID="lblRequestID" runat="server" Text=""></asp:Label></strong>
                            </td>
                      <td align="center" valign="top">
                           Request Date : <br /><strong> <asp:Label ID="lblRequestDate" runat="server" Text=""></asp:Label></strong>
                            </td>
                      <td align="right" valign="top">
                         
                          </td>
                      </tr>
                  
                    <tr>
                        <td align="left" valign="top">
                                      Name : <br /><strong><asp:Label ID="lblName" runat="server" Text=""></asp:Label> </strong>
                        </td>
                        <td align="center" valign="top">
                            Phone Number :  <br /><strong> <asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label></strong>
                            </td>
                        <td align="right" valign="top">
                             
                        </td>
                    </tr>
                        <tr>
                        <td align="left" valign="top">
                                       Aadhar Number : <br /><strong><asp:Label ID="lblAadharNo" runat="server" Text=""></asp:Label> </strong>
                        </td>
                        <td align="center" valign="top">
                            Card Number : <br /> <strong> <asp:Label ID="lblCardNo" runat="server" Text=""></asp:Label></strong>
                            </td>
                        <td align="right" valign="top">
                              Reference Number : <br /> <strong> <asp:Label ID="lblReferenceNo" runat="server" Text=""></asp:Label></strong>
                        </td>
                    </tr>
                        <tr>
                        <td align="left" valign="top" colspan="3">
                                       Address :<br /> <strong><asp:Label ID="lblAddress" runat="server" Text=""></asp:Label> </strong>
                        </td>
                        
                       
                    </tr>
                        <tr>
                        <td align="left" valign="top">
                                         <asp:Image ID="Image_Photo" runat="server" BorderStyle="None" CssClass="" 
        Height="150px" ImageUrl="~/images/uploadimage.png" Width="170px" /> <br /> <strong>Photo :</strong>
                        </td>
                        <td align="center" valign="top">
                              <asp:Image ID="Image_AddharCardFront" runat="server" BorderStyle="None" CssClass="" 
         Height="150px" ImageUrl="~/images/uploadimage.png" Width="170px"/><br /><strong>Aadhar Card Front :</strong> 
                            </td>
                        <td align="right" valign="top">
                                <asp:Image ID="Image_AddharCardBack" runat="server" BorderStyle="None" CssClass="" 
         Height="150px" ImageUrl="~/images/uploadimage.png" Width="170px" /><br /> <strong>Aadhar Card Back :</strong>
                        </td>
                    </tr>
                          <tr>
                        <td align="left" valign="top">
                                        <asp:Image ID="imgFinger" runat="server" BorderStyle="None" CssClass="" 
        Height="150px" ImageUrl="~/images/uploadimage.png" Width="170px" /> <br /><strong>Fingure Print :</strong> 
                        </td>
                        <td align="center" valign="top">
                           
                            </td>
                        <td align="right" valign="top">
                            
                        </td>
                    </tr>


                </tbody></table>
                
             
                </div>
            </div>
            </fieldset>
            </center>
            </div>
           
        </div>
    
</div>
     



</center>
</body>
</html>
