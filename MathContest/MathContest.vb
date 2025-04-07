'ChristopherZ
'Spring 2025
'RCET2265
'Math Contest
'https://github.com/Christopher-isu/MathContest.git

Option Explicit On
Option Strict On

Public Class MathContest
    Private correctCount As Integer = 0 'Correct answers counter
    Private incorrectCount As Integer = 0 'Incorrect answers counter
    Private random As New Random() 'initialize random number generator
    Private toolTip As New ToolTip() 'declare tooltip object

    Private Sub MathContest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AcceptButton = SubmitButton ' Set submitt to respond to Enter key
        Me.CancelButton = ClearButton ' Set clear to respond to Esc key
        ResetRadioButtons() ' Reset radio buttons and random numbers
        GenerateToolTips() ' Generate tooltips for all TextBox controls
        ' Configure tooltip
        toolTip.ShowAlways = True 'Always show tooltip
        toolTip.AutoPopDelay = 5000 'The time the tooltip is displayed
        toolTip.InitialDelay = 300 'The delay before the tooltip appears
        toolTip.ReshowDelay = 500 'The delay before the tooltip is shown again

    End Sub

    Private Sub ResetRadioButtons()
        ' Uncheck all radio buttons except AddRadioButton
        AddRadioButton.Checked = True ' Default to Add
        SubtractRadioButton.Checked = False 'Clear other radio buttons
        MultiplyRadioButton.Checked = False
        DivideRadioButton.Checked = False

        ' Update random numbers in FirstNumberValue and SecondNumberValue
        UpdateRandomNumbers()
    End Sub

    ''' <summary>
    ''' The following section handles the CheckedChanged events for the radio buttons.
    ''' When a radio button is checked, it updates the random numbers displayed.
    '''</summary>
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
        Dim number1 As Integer = random.Next(1, 21) ' Random number between 1 and 20
        Dim number2 As Integer = random.Next(1, 21) ' Random number between 1 and 20
        FirstNumberValue.Text = number1.ToString() ' Convert to string
        SecondNumberValue.Text = number2.ToString() ' Convert to string
    End Sub

    Private Sub SubmitButton_Click(sender As Object, e As EventArgs) Handles SubmitButton.Click
        ' Get numbers from form objects
        Dim number1 As Integer = Convert.ToInt32(FirstNumberValue.Text)
        Dim number2 As Integer = Convert.ToInt32(SecondNumberValue.Text)
        Dim correctAnswer As Integer
        ' Validate inputs
        Dim validationResult As String = ValidateInputs()
        If Not String.IsNullOrEmpty(validationResult) Then ' Show error messages
            MessageBox.Show(validationResult, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Focus on the first invalid field
            Return
        End If



        ' Perform operation
        If AddRadioButton.Checked Then
            correctAnswer = number1 + number2 ' Addition
        ElseIf SubtractRadioButton.Checked Then
            correctAnswer = number1 - number2 ' Subtraction
        ElseIf MultiplyRadioButton.Checked Then
            correctAnswer = number1 * number2 ' Multiplication
        ElseIf DivideRadioButton.Checked Then
            correctAnswer = number1 \ number2 ' Integer division
        Else
            MessageBox.Show("Please select a math operation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'Messsage for user to select a math operation
            Return
        End If

        ' Compare answer
        Dim userAnswer As Integer
        If Integer.TryParse(AnswerTextBox.Text, userAnswer) AndAlso userAnswer = correctAnswer Then
            ' Check if the answer is correct
            MessageBox.Show("Congratulations! Correct answer.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            correctCount += 1 'Correct answers counter
        Else
            MessageBox.Show($"Incorrect. The correct answer is {correctAnswer}.", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Show the correct answer
            incorrectCount += 1 'Incorrect answers counter
        End If

        ' Clear RadioButtons and Answer field and update random numbers
        ResetRadioButtons()
        AnswerTextBox.Clear()
    End Sub

    Private Function ValidateInputs() As String 'Handles validation of inputs
        Dim errorMessages As New List(Of String)() 'List to store error messages

        ' Validate Name
        If String.IsNullOrWhiteSpace(NameTextBox.Text) OrElse Not NameTextBox.Text.All(AddressOf Char.IsLetter) Then
            errorMessages.Add("Student's name must contain only letters.")
            NameTextBox.Focus() ' Focus on NameTextBox
            Return String.Join(Environment.NewLine, errorMessages) 'Return error messages
        End If

        ' Validate Grade
        Dim grade As Integer
        If Not Integer.TryParse(GradeTextBox.Text, grade) OrElse grade < 1 OrElse grade > 4 Then
            errorMessages.Add("Grade must be between 1 and 4.")
            errorMessages.Add("Student not eligible to compete")
            GradeTextBox.Focus() ' Focus on GradeTextBox
            Return String.Join(Environment.NewLine, errorMessages) 'Return error messages
        End If

        ' Validate Age
        Dim age As Integer
        If Not Integer.TryParse(AgeTextBox.Text, age) OrElse age < 7 OrElse age > 11 Then
            errorMessages.Add("Age must be between 7 and 11.")
            errorMessages.Add("Student not eligible to compete")
            AgeTextBox.Focus() ' Focus on AgeTextBox
            Return String.Join(Environment.NewLine, errorMessages) 'Return error messages
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

    Private Sub GenerateToolTips()
        ' Define tooltips for each TextBox
        toolTip.SetToolTip(NameTextBox, "Enter the student's full name. Only letters are allowed.")
        toolTip.SetToolTip(GradeTextBox, "Enter the student's grade (1-4).")
        toolTip.SetToolTip(AgeTextBox, "Enter the student's age (7-11).")
        toolTip.SetToolTip(AnswerTextBox, "Enter the student's answer to the math problem.")
        toolTip.SetToolTip(FirstNumberValue, "This is the first random number generated.")
        toolTip.SetToolTip(SecondNumberValue, "This is the second random number generated.")
    End Sub

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close() ' Close the application
    End Sub

    Private Sub SummaryButton_Click(sender As Object, e As EventArgs) Handles SummaryButton.Click
        ' Show summary of correct and incorrect answers
        MessageBox.Show($"Correct: {correctCount}{Environment.NewLine}Incorrect: {incorrectCount}", "Summary", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
