package com.vestris.vmwarecomtools  ;

import com4j.*;

@IID("{8CEC2F45-71F2-446A-B878-C9E5AA79D34C}")
public interface IGuestOS extends Com4jObject {
    @VTID(7)
    java.lang.String ipAddress();

    @VTID(8)
    java.lang.String readFile(
        java.lang.String guestFilename);

    @VTID(9)
    byte[] readFileBytes(
        java.lang.String guestFilename);

    @VTID(10)
    java.lang.String[] readFileLines(
        java.lang.String guestFilename);

    @VTID(11)
    com.vestris.vmwarecomtools.IVMWareVirtualMachine virtualMachine();

    @VTID(12)
    void virtualMachine(
        com.vestris.vmwarecomtools.IVMWareVirtualMachine pRetVal);

}
