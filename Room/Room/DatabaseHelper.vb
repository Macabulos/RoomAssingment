Imports MySql.Data.MySqlClient

Public Class DatabaseHelper
    Private connectionString As String = "Server=localhost; Database=adfc; Uid=root; Pwd=;"

    ' Function to get a MySQL connection
    Public Function GetConnection() As MySqlConnection
        Return New MySqlConnection(connectionString)
    End Function

    ' Function to execute a query and return the result
    Public Function ExecuteQuery(query As String, Optional parameters As Dictionary(Of String, Object) = Nothing) As MySqlDataReader
        Dim connection As MySqlConnection = GetConnection()
        Try
            connection.Open()
            Dim cmd As New MySqlCommand(query, connection)

            If parameters IsNot Nothing Then
                For Each param In parameters
                    cmd.Parameters.AddWithValue(param.Key, param.Value)
                Next
            End If

            Return cmd.ExecuteReader()
        Catch ex As Exception
            Throw New Exception("Error executing query: " & ex.Message)
        End Try
    End Function

    ' Function to execute non-query commands (insert/update/delete)
    Public Sub ExecuteNonQuery(query As String, Optional parameters As Dictionary(Of String, Object) = Nothing)
        Dim connection As MySqlConnection = GetConnection()
        Try
            connection.Open()
            Dim cmd As New MySqlCommand(query, connection)

            If parameters IsNot Nothing Then
                For Each param In parameters
                    cmd.Parameters.AddWithValue(param.Key, param.Value)
                Next
            End If

            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Error executing non-query: " & ex.Message)
        End Try
    End Sub
End Class
