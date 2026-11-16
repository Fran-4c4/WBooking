Imports Microsoft.VisualBasic
Imports cfr

Namespace cfr.pages


    Public MustInherit Class SiteBaseMasterPage
        Inherits System.Web.UI.MasterPage
        Implements cfr.pages.ICFRMasterPage







#Region "Variables"
        Private siteWebVar As cfr.SiteObj
        Private boe As New cfr.BookOperationErrors
        Private myBasePageVar As BookBasePage
#End Region

#Region "properties"
        Public Property siteWeb() As SiteObj Implements ICFRMasterPage.siteWeb
            Get
                Return Me.siteWebVar
            End Get
            Set(ByVal value As SiteObj)
                Me.siteWebVar = value
                Me.Page.Title = Me.siteWebVar.description
            End Set
        End Property


        Public Property myBasePage() As BookBasePage Implements ICFRMasterPage.myBasePage
            Get
                Return Me.myBasePageVar
            End Get
            Set(ByVal value As BookBasePage)
                Me.myBasePageVar = value
            End Set
        End Property


        Public ReadOnly Property holdProperty As obj.Hotel Implements ICFRMasterPage.holdProperty
            Get
                If Not Me.myBasePageVar Is Nothing Then
                    Return Me.myBasePageVar.holdprop
                Else
                    Return Nothing
                End If
            End Get

        End Property

        Public ReadOnly Property bookData As BookRes Implements ICFRMasterPage.bookData
            Get
                If Not Me.myBasePageVar Is Nothing Then
                    Return Me.myBasePageVar.bookData
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public ReadOnly Property langCaption As languagePageManager Implements ICFRMasterPage.langCaption
            Get
                Return Me.myBasePageVar.langCaption
            End Get
        End Property


#End Region





        Private Sub initializePage() Implements ICFRMasterPage.initializePage

        End Sub

#Region "Configuration"


#Region "Query helpers"


        Public Function getQuery() As String
            Dim s As String = ""
            Dim sw As String = ""
            For x As Integer = 0 To Page.Request.QueryString.Count - 1
                Dim keyValue As String = Page.Request.QueryString.GetKey(x)
                s += sw & keyValue & "=" & Page.Request.QueryString(x)
                sw = "&"
            Next
            Return s
        End Function

        Public Function getQueryNormal() As String
            If Me.myBasePageVar Is Nothing Then
                Return ""
            End If


            Return Me.myBasePage.GetQueryNormal
        End Function

#End Region

#End Region



        Public MustOverride ReadOnly Property getLitMsgError() As System.Web.UI.WebControls.Literal Implements ICFRMasterPage.getLitMsgError


        Public ReadOnly Property masterPageLiteral As Literal Implements ICFRMasterPage.masterPageLiteral
            Get
                Return Me.FindControl("masterPageTitleLiteral")
            End Get
        End Property



    End Class
End Namespace