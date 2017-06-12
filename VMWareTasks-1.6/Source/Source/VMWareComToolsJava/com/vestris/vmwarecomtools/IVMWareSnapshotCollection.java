package com.vestris.vmwarecomtools  ;

import com4j.*;

@IID("{C828C67A-15A7-431B-95DF-B6EEF6467408}")
public interface IVMWareSnapshotCollection extends Com4jObject {
    @VTID(7)
    int count();

    @VTID(8)
    com.vestris.vmwarecomtools.IVMWareSnapshot findSnapshot(
        java.lang.String pathToSnapshot);

    @VTID(9)
    com.vestris.vmwarecomtools.IVMWareSnapshot findSnapshotByName(
        java.lang.String name);

}
