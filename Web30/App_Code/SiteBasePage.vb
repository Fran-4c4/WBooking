Imports System.Globalization
Imports System.Linq
Imports Microsoft.VisualBasic
Imports cfr

Namespace cfr.pages
    ''' <summary>
    ''' SiteBase for pages
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SiteBasePage
        Inherits WBasePage
        ''' <summary>
        ''' type of site
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum siteTypeEnum As Byte
            mainSite = 0
            hotelsite = 1
            carSite = 2
        End Enum

#Region "Variables"
        Public myCFRMasterPage As cfr.pages.ICFRMasterPage
        Public sitepagetype As siteTypeEnum = siteTypeEnum.mainSite

#End Region


#Region "init"

        'page life cycle: https://msdn.microsoft.com/en-us/library/ms178472.aspx


        ''' <summary>
        ''' Check the IsPostBack property to determine whether this is the first time the page is being processed. The IsCallback and IsCrossPagePostBack properties have also been set at this time.
        ''' Create or re-create dynamic controls.
        ''' Set a master page dynamically.
        ''' Set the Theme property dynamically.
        ''' Read or set profile property values.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Overridable Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit



            System.Diagnostics.Debug.WriteLine("SiteBasePage_Page_PreInit")

            ' Configure site based on querystring to select the masterpage
            Me.configureLangData()
            Me.configureSiteData()

            Dim masterPageVar As String = Me.siteWeb.masterPageCode.Trim
            If (masterPageVar <> "") Then
                If masterPageVar.EndsWith(".master") = False Then
                    masterPageVar = masterPageVar & ".master"
                End If
            End If

            'The public search entry is embedded by customer sites, so it must
            'never inherit the application navigation, header or footer.
            Dim isPublicBookingRequest As Boolean =
                String.Equals(Me.Page.Request.Url.Segments.Last,
                              "B_REQUEST.ASPX",
                              StringComparison.OrdinalIgnoreCase)

            If isPublicBookingRequest Then
                MyBase.MasterPageFile = "~/blank.master"
            ElseIf typePageIs() Then
                If Me.siteWeb.showPrincipal = True Then
                    If (masterPageVar <> "") Then
                        MyBase.MasterPageFile = "~/" & masterPageVar
                    Else
                        MyBase.MasterPageFile = "~/blank.master"
                    End If
                ElseIf Me.siteWeb.showPrincipal = False Then
                    MyBase.MasterPageFile = "~/blank.master"
                End If
            Else
                If Me.pageType = pageTypeEnum.composed Then
                    If (masterPageVar <> "") Then
                        MyBase.MasterPageFile = "~/" & masterPageVar
                    Else
                        MyBase.MasterPageFile = "~/blank.master"
                    End If
                Else
                    MyBase.MasterPageFile = "~/blank.master"
                End If


            End If


            Me.myCFRMasterPage = MyBase.Master
            Me.myCFRMasterPage.siteWeb = Me.siteWeb

            If TypeOf (Me) Is BookBasePage Then
                Me.myCFRMasterPage.myBasePage = Me
            Else
                Me.myCFRMasterPage.myBasePage = Nothing
            End If


            If Page.IsPostBack = False Then

            End If


        End Sub
        ''' <summary>
        ''' Raised after all controls have been initialized and any skin settings have been applied. The Init event of individual controls occurs before the Init event of the page.
        ''' Use this event to read or initialize control properties.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            'System.Diagnostics.Debug.WriteLine("SiteBasePage_Page_Init")


        End Sub

        ''' <summary>
        ''' Raised at the end of the page's initialization stage. Only one operation takes place between the Init 
        ''' and InitComplete events: tracking of view state changes is turned on. View state tracking enables controls to persist any values
        '''  that are programmatically added to the ViewState collection. Until view state tracking is turned on, any values added to view state 
        ''' are lost across postbacks. Controls typically turn on view state tracking immediately after they raise their Init event.
        ''' Use this event to make changes to view state that you want to make sure are persisted after the next postback.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Page_InitComplete(sender As Object, e As EventArgs) Handles Me.InitComplete

        End Sub

        ''' <summary>
        ''' Raised after the page loads view state for itself and all controls, and after it processes postback data that is included with the Request instance.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Page_PreLoad(sender As Object, e As EventArgs) Handles Me.PreLoad

        End Sub
        ''' <summary>
        ''' The Page object calls the OnLoad method on the Page object, and then recursively does the same for each child control until
        '''  the page and all controls are loaded. The Load event of individual controls occurs after the Load event of the page.
        ''' Use the OnLoad event method to set properties in controls and to establish database connections.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            System.Diagnostics.Debug.WriteLine("SiteBasePage_Page_Load")
            Me.showMessage("")

            Me.myQuery = Me.getQuery

            If Not Me.Page.IsPostBack Then
                Me.SetStyleSheet()
                Me.setPoweredFooter()
                Me.Page.Title = Me.siteWeb.description
                'Me.idiom = cfr.Language.getIdiomaMenuGenData(cfr.Language.getLanguage)
            Else

            End If

        End Sub
        ''' <summary>
        ''' Raised at the end of the event-handling stage.Use this event for tasks that require that all other controls on the page be loaded.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete

        End Sub


        ''' <summary>
        ''' Raised after the Page object has created all controls that are required in order to render the page, including child controls of composite controls.
        '''  (To do this, the Page object calls EnsureChildControls for each control and for the page.)
        ''' The Page object raises the PreRender event on the Page object, and then recursively does the same for each child control. 
        '''  The PreRender event of individual controls occurs after the PreRender event of the page.
        ''' Use the event to make final changes to the contents of the page or its controls before the rendering stage begins.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

        End Sub
        ''' <summary>
        ''' Raised after each data bound control whose DataSourceID property is set calls its DataBind method. 
        ''' For more information, see Data Binding Events for Data-Bound Controls later in this topic.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Page_PreRenderComplete(sender As Object, e As EventArgs) Handles Me.PreRenderComplete

        End Sub

        ''' <summary>
        ''' Raised for each control and then for the page.
        ''' In controls, use this event to do final cleanup for specific controls, such as closing control-specific database connections.
        ''' For the page itself, use this event to do final cleanup work, such as closing open files and database connections, or finishing up logging or other request-specific tasks.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
            Me.cancelFlow = False
        End Sub

#End Region


        Public Function typePageIs() As Boolean
            Dim pagename As String = Me.Page.Request.Url.Segments.Last.ToUpper
            Select Case pagename
                Case "B_REQUEST.ASPX", "B_CONFIRMATION.ASPX"
                    Return True
                Case Else
                    Return False

            End Select



        End Function

#Region "Configuration"

        Private Sub configureLangData()
            Dim language As String = ""

            language = Request.QueryString(cfr.REQCte.query.language)

            If Not Me.IsPostBack Then


                If language = "" Then
                    language = getUserLanguage()
                End If

                Me.languageID_ViewState = language

            Else
                If language <> "" Then
                    If language <> Me.languageID_ViewState Then 'if actual language is diferent
                        Me.languageID_ViewState = language
                    End If
                End If

                language = Me.languageID_ViewState

            End If

            Me.configCulture(language)

            System.Diagnostics.Debug.WriteLine("Config language: " & language)

        End Sub
        ''' <summary>
        ''' Configure basic settings: language and site code
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub configureSiteData()

            Dim sitemgr As New cfr.SiteManager
            Dim siteCode As String

            If Not Me.IsPostBack Then
                siteCode = Request.QueryString(cfr.REQCte.query.site)
                Me.siteCode_ViewState = siteCode
            Else
                '1-Get from viewstate
                siteCode = Me.siteCode_ViewState
                '2-Get from querystring
                If siteCode = "" Then
                    siteCode = Request.QueryString(cfr.REQCte.query.site)
                    Me.siteCode_ViewState = siteCode
                End If

            End If


            Me.siteWeb = Session(cfr.REQCte.session.Site)
            If siteCode = "" And Me.siteWeb Is Nothing Then 'set site by default
                siteCode = "S1"
            End If

            If siteCode <> "" And Me.siteWeb Is Nothing Then
                Me.siteWeb = sitemgr.getSite(siteCode)
                Session(cfr.REQCte.session.Site) = Me.siteWeb

            ElseIf siteCode <> "" And Not Me.siteWeb Is Nothing Then
                If siteCode <> Me.siteWeb.id Then
                    Me.siteWeb = sitemgr.getSite(siteCode)
                    Session(cfr.REQCte.session.Site) = Me.siteWeb
                End If
            End If

            SiteBasePage.getSiteData(Me.siteWeb)

            System.Diagnostics.Debug.WriteLine("Viewstate sitecode=" & Me.siteCode_ViewState)

            myQueryBase = String.Format("{0}={1}", cfr.REQCte.query.language, Me.languageID_ViewState)
            myQueryBase += String.Format("&{0}={1}", cfr.REQCte.query.site, Me.siteCode_ViewState)

        End Sub

        Private Sub configureSiteForSelectedPage()

        End Sub


        Public Sub SetStyleSheet()
            If Me.siteWeb Is Nothing Then
                Exit Sub
            End If

            Dim StyleSheetLiteralBooking As Literal = Me.Master.FindControl("StyleSheetLiteralBooking")
            If StyleSheetLiteralBooking Is Nothing Then
                Exit Sub
            End If

            Dim StyleSheetLiteral As Literal = Me.Master.FindControl("StyleSheetLiteral")
            If StyleSheetLiteral Is Nothing Then
                Exit Sub
            End If

            Dim styleFormat As String = "<LINK href='css/customsite_{0}.css' type='text/css' rel='stylesheet'>"
            Dim linkText As String = String.Format(styleFormat, siteWeb.id)
            StyleSheetLiteral.Text = linkText

        End Sub



        Public Sub setPoweredFooter()
            Dim ctl As Literal = Me.Master.FindControl("litPowered")
            If ctl Is Nothing Then
                Exit Sub
            End If


            ctl.Text = "<br/>" & cfr.IUFunctions.getPoweredFooter()
        End Sub



#End Region

        Public Shared Sub getSiteData(ByRef siteToShow As cfr.SiteObj)


            Select Case siteToShow.id.ToString.ToUpper.Trim
                Case "S1"
                    siteToShow.description = "WBooking"
                    siteToShow.website = ""
                    siteToShow.emailcontacto = ""
                    siteToShow.emailreservas = ""
                    siteToShow.hostname = ""
                    siteToShow.showPrincipal = True
                    siteToShow.tipo = "0"
                    siteToShow.masterPageCode = "CFRMasterPage"
            End Select

        End Sub



    End Class






End Namespace
