

Namespace ind.bk.obj

    Partial Public Class R_Res_Hot_Param_Manager


        Public Function getDataTableByCod_hotel(cod_hotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Res_Hot_Param_GetByID")
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.cod_hotel, cod_hotel)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        ''' <summary>
        ''' update basic hotel configuration. 
        ''' </summary>
        ''' <param name="obj2"></param>
        ''' <param name="trans"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function UpdateConfigurationBasic(obj2 As R_Res_Hot_Param, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Res_Hot_Param_UpdateBasicByCod_hotel")

            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.EdadMaxNinoGratis, obj2.EdadMaxNinoGratis)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.EdadMaxNinoPago, obj2.EdadMaxNinoPago)
            'board
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.Alojamiento, obj2.Alojamiento)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.AD, obj2.AD)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.MP, obj2.MP)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.PC, obj2.PC)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.TodoIncluido, obj2.TodoIncluido)

            'bank
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.CCC, IIf(IsNothing(obj2.CCC), "", obj2.CCC))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.CCCIBAN, IIf(IsNothing(obj2.CCCIBAN), "", obj2.CCCIBAN))

            'confirmation email
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.emailConfirmacion, IIf(IsNothing(obj2.emailConfirmacion), "", obj2.emailConfirmacion))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.emailConfirmacion2, IIf(IsNothing(obj2.emailConfirmacion2), "", obj2.emailConfirmacion2))

            'release
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.diasdeAntelacion, obj2.diasdeAntelacion)

            'TPV
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.HasTPV, obj2.HasTPV)
            'board
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.boardAD, obj2.boardAD)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.boardMP, obj2.boardMP)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.boardPC, obj2.boardPC)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.boardTI, obj2.boardTI)


            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function


        'Public Function UpdatePolicyCancel(obj2 As R_Res_Hot_Param, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
        '    Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Res_Hot_Param_UpdateBasicByCod_hotel")

        '    addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.cod_hotel, obj2.cod_hotel)
        '    addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtCancelEsp, obj2.txtCancelEsp)
        '    addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtCancelIngles, obj2.txtCancelIngles)
        '    addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtCancelAleman, obj2.txtCancelAleman)
        '    addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtCancelFrances, obj2.txtCancelFrances)

        '    addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.polEsp, obj2.polEsp)
        '    addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.polIngles, obj2.polIngles)
        '    addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.polAleman, obj2.polAleman)
        '    addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.polFrances, obj2.polFrances)

        '    addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtCancelEsp, obj2.txtCancelEsp)
        '    addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtCancelIngles, obj2.EdadMaxNinoPago)
        '    addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtCancelEsp, obj2.txtCancelEsp)
        '    addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtCancelIngles, obj2.EdadMaxNinoPago)


        '    Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
        '    Return dt
        'End Function

    End Class

End Namespace

