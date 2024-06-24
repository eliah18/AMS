Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class index_Farai
    Inherits System.Web.UI.Page
    Dim adp As SqlDataAdapter
    Dim con As New SqlConnection
    Dim cnstr As String = System.Configuration.ConfigurationManager.AppSettings("connpath")
    Public LoginURL As String = System.Configuration.ConfigurationManager.AppSettings("LoginURL")
    Public SessionTimoutTime As Integer
    Public Sub msgbox(ByVal strMessage As String)
        'finishes server processing, returns to client.
        Dim strScript As String = "<script language=JavaScript>"
        strScript += "window.alert(""" & strMessage & """);"
        strScript += "</script>"
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = strScript
        Page.Controls.Add(lbl)
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Page.MaintainScrollPositionOnPostBack = True
            If Not IsPostBack Then

            End If
        Catch ex As Exception
            ErrorLogging.WriteLogFile(HttpContext.Current.Session("Username"), HttpContext.Current.Request.Url.ToString & " --- page_load()", ex.ToString)
        End Try
    End Sub
End Class
