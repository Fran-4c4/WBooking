'//----------------------------------------------------------------------
'//  Copyright (c) 2011 myims.com.au  All Rights Reserved.
'//  This copyright notice MUST stay intact for use.
'//----------------------------------------------------------------------
#Region " File Information "

'=====================================================================
' This class is a generic encryption object
'=====================================================================

#Region " Revision History "

'=====================================================================
' Name      Date        Description
'---------------------------------------------------------------------
' AW       12-SEP-2008  Implemented.
'=====================================================================

#End Region

#End Region

Imports System.Security.Cryptography
Imports System.Text
'###########################################################################################
'###### Do not make changes to this file, it may be replaced in an a version update   ######
'###### Instead you may want to Inherit this class in order to extend and override it ######
'###########################################################################################
Public Class clsEncrypt

#Region " Member Variables "
    Private Const sKeyEncrpt As String = "$aKamy6Y3Ims7$**"

#End Region

#Region " Properties "
    Public ReadOnly Property Key() As String
        Get
            Return sKeyEncrpt
        End Get
    End Property

#End Region


    Private Shared Function EncryptTripleDES(ByVal sIn As String, Optional ByVal sKey As String = sKeyEncrpt) As String
        Dim DES As New TripleDESCryptoServiceProvider
        Dim hashMD5 As New MD5CryptoServiceProvider

        ' Compute the MD5 hash.
        DES.Key = hashMD5.ComputeHash(Encoding.ASCII.GetBytes(sKey))
        ' Set the cipher mode.
        DES.Mode = CipherMode.ECB
        'padding mode(if any extra byte added)
        DES.Padding = PaddingMode.PKCS7

        ' Create the encryptor.
        Dim DESEncrypt As ICryptoTransform = DES.CreateEncryptor()
        ' Get a byte array of the string.
        Dim Buffer As Byte() = Encoding.ASCII.GetBytes(sIn)
        ' Transform and return the string.
        Return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length))
    End Function

    Private Shared Function DecryptTripleDES(ByVal sOut As String, Optional ByVal sKey As String = sKeyEncrpt) As String
        Dim DES As New TripleDESCryptoServiceProvider
        Dim hashMD5 As New MD5CryptoServiceProvider

        ' Compute the MD5 hash.
        DES.Key = hashMD5.ComputeHash(Encoding.ASCII.GetBytes(sKey))
        ' Set the cipher mode.
        DES.Mode = CipherMode.ECB
        'padding mode(if any extra byte added)
        DES.Padding = PaddingMode.PKCS7

        ' Create the decryptor.
        Dim DESDecrypt As ICryptoTransform = DES.CreateDecryptor()
        Dim Buffer As Byte() = Convert.FromBase64String(sOut)
        ' Transform and return the string.
        Return Encoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length))
    End Function

    Public Shared Function GetDecryptString(ByVal sSource As String, Optional ByVal sKey As String = sKeyEncrpt) As String
        Return DecryptTripleDES(sSource, sKey)
    End Function

    Public Shared Function GetEncryptString(ByVal sSource As String, Optional ByVal sKey As String = sKeyEncrpt, Optional ByVal blnSensitive As Boolean = False) As String
        If blnSensitive = False Then sSource = sSource.ToLower
        Return EncryptTripleDES(sSource, sKey)
    End Function

End Class

