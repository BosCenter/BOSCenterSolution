
Imports System.Text
Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.IO


Public Class GoToClientAccount
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("SUPERADMIN")




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub clear()
        Try
            ds = New DataSet
            txtSearchString.Text = ""

            GridView1.DataSource = Nothing
            GridView1.DataBind()
            lblError1.Text = ""
            lblError1.CssClass = ""
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try

            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""

            If Not ddlSelectCriteria.SelectedIndex = 0 Then
                If txtSearchString.Text.Trim = "" Then
                    lblError1.Text = "Please Enter the Value"
                    lblError1.CssClass = "errorlabels"
                    GridView1.DataSource = Nothing
                    GridView1.DataBind()
                    Exit Sub
                End If
            End If
            Bind()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            Dim CompanyName, mobno, Plancode, EmailID, CompanyID, CompanyType, Status As String
            CompanyName = ""
            mobno = ""
            Plancode = ""
            EmailID = ""
            CompanyID = ""
            Status = ""
            CompanyType = ""


            If ddlSelectCriteria.SelectedValue = "All Records" Then
                ddlSelectCriteria.Text = ""
                Querystring = "select CompanyName,CompanyCode,ClientPassword as 'Password',CinNo,GSTNo,Status as 'Account Status',IsNewDatabase as 'separate DB' from BOS_ClientRegistration  order by  rid Desc"
            ElseIf ddlSelectCriteria.SelectedValue = "Company Code" Then
                Querystring = "select CompanyName,CompanyCode,ClientPassword as 'Password',CinNo,GSTNo,Status as 'Account Status',IsNewDatabase as 'separate DB' from BOS_ClientRegistration  Where CompanyCode='" & txtSearchString.Text.Trim & "'  order by  rid Desc"
            End If


            If Not Querystring = "" Then
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)
                GridView1.DataBind()
                If GridView1.Rows.Count > 0 Then
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    GV.FL.showSerialnoOnGridView(GridView1, 0)
                Else
                    clear()
                    lblNoRecords.Text = "No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            lblNoRecords.Text = ex.Message
        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim loginID, Password As String

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            loginID = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
            Password = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)

            ds = GV.FL.OpenDs("BOS_ClientRegistration where CompanyCode='" & loginID & "' and ClientPassword='" & Password & "' ")
            If ds.Tables(0).Rows.Count > 0 Then

                createMenuCookie()

                Dim found As Boolean = False
                Dim dataAdd As Integer = 0

                If Request.Cookies.Count > 0 Then

                    For i As Integer = 0 To Request.Cookies.Count - 1
                        If Request.Cookies(i).Name = "AdminLoginInfo" Then
                            found = True
                            Exit For
                        End If
                    Next
                    If found = True Then
                        GV.AdminLoginInfo = New HttpCookie("AdminLoginInfo")
                        GV.AdminLoginInfo("UserRole") = ""
                        GV.AdminLoginInfo("ContactPerson") = ""
                        GV.AdminLoginInfo("BranchCode") = ""
                        GV.AdminLoginInfo("BranchName") = ""
                        GV.AdminLoginInfo("CompanyCode") = ""
                        GV.AdminLoginInfo("LoginID") = ""
                        GV.AdminLoginInfo("UserName") = ""
                        GV.AdminLoginInfo("UserType") = ""
                        GV.AdminLoginInfo("ProfilePic") = ""
                        GV.AdminLoginInfo("LastLogin") = ""
                        GV.AdminLoginInfo("LoggedInAs") = ""
                        GV.AdminLoginInfo("DataBaseName") = ""
                        GV.AdminLoginInfo.Expires = Now.AddDays(-12)
                        Response.Cookies.Add(GV.AdminLoginInfo)
                        dataAdd = 0
                    Else
                        dataAdd = 0
                    End If
                Else
                    dataAdd = 0
                End If

                If dataAdd = 0 Then
                    GV.AdminLoginInfo = New HttpCookie("AdminLoginInfo")
                    GV.AdminLoginInfo("ContactPerson") = ds.Tables(0).Rows(0).Item("ContactPerson").ToString()
                    GV.AdminLoginInfo("UserRole") = ds.Tables(0).Rows(0).Item("ClientRole").ToString()
                    GV.AdminLoginInfo("BranchCode") = ds.Tables(0).Rows(0).Item("HO_BranchCode").ToString()
                    GV.AdminLoginInfo("BranchName") = ds.Tables(0).Rows(0).Item("HO_BranchName").ToString()
                    GV.AdminLoginInfo("CompanyCode") = ds.Tables(0).Rows(0).Item("CompanyCode").ToString()
                    GV.AdminLoginInfo("LoginID") = ds.Tables(0).Rows(0).Item("CompanyCode").ToString()
                    'Dim VIsCreateDatabase As String = ds.Tables(0).Rows(0).Item("IsNewDatabase").ToString()
                    'Dim DBNAME As String = ""
                    'If VIsCreateDatabase.Trim.ToUpper = "Yes".ToUpper Then
                    '    'connectDatabaseName = ds.Tables(0).Rows(0).Item("DatabaseName").ToString()
                    '    DBNAME = ds.Tables(0).Rows(0).Item("DatabaseName").ToString()
                    '    'FL = New EmvFuncLibForWebVBNET.WebVBDOTNET(connectDatabaseName, "S", "SAPNA-PC", "sapnarana", "sa")
                    '    GV.FL = New EmvFuncLibForWebVBNET.WebVBDOTNET(DBNAME, "S", "103.90.242.88", "PSUnf72dTmkb9CFY", "sa")
                    'ElseIf VIsCreateDatabase.Trim.ToUpper = "No".ToUpper Then
                    '    'connectDatabaseName = DefaultDatabase()
                    '    DBNAME = GV.DefaultDatabase()
                    '    'FL = New EmvFuncLibForWebVBNET.WebVBDOTNET(connectDatabaseName, "S", "SAPNA-PC", "sapnarana", "sa")
                    '    GV.FL = New EmvFuncLibForWebVBNET.WebVBDOTNET(DBNAME, "S", "103.90.242.88", "PSUnf72dTmkb9CFY", "sa")
                    'End If


                    GV.AdminLoginInfo("DataBaseName") = ds.Tables(0).Rows(0).Item("DatabaseName").ToString()
                    GV.AdminLoginInfo("ShowUserRightsPanel") = ""
                    GV.AdminLoginInfo("UserType") = ds.Tables(0).Rows(0).Item("ClientRole").ToString()
                    GV.AdminLoginInfo("UserName") = ds.Tables(0).Rows(0).Item("CompanyName").ToString()
                    GV.AdminLoginInfo("ProfilePic") = ds.Tables(0).Rows(0).Item("Companylogo").ToString()
                    GV.AdminLoginInfo("LastLogin") = ds.Tables(0).Rows(0).Item("LastLogin").ToString()
                    GV.AdminLoginInfo("LoggedInAs") = "SuperAdmin"
                    GV.AdminLoginInfo.Expires = Now.AddHours(9)
                    Response.Cookies.Add(GV.AdminLoginInfo)
                End If

                'Response.Redirect("../Admin/AdminDashBoard.aspx")
                Response.Redirect("../Admin/AdminHome.aspx")
            Else

            End If


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub DeleteRow_click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblDialogMsg.Text = ""
            lblDialogMsg.CssClass = ""
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            'lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)

            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text


            lblDialogMsg.Text = "Are you sure you want to delete ?"
            btnCancel.Text = "Cancel"
            Button2.Visible = True
            ModalPopupExtender1.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
        Try
            clear()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Try
            If Not lblRID.Text = "" Then
                '  Dim result As Boolean = GV.FL.DMLQueries("delete from BOS_ClientRegistration where RID=" & lblRID.Text & "")
                Dim result As Boolean = GV.FL.DMLQueries("update BOS_ClientRegistration set Recordstatus='Deleted' where RID=" & lblRID.Text & "")
                lblDialogMsg.Text = result
                If result = True Then
                    lblDialogMsg.Text = "Record deleted Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                    Bind()
                    clear()
                Else
                    lblDialogMsg.Text = "Sorry !! Record deletion Failed."
                    lblDialogMsg.CssClass = "errorlabels"
                End If
                btnCancel.Text = "OK"
                Button2.Visible = False
                ModalPopupExtender1.Show()
            End If
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

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub
    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            GV.ExportToExcel(GridView1, Response, "ClientDetails")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub Imagebtnword_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagebtnword.Click
        Try
            GV.ExportToWord(GridView1, Response, "ClientDetails")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub Imagebtnpdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagebtnpdf.Click
        Try
            GV.ExportToPdf(GridView1, Response, "ClientDetails")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub



    Public Sub createMenuCookie()
        Try
            Dim found As Boolean = False
            Dim dataAdd As Integer = 0
            If Request.Cookies.Count > 0 Then

                For i As Integer = 0 To Request.Cookies.Count - 1
                    If Request.Cookies(i).Name = "Admin_MenuCookie" Then
                        found = True
                        Exit For
                    End If
                Next
                If found = True Then
                    GV.Admin_MenuCookie = New HttpCookie("Admin_MenuCookie")

                    GV.Admin_MenuCookie("Selected_MainMenu") = ""
                    GV.Admin_MenuCookie("Selected_SubMenu") = ""
                    GV.Admin_MenuCookie.Expires = Now.AddDays(1)
                    Response.Cookies.Add(GV.Admin_MenuCookie)
                    dataAdd = 0

                Else
                    dataAdd = 0
                End If
            Else
                dataAdd = 0
            End If

            If dataAdd = 0 Then
                GV.Admin_MenuCookie = New HttpCookie("Admin_MenuCookie")
                GV.Admin_MenuCookie("Selected_MainMenu") = ""
                GV.Admin_MenuCookie("Selected_SubMenu") = ""

                GV.Admin_MenuCookie.Expires = Now.AddHours(9)
                Response.Cookies.Add(GV.Admin_MenuCookie)
            End If




        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

End Class