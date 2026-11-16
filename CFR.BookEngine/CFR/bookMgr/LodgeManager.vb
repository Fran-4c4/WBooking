Imports Microsoft.VisualBasic

Namespace cfr
    Public Class LodgeManager

        Public Shared Function getdescRegimen(ByVal regimen As cfr.CFRConst.RegimenEnum) As String
            Select Case regimen
                Case cfr.CFRConst.RegimenEnum.SA
                    Return cfr.Language.getCaption(18) '"Solo Alojamiento"
                Case cfr.CFRConst.RegimenEnum.AD
                    Return cfr.Language.getCaption(19) '"Alojam. y desayuno"
                Case cfr.CFRConst.RegimenEnum.MP
                    Return cfr.Language.getCaption(60) '"Media Pension"
                Case cfr.CFRConst.RegimenEnum.PC
                    Return cfr.Language.getCaption(61) '"Pension completa"
                Case cfr.CFRConst.RegimenEnum.TI
                    Return cfr.Language.getCaption(62) '"Todo Incluido"
            End Select
            Return ""
        End Function
    End Class
End Namespace