package com.vestris.vmwarecomtools  ;

import com4j.*;

@IID("{01E37251-FEB4-497F-9302-E05B1DEE9DAA}")
public interface IProcess extends Com4jObject {
    @VTID(7)
    java.lang.String command();

    @VTID(8)
    int exitCode();

    @VTID(9)
    long id();

    @VTID(10)
    boolean isBeingDebugged();

    @VTID(11)
    void killProcessInGuest();

    @VTID(12)
    void killProcessInGuest_2(
        int timeoutInSeconds);

    @VTID(13)
    java.lang.String name();

    @VTID(14)
    java.lang.String owner();

    @VTID(15)
    java.util.Date startDateTime();

}
