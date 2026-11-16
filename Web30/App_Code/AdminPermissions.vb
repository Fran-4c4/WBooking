Imports Microsoft.VisualBasic



Namespace cfr.admin


    Public Class AdminPermissions

        ''' <summary>
        ''' Check access to section. I false redirects to caller page and show message.
        ''' </summary>
        ''' <param name="permisionRequired"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function UserAccessToSectioIsAllowed(user As cfr.mgr.UserHotelMgr, permisionRequired As cfr.web.enum.adminWebPageEnum) As Boolean


            'permiso de administrador
            For Each p As ind.bk.obj.R_Usuarios_Permisos In user.permisos
                If p.cod_permiso = cfr.enum.adminAllowEnum.P1_Control_Total_Sistema Then
                    Return True
                End If
            Next

            Select Case permisionRequired
                Case cfr.web.enum.adminWebPageEnum.modifyhotelpwdadmin
                    For Each p As ind.bk.obj.R_Usuarios_Permisos In user.permisos
                        If p.cod_permiso = cfr.enum.adminAllowEnum.P1_Control_Total_Sistema Then
                            Return True
                        End If
                    Next
                Case cfr.web.enum.adminWebPageEnum.reservasactivas ' rGesinf_FR_resxFec reservas activas
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P7_Informes_de_reservas)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If

                Case cfr.web.enum.adminWebPageEnum.informes ' rGesInformes   menu informes
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P7_Informes_de_reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P8_Informes_de_facturacion)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P9_Informes_de_Cancelaciones)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P10_Informes_de_Confirmaciones)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P18_Informes_Disponibilidad)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P19_Informes_de_Tarifas)

                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If

                Case cfr.web.enum.adminWebPageEnum.availability ' rGesDisponib   disponibilidad
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P2_Disponibilidad)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If
                Case cfr.web.enum.adminWebPageEnum.closeSelling ' cierre de ventas r_YM_cierreVentas
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P5_Yield_Management_Cierre_Ventas)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If

                Case cfr.web.enum.adminWebPageEnum.ym_minimumDays ' Yield Management Dias Minimos r_YM_diasminimos
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P4_Yield_Management_Dias_Minimos)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If
                Case cfr.web.enum.adminWebPageEnum.ym_minimumprice ' Yield Management Precio  Minimo r_YMPrecio
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P3_Yield_Management_Precio_minimo)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If
                Case cfr.web.enum.adminWebPageEnum.bookConfiguration ' configuracion sistema de reservas rmenuinicio
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If
                Case cfr.web.enum.adminWebPageEnum.hotelConfiguration ' configuracion datos del hotel adminreghotel
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P6_Configuracion_de_Datos_hotel)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If
                Case cfr.web.enum.adminWebPageEnum.userSecurity ' usuarios
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P17_Usuarios_Crear_Editar_Borrar)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If
                Case cfr.web.enum.adminWebPageEnum.tariff ' tarifas
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P15_Tarifas_Crear_Editar_Borrar)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If

                Case cfr.web.enum.adminWebPageEnum.operators ' clientes rgesoperador
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P11_Clientes_Crear_Editar_Borrar)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If

                Case cfr.web.enum.adminWebPageEnum.offers ' ofertas  
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P13_Ofertas_Crear_Editar_Borrar)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If
                Case cfr.web.enum.adminWebPageEnum.factreports '   informes facturacion
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P8_Informes_de_facturacion)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If

                Case cfr.web.enum.adminWebPageEnum.cancelReports '  informes reservas canceladas
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P9_Informes_de_Cancelaciones)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If

                Case cfr.web.enum.adminWebPageEnum.confirmedReports '  informes reservas confirmadas
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P10_Informes_de_Confirmaciones)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If

                Case cfr.web.enum.adminWebPageEnum.AvailabilityReports '   informes disponibilidad
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P18_Informes_Disponibilidad)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If

                Case cfr.web.enum.adminWebPageEnum.tariffReports '   informes tarifas
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P19_Informes_de_Tarifas)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If

                Case cfr.web.enum.adminWebPageEnum.bookingcontrol '  control de reservas
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P20_Reservas_Confirmar_Cancelar)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If

                Case cfr.web.enum.adminWebPageEnum.Design  ' Diseño de las reservas
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P14_Configurar_Diseño_reservas)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If

                Case cfr.web.enum.adminWebPageEnum.ExtraServices  'servicios extra
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P20_Reservas_Confirmar_Cancelar)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If

                Case cfr.web.enum.adminWebPageEnum.CFRFact ' Facturacion CFR / Control Total
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P23_Facturacion_yaHootel)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If

                Case cfr.web.enum.adminWebPageEnum.CFRFactReadOnly ' Facturacion CFR / readonly
                    Dim availPermissions As New ArrayList
                    availPermissions.Add(cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas)
                    availPermissions.Add(cfr.enum.adminAllowEnum.P22_Facturacion_yaHootel_Solo_lectura)
                    If validatePermissionHelper(user, availPermissions) = True Then
                        Return True
                    End If
                Case Else

            End Select

            ' si no hay permisos
            Return False


        End Function

        ''' <summary>
        ''' validate permission of the user from an array
        ''' </summary>
        ''' <param name="availablePermission"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function validatePermissionHelper(user As cfr.mgr.UserHotelMgr, availablePermission As ArrayList) As Boolean
            For Each ap As Integer In availablePermission
                For Each p As ind.bk.obj.R_Usuarios_Permisos In user.permisos
                    If p.cod_permiso = ap Then
                        Return True
                    End If
                Next
            Next
            Return False
        End Function



    End Class
End Namespace