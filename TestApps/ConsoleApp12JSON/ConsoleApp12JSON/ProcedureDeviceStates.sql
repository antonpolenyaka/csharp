USE [TedisNet_Test1_v149_marc]
GO
/****** Object:  StoredProcedure [System].[TICGetDeviceStates]    Script Date: 31/07/2020 15:56:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [System].[TICGetDeviceStates]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT D.[ShortName] AS [DeviceName] -- Nombre
		,DT.[Name] AS [DeviceType] -- Tipo de dispositivo
		,(SELECT [Name] FROM [System].[Channels] WHERE Id = DS.CurrentChannelId) AS [CurrentChannel] -- Canal actual
		,(SELECT [Name] FROM [Lib].[EnumValues] WHERE Id = DS.[StateId]) AS [State] -- Estado
		,DS.[StateTimestamp] AS [StateTime] -- Fecha/hora estado
		,(SELECT [Name] FROM [Lib].[EnumValues] WHERE Id = DS.[RequestStateId]) AS [RequestState] -- Peticion estado
		,DS.[RequestStateTimestamp] AS [RequestStateTime] -- Fecha/hora peticion
		,U.[Name] AS [UserName] -- Usuario
		,(SELECT [Name] FROM [Lib].[EnumValues] WHERE Id = DS.[TransientStateId]) AS [TransientState] -- Estado transitorio
		,(SELECT [Name] FROM [System].[Channels] WHERE Id = DS.[TransientChannelId]) AS [TransientChannel] -- Canal transitorio
		,DS.[StatusMessage] AS [Message] -- Mensaje
		,DS.[TransientStateTimestamp] AS [TransientStateTime] -- Fecha/hora transitorio
		,(CASE WHEN DS.[IsEnabled] = 1 THEN 1 ELSE 0 END) AS [Enabled] -- Habilitado
		,(CASE WHEN DS.[IsConnected] = 1 THEN 'True' ELSE 'False' END) AS [Connected] -- Conectado
		,DS.[LastConnection] AS [LastConnectionTime]-- Ultima conexión
	  FROM [System].[DeviceStates] AS DS, 
		[System].[Devices] AS D, 
		[Lib].[DeviceTypes] AS DT,
		[System].[Channels] AS C,
		[System].[Ports] AS P,
		[Admin].[Users] AS U
	  WHERE D.Id = DS.DeviceId 
		AND D.ParentDeviceId IS NULL 
		AND DT.Id = D.DeviceTypeId
		AND DS.CurrentChannelId = C.Id 
		AND P.Id = C.MasterPortId
		AND DS.UserId = U.Id
		AND (SELECT COUNT(*) FROM System.[Ports] AS P2 WHERE P2.DeviceId = 1 AND P2.Id = C.MasterPortId) = 1
	ORDER BY D.[ShortName] FOR JSON AUTO;

END
