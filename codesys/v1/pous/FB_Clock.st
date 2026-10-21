FUNCTION_BLOCK FB_Clock
VAR_INPUT
	Enable : BOOL;
	Clock  : TIME;
END_VAR
VAR_OUTPUT
	Q      : BOOL;
END_VAR
VAR
	fbBlink : BLINK;
	fbRtrig : R_TRIG;
END_VAR

fbBlink(ENABLE:= Enable, TIMELOW:= Clock/2, TIMEHIGH:= Clock/2, OUT=> );
fbRtrig(CLK:= fbBlink.OUT, Q=> Q);
