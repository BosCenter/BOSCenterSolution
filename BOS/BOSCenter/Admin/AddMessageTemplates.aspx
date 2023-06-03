<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="AddMessageTemplates.aspx.vb" Inherits="BOSCenter.AddMessageTemplates" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
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
	<div class="row col-sm-12">
    

    	<div class="col-sm-8">
        
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	Message Templates
                    </div>
                </div>
                	
            </div>
        	<div class="log_form1">
            
                    <div class="form-horizontal">
                    
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-1 control-label">Msg ID</label>
                        <div class="col-sm-11">
                            <asp:TextBox ID="txtMsgID" cssclass="form-control inputtext" runat="server"  
                                ClientIDMode="Static"  TextMode="SingleLine" ReadOnly="True"></asp:TextBox>
                   
                        </div>
                      </div>
                      

                      
            <div class="clearfix" style="margin-bottom:5px;"></div>
                    
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-1 control-label">Category</label>
                        <div class="col-sm-11">
                            <asp:DropDownList ID="ddlCategory" runat="server" 
                                cssclass="form-control inputtext" AutoPostBack="True">
                            </asp:DropDownList>

                        </div>
                       
                      </div>


            <div class="clearfix" style="margin-bottom:5px;"></div>
                    
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-1 control-label">Title</label>
                        <div class="col-sm-11">
                            <asp:TextBox ID="txtMsgTitle" cssclass="form-control inputtext" runat="server"  
                                ClientIDMode="Static" TextMode="SingleLine" ></asp:TextBox>
                        </div>
                       
                      </div>

                      <div class="clearfix" style="margin-bottom:5px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-1 control-label">Message</label>
                        <div class="col-sm-11">
                            <asp:TextBox ID="txtMessage" runat="server" Height="200px" TextMode="MultiLine" cssclass="form-control inputtext"
                                ></asp:TextBox>

                       <%--<cc1:editor ID="txtMessage1" runat="server" Height="200px" />--%>
                   </div>
                   
                      </div>
                        <div class="clearfix" style="margin-bottom:5px;"></div>
                    
               </div>
                 
                
            </div>
            
            
        </div>
        

<div class="col-sm-3">
        <div class="row">
 <div class="col-sm-12">

                	<div class="log_form_head1" style="margin-top:7px;">
                    	Select Templates
                    </div>
                </div>

                	</div>
                   <div class="log_form1">
                   <div class="form-horizontal">
                      <div class="form-group">
                       <div class="col-sm-12 ">
                        <asp:ListBox ID="lstTemplates" runat="server"  CssClass="form-control"
                               Height="295px" AutoPostBack="True">
                         
                            
                           </asp:ListBox>


                           


                       </div>                       
</div>

                    </div>
                     </div> 
  </div>
  <div class="clearfix" style="margin-bottom:5px;"></div>
   <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                          <asp:Label ID="lblError" runat="server" ></asp:Label>
                            
                            <asp:Label ID="lblUpadate" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSessionFlag" runat="server" Visible="False"></asp:Label>
                        </div>
                      </div>
        
        <div class="form-group">
                  	<div class="col-sm-12">
                    <div class="col-sm-3">
                    </div> 
                          <div class="col-sm-9">
                           <asp:Button ID="btnSave" runat="server" Text="Save" 
                             cssclass="btn btn-primary mar_top10" ValidationGroup="a" />&nbsp;  

                            	 <asp:Button ID="btndelete" runat="server" Text="Delete" 
                             cssclass="btn btn-primary mar_top10" />  &nbsp; 
                         
                             <asp:Button ID="btnClear" runat="server" Text="Reset" 
                             cssclass="btn btn-primary mar_top10" />  
                         


                        
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




</asp:Content>
