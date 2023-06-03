
Public Class ChangeOperatorPassword
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
            'lblError.Text = ""
            'lblError.CssClass = ""
            'lbloldPass.Visible = False
            'lblnewPass.Visible = False
            'lblConfirmPass.Visible = False
            'If txtOldPassword.Text = "" Then
            '    lbloldPass.Visible = True
            '    Exit Sub
            'End If
            'If txtNewPassword.Text = "" Then
            '    lblnewPass.Visible = True
            '    Exit Sub
            'End If
            'If txtConfirmPassword.Text = "" Then
            '    lblConfirmPass.Visible = True
            '    Exit Sub
            'End If
            Dim str As String
            Dim Old_Encrypted_Pass As String = GV.EncryptString(GV.key, txtOldPassword.Text.Trim)
            Dim Encrypted_Pass As String = GV.EncryptString(GV.key, txtNewPassword.Text.Trim)


            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Distributor" Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Master Distributor" Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Retailer" Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Customer" Then
                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where [RegistrationId]='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' and AgentPassword='" & Old_Encrypted_Pass & "' ") > 0 Then

                    str = " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set AgentPassword='" & Encrypted_Pass & "'  where [RegistrationId]='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' ; "
                    GV.FL.DMLQueries(str)
                    clear()
                    lblError.Text = "Password Changed Successfully."
                    lblError.CssClass = "Successlabels"

                Else
                    lblError.Text = "Sorry !! Password Is Incorrect."
                    lblError.CssClass = "errorlabels"
                End If


            Else
                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where [User_ID]='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' and User_Password='" & GV.parseString(GV.EncryptString(GV.key, txtOldPassword.Text.Trim)) & "' ") > 0 Then

                    str = " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details set  User_Password='" & Encrypted_Pass & "'  where [User_ID]='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' ; "

                    If GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).ToString.ToUpper = "ADMIN" Then
                        If GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim.ToUpper = "BosCenter_DB".ToUpper Then
                            str = str & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ClientRegistration set  ClientPassword='" & Encrypted_Pass & "' , UpdatedBy='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "', UpdatedOn=getdate() where DatabaseName='" & GV.parseString(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) & "';"
                        Else
                            str = str & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ClientRegistration set  ClientPassword='" & Encrypted_Pass & "' UpdatedBy='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "', UpdatedOn=getdate() where DatabaseName='" & GV.parseString(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) & "';"
                            str = str & " update " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration set  ClientPassword='" & Encrypted_Pass & "' , UpdatedBy='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "', UpdatedOn=getdate() where DatabaseName='" & GV.parseString(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) & "';"
                        End If
                    End If

                    GV.FL.DMLQueries(str)
                    clear()
                    lblError.Text = "Password Changed Successfully."
                    lblError.CssClass = "Successlabels"

                Else
                    lblError.Text = "Sorry !! Password Is Incorrect."
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


    Public Sub ChangePasswordWithMd5()
        Try
            Dim str1 As String = ""
            Dim RegistrationId, AgentPassword, Encrypted_Pass As String
            Dim str As String = "select [RegistrationId],[AgentPassword], Encrypted_Pass FROM BosCenter_DB.dbo.[BOS_Dis_SubDis_Retailer_Registration]"
            DS = GV.FL.OpenDsWithSelectQuery(str)
            If Not DS Is Nothing Then
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To DS.Tables(0).Rows.Count - 1
                            If Not IsDBNull(DS.Tables(0).Rows(i).Item("RegistrationId")) Then
                                If Not DS.Tables(0).Rows(i).Item("RegistrationId").ToString() = "" Then
                                    RegistrationId = GV.parseString(DS.Tables(0).Rows(i).Item("RegistrationId").ToString())
                                End If
                            End If
                            If Not IsDBNull(DS.Tables(0).Rows(i).Item("AgentPassword")) Then
                                If Not DS.Tables(0).Rows(i).Item("AgentPassword").ToString() = "" Then
                                    AgentPassword = GV.parseString(DS.Tables(0).Rows(i).Item("AgentPassword").ToString())
                                End If
                            End If
                            'If Not IsDBNull(DS.Tables(0).Rows(i).Item("Encrypted_Pass")) Then
                            '    If Not DS.Tables(0).Rows(i).Item("Encrypted_Pass").ToString() = "" Then
                            '        Encrypted_Pass = GV.parseString(DS.Tables(0).Rows(i).Item("Encrypted_Pass").ToString())
                            '    End If
                            'End If
                            Encrypted_Pass = GV.EncryptString(GV.key, AgentPassword)

                            If str1 = "" Then
                                str1 = " update BosCenter_DB.dbo.BOS_Dis_SubDis_Retailer_Registration set AgentPassword='" & Encrypted_Pass & "'  where [RegistrationId]='" & RegistrationId & "' ; "

                            Else
                                str1 = str1 & " " & " update BosCenter_DB.dbo.BOS_Dis_SubDis_Retailer_Registration set AgentPassword='" & Encrypted_Pass & "' where [RegistrationId]='" & RegistrationId & "' ; "

                            End If


                        Next
                        str1 = str1

                    End If
                End If
            End If

            '
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub



End Class