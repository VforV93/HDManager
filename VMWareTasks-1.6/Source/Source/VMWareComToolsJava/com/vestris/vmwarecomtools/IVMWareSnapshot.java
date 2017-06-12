package com.vestris.vmwarecomtools  ;

import com4j.*;

@IID("{BBE59046-66AF-458C-AD14-3F25546193E9}")
public interface IVMWareSnapshot extends Com4jObject {
    @VTID(7)
    void beginReplay();

    @VTID(8)
    void beginReplay2(
        int powerOnOptions,
        int timeoutInSeconds);

    @VTID(9)
    java.lang.String description();

    @VTID(10)
    java.lang.String displayName();

    @VTID(11)
    void endReplay();

    @VTID(12)
    void endReplay2(
        int timeoutInSeconds);

    @VTID(13)
    boolean isReplayable();

    @VTID(14)
    com.vestris.vmwarecomtools.IVMWareSnapshot parent();

    @VTID(15)
    java.lang.String path();

    @VTID(16)
    int powerState();

    @VTID(17)
    void removeSnapshot();

    @VTID(18)
    void removeSnapshot2(
        int timeoutInSeconds);

    @VTID(19)
    void revertToSnapshot();

    @VTID(20)
    void revertToSnapshot2(
        int powerOnOptions,
        int timeoutInSeconds);

    @VTID(21)
    com.vestris.vmwarecomtools.IVMWareSnapshotCollection childSnapshots();

}
