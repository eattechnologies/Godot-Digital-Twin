FUNCTION_BLOCK FB_DigitalTwinTCPCommunication
VAR_INPUT
	i_xEnable 	: BOOL;
END_VAR
VAR_OUTPUT
END_VAR
VAR
	fbTcpServer       : NBS.TCP_Server;
	fbTcpConnection   : NBS.TCP_Connection;
	fbSendDataClock	  : FB_Clock;
	fbIpAddress       : NBS.IPv4Address;
	xEnableConnection : BOOL;
	abSendData		  : ARRAY[1..GVL_DigitalTwinConstants.ByteArraySize] OF BYTE;
	udiSendDataSize	  : UDINT;
	iIndex			  : INT;
	
END_VAR

VAR_IN_OUT
	
	iq_stConfiguration : ST_DigitalTwinTCPConfiguration;
	iq_StData		   : ST_DigitalTwinTCPData;
END_VAR

IF fbTcpConnection._xSocketClosed THEN
	xEnableConnection := FALSE;

ELSIF i_xEnable THEN
	xEnableConnection := TRUE;

ELSE
	xEnableConnection := FALSE;
	
END_IF

IF fbIpAddress.ipAddress <> iq_stConfiguration.sIpAddress THEN
	fbIpAddress.SetInitialValue(ipAddress := iq_stConfiguration.sIpAddress);
END_IF

fbTcpServer(
	xEnable := i_xEnable,   
    itfIPAddress := fbIpAddress,
    uiPort := iq_stConfiguration.uiPort );
	
	
fbTcpConnection(
	xEnable:= xEnableConnection,  
     itfServer := fbTcpServer);	
	 
	 fbSendDataClock(Enable:= i_xEnable, Clock:= iq_stConfiguration.timSendDataTime, Q=> );
	 
	 
IF fbTcpConnection.xActive AND fbSendDataClock.Q THEN
	BuildSendData();
	fbTcpConnection.Write(pData := ADR(abSendData), udiSize := udiSendDataSize);
END_IF

// ===== BuildSendData =====
// Build the header

FOR iIndex := 1 TO Standard.LEN(iq_stConfiguration.sDataHeader) DO
	abSendData[iIndex] := iq_stConfiguration.sDataHeader[iIndex - 1];
END_FOR

// Number of variables - Byte

abSendData[Standard.LEN(iq_stConfiguration.sDataHeader) + 1] := UINT_TO_BYTE(iq_StData.uiNumberOfVariablesToSend);
MEMUtils.MemCpy(ADR(abSendData[Standard.LEN(iq_stConfiguration.sDataHeader)+ 2]) ,ADR(iq_StData.arDataVariablesToSend[1]),iq_stData.uiNumberOfVariablesToSend*4);
udiSendDataSize := INT_TO_UDINT(Standard.LEN(iq_stConfiguration.sDataHeader)) + 1 + iq_stData.uiNumberOfVariablesToSend*4;
