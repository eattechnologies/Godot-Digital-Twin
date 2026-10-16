PROGRAM TCP_Communication
VAR
	fbDigitalTwinTCPCommunication : FB_DigitalTwinTCPCommunication;
	fbDigitalTwinConfiguration    : ST_DigitalTwinTCPConfiguration;
	stDigitalTwinData			  : ST_DigitalTwinTCPData;
	xInitializationDone			  : BOOL;
END_VAR

IF NOT xInitializationDone THEN
	fbDigitalTwinConfiguration.sIpAddress      := '127.0.0.1';
	fbDigitalTwinConfiguration.uiPort          := 50003;
	fbDigitalTwinConfiguration.sDataHeader     := 'CRD';
	fbDigitalTwinConfiguration.timSendDataTime := T#10MS;
 
	xInitializationDone := TRUE;
END_IF

stDigitalTwinData.uiNumberOfVariablesToSend := 1;
stDigitalTwinData.arDataVariablesToSend[1] 	:= stDigitalTwinData.arDataVariablesToSend[1] + 1;

fbDigitalTwinTCPCommunication(i_xEnable:= TRUE, iq_stConfiguration:= fbDigitalTwinConfiguration, iq_StData:= stDigitalTwinData);
