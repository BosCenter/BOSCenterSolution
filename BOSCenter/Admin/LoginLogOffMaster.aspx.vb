
'Imports System.Text

'Imports System.Web.UI.HtmlControls
'Imports iTextSharp.text
'Imports iTextSharp.text.pdf
'Imports iTextSharp.text.html
'Imports iTextSharp.text.html.simpleparser
'Imports System.IO




Public Class LoginLogOffMaster
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")


    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            Dim srt As String = Session("UserName")
            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Super Admin" Then
                Querystring = "select RID as SrNo,* from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details order by RID Desc"
                lblExportQry.Text = "select  Row_Number() Over(order by rid desc)  as SrNo,* from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details order by RID Desc"
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Admin" Then
                Querystring = "select RID as SrNo,* from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where not User_Type='Super Admin' order by RID Desc"
                lblExportQry.Text = "select  Row_Number() Over(order by rid desc)  as SrNo,* from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where not User_Type='Super Admin' order by RID Desc"
            Else
                Querystring = ""
            End If


            'Querystring = "select RID as SrNo,* from CRM_Login_Details where User_Type='Operator' order by RID Desc"

            If Not Querystring = "" Then

                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                GridView1.DataBind()

                If GridView1.Rows.Count > 0 Then

                    lblExportQry.Text = "select Row_Number() Over(order by rid desc)  as SerialNo,* from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where not User_Type='Super Admin' order by RID Desc"
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    GV.FL.showSerialnoOnGridView(GridView1, 1)
                Else

                    lblNoRecords.Text = "No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If
            End If
            changeButtonText()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Dim QryStr As String
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            If e.CommandName = "btnfrmLogoffAll" Then
                Dim btn As Button
                btn = GridView1.HeaderRow.Cells(0).FindControl("frmLogoffAll")
                If btn.Text.ToUpper = "Log Off ALL".ToUpper Then
                    Session("btntext") = "Active ALL"
                    For i As Integer = 0 To GridView1.Rows.Count - 1

                        QryStr = ("update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details set LoggedinStatus='No' , AccountStatus='Inactive' where User_ID='" & GV.parseString(GridView1.Rows(i).Cells(2).Text.Trim) & "';")
                        If GV.FL.DMLQueries(QryStr) = True Then
                        Else
                            lblError.Text = "Record Updation failed."
                            lblError.CssClass = "errorlabels"
                            lblError.Focus()
                        End If
                    Next
                ElseIf btn.Text.ToUpper = "Active ALL".ToUpper Then
                    Session("btntext") = "Log Off ALL"
                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        QryStr = ("update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details set  AccountStatus='Active' where User_ID='" & GV.parseString(GridView1.Rows(i).Cells(2).Text.Trim) & "';")
                        If GV.FL.DMLQueries(QryStr) = True Then
                        Else
                            lblError.Text = "Record Updation failed."
                            lblError.CssClass = "errorlabels"
                            lblError.Focus()
                        End If
                    Next
                End If
                Bind()
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            If btndetails.Text.Trim.ToUpper = "Active".ToUpper Then
                QryStr = ("update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details set  AccountStatus='Active' where User_ID='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text.Trim) & "';")
                If GV.FL.DMLQueries(QryStr) = True Then
                Else
                    lblError.Text = "Record Updation failed."
                    lblError.CssClass = "errorlabels"
                    lblError.Focus()
                End If
            Else
                QryStr = ("update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details set LoggedinStatus='No' , AccountStatus='Inactive' where User_Id='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text.Trim) & "';")
                If GV.FL.DMLQueries(QryStr) = True Then
                Else
                    lblError.Text = "Record Updation failed."
                    lblError.CssClass = "errorlabels"
                    lblError.Focus()
                End If
            End If
            Bind()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            Try
                GridView1.PageIndex = e.NewPageIndex
            Catch ex As Exception
                GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

            End Try
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.PageIndexChanged
        Try
            Bind()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Bind()
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub



    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        Try
            If Not lblUserId.Text = "" Then
                Dim Fromtime, Totime As String
                Fromtime = ""
                Totime = ""
                Fromtime = GV.parseString(txtFromTime.Text.Trim) & "-" & ddlfromAm_PM.SelectedValue.Trim
                Totime = GV.parseString(txtTotime.Text.Trim) & "-" & ddlToAm_Pm.SelectedValue.Trim


                If Not txtFromTime.Text = "" Or txtTotime.Text = "" Then
                    Dim str As String
                    str = ("update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details set Fromtime='" & Fromtime & "',Totime='" & Totime & "' where User_ID='" & lblUserId.Text & "'")
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
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub changeButtonText()
        Try
            For i As Integer = 0 To GridView1.Rows.Count - 1
                If GridView1.Rows(i).Cells(4).Text.Trim.ToUpper = "ACTIVE" Then
                    Dim btndetails As LinkButton = GridView1.Rows(i).Cells(0).FindControl("btnLogOff")
                    btndetails.Text = "Log Off"
                    btndetails.CssClass = "btn btn-success btn-sm mb3"
                    'btndetails.ForeColor = Drawing.Color.White
                    btndetails.Font.Bold = True

                Else
                    Dim btndetails As LinkButton = GridView1.Rows(i).Cells(0).FindControl("btnLogOff")
                    btndetails.Text = "Active"
                    btndetails.CssClass = "btn btn-danger btn-sm mb3"
                    'btndetails.ForeColor = Drawing.Color.White
                    btndetails.Font.Bold = True
                End If
            Next
            Dim btn As Button = GridView1.HeaderRow.Cells(0).FindControl("frmLogoffAll")
            If Session("btntext") = "" Or Session("btntext") Is Nothing Then
                Session("btntext") = "Log Off ALL"
            End If
            btn.Text = Session("btntext")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub SelectNone(ByVal cellIndex As Integer, ByVal controlID As String)
        Try
            Dim chk As CheckBox

            For i As Integer = 0 To GridView1.Rows.Count - 1
                chk = GridView1.Rows(i).Cells(cellIndex).FindControl(controlID)
                chk.Checked = False
            Next
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    'exporting
    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToExcel_New(GridView1, Response, "", "logInLogOffReport", lblExportQry.Text, "static")
                End If
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub ImagebtnWOrd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagebtnword.Click
        Try
            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToWord_New(GridView1, Response, "logInLogOffReport", lblExportQry.Text, "static")
                End If
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub ExportToPdf_DivTag_HavingGridview(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnPdf.Click
        Try

            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToPdf_New(GridView1, "", Response, "logInLogOffReport", lblExportQry.Text, "static")
                End If
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub
End Class