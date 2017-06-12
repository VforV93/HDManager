package com.vestris.vmwarecomlib  ;

import com4j.*;

/**
 * Defines methods to create COM objects
 */
public abstract class ClassFactory {
    private ClassFactory() {} // instanciation is not allowed


    public static com.vestris.vmwarecomlib.IVMWareRootSnapshotCollection createVMWareRootSnapshotCollection() {
        return COM4J.createInstance( com.vestris.vmwarecomlib.IVMWareRootSnapshotCollection.class, "{7A733057-9205-424E-852B-C5F64F91719F}" );
    }

    public static com.vestris.vmwarecomlib.IVMWareSharedFolder createVMWareSharedFolder() {
        return COM4J.createInstance( com.vestris.vmwarecomlib.IVMWareSharedFolder.class, "{7D371600-E174-406D-B32D-DF9A1C6C0A83}" );
    }

    public static com.vestris.vmwarecomlib.IVMWareSharedFolderCollection createVMWareSharedFolderCollection() {
        return COM4J.createInstance( com.vestris.vmwarecomlib.IVMWareSharedFolderCollection.class, "{A5C7F11C-6869-4E2D-8418-79A9E00E32FA}" );
    }

    public static com.vestris.vmwarecomlib.IVMWareSnapshot createVMWareSnapshot() {
        return COM4J.createInstance( com.vestris.vmwarecomlib.IVMWareSnapshot.class, "{0D890EF7-B433-41F7-95EC-A5AAA2E046A0}" );
    }

    public static com.vestris.vmwarecomlib.IVMWareSnapshotCollection createVMWareSnapshotCollection() {
        return COM4J.createInstance( com.vestris.vmwarecomlib.IVMWareSnapshotCollection.class, "{943582FB-8CC2-47A7-83F6-7D44A5CB62E0}" );
    }

    public static com.vestris.vmwarecomlib.IVMWareVirtualHost createVMWareVirtualHost() {
        return COM4J.createInstance( com.vestris.vmwarecomlib.IVMWareVirtualHost.class, "{23333B5F-D53F-40C6-8B1F-96D11004CA91}" );
    }

    public static com.vestris.vmwarecomlib.IVMWareVirtualMachine createVMWareVirtualMachine() {
        return COM4J.createInstance( com.vestris.vmwarecomlib.IVMWareVirtualMachine.class, "{F4BC0794-4BE4-47E0-9FC4-804ADAAEB4C1}" );
    }

    public static com.vestris.vmwarecomlib.IGuestFileInfo createGuestFileInfo() {
        return COM4J.createInstance( com.vestris.vmwarecomlib.IGuestFileInfo.class, "{FD69E775-94E4-425B-BA1F-967BB258993F}" );
    }

    public static com.vestris.vmwarecomlib.IProcess createProcess() {
        return COM4J.createInstance( com.vestris.vmwarecomlib.IProcess.class, "{58954C2A-F83C-43F1-B735-431C9794072E}" );
    }

    public static com.vestris.vmwarecomlib.IVariableIndexer createVariableIndexer() {
        return COM4J.createInstance( com.vestris.vmwarecomlib.IVariableIndexer.class, "{F9FE604D-6E08-4164-A068-2DC505A79470}" );
    }
}
