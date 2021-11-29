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
		,(CASE WHEN DS.[IsEnabled] = 1 THEN 'True' ELSE 'False' END) AS [Enabled] -- Habilitado
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
	ORDER BY D.[ShortName];