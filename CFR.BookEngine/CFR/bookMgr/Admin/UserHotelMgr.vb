Imports Microsoft.VisualBasic
Imports System.Data
Imports ind.bk.obj


Imports System.Runtime.Serialization

Namespace cfr.mgr

    Public Class UserHotelMgr
        Inherits R_Usuarios
        ''' <summary>
        ''' user session key used for validation by cookies in computer
        ''' </summary>
        Public usersessionkey As String

        Public Property idEntity As Integer
            Get
                Return MyBase.id_usuario
            End Get
            Set(value As Integer)
                MyBase.id_usuario = value
            End Set
        End Property


        Private mpermisos As New ind.bk.obj.R_Usuarios_PermisosCollection
        <DataMemberAttribute()>
        Public Overridable Property permisos() As ind.bk.obj.R_Usuarios_PermisosCollection
            Get
                Return mpermisos
            End Get
            Set(value As ind.bk.obj.R_Usuarios_PermisosCollection)
                mpermisos = value

            End Set
        End Property

        Public userlogaccessVar As New ind.bk.obj.Userlogaccess
        Public Property userlogaccess As ind.bk.obj.Userlogaccess
            Get
                Return Me.userlogaccessVar
            End Get
            Set(value As ind.bk.obj.Userlogaccess)
                Me.userlogaccessVar = value
            End Set
        End Property

        ''' <summary>
        ''' Get user data by ID
        ''' </summary>
        ''' <param name="id_Usuario"></param>
        ''' <param name="trans"></param>
        ''' <returns></returns>
        Public Shared Function GetUserByID(ByVal id_Usuario As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As UserHotelMgr

            Dim obj As UserHotelMgr = Nothing

            Dim u As R_Usuarios
            Using ctx As New cfrbookEntities
                u = (From s In ctx.R_Usuarios Where s.id_usuario = id_Usuario).FirstOrDefault
            End Using

            If Not u Is Nothing Then
                obj = New UserHotelMgr
                obj.id_usuario = u.id_usuario
                obj.cod_hotel = u.cod_hotel
                obj.usuario = u.usuario
                obj.login = u.login
                obj.clave = u.clave
                obj.perfil = u.perfil
                obj.email = u.email
                obj.ultimoacceso = u.ultimoacceso

                cfr.mgr.HotelMgr.updateUserLastAccess(obj.cod_hotel)
                'get permisos
                Dim rp As New ind.bk.obj.R_Usuarios_Permisos

                obj.permisos = R_Usuarios_PermisosCollection.getCollectionByTable(rp.getDataTableByIDUsuario(obj.id_usuario))
            End If

            If Not obj Is Nothing Then
                Dim opermgr As New ind.bk.obj.Userlogaccess_Manager
                Dim userlogaccessVar As ind.bk.obj.Userlogaccess = obj.userlogaccessVar
                userlogaccessVar.userid = obj.id_usuario
                userLog(userlogaccessVar, True)
            End If

            Return obj
        End Function
        ''' <summary>
        ''' Validate login/password for user. Used only in login operation in login page
        ''' </summary>
        ''' <param name="login"></param>
        ''' <param name="clave"></param>
        ''' <param name="trans"></param>
        ''' <returns></returns>
        Public Shared Function GetUserByCredentials(ByVal login As String, ByVal clave As String, ipAccess As String, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As UserHotelMgr

            Dim obj As UserHotelMgr = Nothing
            Dim userEntity As R_Usuarios = Nothing

            Using ctx As New cfrbookEntities
                userEntity = (From candidate In ctx.R_Usuarios
                              Where candidate.login = login
                              Select candidate).FirstOrDefault()

                If userEntity Is Nothing Then
                    Return Nothing
                End If

                Dim passwordMatches As Boolean
                If cfr.security.PasswordHasher.IsPasswordHash(userEntity.clave) Then
                    passwordMatches = cfr.security.PasswordHasher.VerifyPassword(clave, userEntity.clave)
                Else
                    'One-time compatibility path for databases restored from a
                    'legacy backup. A successful login immediately removes the
                    'remaining plaintext value.
                    passwordMatches = String.Equals(userEntity.clave, clave, StringComparison.Ordinal)
                End If

                If Not passwordMatches Then
                    Return Nothing
                End If

                If cfr.security.PasswordHasher.NeedsUpgrade(userEntity.clave) Then
                    userEntity.clave = cfr.security.PasswordHasher.HashPassword(clave)
                End If

                obj = New UserHotelMgr
                obj.id_usuario = userEntity.id_usuario
                obj.cod_hotel = userEntity.cod_hotel
                obj.usuario = userEntity.usuario
                obj.login = userEntity.login
                obj.clave = userEntity.clave
                obj.perfil = userEntity.perfil
                obj.email = userEntity.email
                obj.ultimoacceso = userEntity.ultimoacceso

                'The raw bearer token is returned only to the browser. The
                'database stores its non-reversible, fixed-length fingerprint.
                obj.usersessionkey = cfr.security.PersistentSessionToken.Create()
                Dim userSession As New R_UsuariosSession With {
                    .id_usuario = obj.id_usuario,
                    .ipaccess = ipAccess,
                    .logon = DateTime.Now,
                    .sessionkey = cfr.security.PersistentSessionToken.FingerprintForStorage(obj.usersessionkey)
                }
                ctx.R_UsuariosSession.Add(userSession)
                ctx.SaveChanges()

                cfr.mgr.HotelMgr.updateUserLastAccess(obj.cod_hotel)
                'get permisos
                Dim rp As New ind.bk.obj.R_Usuarios_Permisos
                obj.permisos = R_Usuarios_PermisosCollection.getCollectionByTable(rp.getDataTableByIDUsuario(obj.id_usuario))
            End Using

            If Not obj Is Nothing Then
                Dim opermgr As New ind.bk.obj.Userlogaccess_Manager
                Dim userlogaccessVar As ind.bk.obj.Userlogaccess = obj.userlogaccessVar
                userlogaccessVar.userid = obj.id_usuario
                userLog(userlogaccessVar, True)
            End If


            Return obj
        End Function

        ''' <summary>
        ''' log user access 
        ''' </summary>
        ''' <param name="userlogaccessVar"></param>
        ''' <param name="loginsert"></param>
        Public Shared Sub userLog(userlogaccessVar As ind.bk.obj.Userlogaccess, loginsert As Boolean)
            Try
                Dim opermgr As New ind.bk.obj.Userlogaccess_Manager
                If loginsert = True Then
                    userlogaccessVar.logon = Date.Now
                    Debug.WriteLine(System.Web.HttpContext.Current.Request.UserHostAddress)

                    userlogaccessVar.ipaccess = System.Web.HttpContext.Current.Request.UserHostAddress
                    opermgr.insert(userlogaccessVar)
                Else
                    userlogaccessVar.logoff = Date.Now
                    opermgr.Update(userlogaccessVar)
                End If
            Catch ex As Exception

            End Try



        End Sub
        

    End Class
End Namespace
