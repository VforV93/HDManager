package com.vestris.vmwarecomtools  ;

import com4j.*;

@IID("{E02DC9CC-322B-4D4A-A7A4-D0140435B68A}")
public interface IShellOutput extends Com4jObject {
    @VTID(7)
    java.lang.String stdErr();

    @VTID(8)
    java.lang.String stdOut();

}
