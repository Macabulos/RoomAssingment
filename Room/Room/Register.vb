Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class Register
    ' Handle the register button click event
    Private Sub btnregister_Click(sender As Object, e As EventArgs) Handles btnregister.Click
        Dim name As String = txtname.Text
        Dim email As String = txtusername.Text
        Dim password As String = txtpassword.Text

        ' Hash the password
        Dim hashedPassword As String = HashPassword(password)

        ' Create the SQL query to insert the user into the database
        Dim query As String = "INSERT INTO users (name, email, password) VALUES (@name, @email, @password)"

        ' Create a dictionary to hold the parameters
        Dim parameters As New Dictionary(Of String, Object) From {
        {"@name", name},
        {"@email", email},
        {"@password", hashedPassword}
    }

        ' Call the DatabaseHelper class to execute the query
        Dim db As New DatabaseHelper()
        db.ExecuteNonQuery(query, parameters)

        ' Show success message
        MessageBox.Show("User registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Redirect to the login form
        Dim loginForm As New Form1()
        loginForm.Show()
        Me.Hide()
    End Sub


    ' Method to hash the password
    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = sha256.Create()
            Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Return BitConverter.ToString(bytes).Replace("-", "").ToLower()
        End Using
    End Function

    Private Sub lbsignin_Click(sender As Object, e As EventArgs) Handles lbsignin.Click
        Dim Form1 As New Form1
        Form1.Show()
        Me.Hide()
    End Sub
End Class
