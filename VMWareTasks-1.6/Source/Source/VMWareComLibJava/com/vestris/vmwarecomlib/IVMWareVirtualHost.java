package com.vestris.vmwarecomlib  ;

import com4j.*;

@IID("{0209B715-5167-42E7-8A32-DFEB9AAC767C}")
public interface IVMWareVirtualHost extends Com4jObject {
    @VTID(7)
    void connectToVMWareServer(
        java.lang.String hostName,
        java.lang.String username,
        java.lang.String password);

    @VTID(8)
    void connectToVMWareServer2(
        java.lang.String hostName,
        java.lang.String username,
        java.lang.String password,
        int timeoutInSeconds);

    @VTID(9)
    void connectToVMWareVIServer(
        java.lang.String hostName,
        java.lang.String username,
        java.lang.String password);

    @VTID(10)
    void connectToVMWareWorkstation();

    @VTID(11)
    void connectToVMWareWorkstation2(
        int timeoutInSeconds);

    @VTID(12)
    void disconnect();

    @VTID(13)
    boolean isConnected();

    @VTID(14)
    com.vestris.vmwarecomlib.IVMWareVirtualMachine open(
        java.lang.String fileName);

    @VTID(15)
    com.vestris.vmwarecomlib.IVMWareVirtualMachine open2(
        java.lang.String fileName,
        int timeoutInSeconds);

    @VTID(16)
    void register(
        java.lang.String fileName);

    @VTID(17)
    void register2(
        java.lang.String fileName,
        int timeoutInSeconds);

    @VTID(20)
    void unregister(
        java.lang.String fileName);

    @VTID(21)
    void unregister2(
        java.lang.String fileName,
        int timeoutInSeconds);

}
