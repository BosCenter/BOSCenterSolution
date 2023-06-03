
Public Class BOS_ChangePinNumber
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                clear()
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub



    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet
    Public Sub clear()
        txtOldPassword.Text = ""
        txtConfirmPassword.Text = ""
        txtNewPassword.Text = ""
        lblError.CssClass = ""
        'lbloldPass.Visible = False
        'lblnewPass.Visible = False
        'lblConfirmPass.Visible = False
        lblError.Text = ""
        txtOldPassword.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChangePassword.Click
        Try

            Dim str As String

            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Distributor" Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Master Distributor" Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Retailer" Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Customer" Then
                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where [RegistrationId]='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' and TransactionPin='" & GV.parseString(txtOldPassword.Text.Trim) & "' ") > 0 Then
                    str = " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set TransactionPin='" & GV.parseString(txtNewPassword.Text.Trim) & "'  where [RegistrationId]='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' ; "
                    GV.FL.DMLQueries(str)
                    clear()
                    lblError.Text = "PinNo Changed Successfully."
                    lblError.CssClass = "Successlabels"

                Else
                    lblError.Text = "Sorry !! PinNo Is Incorrect."
                    lblError.CssClass = "errorlabels"
                End If


            Else
                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where [User_ID]='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' and TransactionPin='" & GV.parseString(txtOldPassword.Text.Trim) & "' ") > 0 Then
                    str = " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details set TransactionPin='" & GV.parseString(txtNewPassword.Text.Trim) & "'  where [User_ID]='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' ; "

                    If GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).ToString.ToUpper = "ADMIN" Then
                        If GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim.ToUpper = "BosCenter_DB".ToUpper Then
                            str = str & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ClientRegistration set  ClientPin='" & GV.parseString(txtNewPassword.Text.Trim) & "', UpdatedBy='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "', UpdatedOn=getdate() where DatabaseName='" & GV.parseString(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) & "';"
                        Else
                            str = str & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ClientRegistration set  ClientPin='" & GV.parseString(txtNewPassword.Text.Trim) & "', UpdatedBy='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "', UpdatedOn=getdate() where DatabaseName='" & GV.parseString(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) & "';"
                            str = str & " update " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration set  ClientPin='" & GV.parseString(txtNewPassword.Text.Trim) & "', UpdatedBy='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "', UpdatedOn=getdate() where DatabaseName='" & GV.parseString(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) & "';"
                        End If
                    End If

                    GV.FL.DMLQueries(str)
                    clear()
                    lblError.Text = "PinNo Changed Successfully."
                    lblError.CssClass = "Successlabels"

                Else
                    lblError.Text = "Sorry !! PinNo Is Incorrect."
                    lblError.CssClass = "errorlabels"
                End If

            End If



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
End Class