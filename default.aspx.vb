Imports System.Net
Imports System.Net.Mail
Imports System.IO

Partial Class _default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.ServerVariables("HTTPS").ToUpper = "OFF" Then
            Dim url As String = Request.ServerVariables("SERVER_NAME")
            Dim page As String = Request.ServerVariables("SCRIPT_NAME")
            Dim qrystring As String = Request.ServerVariables("QUERY_STRING")

            'redirect to current url with secure protocol
            Response.Redirect("https://" & url & page & "?" & qrystring)
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim strEmailBody As String = ""
        Dim strUserIP As String = ""
        Dim boolPageValid As Boolean = False

        Try
            strUserIP = Request.UserHostAddress
        Catch ex As Exception

        End Try

        Page.Validate()

        If Page.IsValid Then
            boolPageValid = True
        End If

        If boolPageValid Then
            strEmailBody += "<p><b>Topic</b><br />" & ddlTopic.SelectedValue.ToString & "</p>"
            strEmailBody += "<p><b>Name</b><br />" & txtFirstName.Text.ToString & "</p>"
            strEmailBody += "<p><b>Email</b><br />" & txtEmail.Text.ToString & "</p>"
            strEmailBody += "<p><b>Message</b><br />" & txtMessage.Text.ToString & "</p>"
        Else
            Exit Sub
        End If

        Try
            Dim NewMail As New MailMessage
            Dim FromAddress As New MailAddress("info@pharmacare-wichita.com")
            'Dim ToOwner As New MailAddress("info@pharmacare-wichita.com")
            Dim ToOwner As New MailAddress("info@pharmacare-wichita.com")
            Dim ToAdmin As New MailAddress("vladimir@highfusion.com")
            Dim ToJennifer As New MailAddress("jlregan@pharmacare-wichita.com")
            Dim MailClient As New SmtpClient

            If fuAttachment.HasFile Then
                Try
                    Dim FileName As String = Path.GetFileName(fuAttachment.FileName)
                    Dim AttachmentFile As Attachment = New Attachment(fuAttachment.FileContent, FileName)
                    NewMail.Attachments.Add(AttachmentFile)
                Catch
                    strEmailBody += "<p>Note: Wesbite tried and failed to attach file to this email</p>"
                End Try
            End If

            NewMail.From = FromAddress
            NewMail.To.Add(ToOwner)
            NewMail.CC.Add(ToAdmin)
            NewMail.CC.Add(ToJennifer)

            NewMail.Subject = "Pharmacare website message: " & ddlTopic.SelectedItem.Text.ToString
            NewMail.Body = strEmailBody
            NewMail.IsBodyHtml = True
            MailClient.Send(NewMail)

            txtFirstName.Text = ""
            txtEmail.Text = ""
            txtMessage.Text = ""
            ddlTopic.Text = ""

            Response.Redirect("#thankyou")

        Catch ex As Exception
            lblMessage.Text += "<p>Oops. The message could not be sent. We apologize for the inconvenience.</p>"
            lblMessage.Text += "<p>" & ex.Message & ".</p>"
            lblMessage.CssClass = "error"
            lblMessage.Visible = True
            Exit Sub
        End Try

    End Sub
End Class
