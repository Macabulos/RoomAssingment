Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class Form1
    ' Handle the login button click event
    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        Dim email As String = txtusername.Text.Trim()
        Dim password As String = txtpassword.Text.Trim()

        ' Input validation
        If String.IsNullOrEmpty(email) OrElse String.IsNullOrEmpty(password) Then
            MessageBox.Show("Please enter both email and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Hash the entered password
        Dim hashedPassword As String = HashPassword(password)

        ' SQL query to check if the user exists and the password matches
        Dim query As String = "SELECT * FROM users WHERE email = @Email AND password = @Password"

        ' Database connection and execution
        Dim db As New DatabaseHelper()
        Dim reader As MySqlDataReader = Nothing

        Try
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@Email", email},
                {"@Password", hashedPassword}
            }
            reader = db.ExecuteQuery(query, parameters)

            If reader.HasRows Then
                ' User found, successful login
                MessageBox.Show("Login successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Proceed to the Dashboard form
                Dim dashboard As New Dashboard()
                dashboard.Show()

                ' Hide the login form
                Me.Hide()
            Else
                ' User not found or invalid credentials
                MessageBox.Show("Invalid login credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Ensure reader is closed and resources are released
            If reader IsNot Nothing AndAlso Not reader.IsClosed Then
                reader.Close()
            End If
        End Try
    End Sub

    ' Method to hash the password
    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Return BitConverter.ToString(bytes).Replace("-", "").ToLower()
        End Using
    End Function

    ' Handle register label click event
    Private Sub Lbregister_Click(sender As Object, e As EventArgs) Handles Lbregister.Click
        Dim registerForm As New Register()
        registerForm.Show()
        Me.Hide()
    End Sub

    ' Event handlers for other UI elements
    Private Sub txtusername_TextChanged(sender As Object, e As EventArgs) Handles txtusername.TextChanged
        ' You can handle input-related events here if needed
    End Sub

    Private Sub txtpassword_TextChanged(sender As Object, e As EventArgs) Handles txtpassword.TextChanged
        ' You can handle input-related events here if needed
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        ' Implement functionality for "Show Password" checkbox if necessary
        txtpassword.UseSystemPasswordChar = Not CheckBox1.Checked
    End Sub

    Private Sub lbquit_Click(sender As Object, e As EventArgs) Handles lbquit.Click
        ' Confirm exit and close the application
        Dim result = MessageBox.Show("Are you sure you want to quit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub
End Class
