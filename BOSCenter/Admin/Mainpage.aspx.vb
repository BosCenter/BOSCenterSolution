
Public Class Mainpage
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")


    Dim MainMenu() As String = {"Masters", "Plan Management", "Client Management", "Operator Management", "Reports", "Mail Management", "Sms Management", "Settings"}
    Dim MainMenuIcon() As String = {"fa fa-book fa-lg", "fa fa-pie-chart fa-lg", "fa fa-user-plus fa-lg", "fa fa-users fa-lg", "fa fa-hourglass fa-lg", "fa fa-envelope fa-lg", "fa fa-phone-square fa-lg", "fa fa-wrench fa-lg"}
    Dim SubMenuar() As String = {"Masters:Country Master,State Master,District Master,Group Master,News & Event Master,Portfolio Master,Client Testimonial,Company", "Plan Management:Plane Master,Module Master,Duration Master,Plan vs Module Master,Plan Type Master", "Client Management:Client Registration,Search & Edit,Client Verification,Go To Client A\C,Change Plan", "Operator Management:Create Operator,Login Time Frame,Logout User", "Reports:Plan Renewal Report,Account Report,Client Password Report,Operator Login Report,Client Remove A/C Report,Client Updation Report", "Mail Management:Send Mail,Add Template", "Sms Management:Send Sms,Add Message Category,Add Template,Search Send Message", "Settings:User Rights,Client DB Allocation,Change Password,Site Map"}
    Dim SubMenuCSS() As String = {"btn btn-primary btn-sm mb3", "btn btn-success btn-sm mb3", "btn btn-danger btn-sm mb3", "btn btn-warning btn-sm mb3", "btn btn-info btn-sm mb3"}

    Dim strMainMenu As String = ""
    Dim strSubMenu As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                Dim dt As New DataTable
                dt.Columns.Add("MainMenu")
                dt.Columns.Add("MainMenuIcon")

                For i As Integer = 0 To MainMenu.Count - 1

                    dt.Rows.Add()
                    dt.AcceptChanges()

                    dt.Rows(i)(0) = MainMenu(i)
                    dt.Rows(i)(1) = MainMenuIcon(i)
                    'strMainMenu = strMainMenu & Environment.NewLine & "<li class='treeview'><a href='#tab-" & i + 1 & "'><i class='" & MainMenuIcon(i) & "'></i> <span>" & MainMenu(i) & "</span></a></li>"
                Next

                ListView1.DataSource = dt
                ListView1.DataBind()

                Dim dtsub As New DataTable
                dtsub.Columns.Add("SubMenu")
                dtsub.Columns.Add("SubMenuCSS")

                Dim ctrcss As Integer = 0
                For i As Integer = 0 To SubMenuar.Count - 1

                    Dim ar() As String = SubMenuar(i).Split(":")
                    Dim strar() As String = ar(1).Split(",")

                    If ar(0) = MainMenu(0) Then
                        For j As Integer = 0 To strar.Count - 1
                            dtsub.Rows.Add()
                            dtsub.AcceptChanges()

                            dtsub.Rows(j)(0) = strar(j)
                            dtsub.Rows(j)(1) = SubMenuCSS(ctrcss)

                            If ctrcss < 4 Then
                                ctrcss += 1
                            Else
                                ctrcss = 0
                            End If
                        Next
                    End If

                Next

                ListView2.DataSource = dtsub
                ListView2.DataBind()

            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub MainMenu_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim lnk As LinkButton = DirectCast(sender, LinkButton)
            Dim pagename As String = ""
            pagename = lnk.CommandArgument



            Dim dtsub As New DataTable
            dtsub.Columns.Add("SubMenu")
            dtsub.Columns.Add("SubMenuCSS")

            Dim ctrcss As Integer = 0
            For i As Integer = 0 To SubMenuar.Count - 1

                Dim ar() As String = SubMenuar(i).Split(":")
                Dim strar() As String = ar(1).Split(",")

                If ar(0) = pagename Then
                    For j As Integer = 0 To strar.Count - 1
                        dtsub.Rows.Add()
                        dtsub.AcceptChanges()

                        dtsub.Rows(j)(0) = strar(j)
                        dtsub.Rows(j)(1) = SubMenuCSS(ctrcss)

                        If ctrcss < 4 Then
                            ctrcss += 1
                        Else
                            ctrcss = 0
                        End If
                    Next
                End If
            Next

            ListView2.DataSource = dtsub
            ListView2.DataBind()
            contentPage.Attributes("src") = "Group.aspx"
            SubMenuPanel.Update()
            UpdatePanel1.Update()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Public Sub SubMenu_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim lnk As Button = DirectCast(sender, Button)

            Dim pagename As String = ""
            If lnk.CommandArgument = "Country Master" Then
                contentPage.Attributes("src") = "Country.aspx"
            ElseIf lnk.CommandArgument = "State Master" Then
                contentPage.Attributes("src") = "State.aspx"
            ElseIf lnk.CommandArgument = "District Master" Then
                contentPage.Attributes("src") = "District.aspx"
            ElseIf lnk.CommandArgument = "Operator Management" Then
                pagename = ""
            ElseIf lnk.CommandArgument = "Reports" Then
                pagename = ""
            ElseIf lnk.CommandArgument = "Mail Management" Then
                pagename = ""
            ElseIf lnk.CommandArgument = "Sms Management" Then
                pagename = ""
            ElseIf lnk.CommandArgument = "Settings" Then
                pagename = ""
            End If

            'contentPage.Attributes.Add("onLoad", "autoResize('contentPage');")


            UpdatePanel1.Update()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub



    Protected Sub ListView1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles ListView1.ItemDataBound
        Try
            Dim lnkFull As LinkButton = TryCast(e.Item.FindControl("LinkButton1"), LinkButton)
            'ScriptManager.GetCurrent(Me.Page).RegisterPostBackControl(lnkFull)
            ScriptManager.GetCurrent(Me.Page).RegisterAsyncPostBackControl(lnkFull)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub



    Protected Sub ListView2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles ListView2.ItemDataBound
        Try
            Dim lnkFull As Button = TryCast(e.Item.FindControl("Button1"), Button)
            ScriptManager.GetCurrent(Me.Page).RegisterPostBackControl(lnkFull)

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub




End Class