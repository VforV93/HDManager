package com.vestris.vmwarecomlib  ;

import com4j.*;

@IID("{E3610635-DF7B-48B1-BFB5-F9D2CED6961C}")
public interface IGuestFileInfo extends Com4jObject {
    @VTID(7)
    long fileSize();

    @VTID(8)
    int flags();

    @VTID(9)
    java.lang.String guestPathName();

    @VTID(10)
    boolean isDirectory();

    @VTID(11)
    boolean isSymLink();

    @VTID(12)
    java.util.Date lastModified();

}
