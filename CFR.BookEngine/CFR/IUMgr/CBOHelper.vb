Imports Microsoft.VisualBasic


Namespace cfr
    ''' <summary>
    ''' class helper for combos
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CBOHelper
        ''' <summary>
        ''' fill a combo with the data passed
        ''' </summary>
        ''' <param name="cbo"></param>
        ''' <param name="dtSource"></param>
        ''' <param name="valueBind"></param>
        ''' <param name="textBind"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function fillCbo(ByRef cbo As Web.UI.WebControls.DropDownList _
                                       , ByVal dtSource As Data.DataTable _
                                       , ByVal valueBind As String _
                                       , ByVal textBind As String) As Web.UI.WebControls.DropDownList
            If cbo Is Nothing Then
                cbo = New Web.UI.WebControls.DropDownList
            End If

            cbo.DataTextField = textBind
            cbo.DataValueField = valueBind
            cbo.DataSource = dtSource
            cbo.DataBind()
            Return cbo
        End Function

        Public Shared Function fillCboByArray(ByRef cbo As Web.UI.WebControls.DropDownList _
                                       , ByVal dtSource As Array _
                                       , ByVal valueBind As String _
                                       , ByVal textBind As String) As Web.UI.WebControls.DropDownList
            If cbo Is Nothing Then
                cbo = New Web.UI.WebControls.DropDownList
            End If

            cbo.DataTextField = textBind
            cbo.DataValueField = valueBind
            cbo.DataSource = dtSource
            cbo.DataBind()
            Return cbo
        End Function

    End Class

End Namespace