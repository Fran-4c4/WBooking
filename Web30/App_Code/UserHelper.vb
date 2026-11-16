Imports ind.bk.obj
Imports Microsoft.VisualBasic

Namespace cfr.book


    Public Class UserHelper
        Public Shared Function getBookSession(ByVal ctx As System.Web.HttpContext) As BookRes
            Return ctx.Session(REQCte.session.book)
        End Function

        ' ''' <summary>
        ' ''' Return hotel user session data
        ' ''' </summary>
        ' ''' <param name="ctx"></param>
        ' ''' <returns></returns>
        ' ''' <remarks></remarks>
        'Public Shared Function getAdminSession(ByVal ctx As System.Web.HttpContext) As cfr.obj.AdminSessionObj
        '    Return ctx.Session(REQCte.session.hotelAdmin)
        'End Function

        'Public Shared Sub setAdminSession(ByVal ctx As System.Web.HttpContext, ByVal AdminSession As cfr.obj.AdminSessionObj)
        '    ctx.Session(REQCte.session.hotelAdmin) = AdminSession
        'End Sub


    End Class

End Namespace