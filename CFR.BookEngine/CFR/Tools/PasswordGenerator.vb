Imports Microsoft.VisualBasic

''' <summary>
''' Class to generate passwords
''' </summary>
''' <remarks></remarks>
Public Class PasswordGenerator
    ''' <summary>
    ''' Generate a random password of 'user input' length
    ''' 08/21/1999 Andrew Ells-O'Brien (andrew@ellsobrien@msn.com)
    ''' </summary>
    ''' <param name="lngLength">the length of the password to be</param>
    ''' <returns>Randomly generated password</returns>
    ''' <remarks></remarks>
    Public Function PasswordGenerator(ByVal lngLength As Integer) As String
        Dim iChr As Integer
        Dim strResult As String = ""
        Dim iAsc As String

        Randomize(Timer)

        For c As Integer = 1 To lngLength

            ' Randomly decide what set of ASCII chars we will use
            iAsc = Int(3 * Rnd() + 1)

            'Randomly pick a char from the random set
            Select Case iAsc
                Case 1
                    iChr = Int((Asc("Z") - Asc("A") + 1) * Rnd() + Asc("A"))
                Case 2
                    iChr = Int((Asc("z") - Asc("a") + 1) * Rnd() + Asc("a"))
                Case 3
                    iChr = Int((Asc("9") - Asc("0") + 1) * Rnd() + Asc("0"))
                Case Else
                    Throw New Exception("PasswordGenerator has a problem.")
            End Select

            strResult = strResult & Chr(iChr)

        Next c

        Return strResult


    End Function
End Class
