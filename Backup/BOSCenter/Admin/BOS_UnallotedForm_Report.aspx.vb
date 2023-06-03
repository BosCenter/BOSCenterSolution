Imports System.Text
Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.IO
Imports System.Net




Public Class BOS_UnallotedForm_Report
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")


    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            GridView1.Columns(4).Visible = True
            GridView1.Columns(5).Visible = True
            GridView1.Columns(10).Visible = True
            GridView1.Columns(11).Visible = True
            Dim grp As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim Filter As String = ""
            Dim GROUPFilter As String = ""
            Dim GroupCode As String = ""

            If grp.Trim.ToUpper = "Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                GROUPFilter = "And kCode='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'"
                GroupCode = ",kCode,kCodeType"
            Else
                GROUPFilter = ""
                GroupCode = ",kCode,kCodeType"
            End If

            If ddlSelectCriteria.SelectedValue.Trim.ToUpper = "All Records".Trim.ToUpper Then
                Filter = ""
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Register ID".Trim.ToUpper Then
                Filter = " And kCode ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Complaint ID".Trim.ToUpper Then
                Filter = " And ComplaintID ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Product Service".Trim.ToUpper Then
                Filter = " And Product like '" & GV.parseString(txtSearchingValue.Text.Trim) & "%'"
            End If


            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                'dis,sd,re,cust
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                'super admin
                If chkduration.Checked = True Then
                    Querystring = "select RID as SrNo,(CONVERT(VARCHAR(11),ComplaintDate,106)) as ComplaintDate,ComplaintID" & GroupCode & ",Product,(Problem+'/'+Complaint) as ComplaintProblem,isnull(Attachment,'') as Attachment,ComplaintStatus,AllotedTo,(CONVERT(VARCHAR(11),AllotedDateTime,106)) as AllotedDate,CompanyCode from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Complaint_Master_SA where ComplaintStatus='Pending' " & GROUPFilter & " " & Filter & " order by RID Desc"
                Else
                    Querystring = "select RID as SrNo,(CONVERT(VARCHAR(11),ComplaintDate,106)) as ComplaintDate,ComplaintID" & GroupCode & ",Product,(Problem+'/'+Complaint) as ComplaintProblem,isnull(Attachment,'') as Attachment,ComplaintStatus,AllotedTo,(CONVERT(VARCHAR(11),AllotedDateTime,106)) as AllotedDate,CompanyCode from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Complaint_Master_SA where ComplaintStatus='Unalloted' " & GROUPFilter & " " & Filter & " order by RID Desc"
                End If
            Else
                'admin and other employees 
                If chkduration.Checked = True Then
                    Querystring = "select RID as SrNo,(CONVERT(VARCHAR(11),ComplaintDate,106)) as ComplaintDate,ComplaintID" & GroupCode & ",Product,(Problem+'/'+Complaint) as ComplaintProblem,isnull(Attachment,'') as Attachment,ComplaintStatus,AllotedTo,(CONVERT(VARCHAR(11),AllotedDateTime,106)) as AllotedDate,CompanyCode from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Complaint_Master where ComplaintStatus='Pending' " & GROUPFilter & " " & Filter & " order by RID Desc"
                Else
                    Querystring = "select RID as SrNo,(CONVERT(VARCHAR(11),ComplaintDate,106)) as ComplaintDate,ComplaintID" & GroupCode & ",Product,(Problem+'/'+Complaint) as ComplaintProblem,isnull(Attachment,'') as Attachment,ComplaintStatus,AllotedTo,(CONVERT(VARCHAR(11),AllotedDateTime,106)) as AllotedDate,CompanyCode from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Complaint_Master where ComplaintStatus='Unalloted' " & GROUPFilter & " " & Filter & " order by RID Desc"
                End If
            End If

          

            If Not Querystring = "" Then

                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)
                '  GridView1.DataBind()

                If GridView1.Rows.Count > 0 Then
                    If grp.Trim.ToUpper = "Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Retailer".Trim.ToUpper Or grp.Trim.ToUpper = "Customer".Trim.ToUpper Then
                        GridView1.Columns(4).Visible = False
                        GridView1.Columns(5).Visible = False
                        GridView1.Columns(10).Visible = False
                        GridView1.Columns(11).Visible = False
                        For i As Integer = 0 To GridView1.Rows.Count - 1
                            Dim LinkButton2 As LinkButton = DirectCast(GridView1.Rows(i).Cells(0).FindControl("LinkButton2"), LinkButton)
                            LinkButton2.Visible = False
                            Dim lnkdwnload As LinkButton = DirectCast(GridView1.Rows(i).FindControl("lnkdwnload"), LinkButton)
                            Dim lblAttachment As Label = DirectCast(GridView1.Rows(i).FindControl("lblAttachment"), Label)
                            If lblAttachment.Text = "" Then
                                lnkdwnload.Visible = False
                            Else
                                lnkdwnload.Visible = True
                            End If
                            GridView1.Rows(i).Cells(1).Text = i + 1
                        Next
                    ElseIf grp.Trim.ToUpper = "Super Admin".ToUpper Then
                        For i As Integer = 0 To GridView1.Rows.Count - 1
                            If Not (grp.Trim.ToUpper = "Admin".ToUpper Or grp.Trim.ToUpper = "Super Admin".ToUpper) Then
                                Dim LinkButton2 As LinkButton = DirectCast(GridView1.Rows(i).Cells(0).FindControl("LinkButton2"), LinkButton)
                                LinkButton2.Visible = False
                            End If

                            If chkduration.Checked = True Then
                                GridView1.Columns(10).Visible = True
                                GridView1.Columns(11).Visible = True
                            Else
                                GridView1.Columns(10).Visible = False
                                GridView1.Columns(11).Visible = False
                            End If
                            Dim lnkdwnload As LinkButton = DirectCast(GridView1.Rows(i).FindControl("lnkdwnload"), LinkButton)
                            Dim lblAttachment As Label = DirectCast(GridView1.Rows(i).FindControl("lblAttachment"), Label)
                            If lblAttachment.Text = "" Then
                                lnkdwnload.Visible = False
                            Else
                                lnkdwnload.Visible = True
                            End If
                            GridView1.Rows(i).Cells(1).Text = i + 1
                        Next
                    Else

                        For i As Integer = 0 To GridView1.Rows.Count - 1
                            If Not (grp.Trim.ToUpper = "Admin".ToUpper Or grp.Trim.ToUpper = "Super Admin".ToUpper) Then
                                Dim LinkButton2 As LinkButton = DirectCast(GridView1.Rows(i).Cells(0).FindControl("LinkButton2"), LinkButton)
                                LinkButton2.Visible = False
                            End If

                            If chkduration.Checked = True Then
                                GridView1.Columns(10).Visible = True
                                GridView1.Columns(11).Visible = True
                            Else
                                GridView1.Columns(10).Visible = False
                                GridView1.Columns(11).Visible = False
                            End If
                            Dim lnkdwnload As LinkButton = DirectCast(GridView1.Rows(i).FindControl("lnkdwnload"), LinkButton)
                            Dim lblAttachment As Label = DirectCast(GridView1.Rows(i).FindControl("lblAttachment"), Label)
                            If lblAttachment.Text = "" Then
                                lnkdwnload.Visible = False
                            Else
                                lnkdwnload.Visible = True
                            End If
                            GridView1.Rows(i).Cells(1).Text = i + 1
                        Next
                    End If
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    ' GV.FL.showSerialnoOnGridView(GridView1, 1)
                Else
                    lblNoRecords.Text = "No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Bind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub UpdateDetails_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text.Trim
            Dim ComplaintStatus As String = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text)

            
            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                'dis,sd,re,cust
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                'super admin
                GV.FL.AddInDropDownListDistinct(ddlEmployee, "User_ID+':'+User_Name", " " & GV.DefaultDatabase.Trim & ".dbo.CRM_Login_Details where User_Type='Employee' ")
            Else
                'admin and other employees 
                GV.FL.AddInDropDownListDistinct(ddlEmployee, "User_ID+':'+User_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where User_Type='Employee' ")
            End If




            If ddlEmployee.Items.Count > 0 Then
                ddlEmployee.Items.Insert(0, "Select Employee")
            Else
                ddlEmployee.Items.Add("Select Employee")
            End If
            If ComplaintStatus = "Pending" Then
                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                    'dis,sd,re,cust
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    'super admin
                    ddlEmployee.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text) & ":" & GV.FL.AddInVar("User_Name", " " & GV.DefaultDatabase.Trim & ".dbo.CRM_Login_Details where User_Type='Employee' and User_ID='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text) & "'")
                Else
                    'admin and other employees 
                    ddlEmployee.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text) & ":" & GV.FL.AddInVar("User_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where User_Type='Employee' and User_ID='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text) & "'")
                End If


            End If

            btnCancel.Visible = True
            btnCancel.Text = "Cancel"
            btnUpdate.Visible = True
            lblDialogMsg.Text = ""
            lblDialogMsg.CssClass = ""
            ModalPopupExtender1.Show()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlNoOfRecords_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlNoOfRecords.SelectedIndexChanged
        Try
            If ddlNoOfRecords.SelectedValue = "10 Record(s)" Then
                GridView1.PageSize = 10
            ElseIf ddlNoOfRecords.SelectedValue = "25 Record(s)" Then
                GridView1.PageSize = 25
            ElseIf ddlNoOfRecords.SelectedValue = "50 Record(s)" Then
                GridView1.PageSize = 50
            ElseIf ddlNoOfRecords.SelectedValue = "100 Record(s)" Then
                GridView1.PageSize = 100
            ElseIf ddlNoOfRecords.SelectedValue = "200 Record(s)" Then
                GridView1.PageSize = 200
            ElseIf ddlNoOfRecords.SelectedValue = "500 Record(s)" Then
                GridView1.PageSize = 500
            End If
            Bind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            Try
                GridView1.PageIndex = e.NewPageIndex
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.PageIndexChanged
        Try
            Bind()
        Catch ex As Exception
        End Try

    End Sub



    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            GV.ExportToExcel(GridView1, Response, "UnallotedReport")
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub ImagebtnPdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnPdf.Click
        Try
            GV.ExportToPdf(GridView1, Response, "UnallotedReport")
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub

    Protected Sub Imagebtnword_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try
            GV.ExportToWord(GridView1, Response, "UnallotedReport")
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        Try
            lblDialogMsg.Text = ""
            lblDialogMsg.CssClass = ""
           
            If Not lblRID.Text = "" Then
                If ddlEmployee.SelectedIndex = 0 Then
                    lblDialogMsg.Text = "Pls Select Employee."
                    lblDialogMsg.CssClass = "errorlabels"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If
                Dim EmployeeId() As String = GV.parseString(ddlEmployee.SelectedValue.Trim).Split(":")
                Dim str As String = ""




                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                    'dis,sd,re,cust
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    'super admin
                    str = ("update " & GV.DefaultDatabase.Trim & ".dbo.BOS_Complaint_Master_SA set  AllotedTo='" & EmployeeId(0) & "',AllotedDateTime=getdate(),ComplaintStatus='Pending' where RID=" & GV.parseString(lblRID.Text) & " ")
                Else
                    'admin and other employees 
                    str = ("update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Complaint_Master set  AllotedTo='" & EmployeeId(0) & "',AllotedDateTime=getdate(),ComplaintStatus='Pending' where RID=" & GV.parseString(lblRID.Text) & " ")
                End If




                Dim result As Boolean = GV.FL.DMLQueries(str)
                lblDialogMsg.Text = result
                If result = True Then
                    lblDialogMsg.Text = "Record Updated Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                    Bind()

                Else
                    lblDialogMsg.Text = "Sorry !! Record Failed."
                    lblDialogMsg.CssClass = "errorlabels"
                End If
                btnCancel.Text = "OK"
                btnUpdate.Visible = False
                ModalPopupExtender1.Show()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            lblExportQry.Text = ""

            lblError1.Text = ""
            lblNoRecords.Text = ""
            lblError1.CssClass = ""
            lblNoRecords.CssClass = ""

            GridView1.DataSource = Nothing
            GridView1.DataBind()

            If Not ddlSelectCriteria.SelectedValue = "All Records" And GV.parseString(txtSearchingValue.Text.Trim) = "" Then
                lblError1.Text = "Enter Search value"
                lblError1.CssClass = "errorlabels"
                Exit Sub
            End If
            Bind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DownloadRow_click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lblAttachment As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).FindControl("lblAttachment"), Label)
            DownloadDoc(lblAttachment.Text)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub DownloadDoc(ByVal DocPath As String)
        Try

            Dim fi As New FileInfo(DocPath)
            Dim strURL As String = DocPath
            Dim req As New WebClient()
            Dim response As HttpResponse = HttpContext.Current.Response
            response.Clear()
            response.ClearContent()
            response.ClearHeaders()
            response.Buffer = True
            response.AddHeader("Content-Disposition", "attachment;filename=""" & fi.Name & """")
            Dim data As Byte() = req.DownloadData(Server.MapPath(strURL))
            response.BinaryWrite(data)
            response.[End]()

        Catch ex As Exception
        End Try
    End Sub
    Public Sub clear()
        Try
            lblError1.Text = ""
            lblError1.CssClass = ""
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            ddlSelectCriteria.SelectedIndex = 0
            txtSearchingValue.Text = ""
            Bind()
            ddlNoOfRecords.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
  
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            clear()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub chkduration_CheckedChanged(sender As Object, e As EventArgs) Handles chkduration.CheckedChanged
        Try
            clear()
        Catch ex As Exception

        End Try
    End Sub
End Class