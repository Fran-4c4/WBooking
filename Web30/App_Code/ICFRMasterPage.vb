Imports Microsoft.VisualBasic
Imports cfr

Namespace cfr.pages

    Public Interface ICFRMasterPage

        Property siteWeb() As cfr.SiteObj
        ReadOnly Property holdProperty As cfr.obj.Hotel
        Property myBasePage() As BookBasePage
        ReadOnly Property bookData As cfr.BookRes

        ReadOnly Property langCaption As cfr.languagePageManager


        ReadOnly Property getLitMsgError() As Literal
        ReadOnly Property masterPageLiteral() As Literal
        'ReadOnly Property getLitStyleForClient() As Literal
        Sub initializePage()


    End Interface

End Namespace