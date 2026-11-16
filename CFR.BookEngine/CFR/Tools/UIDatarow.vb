
Namespace cfr.tool
    ''' <summary>
    ''' class to encapsulate a datarow to avoid nulls in UI presentation 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UIDatarow
        Public dr As DataRow
        Public Sub New(dr As DataRow)
            Me.dr = dr
        End Sub
        ''' <summary>
        ''' return an empty string when value is null
        ''' </summary>
        ''' <param name="field"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function asStr(field As String) As String
            Return cfr.tool.Tools.cNullStr(Me.dr(field))
        End Function

        ''' <summary>
        ''' return 0 when value is null
        ''' </summary>
        ''' <param name="field"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function asInteger(field As String) As Integer
            Return cfr.tool.Tools.cNullStr(Me.dr(field), 0)
        End Function

        Public Function asIntegerStr(field As String) As String
            Return CType(cfr.tool.Tools.cNullStr(Me.dr(field), "0"), String)
        End Function

        Public Function asObj(field As String, ByVal valxDefect As Object) As Object
            Return cfr.tool.Tools.cNullStr(Me.dr(field), valxDefect)
        End Function

    End Class

End Namespace


