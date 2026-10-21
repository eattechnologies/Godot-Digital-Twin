TYPE ST_DigitalTwinTCPData :
STRUCT
	uiNumberOfVariablesToSend		: UINT;
	arDataVariablesToSend			: ARRAY[1..GVL_DigitalTwinConstants.MaxNumberOfVariablesToSend] OF REAL;
END_STRUCT
END_TYPE
