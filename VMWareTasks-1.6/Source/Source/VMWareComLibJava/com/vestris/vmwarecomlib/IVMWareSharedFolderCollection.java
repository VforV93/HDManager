package com.vestris.vmwarecomlib  ;

import com4j.*;

@IID("{D43E20F9-F151-4B44-BD5B-13AFA7FB27D5}")
public interface IVMWareSharedFolderCollection extends Com4jObject {
    @VTID(7)
    void clear();

    @VTID(8)
    int count();

    @VTID(9)
    void enabled(
        boolean rhs);

    @VTID(10)
    @DefaultMethod
    com.vestris.vmwarecomlib.IVMWareSharedFolder item(
        int index);

}
