Imports Microsoft.VisualBasic

Public Class basERR
    'ULTIMA SECUENCIA DE ERROR UTILIZADA------->&HE0010114
    'SSCR FFFF FFFF FFFF  CCCC CCCC CCCC CCCC
    Public Const cteBaseErr As Long = &HE0000000   '1110 0000 0000 0000  0000 0000 0000 0000
    'Códigos de retorno correctos
    Public Const S_OK As Long = 0                                                       'Correcto

    Public Const E_FAIL As Long = &HE0010001                       'Error sin controlar

    'Base de datos
    Public Const E_DB_CONN As Long = &HE0010002                    'Error en conexión a BD
    Public Const E_DB_REC As Long = &HE0010003
    'Error al abrir recordset

    Public Const E_FechaReservaFueraDeHora As Long = &HE0010106       'La hora de reserva es mayor que el maximo permitido.
    Public Const E_FechaReservaMenorActual As Long = &HE0010105       'La fecha de reserva es menor que el dia actual.
    Public Const E_MaximumStay As Long = &HE001010F       'Maximum stay reached

    Public Const E_LenFecha As Long = &HE0010801                      'La longitud de la fecha es incorrecta
    Public Const E_FAIL_VerifyDate As Long = &HE0010802               'La fecha es incorrecta
    Public Const E_Data_Res As Long = &HE0010803                        'Error al guardar los datos del cliente de la reserva
    Public Const E_Get_NewCodReservaO As Long = &HE0010804              'Error al recuperar el codigo  de la reserva
    Public Const E_GetReservGlobal As Long = &HE0010805              'Error al recuperar Recordset con los datos de la reserva global
    Public Const E_GetReservDetail As Long = &HE0010806             'Error al recuperar Recordset con los datos de la reserva global
    Public Const E_GetCondTarifas As Long = &HE0010807             'Error al recuperar Recordset con las condiciones de una tarifa y habitacion
    Public Const E_SetResDisp As Long = &HE0010808             'Error al actualizar la disponibilidad
    Public Const E_NoDisponibility As Long = &HE0010809             'No existe disponibilidad
    Public Const E_OperatorFreeSale As Long = &HE001080A             'Error al recuperar si es freesale el operador
    Public Const E_GetCod_tarifa As Long = &HE001080B             'Error al recuperar el cod_tarifa de un operador
    Public Const E_Incorrect_Dates As Long = &HE001080D            '
    Public Const E_sSql_empty As Long = &HE001080E            'La cadena para lanzar GetRS viene vacia
    Public Const E_Get_CodReservaO_Identity As Long = &HE001080F    'Error al obtener el identificador de reserva
    Public Const E_TARIFA_INEXISTENTE As Long = &HE0010810    'nO EXISTE TARIFA PARA LA HABITACION SELECCIONADA
    Public Const E_TARIFA_INEXISTENTE_oper As Long = &HE0010811   'nO EXISTE TARIFA PARA el operador
    Public Const E_NewTemporada As Long = &HE0010812   'No se ha podido crear la temporada
    Public Const E_SetPeriodos As Long = &HE0010813   'Error al crear el periodo
    Public Const E_Periodo_Duplicado As Long = &HE0010814   'El periodo esta duplicado
    Public Const E_Comprobar_Periodos As Long = &HE0010815   'Error periodos duplicados
    Public Const E_Periodos_Mezclados As Long = &HE0010816   'Error las mezclas de periodos no estan permitidas, comprobar las fechas con rs devuelto
    Public Const E_NewTarifaHabitacion As Long = &HE0010817   'Error al crear la nueva tarifa
    Public Const E_Tarifa_Duplicada As Long = &HE0010818   'esta intentando crear una tarifa que ya existe
    Public Const E_GetHabitRes As Long = &HE0010819   'Error al obtener los datos de habitaciones
    Public Const E_GetCodOperadorFreeSale As Long = &HE001081A   'Error al obtener el Rs con el codigo de operador freesale
    Public Const E_Datos_Extras_Habitacion As Long = &HE001081B   'Error al obtener los datos de la habitacion
    Public Const E_MaxOcupantes As Long = &HE001081C   'Hay mas ocupantes en la reserva de los permitidos para la habitacion
    Public Const E_No_Hay_Disponibilidad As Long = &HE001081D   'No hay disponibilidad
    Public Const E_GetHotelParam As Long = &HE001081E   'Error al obtener los parametros del hotel como edad de los niños
    Public Const E_GetRS As Long = &HE001081F             'Error al recuperar Recordset
    Public Const E_GetHabPricesToRes As Long = &HE0010820   'Error al obtener los precios y disponibilidad de las habitaciones
    Public Const E_NewTarifaDesc As Long = &HE0010821   'No se ha podido crear la descripcion de la tarifa
    Public Const E_ListTarifas As Long = &HE0010822   'Error al listar las tarifas del hotel
    Public Const E_TarifaDesc_Duplicada As Long = &HE0010823   'esta intentando crear una descripcion de tarifa que ya existe
    Public Const E_ListOperadores As Long = &HE0010824   'Error al listar los operadores del hotel
    Public Const E_Operador_Duplicado As Long = &HE0010825   'esta intentando crear un operador que ya existe
    Public Const E_NewOperador As Long = &HE0010826   'No se ha podido crear el operador
    Public Const E_Temporada_Duplicada As Long = &HE0010827   'esta intentando crear temporada que ya existe
    Public Const E_ListTemporadas As Long = &HE0010828   'Error al listar las temporadas del hotel
    Public Const E_ListOperadoresTarifas As Long = &HE0010829   'Error al listar los operadores asignados al hotel y sus tarifas asignadas
    Public Const E_NoAsigOperadorToTarifa As Long = &HE001082A   'No existen valores para asignar
    Public Const E_DeleteAsigOperadorToTarifa As Long = &HE001082B  'No se ha podido borrar la asignacion de operadores a tarifas
    Public Const E_InsertAsigOperadorToTarifa As Long = &HE001082C  'No se ha podido crear la asignacion de operadores a tarifas
    Public Const E_ListPeriodos As Long = &HE001082D   'Error al listar los periodos de una temporada
    Public Const E_NewPeriodo_Duplicado As Long = &HE001082E   'esta intentando crear periodo que ya existe
    Public Const E_NewPeriodo As Long = &HE001082F   'No se ha podido crear el periodo
    Public Const E_DeletePeriodo As Long = &HE0010830   'No se ha podido BORRAR el periodo
    Public Const E_DeleteTemporada As Long = &HE0010831   'No se ha podido BORRAR la temporada
    Public Const E_NewPeriodo_Fechas As Long = &HE0010832   'La fecha de fin de periodo es menor que la de inicio del periodo
    Public Const E_DeleteTarifa As Long = &HE0010833   'No se ha podido BORRAR la Tarifa
    Public Const E_DeleteOperador As Long = &HE0010834   'No se ha podido BORRAR el operador
    Public Const E_NewIdentity As Long = &HE0010835   'No se ha podido crear la identidad deseada
    Public Const E_UpdateOperador As Long = &HE0010836   'No se ha podido modificar el operador
    Public Const E_UpdateTarifaHabitacion As Long = &HE0010837   'No se ha podido modificar la tarifa
    Public Const E_ListFormas_Pago As Long = &HE0010838   'Error al listar las formas de pago
    Public Const E_DeleteAsigTemporadaToTarifa As Long = &HE0010839  'No se ha podido borrar la asignacion de temporadas a tarifas
    Public Const E_InsertAsigTemporadaToTarifa As Long = &HE001083A  'No se ha podido crear la asignacion de temporadas a tarifas
    Public Const E_ListTarifasTemporadas As Long = &HE001083B   'Error al listar las temporadas left tarifa
    Public Const E_AsigOperadorToTarifa As Long = &HE001083C   'Error al asignar operadores a la tarifa
    Public Const E_NoAsigTemporadaToTarifa As Long = &HE001083D   'No existen valores para asignar
    Public Const E_AsigTemporadaToTarifa As Long = &HE001083E    'Error al asignar Temporadas a la tarifa
    Public Const E_ListHabitacionesGenericas As Long = &HE001083F   'Error al listar las Habitaciones GENERICAS
    Public Const E_ListHabHotel As Long = &HE0010840   'Error al listar las Habitaciones DEL HOTEL
    Public Const E_NewHabHotel As Long = &HE0010841   'No se ha podido crear la habitacion
    Public Const E_Habitacion_Duplicada As Long = &HE0010842   'esta intentando crear una habitacion que ya existe
    Public Const E_NewHabHotel_MaxPersonas As Long = &HE0010843   'esta intentando crear una habitacion con un numero de personas mayor que el maximo que ha seleccionado
    Public Const E_UpdateHabHotel_MaxPersonas As Long = &HE0010844   'esta intentando crear una habitacion con un numero de personas mayor que el maximo que ha seleccionado
    Public Const E_DeleteHabHotel As Long = &HE0010845   'No se ha podido borrar la habitacion del hotel
    Public Const E_UpdateHabHotel As Long = &HE0010846    'No se ha podido modificar la habitacion
    Public Const E_ListTarifaHabHotel As Long = &HE0010847   'Error al listar las tarifas de Habitaciones DEL HOTEL
    Public Const E_ListTarifas_Habitaciones As Long = &HE0010848   'Error al listar las tarifas de Habitaciones DEL HOTEL
    Public Const E_UpdateTarifas As Long = &HE0010849   'Error al aplicar el cambio generico de tarifas
    Public Const E_ListHotelParam As Long = &HE001084A   'Error al listar los parametros del hotel
    Public Const E_UpdateHotelParam As Long = &HE001084B   'Error al aplicar el cambio generico de parametros
    Public Const E_InicializarHotel As Long = &HE001084C   'Error al inicializar el hotel
    Public Const E_GetCalendarDisp As Long = &HE001084D   'Error al recuperar el calendario para la pantalla
    Public Const E_ListCupos As Long = &HE001084E   'Error al RECUPERAR los cupos para el calendario
    Public Const E_UpdateDisponib_No_Data As Long = &HE001084F   'Se ha pasado un RS vacio
    Public Const E_UpdateDisponib_Set As Long = &HE0010850   'Error al actualizar la disponibilidad
    Public Const E_UpdateDisponib As Long = &HE0010851   'Error generico al actualizar la disponibilidad
    Public Const E_ListDiasDisp As Long = &HE0010852   'Error al obtener el listado de dias
    Public Const E_id_Disp_Hab_IDENTITY As Long = &HE0010853   'No se ha podido obtener el id de la habitacion
    Public Const E_InsertDisponib As Long = &HE0010854   'Error al registrar la disponibilidad
    Public Const E_NewReserv_PrecioDiferente As Long = &HE0010855   'El precio que viene de la web es diferente del registrado en la bbdd. Debido a un cambio en el codigo de la pagina;) o por el hotel mientras se hacia la reserva
    Public Const E_OtroRegimen As Long = &HE0010856   'No hay precios para MP, PC y se devuelve solo alojamiento
    Public Const E_GetReservByLocalizador As Long = &HE0010857   'Error al recuperar los datos de una reserva x el localizador
    Public Const E_CancelarReserva As Long = &HE0010858   'Error al cancelar la reserva
    Public Const E_UpdateSql As Long = &HE0010859   'Error en el metodo de actualizacion generico
    Public Const E_Registro_Duplicado As Long = &HE001085A   'Error en el metodo de actualizacion generico, Registro duplicado
    Public Const E_ReservaYaCancelada As Long = &HE001085B   'La reserva ya estaba cancelada
    Public Const E_LoginOrClaveIncorrectos As Long = &HE001085C   'El login o la clave de la reserva son incorrectos para esa reserva
    Public Const E_NewReserv As Long = &HE001085D   'Error al realizar la reserva
    Public Const E_RsDatosReservaVacio As Long = &HE001085E   'Error al realizar la reserva, el REC viene vacio
    Public Const E_NewReserv_Nombre As Long = &HE001085F   'El nombre del cliete para ala reserva esta vacio
    Public Const E_NewReserv_apellido1 As Long = &HE0010860   'El Apellido del cliete para ala reserva esta vacio
    Public Const E_NewReserv_telefono As Long = &HE0010861   'El telefono del cliete para ala reserva esta vacio
    Public Const E_NewReserv_email As Long = &HE0010862   'El email del cliete para ala reserva esta vacio
    Public Const E_GetTarifas As Long = &HE0010863             'Error al recuperar las tarifas
    Public Const E_GetTarifas1 As Long = &HE0010864     'Error al recuperar las tarifas
    Public Const E_GetReservTOTAL As Long = &HE0010865      'Error al recuperar TODOS los datos de la reserva
    Public Const E_GetOperador As Long = &HE0010866      'Error al recuperar los datos del operador
    Public Const E_RegVacio As Long = &HE0010867      'Generico, un campo del metodo llamado viene vacio y es necesario
    Public Const E_GetCodOperadorByLoginClave As Long = &HE0010868      'Error al obtener el codigo del operador con la clave dada
    Public Const E_NewPeriodo_FechaInexistente As Long = &HE0010869      'La fecha no existe, por ejemplo el 31/09/2002
    Public Const E_getHotel As Long = &HE001086A      'Error al obtener los datos del hotel
    Public Const E_Update_R_Tarifas As Long = &HE001086B      'Error guardar los datos de la tarifa
    Public Const E_UpdateHotelParam_NO_REGIMEN As Long = &HE001086C      'NO ha seleccionado ningun regimen y es obligatorio 1 al menos
    Public Const E_VerDisponibByHotel As Long = &HE001086D      'Generico del metodo
    Public Const E_GetInfTarifas As Long = &HE001086E      'Generico del metodo
    Public Const E_AsigTarifaToOperador As Long = &HE001086F      'Error al insertar una tarifa
    Public Const E_ListTarifasxOperador As Long = &HE0010870      'Error obtener las tarifas del operador
    Public Const E_GetCodCRSByLoginClave As Long = &HE0010871      'Error al obtener el codigo del CRS con la clave dada
    Public Const E_ListHotelesCRS As Long = &HE0010872      'Error LISTAR LOS HOTELS DE UNA CRS
    Public Const E_ListUsers As Long = &HE0010873      'Error LISTAR LOS usuarios un hotel o CRS
    Public Const E_NewUsuario As Long = &HE0010874      'Error al crear un nuevo usuario del hotel
    Public Const E_NewUsuario_Duplicado As Long = &HE0010875      'El usuario esta duplicado
    Public Const E_LASTUsers As Long = &HE0010876      'Error al obtener el ultimo usuario
    Public Const E_UpdateUsuario As Long = &HE0010877     'Error al actualizar los datos de un usuario
    Public Const E_UpdateUsuario_login_clave As Long = &HE0010879     'Error: falta el login o clave
    Public Const E_UpdateOperador_login_clave As Long = &HE001087A     'Error: falta el login o clave
    Public Const E_NewUsuario_login_clave As Long = &HE001087B     'Error: falta el login o clave
    Public Const E_DeleteSql As Long = &HE001087C     'Error del metodo generico para borrar
    Public Const E_CheckLoginHotel As Long = &HE001087D     'Error al comprobar el login
    Public Const E_LoginDuplicado As Long = &HE001087E     'El login esta duplicado
    Public Const E_getHotelUserByCredential As Long = &HE001087F     'Error al buscar el hotel o usuario por su login y clave
    Public Const E_MapaDisponibilidad As Long = &HE0010880     'Error comprobar la disponibilidad
    Public Const E_GetCalendarCAB As Long = &HE0010881     'Error AL GENERAR LA CABECERA DE FECHAS
    Public Const E_UpdateTarifas_Numero As Long = &HE0010882     'Hay un numero incorrecto, no es un numero o no es del tipo esperado
    Public Const E_Data_Res1 As Long = &HE0010883              'Error al guardar los datos del cliente de la reserva
    Public Const E_Data_Res2 As Long = &HE0010884              'Error al guardar los datos del cliente de la reserva
    Public Const E_GetTarifas0 As Long = &HE0010885             'Error al recuperar las tarifas
    Public Const E_GetTarifas2 As Long = &HE0010886             'Error al recuperar las tarifas
    Public Const E_GetTarifas3 As Long = &HE0010887             'Error al recuperar las tarifas
    Public Const E_NewPeriodoFinsemana_FechaInexistente As Long = &HE0010888   'La fecha no es correcta
    Public Const E_NewPeriodoFinsemana_Fechas As Long = &HE0010889   'La fecha de fin de periodo es menor que la de inicio del periodo
    Public Const E_NewPeriodoFinsemana As Long = &HE001088A   'Error generico del metodo
    Public Const E_GetCodOperadorParticular As Long = &HE001088B   'Error generico del metodo
    Public Const E_CancelarRes_NoExiste As Long = &HE001088C   'No existe la reserva que intenta cancelar
    Public Const E_ConfirmarReserva As Long = &HE001088D   'Error al confirmar la reserva
    Public Const E_ConfirmarReserva_NumReg As Long = &HE001088E   'Error al confirmar la reserva, puede que no exista esa reserva
    Public Const E_TarifasPeriodos_NotExist As Long = &HE001088F   'No existen tarifas para este operador o no existen tarifas a aplicar en ese periodo.
    Public Const E_CheckFechaActual As Long = &HE0010890   'las fechas de reserva son menores que el dia de hoy
    Public Const E_Update_R_Tarifas_Desc As Long = &HE0010891   'No hay descripcion de la tarifa
    Public Const E_YMNewRestriccion_FechaInexistente As Long = &HE0010892   '
    Public Const E_YMNewRestriccion_Fechas As Long = &HE0010893   'La fecha de inicio es menor que la de fin
    Public Const E_YMNewRestriccion_Duplicado As Long = &HE0010894   'la restriccion esta duplicada
    Public Const E_YMNewRestriccion As Long = &HE0010895   'error generico del metodo
    Public Const E_ListYieldMng As Long = &HE0010896  'Error al listar el yield management
    Public Const E_DeleteYieldMng As Long = &HE0010897  'Error al BORRAR  el yield management
    Public Const E_CancelarReservaGen As Long = &HE0010898   'Error generico al cancelar la reserva
    Public Const E_TARIFA_INEXISTENTE_Hab As Long = &HE0010899   'No existe tarifas para esa habitacion o ese dia o estan incompletos los datos
    Public Const E_NewPeriodoFinsemana_dias As Long = &HE001089A   'Error con los dias seleccionados jueves, viernes,...
    Public Const E_YM_DiasMinimos As Long = &HE001089B   'no cumple el nº de dias minimos
    Public Const E_YMNewRestriccionDiasMin As Long = &HE001089C   'Error al crear una nueva restriccion de dias minimos
    Public Const E_YMNewRestriccionDiasMin_Duplicado As Long = &HE001089D   'Error al crear una nueva restriccion de dias minimos, esta duplicada
    Public Const E_ListYieldMngDiasMin As Long = &HE001089E   'Error al obtener lso dias minimos de yield management
    Public Const E_DeleteYM_Diasminimos As Long = &HE001089F   'Error al borrar el YM de dias minimos
    Public Const E_ReservaYaConfirmadaxHotel As Long = &HE00108A0   'La reserva ya estaba confirmada x el hotel
    Public Const E_ListYieldMngCierreGen As Long = &HE00108A1  'Error al listar la los cierres de ventas del hotel.
    Public Const E_YMNewCierreVentas As Long = &HE00108A2   'Error al crear cierre de ventas
    Public Const E_YMNewCierreVentas_Duplicado As Long = &HE00108A3   'Error al crear cierre de ventas
    Public Const E_DeleteYM_CierreVentas As Long = &HE00108A4   'Error al borrar cierre de ventas
    Public Const E_ExistCierreVentas As Long = &HE00108A5   'Existe un cierre de ventas
    Public Const E_UpdateOper_politica As Long = &HE00108A6   'Error al actualizar la politica del cliente
    Public Const E_UpdateHotel_TxtConfirm As Long = &HE00108A7  'Error al actualizar el texto de confirmacion de las reservas
    Public Const E_ListYieldMngCierre As Long = &HE00108A8   'Error al listar la los cierres de ventas del hotel.
    Public Const E_ListYieldMngCierreFechas As Long = &HE00108A9   'Error al listar la los cierres de ventas del hotel.
    Public Const E_UpdateServiciosHabit As Long = &HE00108AA   'Error al actualizar los servicios de la habitacion
    Public Const E_GetHabHotel As Long = &HE00108AB   'Error al actualizar los servicios de la habitacion
    Public Const E_getCod_CadenaByLogin As Long = &HE00108AC   'error al obtener codigo Cadena del hotel x login y clave
    Public Const E_Get_Cod_Tarifa_Identity As Long = &HE00108AD   'error al obtener codigo de tarifa registrada previamente
    Public Const E_Get_Cod_operador_Gen_CRS As Long = &HE00108AE   'error al obtener codigo de operador generico del CRs
    Public Const E_ListHotelesCadena As Long = &HE00108AF   'error al listar los hoteles de la cadena

    Public Const E_CRS_ListOperadores As Long = &HE00108B0   'error al listar los CLIENTES DEL CRS
    Public Const E_CRS_NewOperador As Long = &HE00108B1   'error al CREAR UN CLIENTE CRS
    Public Const E_CRS_Operador_Duplicado As Long = &HE00108B2   'error al CREAR UN CLIENTE CRS, CLIENTE DUPLICADO
    Public Const E_CRS_UpdateOper_politica As Long = &HE00108B3   'error al ACTUALIZAR LA POLITICA DEL CLIENTE
    Public Const E_CRS_UpdateOperador As Long = &HE00108B4   'error al ACTUALIZAR EL CLIENTE
    Public Const E_CRS_DeleteOperador As Long = &HE00108B5   'error al BORRAR EL CLIENTE
    Public Const E_CRS_UpdateOperador_login_clave As Long = &HE00108B6   'error CON EL LOGIN, LA CLAVE o la descripcion del operador, VIENEN VACIOS
    Public Const E_CRS_GetOperador As Long = &HE00108B7   'error AL RECUEPRAR LOS DATOS DE UN OPERADOR
    Public Const E_CRS_GetCodOperadorByLoginClave As Long = &HE00108B8   'error AL comprobar el login y la clave
    Public Const E_CRS_cod_OperadorVacio As Long = &HE00108B9   'el codigo del operador CRS viene vacio
    Public Const E_CRS_GetReservTOTAL As Long = &HE00108BA      'Error al recuperar TODOS los datos de la reserva
    Public Const E_CRS_GetCRS As Long = &HE00108BB      'Error al recuperar los datos del CRS
    Public Const E_CRS_ListHotelParam As Long = &HE00108BC      'Error al recuperar los parametrosdel hotel del CRS
    Public Const E_CRS_UpdateHotelParam_NO_REGIMEN As Long = &HE00108BD      'No ha seleccionado ningun regimen
    Public Const E_CRS_UpdateHotelParam As Long = &HE00108BE      'Error al guardar o actualizar los datos de los parameetros

    Public Const E_NewHotel_RecDatos As Long = &HE00108BF      'El rec de datos es incorrecto
    Public Const E_NewHotel_RecDatos_Login As Long = &HE00108C0      'El login o la clave vienen vacios
    Public Const E_NewHotel As Long = &HE00108C1      'Error generico
    Public Const E_NewHotel_Login_Duplicado As Long = &HE00108C2      'El login ya existe en la BBDD
    Public Const E_New_Hotel_Duplicado As Long = &HE00108C3      'Registro duplicado, quizas otro hotel se haya registrado justo en el mismo momento, improbable que ocurra
    Public Const E_NewHotel_compareLogin As Long = &HE00108C4      'error al buscar el login para saber si existe
    Public Const E_NewHotel_GetId As Long = &HE00108C5     'error al obtenr el ultimo codigo de hotel
    Public Const E_CRS_UpdateCRS As Long = &HE00108C6     'error al actualizar los parametros del CRS
    Public Const E_Get_CodReservaO_Identity_0 As Long = &HE00108C7     'error al hacer la reserva, no se ha devuelto el registro del codigo de la reserva
    Public Const E_BuscarHotel As Long = &HE00108C8     'error al realizar la busqueda por criterios
    Public Const E_BorrarHotel As Long = &HE00108C9     'error al borrar el hotel sin transaccion
    Public Const E_InsertCSS As Long = &HE00108CA     'error al insertar la hoja de estilos basica
    Public Const E_getCSS As Long = &HE00108CB     'error al obtener la hoja de estilos
    Public Const E_UpdateCSS As Long = &HE00108CC     'error al guardar la hoja de estilos
    Public Const E_VerDisponibilidad As Long = &HE00108CD     'error

    Public Const E_CREATEPIC As Long = &HE00108CE     'error al crear las imagenes en el servicio de creacion de imagenes

    Public Const E_CheckDiasDeAntelacion As Long = &HE00108CF     'error al comprobar los dias de antelacion

    Public Const E_ListHotelesSustitutos As Long = &HE00108D0     'Error al listar los hoteles sustitutos
    Public Const E_DeleteAsigHotelesSustitutos As Long = &HE00108D1     'Error al borrar los hoteles sustitutos
    Public Const E_InsertAsigHotelesSustitutos As Long = &HE00108D2     'Error al insertar los hoteles sustitutos
    Public Const E_AsigHotelesSustitutos As Long = &HE00108D3     'Error generico al insertar los hoteles sustitutos
    Public Const E_ListHotSubDisponib_getHotel As Long = &HE00108D4     'Error al obtener el codigo de cadena del hotel
    Public Const E_ListHotSubDisponib_nocadena As Long = &HE00108D5     'Error al obtener el codigo de cadena del hotel, NO EXISTE
    Public Const E_ListHotSubDisponib As Long = &HE00108D6     'Error generico al obtener los hoteles sustitutos con disponibilidad

    Public Const E_CheckDatosOperador As Long = &HE00108D7    'Error generico al confirmar si se puede continuar la reserva.
    Public Const E_ListOfertas As Long = &HE00108D8    'Error generico al listar las ofertas
    Public Const E_NewOferta_FechaInexistente As Long = &HE00108D9    'Fecha inexistente
    Public Const E_NewOferta_Fechas As Long = &HE00108DA    'La fecha final es anterior a la fecha de inicio
    Public Const E_NewOferta As Long = &HE00108DB    'Error al crear la oferta
    Public Const E_DeleteOferta As Long = &HE00108DC    'Error al borrar la oferta

    Public Const E_UpdateOferta_FechaInexistente As Long = &HE00108DD    'Fecha inexistente
    Public Const E_UpdateOferta_Fechas As Long = &HE00108DE    'La fecha final es anterior a la fecha de inicio
    Public Const E_UpdateOferta As Long = &HE00108DF    'Error al actualizar la oferta
    Public Const E_GetOferta As Long = &HE00108E0    'Error al listar la oferta
    Public Const E_NewOferta_Identity As Long = &HE00108E1    'Error al obtener el id_oferta de la nueva oferta
    Public Const E_HayOfertas As Long = &HE00108E2    'Error al obtener si hay ofertas
    Public Const E_getOperadorByCodHotel As Long = &HE00108E3    'Error al obtener datos del operador por el codigo propio del hotel en su bbdd

    Public Const E_CRS_CheckDiasDeAntelacion As Long = &HE00108E4     'error al comprobar los dias de antelacion
    Public Const E_XML_GetReservas As Long = &HE00108E5     'error generico
    Public Const E_XML_SetDisponibilidad As Long = &HE00108E6     'error generico
    Public Const E_XML_GetDisponibilidad As Long = &HE00108E7     'error generico

    Public Const E_UpdateHotel_Com_Politica As Long = &HE00108E8     'error al guardar los comentarios politicas genericas
    Public Const E_UpdateHotel_Cond_Cancel As Long = &HE00108E9     'error al guardar las condiciones de cancelacion

    Public Const E_FichaCompleta As Long = &HE00108EA     'error generico al recuperar la ficha completa del hotel
    Public Const E_FichaTec_Hotel As Long = &HE00108EB     'error al recuperar la ficha completa del hotel, los datos del hotel
    Public Const E_FichaTec_Tarifas As Long = &HE00108EC     'error al recuperar la ficha completa del hotel,
    Public Const E_FichaTec_Salones As Long = &HE00108ED     'error al recuperar la ficha completa del hotel,
    Public Const E_FichaTec_Cadenas As Long = &HE00108EE     'error al recuperar la ficha completa del hotel,
    Public Const E_FichaTec_Bonos As Long = &HE00108EF     'error al recuperar la ficha completa del hotel,
    Public Const E_FichaTec_FormaPago As Long = &HE00108F0     'error al recuperar la ficha completa del hotel,
    Public Const E_FichaTec_PuntosClave As Long = &HE00108F1     'error al recuperar la ficha completa del hotel,
    Public Const E_FichaTec_Situaciones As Long = &HE00108F2     'error al recuperar la ficha completa del hotel,
    Public Const E_FichaTec_Servicios As Long = &HE00108F3     'error al recuperar la ficha completa del hotel,
    Public Const E_HotelReservSystemActive As Long = &HE00108F4     'error al comprobar si ya tiene los parametros el hotel
    Public Const E_ListHabDataforReserv As Long = &HE00108F5     'error al listar datos basicos de la hab para la reserva del hotel
    Public Const E_NewReserv_GetTarifa As Long = &HE00108F6     'Ya no existe tarifa al realizar la reserva(por cambios, etc.)
    Public Const E_UpdCheckSendMail As Long = &HE00108F7     'Error al actualizar el control de envio de email de la reserva
    Public Const E_ListCheckSendMail As Long = &HE00108F8     'Error al listar el control de envio de email de la reserva
    Public Const E_GetTarCalculoPrecioRegimen As Long = &HE00108F9     'Error al calcular el precio mas el regimen
    Public Const E_GetTarCalculoNinosPago As Long = &HE00108FA     'Error al calcular el precio de los niños de pago.
    Public Const E_UltimoPeriodoFueraIntervalo As Long = &HE00108FB     'Al crear periodos de temporada por dias el ultimo calculado se sale fuera del intervalo.
    Public Const E_GetCountGroupByTipo As Long = &HE00108FC     'Error al obtener los clientes agrupados por tipos.
    Public Const E_UpdRegistroInternoHotel As Long = &HE00108FD     'Error modificar el registro interno de la reserva para un hotel
    Public Const E_CountNewReservas As Long = &HE00108FE     'Error al obtener la cuenta de reservas sin registrar en el sistema del hotel
    Public Const E_Get_Cod_Tarifa_Identity_0 As Long = &HE00108FF     'Error al obtener el codigo de la nueva tarifa registrada
    Public Const E_Get_Cod_Tarifa_Identity_1 As Long = &HE0010900     'Ha devuelto 0 al obtener el codigo de tarifa
    Public Const E_CopyTarifa As Long = &HE0010901     'Error al copiar la tarifa
    Public Const E_CopyTarifa_Temporada As Long = &HE0010902     'Error al copiar en la tarifa las temporadas
    Public Const E_ListTemporadasByTarifa As Long = &HE0010903     'Error al listar las temporadas de una tarifa
    Public Const E_ListGrupos As Long = &HE0010904     'Error al listar los grupos
    Public Const E_NewGrupo As Long = &HE0010905     'Error al crear los grupos
    Public Const E_Grupo_Duplicado As Long = &HE0010906     '
    Public Const E_DelGrupo As Long = &HE0010907     'error al borrar el grupo
    Public Const E_updateGrupo As Long = &HE0010908     'error al actualizar el grupo

    Public Const E_ListAgencias As Long = &HE0010909     'Error al listar los Agencias
    Public Const E_NewAgencia As Long = &HE001090A     'Error al crear los Agencias
    Public Const E_Agencia_Duplicado As Long = &HE001090B     '
    Public Const E_DelAgencia As Long = &HE001090C     'error al borrar el Agencia
    Public Const E_updateAgencia As Long = &HE001090D     'error al actualizar el Agencia
    Public Const E_ListAgenciasGrupos As Long = &HE001090E     '
    Public Const E_NewAgencia_oRec As Long = &HE001090F     'Error con el recordset de datos para crear una agencia
    Public Const E_Get_Cod_agencia_Identity As Long = &HE0010910     'Error al obtener el nuevo codigo de agencia
    Public Const E_login_agencia_exist As Long = &HE0010911     'el login ya existe
    Public Const E_GetAgenciaGrupo As Long = &HE0010912     'Error al obtener la agencia y el grupo
    Public Const E_updateAgenciaGrupo As Long = &HE0010913     'Error al actualizar o meter el grupo de la agencia
    Public Const E_DeleteAgenciaGrupo As Long = &HE0010914     'Error al borrar la relacion de agencia grupo
    Public Const E_ListAgenciasDelGrupo As Long = &HE0010915     'Error al obtener la lista de agencias asociadas al grupo
    Public Const E_ListAgenciasSinGrupo As Long = &HE0010916     'Error al obtener la lista de agencias sin ningun grupo
    Public Const E_CloseConnection As Long = &HE0010917     'error al cerrar la conexion
    Public Const E_DeleteAsigAgenciaGrupo As Long = &HE0010918     'error eliminar la relacion del grupo
    Public Const E_DelGrupo_RelacionGrupo As Long = &HE0010919     'error al eliminar la relacion del grupo
    Public Const E_DelAgencia_RelacionGrupo As Long = &HE001091A     'error al eliminar la relacion del grupo
    Public Const E_ListOperGrupos As Long = &HE001091B     'error al listar las Agencias/grupos asociadas al hotel/CRS
    Public Const E_GetOperGrupos As Long = &HE001091C     'error al obtener la Agencias/grupo asociadas al hotel/CRS
    Public Const E_AgregarOperador As Long = &HE001091D     'error al agregar una agencia registrada yahootel al hotel
    Public Const E_NoAgregarOperador As Long = &HE001091E     'error al agregar una agencia registrada yahootel al hotel, No se ha enviado nada que agregar
    Public Const E_ListOperador_Tarifas As Long = &HE001091F     'error al OBTENER todas las tarifas asignadas al operador
    Public Const E_ListTarifas_Asignar As Long = &HE0010920     'error al generar el recordset de salida con las tarifas asignadas y el resto a asignar
    Public Const E_ListGruposAgregar As Long = &HE0010921     'error al LISTAR LOS GRUPOS NO AGREGADOS AL HOTEL
    Public Const E_CalendarioTemporadas As Long = &HE0010922     'error
    Public Const E_GetReservForCalendar As Long = &HE0010923     'error
    Public Const E_CalendarioEntradas As Long = &HE0010924     'error al crear el calendario de entradas de clientes
    Public Const E_MapaDisponWeb As Long = &HE0010925     'error al crear el calendario de disponibilidad
    Public Const E_CalendarioCierres As Long = &HE0010926     'error al crear el calendario de cierres de ventas
    Public Const E_checkLoginOperadorCadena As Long = &HE0010927     'error al comprobar si existe el login de ese operador para la cadena
    Public Const E_LoginOperadorCadenaExist As Long = &HE0010928     'el login ya existe para un operador de la cadena
    Public Const E_checkLoginOperador As Long = &HE0010929     'el login ya existe para un operador del hotel
    Public Const E_LoginOperadorHotelExist As Long = &HE001092A     'el login ya existe para un operador del hotel
    Public Const E_UpdateOperador_login_exists As Long = &HE001092B     'el login ya existe
    Public Const E_UpdateDescTemporada As Long = &HE001092C     'error al actualizar datos genericos de la temporada

    Public Const E_NewReserv_Datos_OperadorGenerico As Long = &HE001092D     'lso datos del operador generico no estan completos
    Public Const E_checkDatosOperadorGenerico As Long = &HE001092E     'error al comprobar los datos del operador generico
    Public Const E_DelHabHotelCombinada As Long = &HE001092F     'No se puede borrar la habitacion por que es la combinada de otras.
    Public Const E_OperadorNoExiste As Long = &HE0010930     'No se ha encontrado ese operador por ese login o clave.

    Public Const E_UpdateLoginPwdHotel As Long = &HE0010931     'No se ha podido actualizar el login clave.
    Public Const E_UpdateLoginPwdHotel_PWDMin As Long = &HE0010932     'la clave tiene menos de 8 caracteres
    Public Const E_UpdateLoginPwdHotel_LogMin As Long = &HE0010933     'la clave tiene menos de 8 caracteres
    Public Const E_UpdateLoginPwdHotelDuplicate As Long = &HE0010934     'Login duplicado.

    Public Const E_Registro_Duplicado_BBDD As Long = -2147217900    'Login duplicado.
    Public Const E_UpdateUsuario_PWDMin As Long = &HE0010935     'la clave tiene menos de 8 caracteres
    Public Const E_UpdateUsuario_LogMin As Long = &HE0010936     'la clave tiene menos de 8 caracteres
    Public Const E_NewUsuario_PWDMin As Long = &HE0010937     'la clave tiene menos de 8 caracteres
    Public Const E_NewUsuario_LogMin As Long = &HE0010938     'la clave tiene menos de 8 caracteres

    Public Const E_ListEmpresasAgencias As Long = &HE0010939     'error al obtener las empresas asociadas
    Public Const E_DeleteAsigEmpresa2Agencia As Long = &HE001093A     'error al borrar las empresas asociadas
    Public Const E_InsertAsigEmpresa2Agencia As Long = &HE001093B     'error al Insertar las empresas asociadas
    Public Const E_AsigEmpresa2Agencia As Long = &HE001093C     'error al generar la asociacion empresas/agencia

    Public Const E_NewReserv_Empresa As Long = &HE001093D     'No ha introducido el nombre de la empresa
    Public Const E_NewReserv_Empresa_telefono As Long = &HE001093E     ''No ha introducido el tlf de la empresa
    Public Const E_NewReserv_Empresa_Email As Long = &HE001093F     ''No ha introducido el email de la empresa
    Public Const E_NewReserv_Empresa_Direcc As Long = &HE0010940     ''No ha introducido la direccion/sucursal de la empresa
    Public Const E_ListOperador_GrancCuenta As Long = &HE0010941     'Error al obtener las empresas asociadas a la agencia

    Public Const E_GetPrecxHabitacion As Long = &HE0010942     'Error al obtener el listado de los precios del hotel
    Public Const E_GetOperadorbyCod_NoExiste As Long = &HE0010943     'No ha encontrado operador con ese codigo
    Public Const E_checkLoginInCadena As Long = &HE0010944     'Error al buscar el login del operador para toda la cadena
    Public Const E_UpdateOperador_login_exists_In_Cadena As Long = &HE0010945     'El login existe para otro operador de la cadena
    Public Const E_GetCodOperadorByLoginClave1 As Long = &HE0010946     'Error en checkPermisoOperador2CancelReserva
    Public Const E_GetReservByLocalizador1 As Long = &HE0010947     'Error en checkPermisoOperador2CancelReserva
    Public Const E_checkPermisoOperador2CancelReservaInvalid As Long = &HE0010948     'Error en checkPermisoOperador2CancelReserva
    Public Const E_checkPermisoOperador2CancelReserva As Long = &HE0010949    'Error en checkPermisoOperador2CancelReserva generico
    Public Const E_GetServicios As Long = &HE001094A    'Error al obtener los servicios
    Public Const E_FacturacionTotal As Long = &HE001094B    'Error al obtener la facturacion
    Public Const E_FactPrecioyaHootel As Long = &HE001094C    'Error al obtener la facturacion
    Public Const E_FactPrecioDelHotel As Long = &HE001094D    'Error al obtener la facturacion
    Public Const E_ListPermisos As Long = &HE001094E    'Error al obtener los permisos
    Public Const E_DeleteAsigPermisos2Usuario As Long = &HE001094F    'Error al borrar los permisos
    Public Const E_InsertAsigPermisos2Usuario As Long = &HE0010950    'Error al insertar los permisos del usuario
    Public Const E_AsigPermisos2Usuario As Long = &HE0010951    'Error generico al asignar los permisos del usuario
    Public Const E_ListPermisos_Usuario As Long = &HE0010952    'Error generico al OBTENER los permisos del usuario
    Public Const E_ListPermisosUsuario As Long = &HE0010953    'Error generico al OBTENER los permisos del usuario Y MONTAR EL RECORDSET
    Public Const E_Get_IdUsuario_Identity As Long = &HE0010954    'Error
    Public Const E_ListReservas As Long = &HE0010955    'Error
    Public Const E_NewReservCHKtarifa As Long = &HE0010956    'Error al comprobar la tarifa para realizar el precio y extraer resto de datos
    Public Const E_ListReservasAndLoginOperator As Long = &HE0010957    'Error obtener el listado de reservas para exportar

    Public Const E_XML_Elem_Identification As Long = &HE0010958    'El login/clave no es correcto
    Public Const E_XML_Elem_Cod_Hotel As Long = &HE0010959    'El codigo del hotel
    Public Const E_XML_Elem_Cod_CRS As Long = &HE001095A    'El codigo del CRS
    Public Const E_XML_checkCliente As Long = &HE001095B    'Error al comprobar el login y la clave del cliente regsitrado
    Public Const E_XML_Elem_requestType As Long = &HE001095C    'Codigo de operacion erroneo
    Public Const E_XML_HotelData As Long = &HE001095D    'Error al generar el XML de datos basicos del hotel
    Public Const E_HOTEL_Data As Long = &HE001095E    'Error al obtener los datos basicos del hotel
    Public Const E_Get_FichaBasica As Long = &HE001095F    'Error al obtener los datos basicos del hotel
    Public Const E_NODATA As Long = &HE0010960    'No hay datos. El recordset viene vacio.
    Public Const E_XML_Elem_Idioma As Long = &HE0010961    'Error al obtener el parametro idioma
    Public Const E_HOTEL_Param As Long = &HE0010962    'Error al obtener los parametros de reserva del hotel
    Public Const E_HOTEL_Data_Param As Long = &HE0010963    'Error al obtener los datos y parametros de reserva del hotel
    Public Const E_XML_HotelData_Param As Long = &HE0010964    'Error al GENERAR EL XML CON los datos y parametros de reserva del hotel

    Public Const E_chxEstadoReserva As Long = &HE0010965    'Error al cambiar el estado de la reserva
    Public Const E_ReservaInexistente As Long = &HE0010966    'Error al cambiar el estado de la reserva
    Public Const E_CancelacionFechaPosterior As Long = &HE0010967    'No puede cancelar la reserva despues de la entrada
    Public Const E_EstadoReservaIncorrecto As Long = &HE0010968    'El estado es incorrecto, verificar que estado se esta pasando.

    Public Const E_UpdatePrecioFinalReserva As Long = &HE0010969    'Error al actualizar el precio de la reserva
    Public Const E_UpdatePrecioFinalReservas As Long = &HE001096A    'Error al actualizar el precio de la reservas (Bucle)

    Public Const E_SystemOutOfDate As Long = &HE001096B    'La fecha de la DLL se ha activado
    Public Const E_ClienteGet As Long = &HE001096C    'Error al obtener el cliente de facturacion
    Public Const E_ClienteNotExist As Long = &HE001096D    'El cliente de facturacion no existe
    Public Const E_ClienteUpdateData As Long = &HE001096E    'El cliente de facturacion no se ha podido actualizar
    Public Const E_HotelesAsignados As Long = &HE001096F    'nO SE HA PODIDO obtener los hoteles asignados
    Public Const E_FacturasEmitidas As Long = &HE0010970    'No se ah podido obtener las facturas del cliente
    Public Const E_TotalComisionxCliente As Long = &HE0010971    'No se ha podido obtener las comisiones de clientes
    Public Const E_ClienteInsert As Long = &HE0010972    '

    '***********************************************************

    Public Const E_ClienteExist As Long = &HE0010973    'El cliente ya existe.
    Public Const E_ClienteDelete As Long = &HE0010974    'Error al borrar el cliente de facturacion
    Public Const E_ClienteGetFactura As Long = &HE0010975    '
    Public Const E_ClienteAsignHotel As Long = &HE0010976   'Error al asignar un hotel al cliente para la facturacion.
    Public Const E_DesAsignarhotel As Long = &HE0010977   'Error al DESasignar un hotel al cliente para la facturacion.
    Public Const E_ClienteNewFactura As Long = &HE0010978   'Error al crear una nueva factura
    Public Const E_ClienteFacturaUpdate As Long = &HE0010979   'Error al modificar una factura
    Public Const E_FacturaNotExist As Long = &HE001097A   '
    Public Const E_FacturaConceptoNew As Long = &HE001097B   '
    Public Const E_FacturaConceptoUpdate As Long = &HE001097C   '
    Public Const E_GetRequestConceptos As Long = &HE001097D   '
    Public Const E_FacturaConceptoDelete As Long = &HE001097E   '
    Public Const E_ClienteHaveFacturas As Long = &HE001097F   '
    Public Const E_ClienteDeleteHaveFacturas As Long = &HE0010980   'El cliente tiene facturas y no se puede borrar
    Public Const E_HotelesPendientes As Long = &HE0010981   '
    Public Const E_HotelesRegistrados As Long = &HE0010982  '
    Public Const E_ContactGet As Long = &HE0010983  '
    Public Const E_ContactFill As Long = &HE0010984  '
    Public Const E_ContactInsert As Long = &HE0010985  '
    Public Const E_ContactUpdateData As Long = &HE0010986  '
    Public Const E_ContactNotExist As Long = &HE0010987  '
    Public Const E_ContactDelete As Long = &HE0010988  '
    Public Const E_ListAllByClient As Long = &HE0010989  '

    Public Const E_InsertEmailBoletin As Long = &HE001098A  '
    Public Const E_EmailBoletinEmpty As Long = &HE001098B  '
    Public Const E_DeleteEmailBoletin As Long = &HE001098C  '
    Public Const E_ListOfertasyah As Long = &HE001098D  '
    Public Const E_ListBoletines As Long = &HE001098E  '
    Public Const E_GetAdminLogin As Long = &HE001098F  '
    Public Const E_AdminLoginNotExist As Long = &HE0010990  'NO EXISTE EL LOGIN/CLAVE
    Public Const E_checkUser As Long = &HE0010991  '
    Public Const E_UpdateComisionHotel As Long = &HE0010992  '
    Public Const E_GetHotelComision As Long = &HE0010993  'Error al obtener las comisiones registradas del hotel.
    Public Const E_chxyyyymmdd2ddmmyyyy As Long = &HE0010994  'Error al convertir la fecha.
    Public Const E_chxDD_MM_YYYYenMM_DD_YYYY As Long = &HE0010995  'Error al convertir la fecha.
    Public Const E_GetReservaAndLoginOperatorByCodReserva As Long = &HE0010996  'Error al convertir la fecha.
    Public Const E_GetOperadorObject As Long = &HE0010997  'Error al obtener el objeto operador
    Public Const E_Release As Long = &HE0010998  'Se ha superado el release del operador.
    Public Const E_GetReservTOTALBy As Long = &HE0010999 'Error al obtener los datos de reserva
    Public Const E_NewPeticion As Long = &HE001099A 'Error al intentar insertar un registro de peticion para estadisticas.
    Public Const E_AddHabToBook As Long = &HE001099B '

    'TPV VIRTUAL
    Public Const E_PAGONOREALIZADO As Long = &HE0010107       'La pasarela de pago ha devuelto un error.
    Public Const E_PAGONOREALIZADOBYUSER As Long = &HE0010108       'La reserva no ha sido pagada o el banco no ha devuelto la confirmacion. Si cree que es un error envienos un email.
    Public Const E_PAGONOREALIZADO1 = &HE0010108       'La reserva no ha sido pagada o el banco no ha devuelto la confirmacion. Si cree que es un error envienos un email.

    Public Const E_SessionExpired As Long = &HE0010109       'La session del usuario ha expirado, comienze de nuevo.
    Public Const E_TPVSendSameDS_OrderRepeated = &HE0010114       'La session del usuario ha expirado, comienze de nuevo.

    'Usuarios
    Public Const E_getUserByCredentials As Long = &HE001010A       'Error al obtener los datos del usuario por login y clave


    'Servicios
    Public Const E_GetServicio As Long = &HE001010B       '
    Public Const E_NewServicio As Long = &HE001010F       '
    Public Const E_UpdateServicio As Long = &HE0010110       '
    Public Const E_DeleteServicio As Long = &HE0010111       '
    Public Const E_FechaInexistente As Long = &HE001010C
    Public Const E_FechaRangoInverso As Long = &HE001010D

    Public Const E_New_Identity As Long = &HE001010E

End Class
