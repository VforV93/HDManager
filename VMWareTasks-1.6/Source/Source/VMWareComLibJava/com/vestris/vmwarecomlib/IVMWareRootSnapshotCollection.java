package com.vestris.vmwarecomlib  ;

import com4j.*;

@IID("{9F3BF070-992F-4188-ACAE-9D588689475C}")
public interface IVMWareRootSnapshotCollection extends Com4jObject {
    @VTID(7)
    void createSnapshot(
        java.lang.String name,
        java.lang.String description);

    @VTID(8)
    void createSnapshot2(
        java.lang.String name,
        java.lang.String description,
        int flags,
        int timeoutInSeconds);

    @VTID(9)
    com.vestris.vmwarecomlib.IVMWareSnapshot getCurrentSnapshot();

    @VTID(10)
    com.vestris.vmwarecomlib.IVMWareSnapshot getNamedSnapshot(
        java.lang.String name);

    @VTID(11)
    void removeSnapshot(
        java.lang.String name);

}
