
Imports System.IO
Imports System.Net

Public Class CreateLogin
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim VCanLogin, VEmpSkill, VEmpType, VUser_Name, VUser_ID, VImagePath, VUser_Password, VUser_Type, VAccountStatus, VUser_CreationDate, VLoggedinStatus, VLastLoginTime, VUpdatedOn, VUpdatedBy As String
    Dim VEmailID, VMobileNO As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""
    Dim fromTime, totime As String
    Dim DS As New DataSet

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If btnSave.Text = "Save" Then
                Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
                Dim LocalDS As New DataSet
                LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
                Dim DataRows() As DataRow
                DataRows = LocalDS.Tables(0).Select(" FormName='" & CurentFOrm & "' ")
                If Not DataRows Is Nothing Then
                    If DataRows.Count > 0 Then
                        For D As Integer = 0 To DataRows.Count - 1
                            If Not DataRows(D).Item("CanSave") = True Then
                                lblDialogMsg.CssClass = ""
                                lblDialogMsg.Text = "Not Autorized To Performe This Action."
                                btnCancel.Text = "Ok"
                                Button2.Visible = False
                                ModalPopupExtender1.Show()
                                Exit Sub
                            End If
                        Next

                    End If
                End If
            ElseIf btnSave.Text = "Update" Then
                Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
                Dim LocalDS As New DataSet
                LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
                Dim DataRows() As DataRow
                DataRows = LocalDS.Tables(0).Select(" FormName='" & CurentFOrm & "' ")
                If Not DataRows Is Nothing Then
                    If DataRows.Count > 0 Then
                        For D As Integer = 0 To DataRows.Count - 1
                            If Not DataRows(D).Item("CanUpdate") = True Then
                                lblDialogMsg.CssClass = ""
                                lblDialogMsg.Text = "Not Autorized To Performe This Action."
                                btnCancel.Text = "Ok"
                                Button2.Visible = False
                                ModalPopupExtender1.Show()
                                Exit Sub
                            End If
                        Next

                    End If
                End If
            End If

            If UCase(btnUpload.Text = "Download") Then
                VImagePath = (btnUpload.ToolTip)

            Else
                VImagePath = ""
            End If

            If Not txtUser_Name.Text.Trim = "" Then
                VUser_Name = GV.parseString(txtUser_Name.Text.Trim)
            Else
                lblError.Text = "User Name Required."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If



            If rdbYes.Checked = True Then
                VCanLogin = "Yes"
            Else
                VCanLogin = "No"
            End If

            If Not txtUser_ID.Text.Trim = "" Then
                VUser_ID = GV.parseString(txtUser_ID.Text.Trim.ToUpper)
            Else
                lblError.Text = "User ID Required."
                lblError.CssClass = "errorlabels"

                Exit Sub
            End If

            If Not txtEmailID.Text.Trim = "" Then
                VEmailID = GV.parseString(txtEmailID.Text.Trim)
            Else
                VEmailID = ""
            End If

            If Not txtMobileNo.Text.Trim = "" Then
                VMobileNO = GV.parseString(txtMobileNo.Text.Trim)
            Else
                VMobileNO = ""
            End If

            If ddlEmployeType.SelectedValue.Trim = ":::: Select type ::::" Then
                lblError.Text = "Select Type."
                lblError.CssClass = "errorlabels"
                Exit Sub
            Else
                VEmpType = GV.parseString(ddlEmployeType.SelectedValue.Trim)
            End If

         

            If Not txtUser_Password.Text.Trim = "" Then
                VUser_Password = GV.parseString(txtUser_Password.Text.Trim)
            Else
                VUser_Password = ""
            End If

            VUser_Password = GV.EncryptString(GV.key, VUser_Password.Trim)



            fromTime = (GV.parseString(txtFromTime.Text.Trim) & "-" & ddlFromAm_Pm.SelectedValue.Trim)
            totime = GV.parseString(txtTotime.Text.Trim) & "-" & ddlToAm_Pm.SelectedValue.Trim

            VUser_Type = GV.parseString(ddlUser_Type.SelectedValue.Trim)
            VAccountStatus = GV.parseString(ddlAccountStatus.SelectedValue.Trim)
            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            If lblSessionFlag.Text = 0 Then

                'If GV.FL.RecCount("CRM_Login_Details where EmailID='" & GV.parseString(VEmailID) & "' ") > 0 Then 'Change where condition according to Criteria 
                '    lblError.Text = "Email id Already Exists."
                '    lblError.CssClass = "errorlabels"
                'Else
                '    If GV.FL.RecCount("CRM_Login_Detailss where MobileNO='" & GV.parseString(VMobileNO) & "' ") > 0 Then 'Change where condition according to Criteria 
                '        lblError.Text = "Mobile No Already Exists."
                '        lblError.CssClass = "errorlabels"
                '    Else


                '    End If
                'End If
                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where (User_ID='" & GV.parseString(txtUser_ID.Text.Trim) & "' and not User_ID='" & GV.parseString(lblUpadate.Text.Trim) & "')") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                Else
                    QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details (Encrypted_Pass,CanLogin,EmpType,EmailID,MobileNo,ImagePath,Fromtime,Totime,RecordStatus,User_Name,User_ID,User_Password,User_Type,AccountStatus,User_CreationDate,LoggedinStatus,LastLoginTime,UpdatedOn,UpdatedBy) values ('null' ,'" & VCanLogin & "', '" & VEmpType & "' ,'" & VEmailID & "','" & VMobileNO & "','" & VImagePath & "','" & fromTime & "','" & totime & "','Active','" & VUser_Name & "','" & VUser_ID & "','" & VUser_Password & "','" & VUser_Type & "','" & VAccountStatus & "', getdate(),'No',getdate(),getdate(),'" & VUpdatedBy & "' );"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        Clear()
                        Bind()
                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"
                    Else
                        lblError.Text = "Sorry!! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If
            ElseIf lblSessionFlag.Text = 1 Then

                Dim str As String = "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where User_ID='" & GV.parseString(txtUser_ID.Text.Trim) & "' and not User_ID='" & GV.parseString(lblUpadate.Text.Trim) & "' and EmailId='" & VEmailID & "' and not EmailId='" & GV.parseString(lblOldEmailId.Text.Trim) & "' and MobileNo='" & VMobileNO & "' and not MobileNo='" & GV.parseString(lblOldMobileNo.Text.Trim) & "'"

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where User_ID='" & GV.parseString(txtUser_ID.Text.Trim) & "' and not User_ID='" & GV.parseString(lblUpadate.Text.Trim) & "' ") > 0 Then
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"

                Else

                    QryStr = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details set  Encrypted_Pass='nul' , CanLogin='" & VCanLogin & "',EmpType='" & VEmpType & "', EmailId='" & VEmailID & "',MobileNo='" & VMobileNO & "', ImagePath='" & VImagePath & "', Fromtime='" & fromTime & "', Totime='" & totime & "', User_Name='" & VUser_Name & "', User_Password='" & VUser_Password & "', User_Type='" & VUser_Type & "', AccountStatus='" & VAccountStatus & "', UpdatedOn=getdate(), UpdatedBy='" & VUpdatedBy & "' where RID=" & lblRID.Text.Trim & ";"

                    If VUser_ID.Trim.ToUpper = "ADMIN" Then

                        If GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim.ToUpper = "BosCenter_DB".ToUpper Then
                            QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ClientRegistration set Encrypted_Pass='null' ,  ClientPassword='" & GV.parseString(VUser_Password) & "', UpdatedBy='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "', UpdatedOn=getdate() where DatabaseName='" & GV.parseString(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) & "';"
                        Else
                            QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ClientRegistration set Encrypted_Pass='null' ,  ClientPassword='" & GV.parseString(VUser_Password) & "', UpdatedBy='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "', UpdatedOn=getdate() where DatabaseName='" & GV.parseString(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) & "';"
                            QryStr = QryStr & " update " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration set  Encrypted_Pass='null' , ClientPassword='" & GV.parseString(VUser_Password) & "', UpdatedBy='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "', UpdatedOn=getdate() where DatabaseName='" & GV.parseString(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) & "';"
                        End If

                    End If

                    If GV.FL.DMLQueries(QryStr) = True Then
                        lblSessionFlag.Text = 0
                        Clear()
                        Bind()
                        lblError.Text = "Record Updated Successfully."
                        lblError.CssClass = "Successlabels"
                    Else
                        lblError.Text = "Sorry!! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If

                End If
            End If




        Catch ex As Exception
        End Try
    End Sub

    Public Sub Bind()
        Try
            Dim Querystring As String = ""
            Dim s As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".ToUpper Then
                Querystring = "select RID as SrNo,User_Name as 'Operator Name',User_ID as 'Login Id',User_Password as Password,User_Type as 'Group',AccountStatus as 'Status',Fromtime as 'From',Totime as 'To',EmailID,MobileNo,EmpType,CanLogin from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details order by RID desc  "
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Admin".ToUpper Then
                Querystring = "select RID as SrNo,User_Name as 'Operator Name',User_ID as 'Login Id',User_Password as Password,User_Type as 'Group',AccountStatus as 'Status',Fromtime as 'From',Totime as 'To',EmailID,MobileNo,EmpType,CanLogin from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where RecordStatus='Active' and not User_Type='Super Admin' order by RID desc  "
            Else
            End If

            'Dim Querystring As String = "select RID as SrNo,User_Name as 'Operator Name',User_ID as 'Login Id',User_Password as Password,User_Type as 'Group',AccountStatus as 'Status',Fromtime as 'From',Totime as 'To',EmailID,MobileNo,BranchCode from CRM_Login_Details where RecordStatus='Active' " & Filter & " order by RID desc  "
            DS = GV.FL.OpenDsWithSelectQuery(Querystring)
            If Not Querystring = "" Then
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)
                GridView1.DataBind()
                If GridView1.Rows.Count > 0 Then
                    lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                    GV.FL.showSerialnoOnGridView(GridView1, 1)
                Else
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Clear()
        Try
            lblheading.Text = "Employee Registration Form"
            rdbNo.Checked = False
            rdbYes.Checked = False
            btnUpload.Text = "Upload"
            btnDeleteUpload.Enabled = False
            Image1.ImageUrl = "..\images\logo_login2.png"
            lblError.CssClass = ""
            VUser_Name = ""
            VUser_ID = ""
            VUser_Password = ""
            VUser_Type = ""
            VAccountStatus = ""
            VUser_CreationDate = ""
            VLoggedinStatus = ""
            VLastLoginTime = ""
            VUpdatedOn = ""
            VUpdatedBy = ""
            txtUser_Name.Text = ""
            txtUser_ID.Text = ""
            lblloginIdError.Text = ""
            lblloginIdError.CssClass = ""
            txtUser_Password.Text = ""
            txtEmailID.Text = ""
            txtMobileNo.Text = ""
            ddlEmployeType.SelectedIndex = 0
            ddlUser_Type.SelectedIndex = 0
            ddlAccountStatus.SelectedIndex = 0
            txtFromTime.Text = "09" & ":" & "00"
            txtTotime.Text = "06" & ":" & "00"
            lblErrorImageError.Text = ""
            lblErrorImageError.CssClass = ""
            ddlFromAm_Pm.SelectedIndex = 0
            ddlToAm_Pm.SelectedIndex = 1
            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            txtUser_ID.ReadOnly = False
            lblError.Text = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnGrdRowDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
            Dim LocalDS As New DataSet
            LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
            Dim DataRows() As DataRow
            DataRows = LocalDS.Tables(0).Select(" FormName='" & CurentFOrm & "' ")
            If Not DataRows Is Nothing Then
                If DataRows.Count > 0 Then
                    For D As Integer = 0 To DataRows.Count - 1
                        If Not DataRows(D).Item("CanDelete") = True Then
                            lblDialogMsg.CssClass = ""
                            lblDialogMsg.Text = "Not Autorized To Performe This Action."
                            btnCancel.Text = "Ok"
                            Button2.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    Next

                End If
            End If

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(1).FindControl("lblgrdRID"), Label)
            Dim LoginID As String = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            If LoginID.Trim.ToUpper = "Admin".Trim.ToUpper Then
                lblRID.Text = lbl.Text
                lblDialogMsg.Text = "You Can't Delete Admin."
                lblDialogMsg.CssClass = "errorlabels"
                btnCancel.Text = "OK"
                btnok.Visible = False

            Else
                lblRID.Text = lbl.Text
                lblDialogMsg.Text = "Are you sure you want to delete ?"
                btnCancel.Text = "Cancel"
                btnok.Visible = True
                lblDialogMsg.CssClass = ""

            End If

         

            ModalPopupExtender1.Show()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try


            If Not lblRID.Text = "" Then
                '  Dim result As Boolean = GV.FL.DMLQueries("delete from Insion_MLM_CLientRegistration where RID=" & lblRID.Text & "")
                Dim result As Boolean = GV.FL.DMLQueries("delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details  where RID=" & lblRID.Text & "")
                lblDialogMsg.Text = result
                If result = True Then
                    lblDialogMsg.Text = "Record deleted Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                    Bind()
                    Clear()
                Else
                    lblDialogMsg.Text = "Sorry !! Record deletion Failed."
                    lblDialogMsg.CssClass = "errorlabels"
                End If
                btnCancel.Text = "OK"
                btnok.Visible = False
                ModalPopupExtender1.Show()
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            Clear()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
              
                lblheading.Text = "Employee Registration Form"


                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).ToUpper = "Super Admin".ToUpper Then
                    GV.FL.AddInDropDownListDistinct(ddlUser_Type, "Group_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Group_Master")
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Admin" Then
                    GV.FL.AddInDropDownListDistinct(ddlUser_Type, "Group_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Group_Master where not Group_name='Super Admin' ")
                Else

                End If


                ddlUser_Type.Items.Insert(0, ":::: Select Group ::::")
                Bind()
                txtFromTime.Text = "09" & ":" & "00"
                txtTotime.Text = "06" & ":" & "00"
                ddlFromAm_Pm.SelectedIndex = 0
                ddlToAm_Pm.SelectedIndex = 1
                lblSessionFlag.Text = 0



                GV.FL.AddInDropDownListDistinct(ddlEmployeType, "Employee_Type", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_EmployeeTypeMaster")
                If ddlEmployeType.Items.Count > 0 Then
                    ddlEmployeType.Items.Insert(0, ":::: Select Type ::::")
                    ddlEmployeType.SelectedIndex = 0
                Else
                    ddlEmployeType.Items.Add(":::: Select Type ::::")
                    ddlEmployeType.SelectedIndex = 0
                End If

              
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            
                Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
                Dim LocalDS As New DataSet
                LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
                Dim DataRows() As DataRow
                DataRows = LocalDS.Tables(0).Select(" FormName='" & CurentFOrm & "' ")
                If Not DataRows Is Nothing Then
                    If DataRows.Count > 0 Then
                        For D As Integer = 0 To DataRows.Count - 1
                            If Not DataRows(D).Item("CanUpdate") = True Then
                            lblDialogMsg.CssClass = ""
                            lblDialogMsg.Text = "Not Autorized To Performe This Action."
                            btnCancel.Text = "Ok"
                            Button2.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                            End If
                        Next

                    End If
                End If


            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(1).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text
            txtUser_Name.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            txtUser_ID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            lblUpadate.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            txtUser_Password.Text = GV.DecryptString(GV.key, GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text))
            ddlUser_Type.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
            ddlAccountStatus.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)

            txtEmailID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text)
            txtMobileNo.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text)
            ddlEmployeType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(11).Text)

            If txtUser_ID.Text.Trim.ToUpper = "Admin".Trim.ToUpper Then
                ddlUser_Type.Enabled = False
                ddlAccountStatus.Enabled = False
                ddlUser_Type.CssClass = "form-control"
                ddlAccountStatus.CssClass = "form-control"
            Else
                ddlUser_Type.Enabled = True
                ddlAccountStatus.Enabled = True
                ddlUser_Type.CssClass = "form-control"
                ddlAccountStatus.CssClass = "form-control"
            End If


            VCanLogin = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(12).Text)
            If VCanLogin = "Yes" Then
                rdbYes.Checked = True
            ElseIf VCanLogin = "No" Then
                rdbNo.Checked = True
            Else
                rdbYes.Checked = False
                rdbNo.Checked = False

            End If


            lblOldEmailId.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text)


            lblOldMobileNo.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text)

            ' lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)

            Dim fromtime() As String = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text).ToString().Split("-")
            txtFromTime.Text = fromtime(0)
            ddlFromAm_Pm.SelectedValue = fromtime(1)
            Dim Totime() As String = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text).ToString().Split("-")
            txtTotime.Text = Totime(0)
            ddlToAm_Pm.SelectedValue = Totime(1)

            If Not lblRID.Text = "" And Not txtUser_ID.Text = "" Then
                DS = GV.FL.OpenDs("CRM_Login_Details where User_ID='" & GV.parseString(txtUser_ID.Text.Trim) & "'")
                If DS.Tables(0).Rows.Count > 0 Then

                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("ImagePath")) Then
                        If Not DS.Tables(0).Rows(0).Item("ImagePath") = "" Then
                            Image1.ImageUrl = DS.Tables(0).Rows(0).Item("ImagePath").ToString()
                            btnUpload.ToolTip = DS.Tables(0).Rows(0).Item("ImagePath").ToString()
                            'If File.Exists(DS.Tables(0).Rows(0).Item("ImagePath").ToString()) Then

                            'Else
                            '    Image1.ImageUrl = "~/images/MissingIcon3.png"
                            'End If
                            Image1.Visible = True
                            btnUpload.Text = "Download"
                            btnDeleteUpload.Enabled = True
                        Else
                            Image1.ImageUrl = "..\images\logo_login2.png"
                            Image1.Visible = True
                            btnUpload.Text = "Upload"
                            btnDeleteUpload.Enabled = False
                        End If

                    Else
                        Image1.ImageUrl = "..\images\logo_login2.png"
                        Image1.Visible = True
                        btnUpload.Text = "Upload"
                        btnDeleteUpload.Enabled = False
                    End If
                    
                    
                End If
            End If

            lblSessionFlag.Text = 1
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            txtUser_ID.ReadOnly = True
            lblError.CssClass = ""
            lblError.Text = ""
            lblheading.Text = "Edit Employee Details"
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try

            Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
            Dim LocalDS As New DataSet
            LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
            Dim DataRows() As DataRow
            DataRows = LocalDS.Tables(0).Select(" FormName='" & CurentFOrm & "' ")
            If Not DataRows Is Nothing Then
                If DataRows.Count > 0 Then
                    For D As Integer = 0 To DataRows.Count - 1
                        If Not DataRows(D).Item("CanDelete") = True Then
                            lblDialogMsg.CssClass = ""
                            lblDialogMsg.Text = "Not Autorized To Performe This Action."
                            btnCancel.Text = "Ok"
                            Button2.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    Next

                End If
            End If

            If txtUser_ID.Text.Trim.ToUpper = "Admin".Trim.ToUpper Then

                lblDialogMsg.Text = "You Can't Delete Admin."
                lblDialogMsg.CssClass = "errorlabels"
                btnCancel.Text = "OK"
                btnok.Visible = False

            Else

                lblDialogMsg.Text = "Are you sure you want to delete ?"
                btnCancel.Text = "Cancel"
                btnok.Visible = True
                btnok.Text = "Yes"
                lblDialogMsg.CssClass = ""

            End If
            ModalPopupExtender1.Show()
        Catch ex As Exception

        End Try
    End Sub

 

    Protected Sub txtUser_ID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtUser_ID.TextChanged
        Try
            lblloginIdError.Text = ""
            lblloginIdError.CssClass = ""
            If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where User_ID='" & txtUser_ID.Text.Trim & "'") > 0 Then 'Change where condition according to Criteria 
                txtUser_ID.Text = ""
                lblloginIdError.Text = "Not Available."
                lblloginIdError.CssClass = "errorlabels-sm"
            Else
                lblloginIdError.Text = "Available."
                lblloginIdError.CssClass = "Successlabels_sm"
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Try
            lblErrorImageError.Text = ""
            lblDialogMsg.CssClass = ""
            lblDialogMsg.Text = ""
            If (btnUpload.Text = "Download") Then
                DownloadDoc(btnUpload.ToolTip)

            Else
                If FileUpload1.HasFile = True Then
                    SaveImage(FileUpload1, btnUpload, btnDeleteUpload)
                    Image1.ImageUrl = btnUpload.ToolTip.ToString()
                    lblDialogMsg.Text = "Image Uploaded Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                    btnok.Visible = False
                    btnCancel.Text = "Ok"
                    ModalPopupExtender1.Show()
                    'lblErrorImageError.Text = "Image Uploaded Successfully."
                    'lblErrorImageError.CssClass = "Successlabels"
                Else
                    lblDialogMsg.Text = "No file Selected for photo"
                    lblDialogMsg.CssClass = "errorlabels"
                    btnok.Enabled = False
                    btnCancel.Text = "Ok"
                    ModalPopupExtender1.Show()
                    'lblErrorImageError.Text = "No file Selected for photo"
                    'lblErrorImageError.CssClass = "errorlabels"
                    btnSave.Focus()
                End If
            End If

        Catch ex As Exception


        End Try
    End Sub

    Dim filePath As String = ""
    Dim filename As String = ""
    Dim ext As String = ""

    Public Sub SaveImage(ByVal imagUpload As FileUpload, ByVal UploadButtonName As Button, ByVal RemoveButtonName As Button)
        Try
            Dim imgPath As String = ""
            If imagUpload.HasFile = True Then
                filePath = imagUpload.PostedFile.FileName
                filename = Path.GetFileName(filePath)
                ext = Path.GetExtension(filename)
                Session("ext") = ext
                If (UCase(Session("ext")) = UCase(".jpg")) Or (UCase(Session("ext")) = UCase(".jpeg")) Or (UCase(Session("ext")) = UCase(".png")) Or (UCase(Session("ext")) = UCase(".gif")) Then
                    Dim completeFilePath As String

                    If Not Directory.Exists(Server.MapPath("..\OperatorImages")) Then
                        Directory.CreateDirectory(Server.MapPath("..\OperatorImages"))
                    End If

                    If Not Directory.Exists(Server.MapPath("..\OperatorImages\" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)) Then
                        Directory.CreateDirectory(Server.MapPath("..\OperatorImages\" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim))
                    End If



                    completeFilePath = Server.MapPath("..\OperatorImages\" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "\" & txtUser_ID.Text & Session("ext"))
                    'imgPath = Server.MapPath("~\Docs\" & txtPatientID.Text & "\" & txtPrescriptionNo.Text & "\Doppler" & Session("ext"))
                    imgPath = "..\OperatorImages\" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "\" & txtUser_ID.Text & Session("ext")

                    imagUpload.PostedFile.SaveAs(completeFilePath)

                    UploadButtonName.ToolTip = imgPath
                    UploadButtonName.Text = "Download"
                    RemoveButtonName.Enabled = True
                Else

                End If
            Else

            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnDeleteUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteUpload.Click
        Try
            Try
                btnDelete_Document.Visible = True
                btnDelete_Document.Text = "Yes"
                btnCancelDeleteDocument.Text = "No"

                lblDeleteInfo.CssClass = ""
                lblDeleteInfo.Text = "This Action will Remove the Logo Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

                ModalPopupExtender4.Show()



                'File.Delete(Server.MapPath("..\OperatorImages\" & txtUser_ID.Text & ".jpg"))
                'File.Delete(Server.MapPath("..\OperatorImages\" & txtUser_ID.Text & ".jpeg"))
                'File.Delete(Server.MapPath("..\OperatorImages\" & txtUser_ID.Text & ".png"))
                'File.Delete(Server.MapPath("..\OperatorImages\" & txtUser_ID.Text & ".gif"))

                'QryStr = "update CRM_Login_Details set ImagePath='' where User_ID='" & GV.parseString(txtUser_ID.Text.Trim) & "';"
                'GV.FL.DMLQueries(QryStr)
                'lblErrorImageError.Text = "Image Remove Successfully."
                'lblErrorImageError.CssClass = "errorlabels"
                'btnUpload.ToolTip = ""
                'btnUpload.Text = "Upload"
                'btnDeleteUpload.Enabled = False
                'Image1.ImageUrl = "..\images\logo_login2.png"
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDelete_Document_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_Document.Click
        Try
            '===========Delete Uploaded Document Popup=============================
            If btnDelete_Document.Text = "Yes" Then
                Dim qry As String = ""
                'VCompanyCode = GV.parseString(txtUser_ID.Text.Trim)
                'VCompanyCode = GV.get_Admin_SessionVariables("CompanyCode", Request, Response)

                ' If lblDeleteDocumentInfo.Text = "IncomeProof" Then

                Dim imgpath As String = btnUpload.ToolTip
                If Directory.Exists(Server.MapPath(imgpath)) Then
                    File.Delete(Server.MapPath(imgpath))
                End If

                qry = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details set ImagePath=''  where User_ID='" & GV.parseString(txtUser_ID.Text.Trim) & "' "
                btnUpload.ToolTip = ""
                btnUpload.Text = "Upload"
                btnDeleteUpload.Enabled = False
                Image1.ImageUrl = "..\images\logo_login2.png"
                btnDeleteUpload.Focus()

                lblDeleteInfo.Text = "Document Removed Successfully."
                lblDeleteInfo.CssClass = "successlabels"
                'End If


                If Not qry = "" Then
                    GV.FL.DMLQueries(qry)
                End If

                btnDelete_Document.Visible = False
                btnDelete_Document.Text = "OK"
                btnCancelDeleteDocument.Text = "OK"
                lblErrorImageError.Text = ""
                lblErrorImageError.CssClass = ""
                'btnCancelDeleteDocument.Attributes("style") = "display:none"

            Else

            End If
            ModalPopupExtender4.Show()
        Catch ex As Exception

        End Try
    End Sub


    Public Sub DownloadDoc(ByVal DocPath As String)
        Try
            Dim strURL As String = DocPath.ToString().Replace("~", "")
            Dim fi As New FileInfo(Server.MapPath(strURL))
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
            GridView1.PageIndex = e.NewPageIndex
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.PageIndexChanged
        Try
            Bind()
        Catch ex As Exception
        End Try

    End Sub


    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ImagebtnExcel.Click
        Try
            '  Dim Excelqry As String = "select RID as SrNo, Product_Date,Product_OrderCategory,Product_Code,Product_Descp,Product_BrandCode,Product_Num,Product_Pic from KRAFTS_ProductID "
            If GridView1.Rows.Count > 0 Then
                GV.ExportToExcel_New(GridView1, Response, "", "EmployeeDetails", lblExportQry.Text, "dyanamic")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ImagebtnWOrd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ImagebtnWOrd.Click
        Try
            '  Dim Wordqry As String = "select RID as SrNo, Product_Date,Product_OrderCategory,Product_Code,Product_Descp,Product_BrandCode,Product_Num,Product_Pic from KRAFTS_ProductID "
            If GridView1.Rows.Count > 0 Then
                GV.ExportToWord_New(GridView1, Response, "EmployeeDetails", lblExportQry.Text, "dyanamic")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Imagepdf_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Imagepdf.Click
        Try
            ' Dim PDFqry As String = "select RID as SrNo, Product_Date,Product_OrderCategory,Product_Code,Product_Descp,Product_BrandCode,Product_Num,Product_Pic from KRAFTS_ProductID "
            If GridView1.Rows.Count > 0 Then
                GV.ExportToPdf_New(GridView1, "", Response, "EmployeeDetails", lblExportQry.Text, "dyanamic")
            End If
        Catch ex As Exception
        End Try
    End Sub


    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub

  
End Class