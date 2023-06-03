Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D


Public Class BOS_MakePayment
    Inherits System.Web.UI.Page
    Dim GV As New GlobalVariable("Admin")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                    'dis,sd,re,cust
                    GV.FL.AddInDropDownListDistinct(ddnbanklist, "Bank_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Admin_BankAccount_Master")
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    'super admin
                Else
                    'admin and other employees 
                    GV.FL.AddInDropDownListDistinct(ddnbanklist, "Bank_Name", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_Admin_BankAccount_Master")
                End If

                If ddnbanklist.Items.Count > 0 Then
                    ddnbanklist.Items.Insert(0, ":::: Select Bank ::::")
                Else
                    ddnbanklist.Items.Add(":::: Select Bank ::::")
                End If
                lblRefrenceID.Text = GV.FL.getAutoNumber("RefrenceID")
                Session("EditFlag") = 0
            End If
        Catch ex As Exception

        End Try
    End Sub
    Dim VPaymentMode, RefrenceID, PaymentDate, DepositBankName, BranchCode_ChecqueNo, Remarks, TransactionID, DocumentPath, VAmount As String
    Protected Sub btnsubmit_Click(sender As Object, e As EventArgs) Handles btnsubmit.Click
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            ' VPaymentMode = rdbPaymentMode.SelectedItem.Text
            If rdbCash.Checked = True Then
                VPaymentMode = "Cash"
            ElseIf rdbCheque.Checked = True Then
                VPaymentMode = "Online Payment"
            ElseIf rdbNetBanking.Checked = True Then
                VPaymentMode = "Net Banking"
            ElseIf rdbATMTransfer.Checked = True Then
                VPaymentMode = "ATM Transfer"
            ElseIf rdbPosMachine.Checked = True Then
                VPaymentMode = "POS Machine"
            ElseIf rdbOthers.Checked = True Then
                VPaymentMode = "Others"
            Else
                lblError.Text = "Please Select Payment Mode."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            If GV.parseString(txtamount.Text.Trim) = "" Then

            End If

            Dim MinAmount As Decimal = 1
            If Not (txtamount.Text.Trim = "") Or Not (txtamount.Text.Trim = "0") Then

                VAmount = GV.parseString(txtamount.Text.Trim)
                If VAmount = "" Then
                    VAmount = "0"
                End If

                If CDec(VAmount) >= MinAmount Then
                    VAmount = GV.parseString(txtamount.Text.Trim)
                Else
                    lblError.Text = "Minimum Amount Should be greater or Equal to " & MinAmount
                    lblError.CssClass = "errorlabels"
                    txtamount.Focus()
                    Exit Sub
                End If
            End If
            If ddnbanklist.SelectedIndex = 0 Then
                lblError.Text = "Please Select Bank"
                lblError.CssClass = "errorlabels"
                ddnbanklist.Focus()
                Exit Sub
            Else
                DepositBankName = GV.parseString(ddnbanklist.SelectedValue.Trim)
            End If
            If txttransactionid.Text = "" Then
                lblError.Text = "Please Enter TransactionId"
                lblError.CssClass = "errorlabels"
                txttransactionid.Focus()
                Exit Sub
            Else
                TransactionID = GV.parseString(txttransactionid.Text.Trim)
            End If
            RefrenceID = GV.parseString(lblRefrenceID.Text.Trim)
            PaymentDate = Now.Date

            BranchCode_ChecqueNo = GV.parseString(txtbranchcode.Text.Trim)
            Remarks = GV.parseString(txtremarks.Text.Trim)

            Dim VPanCard_Path As String
            If Not btnUpload_PanCard.ToolTip.Trim = "" Then
                Dim fi As New FileInfo(btnUpload_PanCard.ToolTip.Trim)
                VPanCard_Path = "~/MakePaymentDocs/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "/" & lblRefrenceID.Text & "/" & fi.Name
            Else
                VPanCard_Path = ""
            End If
            If Session("EditFlag") = 0 Then
                Dim qry As String = ""

                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                    'dis,sd,re,cust
                    qry = "Insert into  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_MakePayemnts_Details] (Amount,RegistrationId,DocumentPath,PaymentMode,RefrenceID, PaymentDate, DepositBankName, BranchCode_ChecqueNo, Remarks, TransactionID, RecordDateTime,UpdatedBy,UpdatedOn,ApporvedStatus,CompanyCode) values (" & VAmount & ",'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','" & VPanCard_Path & "','" & VPaymentMode & "','" & RefrenceID & "','" & PaymentDate & "','" & DepositBankName & "','" & BranchCode_ChecqueNo & "','" & Remarks & "','" & TransactionID & "',getdate(),'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "',getdate(),'Pending','" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "')"
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    'super admin
                Else
                    'admin and other employees 
                    qry = "Insert into  " & GV.DefaultDatabase.Trim & ".dbo.[BOS_MakePayemnts_Details_SA] (Amount,RegistrationId,DocumentPath,PaymentMode,RefrenceID, PaymentDate, DepositBankName, BranchCode_ChecqueNo, Remarks, TransactionID, RecordDateTime,UpdatedBy,UpdatedOn,ApporvedStatus,CompanyCode) values (" & VAmount & ",'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','" & VPanCard_Path & "','" & VPaymentMode & "','" & RefrenceID & "','" & PaymentDate & "','" & DepositBankName & "','" & BranchCode_ChecqueNo & "','" & Remarks & "','" & TransactionID & "',getdate(),'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "',getdate(),'Pending','" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "')"
                End If



                If Not qry.Trim = "" Then

                    Dim Result As Boolean = GV.FL.DMLQueriesBulk(qry)
                    If Result = True Then

                        Dim destinationpath As String = "~/MakePaymentDocs/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "/" & lblRefrenceID.Text.Trim

                        If Not btnUpload_PanCard.ToolTip.Trim = "" Then
                            If Not Directory.Exists(Server.MapPath(destinationpath)) Then
                                Directory.CreateDirectory(Server.MapPath(destinationpath))
                            End If
                            If File.Exists(Server.MapPath(btnUpload_PanCard.ToolTip.Trim)) Then
                                File.Move(Server.MapPath(btnUpload_PanCard.ToolTip.Trim), Server.MapPath(VPanCard_Path))
                            End If
                        End If
                        Clear()
                        lblError.Text = "Request Submitted Successfully."
                        lblError.CssClass = "Successlabels"

                    Else
                        lblError.Text = "Process Can't be Completed."
                        lblError.Text = lblError.Text & Environment.NewLine & qry
                        lblError.CssClass = "errorlabels"
                    End If
                End If


            End If


        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.CssClass = "errorlabels"
        End Try
    End Sub

    Private Sub Clear()
        Try
            rdbCash.Checked = False
            rdbCheque.Checked = False
            rdbNetBanking.Checked = False
            rdbATMTransfer.Checked = False
            ddnbanklist.SelectedIndex = 0
            txtamount.Text = ""
            txtbranchcode.Text = ""
            txtremarks.Text = ""
            txttransactionid.Text = ""
            Image_PanCard.ImageUrl = "~/images/uploadimage.png"
            btnUpload_PanCard.Text = "Upload"
            lbltid.Visible = True
            txttransactionid.Visible = True
            Div_Upload.Visible = True
            lbltid.Visible = True
            txttransactionid.Visible = True
            lblbranchcode.Visible = True
            txtbranchcode.Visible = True
            lblbranchcode.Text = "Branch Name"

            btnDeleteUpload_PanCard.Enabled = False


            Session("EditFlag") = 0

            lblError.Text = ""
            lblError.CssClass = ""

        Catch ex As Exception
        End Try
    End Sub


    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Clear()
        Catch ex As Exception

        End Try
    End Sub
    Dim filePath As String = ""
    Dim filename As String = ""
    Dim ext As String = ""

    Public Sub SaveImage(ByVal imagUpload As FileUpload, ByVal UploadButtonName As Button, ByVal RemoveButtonName As Button, ByVal imgstr As String)
        Try
            Dim imgPath As String = ""
            If imagUpload.HasFile = True Then
                filePath = imagUpload.PostedFile.FileName
                filename = Path.GetFileName(filePath)
                ext = Path.GetExtension(filename)
                Session("ext") = ext
                If (UCase(Session("ext")) = UCase(".jpg")) Or (UCase(Session("ext")) = UCase(".jpeg")) Or (UCase(Session("ext")) = UCase(".png")) Or (UCase(Session("ext")) = UCase(".gif")) Then
                    Dim completeFilePath As String = ""


                    If Not Directory.Exists(Server.MapPath("~/Temp")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp"))
                    End If

                    If Not Directory.Exists(Server.MapPath("~/Temp/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim))
                    End If

                    completeFilePath = Server.MapPath("~/Temp" & "/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "/" & lblRefrenceID.Text.Trim & "_" & imgstr & Session("ext"))
                    imgPath = "~/Temp" & "/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "/" & lblRefrenceID.Text.Trim & "_" & imgstr & Session("ext")

                    imagUpload.PostedFile.SaveAs(completeFilePath)

                    Dim bytesInStream As Byte() = New Byte(imagUpload.PostedFile.InputStream.Length - 1) {}
                    Dim image As System.Drawing.Image = System.Drawing.Image.FromStream(imagUpload.PostedFile.InputStream)

                    Dim bmp As New Bitmap(image)
                    bmp = ImageOptimization(bmp, completeFilePath, filename, "THUMBNAIL")
                    bmp.Save(completeFilePath, System.Drawing.Imaging.ImageFormat.Jpeg)
                    'bmp.Save(completeFilePath + "/Thumbnail_" + filename, System.Drawing.Imaging.ImageFormat.Jpeg)

                    bmp.Dispose()


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



    Private Function ImageOptimization(ByVal image As Image, ByVal targetPath As String, ByVal fileName As String, ByVal imagetype As String) As Bitmap

        fileName = Path.GetFileName(fileName)
        fileName.Replace("\", "")
        Dim namefileName = fileName.Substring(0, fileName.LastIndexOf("."))
        Dim ext1 As String = Path.GetExtension(fileName)
        Dim imgPath As String = ""


        Dim reqWidth As Integer = 0
        Dim reqHeight As Integer = 0
        reqWidth = image.Width
        reqHeight = image.Height

        Dim thumbnailImg = Resize_Image(DirectCast(image, Bitmap), reqWidth, reqHeight)


        imgPath = Convert.ToString((targetPath & Convert.ToString("/"c)) + namefileName) & ext1

        If System.IO.File.Exists(imgPath) Then
            System.IO.File.Delete(imgPath)
        End If
        'thumbnailImg.Save(imgPath, image.RawFormat);

        Return thumbnailImg
    End Function

    Private Function Resize_Image(ByVal streamImage As Bitmap, ByVal maxWidth As Integer, ByVal maxHeight As Integer) As Bitmap
        Dim originalImage As New Bitmap(streamImage)
        Dim newWidth As Integer = originalImage.Width
        Dim newHeight As Integer = originalImage.Height
        Dim aspectRatio As Double = Convert.ToDouble(originalImage.Width) / Convert.ToDouble(originalImage.Height)

        If aspectRatio <= 1 AndAlso originalImage.Width > maxWidth Then
            newWidth = maxWidth
            newHeight = Convert.ToInt32(Math.Round(newWidth / aspectRatio))
        ElseIf aspectRatio > 1 AndAlso originalImage.Height > maxHeight Then
            newHeight = maxHeight
            newWidth = Convert.ToInt32(Math.Round(newHeight * aspectRatio))
        End If
        Return New Bitmap(originalImage, newWidth, newHeight)
    End Function

    Protected Sub btnDeleteUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteUpload_PanCard.Click
        Try

            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"

            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the document Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

            lblDeleteDocumentInfo.Text = "SlipAttach"
            ModalPopupExtender4.Show()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload_PanCard.Click
        Try
            Try
                lblErrorImage_PanCard.Text = ""
                lblErrorImage_PanCard.CssClass = ""
                If (btnUpload_PanCard.Text = "Download") Then
                    DownloadDoc(btnUpload_PanCard.ToolTip)
                Else

                    If FileUpload_PanCard.HasFile = True Then

                        filePath = FileUpload_PanCard.PostedFile.FileName
                        filename = Path.GetFileName(filePath)
                        ext = Path.GetExtension(filename)

                        If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then
                            SaveImage(FileUpload_PanCard, btnUpload_PanCard, btnDeleteUpload_PanCard, "SlipAttach")
                            Dim fi As New FileInfo(btnUpload_PanCard.ToolTip.ToString())
                            Dim ext As String = fi.Extension.ToUpper
                            If fi.Extension.ToUpper = UCase(".jpg") Or fi.Extension.ToUpper = UCase(".jpeg") Or fi.Extension.ToUpper = UCase(".png") Or fi.Extension.ToUpper = UCase(".gif") Then
                                Image_PanCard.ImageUrl = btnUpload_PanCard.ToolTip.ToString()
                                btnUpload_PanCard.Focus()

                            End If
                        Else
                            lblInformation.Text = "Invalid Image Type."
                            lblInformation.CssClass = "errorlabels"
                            ModalPopupExtender3.Show()
                            btnUpload_PanCard.Focus()
                        End If

                    Else
                        lblErrorImage_PanCard.Text = "Please Select Image To Upload."
                        lblErrorImage_PanCard.CssClass = "errorlabels"
                        btnUpload_PanCard.Focus()
                    End If

                End If
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
                Dim VMemberID As String = GV.parseString(lblRefrenceID.Text.Trim)


                Dim imgpath As String = btnUpload_PanCard.ToolTip
                File.Delete(Server.MapPath(imgpath))

                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                    'dis,sd,re,cust
                    qry = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_MakePayemnts_Details] set DocumentPath=''  where RefrenceID='" & VMemberID & "' "
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    'super admin
                Else
                    'admin and other employees 
                    qry = "update " & GV.DefaultDatabase.Trim & ".dbo.[BOS_MakePayemnts_Details] set DocumentPath=''  where RefrenceID='" & VMemberID & "' "
                End If


                btnUpload_PanCard.ToolTip = ""
                btnUpload_PanCard.Text = "Upload"
                btnDeleteUpload_PanCard.Enabled = False
                Image_PanCard.ImageUrl = "~/images/uploadimage.png"
                btnDeleteUpload_PanCard.Focus()

                lblDeleteInfo.Text = "Document Removed Successfully."
                lblDeleteInfo.CssClass = "successlabels"

                If Not qry = "" Then
                    GV.FL.DMLQueries(qry)
                End If

                btnDelete_Document.Visible = False
                btnDelete_Document.Text = "OK"
                btnCancelDeleteDocument.Text = "OK"

              
            Else

            End If
            ModalPopupExtender4.Show()
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

    Protected Sub rdbCash_CheckedChanged(sender As Object, e As EventArgs) Handles rdbCash.CheckedChanged
        Try
            If rdbCash.Checked = True Then
                lbltid.Text = "Transaction ID:"
                txttransactionid.Visible = True
                Div_Upload.Visible = True
                lbltid.Visible = True
                txttransactionid.Visible = True
                lblbranchcode.Visible = True
                txtbranchcode.Visible = True
                lblbranchcode.Text = "Branch Name"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rdbCheque_CheckedChanged(sender As Object, e As EventArgs) Handles rdbCheque.CheckedChanged
        Try
            If rdbCheque.Checked = True Then
                lbltid.Text = "Transaction ID:"
                txttransactionid.Visible = False
                Div_Upload.Visible = False
                lbltid.Visible = True
                txttransactionid.Visible = True
                Div_Upload.Visible = True
                lblbranchcode.Visible = False
                txtbranchcode.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rdbNetBanking_CheckedChanged(sender As Object, e As EventArgs) Handles rdbNetBanking.CheckedChanged
        Try
            If rdbNetBanking.Checked = True Then
                lbltid.Visible = True
                lbltid.Text = "Transaction ID:"
                txttransactionid.Visible = True
                Div_Upload.Visible = False
                lblbranchcode.Visible = False
                txtbranchcode.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rdbATMTransfer_CheckedChanged(sender As Object, e As EventArgs) Handles rdbATMTransfer.CheckedChanged
        Try
            If rdbATMTransfer.Checked = True Then
                lbltid.Text = "Transaction ID:"
                lblbranchcode.Visible = False
                txtbranchcode.Visible = False
                lbltid.Visible = True
                txttransactionid.Visible = True
                Div_Upload.Visible = True
            End If

        Catch ex As Exception

        End Try
    End Sub

   
    Protected Sub rdbPosMachine_CheckedChanged(sender As Object, e As EventArgs) Handles rdbPosMachine.CheckedChanged
        Try
            If rdbPosMachine.Checked = True Then
                lbltid.Text = "Tid No:"
                lblbranchcode.Visible = False
                txtbranchcode.Visible = False
                lbltid.Visible = True
                txttransactionid.Visible = True
                Div_Upload.Visible = True
            End If
            'lbltid.Text = "Transaction ID:"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rdbOthers_CheckedChanged(sender As Object, e As EventArgs) Handles rdbOthers.CheckedChanged
        Try
            If rdbOthers.Checked = True Then
                lbltid.Text = "Transaction ID:"
                lbltid.Visible = True
                txttransactionid.Visible = True
                Div_Upload.Visible = True

                txttransactionid.Visible = True
                lblbranchcode.Visible = True
                txtbranchcode.Visible = True
                lblbranchcode.Text = "Branch Name"
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class