Imports Microsoft.VisualBasic
Imports System.Security.Cryptography

Public Class UIHelper

    ''' <summary>
    ''' return the correct string for boolean values
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function cboYesNo(value As Object) As String
        If value = 1 Then
            Return "Si"
        Else
            Return "No"
        End If
    End Function

    Public Shared Function cboSelectValue(valueSelected As String, optionValue As String) As String
        If optionValue = valueSelected Then
            Return " selected "
        Else
            Return ""
        End If
    End Function

#Region "Encryption/decryption"
    Public Shared Function URLencryptData(dataToEncrypt As String) As String
        If dataToEncrypt.Trim = "" Then
            Return ""
        End If
        Dim str As String = encryptData(dataToEncrypt)
        str = HttpContext.Current.Server.UrlEncode(str)
        Return str
    End Function
    ''' <summary>
    ''' do not use when you get the string from querystring
    ''' </summary>
    ''' <param name="dataToDecrypt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function URLDeCryptData(dataToDecrypt As String) As String

        If dataToDecrypt.Trim = "" Then
            Return ""
        End If

        Dim str As String = HttpContext.Current.Server.UrlDecode(dataToDecrypt)
        str = decryptData(str)
        Return str
    End Function


    Private Const mykey As String = "mi-mono-amelio-y-yo-tenemos-1-slk"

    ''' <summary>
    ''' encode data
    ''' </summary>
    ''' <param name="dataToEncrypt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function encryptData(dataToEncrypt As String) As String
        Try
            If dataToEncrypt = "" Then
                Return dataToEncrypt
            End If
            Dim wrapper As New Simple3Des(mykey)
            Dim cipherText As String = wrapper.EncryptData(dataToEncrypt)
            Return cipherText
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function decryptData(dataToDecrypt As String) As String
        Try
            If dataToDecrypt = "" Then
                Return dataToDecrypt
            End If
            Dim wrapper As New Simple3Des(mykey)
            Dim decipherText As String = wrapper.DecryptData(dataToDecrypt)
            Return decipherText
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region


#Region "UI filling"
    
    Public Shared Function getUItxtValue(txtBox As System.Web.UI.WebControls.TextBox, Optional defaultValue As Object = Nothing) As Object
        Dim retValue As Object
        If txtBox.Text.Trim = "" Then
            retValue = defaultValue
        Else
            retValue = txtBox.Text.Trim
        End If
        Return retValue
    End Function

    Public Shared Function setUIValueTxt(refValue As String, Optional defaultValue As String = "") As String
        If refValue Is Nothing Then
            Return defaultValue
        End If

        Return refValue

    End Function

    Public Shared Function setUIValueDate(refValue As Nullable(Of Date), Optional defaultValue As String = "") As String
        If refValue Is Nothing Then
            Return defaultValue
        End If

        Try
            Dim dt As Date = CDate(refValue)

            'Return DateTime.ParseExact(refValue.ToString, "dd/MM/yyyy", Nothing).ToString("dd/MM/yyyy")
            Return dt.ToString("dd/MM/yyyy")
        Catch ex As Exception
            Return ""
        End Try

    End Function
    ''' <summary>
    ''' return value boolean for checkbox value in request. When chkval is in request is because user selected it else it is empty
    ''' </summary>
    ''' <param name="chkVal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getCheckBoxValue(chkVal As String) As Boolean

        If chkVal = "" Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region


End Class



