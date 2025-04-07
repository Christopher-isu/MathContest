'ChristopherZ
'Spring 2025
'RCET2265
'Adress Label
'

Option Explicit On
Option Strict On

Public Class MathContest
    Private correctCount As Integer = 0
    Private incorrectCount As Integer = 0
    Private random As New Random()

    Private Sub MathContest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set AddRadioButton as default on form load
        ResetRadioButtons()
    End Sub

    Private Sub ResetRadioButtons()
        ' Uncheck all radio buttons except AddRadioButton
        AddRadioButton.Checked = True
        SubtractRadioButton.Checked = False
        MultiplyRadioButton.Checked = False
        DivideRadioButton.Checked = False
    End Sub

    Private Sub AddRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AddRadioButton.CheckedChanged
        If AddRadioButton.Checked Then UpdateRandomNumbers()
    End Sub

    Private Sub SubtractRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles SubtractRadioButton.CheckedChanged
        If SubtractRadioButton.Checked Then UpdateRandomNumbers()
    End Sub

    Private Sub MultiplyRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles MultiplyRadioButton.CheckedChanged
        If MultiplyRadioButton.Checked Then UpdateRandomNumbers()
    End Sub

    Private Sub DivideRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles DivideRadioButton.CheckedChanged
        If DivideRadioButton.Checked Then UpdateRandomNumbers()
    End Sub

    Private Sub UpdateRandomNumbers()
        ' Generate random numbers and update labels
        Dim number1 As Integer = random.Next(1, 21)
        Dim number2 As Integer = random.Next(1, 21)
        FirstNumberValue.Text = number1.ToString()
        SecondNumberValue.Text = number2.ToString()
    End Sub

    Private Sub SubmitButton_Click(sender As Object, e As EventArgs) Handles SubmitButton.Click
        ' Validate inputs
        Dim validationResult As String = ValidateInputs()
        If Not String.IsNullOrEmpty(validationResult) Then
            MessageBox.Show(validationResult, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Get numbers from form objects
        Dim number1 As Integer = Convert.ToInt32(FirstNumberValue.Text)
        Dim number2 As Integer = Convert.ToInt32(SecondNumberValue.Text)
        Dim correctAnswer As Integer

        ' Perform operation
        If AddRadioButton.Checked Then
            correctAnswer = number1 + number2
        ElseIf SubtractRadioButton.Checked Then
            correctAnswer = number1 - number2
        ElseIf MultiplyRadioButton.Checked Then
            correctAnswer = number1 * number2
        ElseIf DivideRadioButton.Checked Then
            correctAnswer = number1 \ number2 ' Integer division
        Else
            MessageBox.Show("Please select a math operation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Compare answer
        Dim userAnswer As Integer
        If Integer.TryParse(AnswerTextBox.Text, userAnswer) AndAlso userAnswer = correctAnswer Then
            MessageBox.Show("Congratulations! Correct answer.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            correctCount += 1
        Else
            MessageBox.Show($"Incorrect. The correct answer is {correctAnswer}.", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error)
            incorrectCount += 1

            ' Clear selected RadioButton and Answer field
            ResetRadioButtons()
            AnswerTextBox.Clear()
        End If
    End Sub

    Private Function ValidateInputs() As String
        Dim errorMessages As New List(Of String)()

        ' Validate Name
        If String.IsNullOrWhiteSpace(NameTextBox.Text) OrElse Not NameTextBox.Text.All(AddressOf Char.IsLetter) Then
            errorMessages.Add("Student's name must contain only letters.")
            NameTextBox.Focus()
            Return String.Join(Environment.NewLine, errorMessages)
        End If

        ' Validate Grade
        Dim grade As Integer
        If Not Integer.TryParse(GradeTextBox.Text, grade) OrElse grade < 1 OrElse grade > 4 Then
            errorMessages.Add("Grade must be between 1 and 4.")
            errorMessages.Add("Student not eligible to compete")
            GradeTextBox.Focus()
            Return String.Join(Environment.NewLine, errorMessages)
        End If

        ' Validate Age
        Dim age As Integer
        If Not Integer.TryParse(AgeTextBox.Text, age) OrElse age < 7 OrElse age > 11 Then
            errorMessages.Add("Age must be between 7 and 11.")
            errorMessages.Add("Student not eligible to compete")
            AgeTextBox.Focus()
            Return String.Join(Environment.NewLine, errorMessages)
        End If

        ' Combine error messages
        Return String.Join(Environment.NewLine, errorMessages)
    End Function

    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        ' Clear all fields
        NameTextBox.Clear()
        GradeTextBox.Clear()
        AgeTextBox.Clear()
        AnswerTextBox.Clear()
        FirstNumberValue.Text = "_"
        SecondNumberValue.Text = "_"
        ResetRadioButtons()
    End Sub

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub

    Private Sub SummaryButton_Click(sender As Object, e As EventArgs) Handles SummaryButton.Click
        MessageBox.Show($"Correct: {correctCount}{Environment.NewLine}Incorrect: {incorrectCount}", "Summary", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
