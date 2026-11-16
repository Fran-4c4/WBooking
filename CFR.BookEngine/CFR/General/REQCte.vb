Imports Microsoft.VisualBasic


Namespace cfr


    Public Class REQCte
        Public Class session
            Public Const book As String = "bookSession"
            Public Const hotelAdmin As String = "hotelAdminSession"
            Public Const Site As String = "SiteSession"
            Public Const SesionError As String = "SesionError"
            Public Const NoAccessAllowed As String = "NoAccessAllowed"
        End Class


        Public Class query
            Public Const cod_hotel As String = "hotelcode"
            Public Const language As String = "language"
            Public Const site As String = "site"
            Public Const cod_crs As String = "cod_crs"
            Public Const userType As String = "userType"
            Public Const login As String = "login"
            Public Const clave As String = "clave"
            Public Const idBook As String = "localizador"
            Public Const errNumber As String = "error"
            Public Const sysOper As String = "OPER"
            Public Const userIDAdmin As String = "userid"
            Public Const sessionlink As String = "sessionlink"
        End Class


        ''' <summary>
        ''' External booking-search launch fields consumed by b_Request.aspx.
        ''' A launch may use GET or POST, but all six date parts must be supplied
        ''' together. Dates are Gregorian numeric day/month/year values; checkout
        ''' must follow check-in. Occupancy accepts 1-5 adults and 0-2 free plus
        ''' 0-5 paying children. Regimen is CFRConst.RegimenEnum (1-5) and must be
        ''' enabled for the selected hotel. Changing a wire name breaks customer
        ''' website integrations and the example under Web30/examples.
        ''' </summary>
        Public Class forms
            Public Const dateInDay As String = "fechad1"
            Public Const dateInMonth As String = "fecham1"
            Public Const dateInYear As String = "fechay1"
            Public Const dateOutDay As String = "fechad2"
            Public Const dateOutMonth As String = "fecham2"
            Public Const dateOutYear As String = "fechay2"
            Public Const ninosfree As String = "f_ninosfree"
            Public Const ninosPago As String = "f_ninosPago"
            Public Const numAdultos As String = "f_numAdultos"
            Public Const regimen As String = "f_regimen"
        End Class

#Region "pages & session"
        Public Class pages

            Public Const bookRequest As String = "b_request.aspx"
            Public Const bookResult As String = "b_results.aspx"
        End Class

#End Region

    End Class
End Namespace
