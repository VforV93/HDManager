package com.vestris.vmwarecomlib  ;

import com4j.*;

@IID("{C828C67A-15A7-431B-95DF-B6EEF6467408}")
public interface IVMWareSnapshotCollection extends Com4jObject {
    @VTID(7)
    int count();

    @VTID(8)
    com.vestris.vmwarecomlib.IVMWareSnapshot findSnapshot(
        java.lang.String pathToSnapshot);

    @VTID(9)
    com.vestris.vmwarecomlib.IVMWareSnapshot findSnapshotByName(
        java.lang.String name);

}
