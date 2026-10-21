# CODESYS

Version 1 of the PLC side of the digital twin (CODESYS V3.5 SP21 Patch 5, CODESYS Control Win V3 x64).

- `Digital Twins.project`: full CODESYS project. Open it with File > Open Project.
- `pous/`: plain-text export of every POU, for reading and diffing on GitHub. Methods are appended to their function block's file.

`TCP_Communication` runs a TCP server on `127.0.0.1:50003` and sends, every 10 ms:

```
"CRD" | N (1 byte) | N x REAL (4 bytes each, little-endian)
```

`arDataVariablesToSend[1]` in CODESYS is index 0 in Godot's `CommData.ReceiveVariables`.
