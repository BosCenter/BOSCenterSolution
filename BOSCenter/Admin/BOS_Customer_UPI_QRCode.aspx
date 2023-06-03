<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_Customer_UPI_QRCode.aspx.vb" Inherits="BOSCenter.BOS_Customer_UPI_QRCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="col-sm-10 col-sm-offset-1">
            
                <div class="log_form_head1 ">
                    My Account Details
                </div>
                <div class="log_form1 ">
                        <div class="row">
                            <div class="col-sm-12">
                                <div style="margin:20px"></div>
                                <div class="row">
                                    <div class="form-group">
                            <div class="col-sm-4">
                                <label>UPI ID</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_UPIID" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                            </div>
                        </div>
                                </div>
                                <div style="margin:20px"></div>
                                <div class="row">
                                    <div class="form-group">
                            <div class="col-sm-4">
                                <label>UPI QR Code</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:Image ID="Image_UPIQRCode_URL" runat="server" borderstyle="None" Height="250px" ImageUrl="~/images/uploadimage.png" Width="250px" />
                            </div>
                        </div>
                                </div>
                                <div style="margin:20px"></div>
                                <div class="row">
                                    <center>
                                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                                    </center>
                                </div>
                            </div>
                        
                    </div>    
                </div>           
        </div>
    </div>
</asp:Content>
