package com.vestris.vmwarecomtools  ;

import com4j.*;

@IID("{392F462E-E4A3-4771-8074-804316D4E426}")
public interface IShell extends Com4jObject {
    @VTID(7)
    com.vestris.vmwarecomtools.IShellOutput runCommandInGuest(
        java.lang.String guestCommandLine);

    @VTID(8)
    com.vestris.vmwarecomtools.IVMWareVirtualMachine virtualMachine();

    @VTID(9)
    void virtualMachine(
        com.vestris.vmwarecomtools.IVMWareVirtualMachine pRetVal);

}
