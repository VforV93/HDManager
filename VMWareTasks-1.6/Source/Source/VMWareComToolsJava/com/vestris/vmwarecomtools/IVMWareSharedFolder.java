package com.vestris.vmwarecomtools  ;

import com4j.*;

@IID("{276BCAEF-0C02-4A4E-9FB6-A826804D67DC}")
public interface IVMWareSharedFolder extends Com4jObject {
    @VTID(7)
    int flags();

    @VTID(8)
    java.lang.String hostPath();

    @VTID(9)
    java.lang.String shareName();

}
