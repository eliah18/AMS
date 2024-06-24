
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<ScriptService> _
Public Class DashboardData
    Inherits System.Web.Services.WebService
    Dim cnstr As String = System.Configuration.ConfigurationManager.AppSettings("connpath")
    <WebMethod> _
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)> _
    Public Function getChatindodata() As String
        Using conn As New SqlConnection()
            conn.ConnectionString = cnstr
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT * FROM IntellegoBarGraph ORDER BY ID"
                cmd.Connection = conn
                Dim adp = New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                adp.Fill(dt)
                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim rows As List(Of Dictionary(Of String, Object)) = New List(Of Dictionary(Of String, Object))()
                Dim row As Dictionary(Of String, Object)
                For Each dr As DataRow In dt.Rows
                    row = New Dictionary(Of String, Object)()
                    For Each col As DataColumn In dt.Columns
                        row.Add(col.ColumnName, dr(col))
                    Next col
                    rows.Add(row)
                Next dr
                Return serializer.Serialize(rows)
            End Using
        End Using
    End Function
End Class