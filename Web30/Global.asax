<%@ Application Language="VB" %>
<%@ Import Namespace="System.Diagnostics" %>


<script runat="server">
    

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        
        cfr.global.AppGeneralData.appFisicalPath = Server.MapPath(".")
        ' Code that runs on application startup
        ConfigureLogging()
        
        ind.bk.DBMgr.configure()
         
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        TransportSecurityPolicy.Apply(HttpContext.Current)
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
        System.Diagnostics.Debug.WriteLine("Session end")
    End Sub
    
    Protected Sub ConfigureLogging()

        'Dim logFile As String = HttpContext.Current.Request.PhysicalApplicationPath + "log4net.config"
        Dim logFile As String = IO.Path.Combine(Server.MapPath("."), "log4net.config")

        If System.IO.File.Exists(logFile) Then
            log4net.Config.XmlConfigurator.ConfigureAndWatch(New System.IO.FileInfo(logFile))
        End If

        
        
    End Sub
    
       
</script>
