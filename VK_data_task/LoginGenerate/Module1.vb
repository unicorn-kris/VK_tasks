Imports System.IO

Module Module1

    Sub Main()
        Dim inputFilePath As String = "C:\Users\User\Downloads\Telegram Desktop\Задания_разраб_IDM\Задания_разраб\ДопЗадачи\Ответ\input.txt"
        Dim outputFilePath As String = "C:\Users\User\Downloads\Telegram Desktop\Задания_разраб_IDM\Задания_разраб\ДопЗадачи\Ответ\output.txt"

        GenerateLogins(inputFilePath, outputFilePath)

    End Sub

    Sub GenerateLogins(inputFilePath As String, outputFilePath As String)

        Dim persons As New List(Of PersonData)()

        Using reader As New StreamReader(inputFilePath)
            Dim line As String = reader.ReadLine()
            While (line IsNot Nothing)
                Dim personSingleData As String() = line.Split(" ")
                persons.Add(New PersonData() With {
                    .Name = personSingleData(1),
                    .Surname = personSingleData(0),
                    .Patronymic = personSingleData(2)
                    })
                line = reader.ReadLine()
            End While
        End Using

        'persons = persons.OrderBy(Function(x) x.Surname).
        '                 ThenBy(Function(y) y.Name).
        '                 ThenBy(Function(z) z.Patronymic).
        '                 ToList()

        Dim logins As New List(Of String)()

        For Each person As PersonData In persons
            Dim login As String = NameSurnameLogin(person)

            If logins.Any(Function(x) x.Equals(login)) Then
                login = NameFirstSurnameLogin(person)

                If logins.Any(Function(x) x.Equals(login)) Then
                    login = NameFirstPatrinymicFirstSurnameLogin(person)

                    If logins.Any(Function(x) x.Equals(login)) Then
                        login = SurnameLogin(person)

                        If logins.Any(Function(x) x.Equals(login)) Then
                            login = NameFirstSurnameLogin(person)
                            Dim countLoginsIncrement As Integer = logins.FindAll(Function(p) InStr(p, login) > 0).Count
                            login += Str(countLoginsIncrement)
                        End If

                    End If

                End If

            End If

            logins.Add(login.Replace(" ", ""))
        Next

        Using writer As New StreamWriter(outputFilePath)
            For Each login As String In logins
                writer.WriteLine(login)
            Next
        End Using

    End Sub

    Function NameSurnameLogin(person As PersonData) As String
        Return (person.Name + "." + person.Surname).ToLower
    End Function

    Function NameFirstSurnameLogin(person As PersonData) As String
        Return (person.Name(0) + "." + person.Surname).ToLower
    End Function

    Function NameFirstPatrinymicFirstSurnameLogin(person As PersonData) As String
        Return (person.Name(0) + "." + person.Patronymic(0) + "." + person.Surname).ToLower
    End Function

    Function SurnameLogin(person As PersonData) As String
        Return person.Surname.ToLower
    End Function

    Public Class PersonData
        Public Property Name() As String
        Public Property Surname() As String
        Public Property Patronymic() As String
    End Class

End Module
