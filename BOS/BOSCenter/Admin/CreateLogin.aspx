<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="CreateLogin.aspx.vb" Inherits="BOSCenter.CreateLogin" %>
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
	
    	<div class="col-sm-10">
        
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                        <asp:Label ID="lblheading" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        	<div class="log_form1">
            
                    <form class="form-horizontal">
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Employee Name<asp:Label ID="Label11" runat="server" Text=" *" ForeColor="Red"></asp:Label></label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtUser_Name" cssclass="form-control inputtext" 
                                runat="server"  ClientIDMode="Static"  ></asp:TextBox>
                   
                        </div>
                        
                      </div>
                           <div class="clearfix" style="margin-bottom:5px;"></div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Employee Type<asp:Label ID="Label10" runat="server" Text=" *" ForeColor="Red"></asp:Label></label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="ddlEmployeType" runat="server" cssclass="form-control inputtext">
                          
                            </asp:DropDownList>
                        </div>
                      </div>

                      <div class="clearfix" style="margin-bottom:5px;"></div>
                   <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Login ID<asp:Label ID="Label9" runat="server" Text=" *" ForeColor="Red"></asp:Label>
                           </label>&nbsp;<div class="col-sm-6">
                            <asp:TextBox ID="txtUser_ID" cssclass="form-control inputtext" runat="server"  
                                ClientIDMode="Static" AutoPostBack="True" ></asp:TextBox>
                      
                        </div>
                       
                         <asp:Label ID="lblloginIdError"  runat="server" cssclass="col-sm-3"  ></asp:Label>
                       
                       
                      </div>
                      <div class="clearfix" style="margin-bottom:5px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Password<asp:Label ID="Label8" runat="server" Text=" *" ForeColor="Red"></asp:Label></label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtUser_Password" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                   
                        </div>
                      </div>


                      <div class="clearfix" style="margin-bottom:5px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Email ID</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtEmailID" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                        </div>
                      </div>


                      <div class="clearfix" style="margin-bottom:5px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Mobile NO</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtMobileNo" cssclass="form-control inputtext" runat="server"  
                                ClientIDMode="Static" MaxLength="10" ></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtMobileNo_FilteredTextBoxExtender" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtMobileNo">
                            </asp:FilteredTextBoxExtender>
                        </div>
                      </div>



                      
                       <div class="clearfix" style="margin-bottom:5px;"></div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Can Login<asp:Label ID="Label12" runat="server" Text=" *" ForeColor="Red"></asp:Label></label>
                        <div class="col-sm-9">
                            <asp:RadioButton ID="rdbYes" GroupName="x" runat="server" Text="Yes" />
                            <asp:RadioButton ID="rdbNo" GroupName="x" runat="server" Text="No" />

                        </div>
                      </div>



                        <div class="clearfix" style="margin-bottom:5px;"></div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Group<asp:Label ID="Label2" runat="server" Text=" *" ForeColor="Red"></asp:Label></label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="ddlUser_Type" runat="server" cssclass="form-control inputtext">
                            </asp:DropDownList>
                        </div>
                      </div>
                    <div class="clearfix" style="margin-bottom:5px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Account Status<asp:Label ID="Label3" runat="server" Text=" *" ForeColor="Red"></asp:Label></label>
                        <div class="col-sm-9">
                             <asp:DropDownList ID="ddlAccountStatus" runat="server" cssclass="form-control inputtext">
                                 <asp:ListItem>Active</asp:ListItem>
                                 <asp:ListItem>Inactive</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                      </div>
                      <div class="clearfix" style="margin-bottom:5px;"></div>
          
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">From Time<asp:Label ID="Label4" runat="server" Text=" *" ForeColor="Red"></asp:Label></label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtFromTime" runat="server" cssclass="form-control inputtext"></asp:TextBox>
           <asp:MaskedEditExtender ID="txtFromTime_MaskedEditExtender" 
                runat="server" ClearTextOnInvalid="True" CultureAMPMPlaceholder="" 
                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time" 
                TargetControlID="txtFromTime" UserTimeFormat="TwentyFourHour">
            </asp:MaskedEditExtender>
                        </div>
                        <div class="col-sm-2">
                             <asp:DropDownList ID="ddlFromAm_Pm" runat="server"  cssclass="form-control inputtext"> 
               <asp:ListItem>AM</asp:ListItem>
               <asp:ListItem>PM</asp:ListItem>
           </asp:DropDownList>
                        </div>
                      </div>
                      <div class="clearfix" style="margin-bottom:5px;"></div>
             <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">To Time<asp:Label ID="Label5" runat="server" Text=" *" ForeColor="Red"></asp:Label></label>
                        <div class="col-sm-2">
                         <asp:TextBox ID="txtTotime" runat="server" cssclass="form-control inputtext"></asp:TextBox>
            &nbsp;<asp:MaskedEditExtender ID="txtToTime_MaskedEditExtender" runat="server" 
                ClearTextOnInvalid="True" CultureAMPMPlaceholder="" 
                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time" 
                TargetControlID="txtToTime" UserTimeFormat="TwentyFourHour">
            </asp:MaskedEditExtender>
                        </div>
                        <div class="col-sm-2">
                              <asp:DropDownList ID="ddlToAm_Pm" runat="server"  cssclass="form-control inputtext">
               <asp:ListItem>AM</asp:ListItem>
               <asp:ListItem>PM</asp:ListItem>
           </asp:DropDownList>
                        </div>
                      </div>


                       <div class="clearfix" style="margin-bottom:5px;"></div>

                     <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Upload Image</label>
                        <div class="col-sm-6 inline ">

                            <asp:FileUpload ID="FileUpload1"  runat="server" />  <asp:Button ID="btnUpload" runat="server" cssclass="btn btn-primary mar_top10" Text="Upload" />&nbsp;<asp:Button 
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
                   
                           <asp:Label ID="lblSessionFlag" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblUpadate" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblOldEmailId" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblOldMobileNo" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblExportQry" runat="server" Visible="False"></asp:Label>
                        </div>
                      </div>

                    
<div style='margin-bottom:10px;'></div>
  <div class='form-group'>
<div class='row'>
<div class='col-md-6'>
<asp:UpdatePanel ID='UpdatePanel2'  runat='server'>
<ContentTemplate>
<asp:ImageButton ID='ImagebtnExcel' runat='server' width='32px' Height='32px' 
ImageUrl='~/images/excel_32X32.png'/>
<asp:ImageButton ID='ImagebtnWOrd' runat='server' width='32px' Height='32px' 
ImageUrl='~/images/word_32X32.png'/>
<asp:ImageButton ID='Imagepdf' runat='server' width='32px' Height='32px' 
ImageUrl='~/images/pdf_32X32.png'/>
</ContentTemplate> 
<Triggers>
<asp:PostBackTrigger ControlID='ImagebtnExcel' />
<asp:PostBackTrigger ControlID='ImagebtnWOrd' />
<asp:PostBackTrigger ControlID='Imagepdf' />
 </Triggers>
</asp:UpdatePanel> 
 </div>
<div class='col-md-6'>
<label class='col-sm-2'></label>
<div class='col-sm-4 pull-right'>
<asp:DropDownList ID='ddlNoOfRecords' runat='server'  
cssclass='form-control inputtext' AutoPostBack='True' >
<asp:ListItem>10 Record(s)</asp:ListItem>
 <asp:ListItem>25 Record(s)</asp:ListItem>
 <asp:ListItem>50 Record(s)</asp:ListItem>
 <asp:ListItem>100 Record(s)</asp:ListItem>
 <asp:ListItem>200 Record(s)</asp:ListItem>
<asp:ListItem>500 Record(s)</asp:ListItem>
</asp:DropDownList>
</div>
</div>
</div>
</div>

                      <div class="clearfix"></div>

                  <div class="form-group">
                  	<div class="col-sm-12">
                    <div class="col-sm-3">
                    </div> 
                          <div class="col-sm-9">
                           <asp:Button ID="btnSave" runat="server" Text="Save" 
                             cssclass="btn btn-primary mar_top10" ValidationGroup="a" />&nbsp;  
                            	
                          <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                             cssclass="btn btn-primary mar_top10" Enabled="False" />&nbsp;
                             <asp:Button ID="btnClear" runat="server" Text="Reset" 
                             cssclass="btn btn-primary mar_top10" />  
                         


                        
                          </div> 
                        
                    </div>
                  </div>

        <div class="clearfix"></div>
                  </form>
                 
                
            </div>
            
            
        </div>
    
     <div class="col-sm-10 table_head">
                        Employee Details
                        </div>
    <div class="row">
                 <div class="col-sm-10">
                 <div class="form-section">
                 <asp:Panel ID="Panel1" Width="100%" runat="server">
                 <div class="table-responsive">
                    <div class="table_wid">
                   
                    <div class="clearfix"></div>
                        
                       
                        <asp:GridView ID="GridView1" runat="server"  cssclass="grid-view-themeclass" style="overflow:scroll;"
                            BorderStyle="None" AllowPaging="True">
                          <%--  <AlternatingRowStyle BackColor="#f5f5f5" />--%>
                          <Columns>
                            
                             <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                      <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btn btn-danger" OnClick="btnGrdRowDelete_Click"
                                          CommandName="Select" Text=""><i class="fa fa-trash"></i></asp:LinkButton>
                                          <asp:Label ID="lblgrdRID" runat="server" Text='<%# Eval("SrNo") %>' Visible="false" ></asp:Label>
                                          <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CssClass="btn btn-default" OnClick="GridView1_SelectedIndexChanged"
                                          CommandName="Select" Text=""><i class="fa fa-pencil-square-o fa-lg"></i></asp:LinkButton>
                                  </ItemTemplate>
                                  <ItemStyle Width="100px" />
                              </asp:TemplateField>
                            
                            
                          </Columns>
                          
                            <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="grid-pagin" />
                          
                      </asp:GridView>
                      

                      </div>
                    </div>
                     </asp:Panel>  
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
