package com.vestris.vmwarecomlib  ;

import com4j.*;

@IID("{9CE66DE2-9BDA-430F-92BC-B9171D5F2A26}")
public interface IVMWareVirtualMachine extends Com4jObject {
    @VTID(7)
    void copyFileFromGuestToHost(
        java.lang.String guestPathName,
        java.lang.String hostPathName);

    @VTID(8)
    void copyFileFromGuestToHost2(
        java.lang.String guestPathName,
        java.lang.String hostPathName,
        int timeoutInSeconds);

    @VTID(9)
    void copyFileFromHostToGuest(
        java.lang.String hostPathName,
        java.lang.String guestPathName);

    @VTID(10)
    void copyFileFromHostToGuest2(
        java.lang.String hostPathName,
        java.lang.String guestPathName,
        int timeoutInSeconds);

    @VTID(11)
    int cpuCount();

    @VTID(12)
    void createDirectoryInGuest(
        java.lang.String guestPathName);

    @VTID(13)
    void createDirectoryInGuest2(
        java.lang.String guestPathName,
        int timeoutInSeconds);

    @VTID(14)
    java.lang.String createTempFileInGuest();

    @VTID(15)
    java.lang.String createTempFileInGuest2(
        int timeoutInSeconds);

    @VTID(16)
    void delete();

    @VTID(17)
    void delete2(
        int deleteOptions,
        int timeoutInSeconds);

    @VTID(18)
    void deleteDirectoryFromGuest(
        java.lang.String guestPathName);

    @VTID(19)
    void deleteDirectoryFromGuest2(
        java.lang.String guestPathName,
        int timeoutInSeconds);

    @VTID(20)
    void deleteFileFromGuest(
        java.lang.String guestPathName);

    @VTID(21)
    void deleteFileFromGuest2(
        java.lang.String guestPathName,
        int timeoutInSeconds);

    @VTID(22)
    com.vestris.vmwarecomlib.IProcess detachProgramInGuest(
        java.lang.String guestProgramName,
        java.lang.String commandLineArgs);

    @VTID(23)
    com.vestris.vmwarecomlib.IProcess detachProgramInGuest2(
        java.lang.String guestProgramName,
        java.lang.String commandLineArgs,
        int timeoutInSeconds);

    @VTID(24)
    com.vestris.vmwarecomlib.IProcess detachScriptInGuest(
        java.lang.String interpreter,
        java.lang.String scriptText);

    @VTID(25)
    com.vestris.vmwarecomlib.IProcess detachScriptInGuest2(
        java.lang.String interpreter,
        java.lang.String scriptText,
        int timeoutInSeconds);

    @VTID(26)
    boolean directoryExistsInGuest(
        java.lang.String guestPathName);

    @VTID(27)
    boolean directoryExistsInGuest2(
        java.lang.String guestPathName,
        int timeoutInSeconds);

    @VTID(28)
    void endRecording();

    @VTID(29)
    void endRecording2(
        int timeoutInSeconds);

    @VTID(30)
    boolean fileExistsInGuest(
        java.lang.String guestPathName);

    @VTID(31)
    boolean fileExistsInGuest2(
        java.lang.String guestPathName,
        int timeoutInSeconds);

    @VTID(32)
    com.vestris.vmwarecomlib.IGuestFileInfo getFileInfoInGuest(
        java.lang.String guestPathName);

    @VTID(33)
    com.vestris.vmwarecomlib.IGuestFileInfo getFileInfoInGuest2(
        java.lang.String guestPathName,
        int timeoutInSeconds);

    @VTID(34)
    com.vestris.vmwarecomlib.IVariableIndexer guestEnvironmentVariables();

    @VTID(34)
    @ReturnValue(defaultPropertyThrough={com.vestris.vmwarecomlib.IVariableIndexer.class})
    java.lang.String guestEnvironmentVariables(
        java.lang.String name);

    @VTID(35)
    com.vestris.vmwarecomlib.IVariableIndexer guestVariables();

    @VTID(35)
    @ReturnValue(defaultPropertyThrough={com.vestris.vmwarecomlib.IVariableIndexer.class})
    java.lang.String guestVariables(
        java.lang.String name);

    @VTID(36)
    void installTools();

    @VTID(37)
    void installTools2(
        int timeoutInSeconds);

    @VTID(38)
    boolean isPaused();

    @VTID(39)
    boolean isRecording();

    @VTID(40)
    boolean isReplaying();

    @VTID(41)
    boolean isRunning();

    @VTID(42)
    boolean isSuspended();

    @VTID(43)
    java.lang.String[] listDirectoryInGuest(
        java.lang.String pathName,
        boolean recurse);

    @VTID(44)
    java.lang.String[] listDirectoryInGuest2(
        java.lang.String pathName,
        boolean recurse,
        int timeoutInSeconds);

    @VTID(45)
    void loginInGuest(
        java.lang.String username,
        java.lang.String password);

    @VTID(46)
    void loginInGuest2(
        java.lang.String username,
        java.lang.String password,
        int options,
        int timeoutInSeconds);

    @VTID(47)
    void waitForVMWareUserProcessInGuest(
        java.lang.String username,
        java.lang.String password);

    @VTID(48)
    void waitForVMWareUserProcessInGuest_2(
        java.lang.String username,
        java.lang.String password,
        int timeoutInSeconds);

    @VTID(49)
    void logoutFromGuest();

    @VTID(50)
    void logoutFromGuest2(
        int timeoutInSeconds);

    @VTID(51)
    int memorySize();

    @VTID(52)
    void openUrlInGuest(
        java.lang.String url);

    @VTID(53)
    void openUrlInGuest2(
        java.lang.String url,
        int timeoutInSeconds);

    @VTID(54)
    java.lang.String pathName();

    @VTID(55)
    void pause();

    @VTID(56)
    void pause2(
        int timeoutInSeconds);

    @VTID(57)
    void powerOff();

    @VTID(58)
    void powerOff2(
        int powerOffOptions,
        int timeoutInSeconds);

    @VTID(59)
    void powerOn();

    @VTID(60)
    void powerOn2(
        int powerOnOptions,
        int timeoutInSeconds);

    @VTID(61)
    int powerState();

    @VTID(62)
    void reset();

    @VTID(63)
    void reset2(
        int resetOptions,
        int timeoutInSeconds);

    @VTID(64)
    com.vestris.vmwarecomlib.IProcess runProgramInGuest(
        java.lang.String guestProgramName,
        java.lang.String commandLineArgs);

    @VTID(65)
    com.vestris.vmwarecomlib.IProcess runProgramInGuest2(
        java.lang.String guestProgramName,
        java.lang.String commandLineArgs,
        int options,
        int timeoutInSeconds);

    @VTID(66)
    com.vestris.vmwarecomlib.IProcess runScriptInGuest(
        java.lang.String interpreter,
        java.lang.String scriptText);

    @VTID(67)
    com.vestris.vmwarecomlib.IProcess runScriptInGuest2(
        java.lang.String interpreter,
        java.lang.String scriptText,
        int options,
        int timeoutInSeconds);

    @VTID(68)
    com.vestris.vmwarecomlib.IVariableIndexer runtimeConfigVariables();

    @VTID(68)
    @ReturnValue(defaultPropertyThrough={com.vestris.vmwarecomlib.IVariableIndexer.class})
    java.lang.String runtimeConfigVariables(
        java.lang.String name);

    @VTID(69)
    void shutdownGuest();

    @VTID(70)
    void suspend();

    @VTID(71)
    void suspend2(
        int timeoutInSeconds);

    @VTID(72)
    void unpause();

    @VTID(73)
    void unpause2(
        int timeoutInSeconds);

    @VTID(74)
    void upgradeVirtualHardware();

    @VTID(75)
    void upgradeVirtualHardware2(
        int timeoutInSeconds);

    @VTID(76)
    void waitForToolsInGuest();

    @VTID(77)
    void waitForToolsInGuest2(
        int timeoutInSeconds);

    @VTID(78)
    com.vestris.vmwarecomlib.IVMWareRootSnapshotCollection snapshots();

    @VTID(79)
    com.vestris.vmwarecomlib.IVMWareSnapshot beginRecording(
        java.lang.String name,
        java.lang.String description);

    @VTID(80)
    com.vestris.vmwarecomlib.IVMWareSnapshot beginRecording2(
        java.lang.String name,
        java.lang.String description,
        int timeoutInSeconds);

}
