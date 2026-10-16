TYPE ST_DigitalTwinTCPConfiguration :
STRUCT
	sIpAddress	    : NBS.IPADDRESS := '127.0.0.1';
	uiPort		    : UINT		    := 50003;
	
	sDataHeader     : STRING	    := 'CRD';
	timSendDataTime : TIME 	        := T#10MS;
END_STRUCT
END_TYPE
