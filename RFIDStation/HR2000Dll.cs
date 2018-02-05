using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

public struct OGeneralCmdType
{
    public uint SrcAddr;
    public uint TargetAddr;
    public uint Flag;
    public uint ErrType;
};

// GetReaderConfig output 
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct OGetReaderConfig
{
    public uint SrcAddr;
    public uint TargetAddr;
    public uint WorkMode;
    public uint ReaderAddr;
    public uint cmdMode;
    public uint AFICtrl;
    public uint UIDSendMode;
    public uint tagStatus;
    public uint baudrate;
    public uint BeepStatus;
    public uint AFI;
};

// GetUIDInField output
[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct OGetUIDInFieldType
{
    public uint SrcAddr;
    public uint TargetAddr;
    public uint UIDSum;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 800)]
    public Byte[] UIDArr;
    public uint remainUIDSum;
};

// FindInterfaceEvent output
public struct OFindInterfaceEventType
{
    public uint SrcAddr;
    public uint TargetAddr;
    public uint IN1;
    public uint IN2;
    public uint IN3;
    public uint IN4;
};

// ReadBlock output
[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct OReadBlockType
{
    public uint SrcAddr;
    public uint TargetAddr;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public Byte[] BlockData;
    public uint Flag;
    public uint ErrType;
};

// ReadMBlock output
[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct OReadMBlockType
{
    public uint SrcAddr;
    public uint TargetAddr;
    public uint BlockSum;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 400)]
    public Byte[] BlockDataArr;
    public uint Flag;
    public uint ErrType;
};
// ReadLblSysInfo output
public struct OReadLblSysInfoType
{
    public uint SrcAddr;
    public uint TargetAddr;
    public uint Flag;
    public uint ErrType;
    public uint InfoFlag;
    public uint DSFID;
    public uint AFI;
    public uint BlockSum;
    public uint LblSize;
    public uint IC;
};

// GeneralInfoOutput (��д�������ϱ������ݣ��磺��ǩ���룻eas����;��
[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct OGeneralInfoOutputType
{
    public uint SrcAddr;
    public uint TargetAddr;
    public uint Cmd;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public Byte[] uidArr;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
    public Byte[] dataArr;
};

[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct OGeneralInfoOutputPackageType
{
    public uint cmdSum;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
    public OGeneralInfoOutputType[] cmdArr;     //�����Խ���256��֡
};

//[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct iso14443aUid
{
    public uint type;
    public uint len;
    public uint sak;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public Byte[] uid;
};

public struct ORcvISO14443AUID
{
    public iso14443aUid uid;
    public uint srcAddr;
    public uint targetAddr;
};

//[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct OGetISO14443AUID
{
    public uint remainNum;
    public uint num;
    public uint srcAddr;
    public uint targetAddr;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
    public iso14443aUid[] uid;

};

//[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct ORWISO14443ABlock
{
    public iso14443aUid uid;
    public uint keyType;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public byte[] key;
    public uint blockNum;
    public uint blockAddr;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
    public byte[] block;
    public uint srcAddr;
    public uint targetAddr;
    public uint flag;
    public uint err;
};

//[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct ORWISO14443AValue
{
    public iso14443aUid uid;
    public uint keyType;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public byte[] key;
    public uint blockAddr;
    public uint transAddr;
    public int value;
    public uint srcAddr;
    public uint targetAddr;
    public uint flag;
    public uint err;
};

//[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct OOpISO14443AValue
{
    public iso14443aUid uid;
    public uint keyType;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public byte[] key;
    public uint opCode;
    public uint blockAddr;
    public uint transAddr;
    public int value;
    public uint srcAddr;
    public uint targetAddr;
    public uint flag;
    public uint err;
};


public struct ORISO14443AInfo{
    public iso14443aUid uid;
    public uint keyType;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public byte[] key;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
    public byte[] info;
    public uint srcAddr;
    public uint targetAddr;
    public uint flag;
    public uint err;
};


namespace RFIDStation
{
    class HR2000Dll
    {
        public const Byte HR2000_ISO15693CMD_ACTIVE_SUID = 0x10;
        public const Byte HR2000_ISO15693CMD_READ_UID = 0x11;
        public const Byte HR2000_ISO15693CMD_ACTIVE_SEAS = 0x13;

        public const Byte HR2000_ISO15693CMD_READ_SBLOCK = 0x20;
        public const Byte HR2000_ISO15693CMD_WRITE_SBLOCK = 0x21;
        public const Byte HR2000_ISO15693CMD_READ_MBLOCK = 0x22;
        public const Byte HR2000_ISO15693CMD_WRITE_MBLOCK = 0x23;
        public const Byte HR2000_ISO15693CMD_LOCK_BLOCK = 0x2C;

        public const Byte HR2000_ISO15693CMD_READ_MBLOCKEX = 0x32;
        public const Byte HR2000_ISO15693CMD_WRITE_MBLOCKEX = 0x33;
        public const Byte HR2000_ISO15693CMD_WRITE_AFI = 0x24;
        public const Byte HR2000_ISO15693CMD_LOCK_AFI = 0x25;
        public const Byte HR2000_ISO15693CMD_WRITE_DSFID = 0x26;
        public const Byte HR2000_ISO15693CMD_LOCK_DSFID = 0x27;
        public const Byte HR2000_ISO15693CMD_GETINFO = 0x28;
        public const Byte HR2000_ISO15693CMD_SET_EAS = 0x29;
        public const Byte HR2000_ISO15693CMD_RESET_EAS = 0x2A;
        public const Byte HR2000_ISO15693CMD_LOCK_EAS = 0x2B;

        //public static int hr2000Device;
        ///<summary>
        /// API LoadLibrary
        ///</summary>
        [DllImport("Kernel32")]
        public static extern int LoadLibrary(String funcname);

        ///<summary>
        /// API GetProcAddress
        ///</summary>
        [DllImport("Kernel32")]
        public static extern int GetProcAddress(int handle, String funcname);

        ///<summary>
        /// API FreeLibrary
        ///</summary>
        [DllImport("Kernel32")]
        public static extern int FreeLibrary(int handle);



        ///<summary>
        ///ͨ�����йܺ�����ת��Ϊ��Ӧ��ί��, by jingzhongrong
        ///</summary>
        ///<param name="dllModule">ͨ��LoadLibrary��õ�DLL���</param>
        ///<param name="functionName">���йܺ�����</param>
        ///<param name="t">��Ӧ��ί������</param>
        ///<returns>ί��ʵ������ǿ��ת��Ϊ�ʵ���ί������</returns>
        public static Delegate GetFunctionAddress(int dllModule, string functionName, Type t)
        {
            int address = GetProcAddress(dllModule, functionName);
            if (address == 0)
            {
                return null;
            }
            else
            {
                return Marshal.GetDelegateForFunctionPointer(new IntPtr(address), t);
            }
        }

        ///<summary>
        ///����ʾ������ַ��IntPtrʵ��ת���ɶ�Ӧ��ί��
        ///</summary>
        public static Delegate GetDelegateFromIntPtr(IntPtr address, Type t)
        {
            if (address == IntPtr.Zero)
            {
                return null;
            }
            else
            {
                return Marshal.GetDelegateForFunctionPointer(address, t);
            }
        }

        ///<summary>
        ///����ʾ������ַ��intת���ɶ�Ӧ��ί��
        ///</summary>
        public static Delegate GetDelegateFromIntPtr(int address, Type t)
        {
            if (address == 0)
            {
                return null;
            }
            else
            {
                return Marshal.GetDelegateForFunctionPointer(new IntPtr(address), t);
            }
        }

        public const Byte HR2000_CFG_TYPE_MASK = 0xF0;
        public const Byte HR2000_CFG_TYPE_ISO15693 = 0x00;
        public const Byte HR2000_CFG_TYPE_ISO14443A = 0x10;
        public const Byte HR2000_CFG_WORKMODE_MASK = 0x0F;
        public const Byte HR2000_CFG_WORKMODE_EAS = 0x01;
        public const Byte HR2000_CFG_WORKMODE_INVENTORY = 0x02;
        public const Byte HR2000_CFG_CMDMODE_AUTO = 0x00;
        public const Byte HR2000_CFG_CMDMODE_TRG = 0x01;
        public const Byte HR2000_CFG_SENDUIDMODE_ACTIVE = 0x00;
        public const Byte HR2000_CFG_SENDUIDMODE_POSITIVE = 0x01;
        public const Byte HR2000_CFG_BEEP_DISABLE = 0x00;
        public const Byte HR2000_CFG_BEEP_ENABLE = 0x01;
        public const Byte HR2000_CFG_AFI_DISABLE = 0x00;
        public const Byte HR2000_CFG_AFI_ENABLE = 0x01;
        public const Byte HR2000_CFG_TAG_QUIET = 0x00;
        public const Byte HR2000_CFG_TAG_NOQUIET = 0x01;
        public const Byte HR2000_CFG_BR_9600 = 0x05;
        public const Byte HR2000_CFG_BR_38400 = 0x07;
        public const Byte HR2000_CFG_BR_115200 = 0x0B;

        ///<summary>
        ///����HR2000ͨ�ŵĴ���
        ///</summary>
        ///<param name="name">��������:COMx</param>
        ///<param name="baudrate">���ڵĲ�����:9600/38400/115200</param>
        [DllImport("HR2000.dll", EntryPoint = "OpenPort")]
        public static extern int openPort(String name, String baudrate);

        ///<summary>
        ///�ر���HR2000ͨ�ŵĴ���
        ///</summary>
        ///<param name="device">�򿪴���ʱ��ȡ���豸ID</param>
        [DllImport("HR2000.dll", EntryPoint = "ClosePort")]
        public static extern int closePort(int device);

        ///<summary>
        ///����HR2000�Ĺ�����������Щ�����������粻��ʧ
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="WorkMode", dir="����">HR2000�Ĺ���ģʽ��EAS(0x01)��Inventory(0x02)</param>
        ///<param name="ReaderAddr", dir="����">�������õĶ�д����ַ���豸������ַ��0x0001</param>
        ///<param name="cmdMode", dir="����">HR2000������ģʽ���Զ���0x00����������0x01��</param>
        ///<param name="AFICtrl", dir="����">HR2000��AFIģʽ����ֹ��0x00����ʹ�ܣ�0x01��</param>
        ///<param name="UIDSendMode", dir="����">UID����ģʽ��������0x00����������0x01��</param>
        ///<param name="tagStatus", dir="����">��ǩ״̬����Ĭ��0x00��������Ĭ��0x01��</param>
        ///<param name="baudrate", dir="����">HR2000���ڲ����ʣ�9600��0x05����38400��0x07����115200��0x0B��</param>
        ///<param name="BeepStatus", dir="����">������״̬����ֹ��0x00����ʹ�ܣ�0x01��</param>
        ///<param name="AFI", dir="����">AFIֵ����AFIģʽ�ǡ���ֹ��ʱ��AFIֵ������</param>
        ///<param name="pGeneralCmd", dir="���">HR2000���صĽ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "SetReaderConfig")]
        public static extern int setReaderConfig(int device, ushort SrcAddr, ushort TargetAddr,
                                                 Byte WorkMode, ushort ReaderAddr, Byte cmdMode, Byte AFICtrl,
                                                 Byte UIDSendMode, Byte tagStatus, Byte baudrate, Byte BeepStatus,
                                                 Byte AFI, ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);
        ///<summary>
        ///��ȡHR2000�Ĺ�������
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="pGetReaderConfig", dir="���">HR2000���صĶ�д����������Ϣ</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "GetReaderConfig")]
        public static extern int getReaderConfig(int device, ushort SrcAddr, ushort TargetAddr,
                                                 ref OGetReaderConfig pGetReaderConfig, Byte[] pSendBuf, Byte[] pRecBuf);

        ///<summary>
        ///����HR2000��Ĭ�Ϲ�������
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="ReaderAddr", dir="����">�Ķ�����ַ��Ĭ����0x0001</param>
        ///<param name="pGeneralCmdType", dir="���">HR2000���صĽ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "SetDefaultReaderConfig")]
        public static extern int setDefaultReaderConfig(int device, ushort SrcAddr, ushort TargetAddr, ushort ReaderAddr,
                                                        ref OGeneralCmdType pGeneralCmdType, Byte[] pSendBuf, Byte[] pRecBuf);

        public const Byte HR2000_RFCTRL_CLOSE = 0x00;
        public const Byte HR2000_RFCTRL_OPEN = 0x01;
        public const Byte HR2000_RFCTRL_RESET = 0x02;
        ///<summary>
        ///�����Ķ�����Ƶ����״̬
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="RFStatus", dir="����">��Ƶ����״̬���رգ�0x00����������0x01������λ��0x02���ر�20ms��Ȼ���ٿ�����</param>
        ///<param name="pGeneralCmd", dir="���">HR2000���ñ�ǩEAS�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "SetRFCtrl")]
        public static extern int setRFCtrl(int device, ushort SrcAddr, ushort TargetAddr, Byte RFStatus,
                                      ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);

        public const Byte HR2000_TRG_NO = 0x00;
        public const Byte HR2000_TRG_INVENTORY = 0x01;
        ///<summary>
        ///����Ѱ��
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="Trigger", dir="����">��������Ч��0x00��������Ѱ����0x01��</param>
        ///<param name="pGeneralCmd", dir="���">HR2000���ñ�ǩEAS�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "SetTriggerCtrl")]
        public static extern int setTriggerCtrl(int device, ushort SrcAddr, ushort TargetAddr, Byte Trigger,
                                      ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);


        public const Byte HR2000_GETUID_MODE_NORMAL = 0x00;
        public const Byte HR2000_GETUID_MODE_REPEAT = 0x01;
        ///<summary>
        ///��ȡHR2000���߳��ڱ�ǩ��UID
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="param", dir="����">������������ͨ��0x00�����ض���0x01��</param>
        ///<param name="pGetUIDInField", dir="���">HR2000��ȡ�ı�ǩUID��Ϣ</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "GetUIDInField")]
        public static extern int getUIDInField(int device, ushort SrcAddr, ushort TargetAddr,
                                               Byte param, ref OGetUIDInFieldType pGetUIDInField, Byte[] pSendBuf, Byte[] pRecBuf);


        ///<summary>
        ///��ȡHR2000�����ϴ������߳��ڱ�ǩ��UID
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="pGeneralInfoOutputPkg", dir="���">HR2000��ȡ�ı�ǩUID��Ϣ</param>
        [DllImport("HR2000.dll", EntryPoint = "Receive")]
        public static extern int receive(int device, ref OGeneralInfoOutputPackageType pGeneralInfoOutputPkg);


        ///<summary>
        ///��ȡ��ǩ�������ݿ��ڵ�����
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="FstAddr", dir="����">���ݿ��ַ</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<param name="pReadBlock", dir="���">HR2000��ȡ�ı�ǩ�������ݿ��ڵ�������Ϣ</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "ReadBlock")]
        public static extern int readBlock(int device, ushort SrcAddr, ushort TargetAddr, Byte FstAddr, Byte[] UID,
                                           ref OReadBlockType pReadBlock, Byte[] pSendBuf, Byte[] pRecBuf);

        ///<summary>
        ///���ǩ�������ݿ���д������
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="FstAddr", dir="����">���ݿ��ַ</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<paam name="dataStr", dir="����">д������ݣ�ע�⣺������������ַ�����ʽ����001122AA����</param>
        ///<param name="pGeneralCmd", dir="���">HR2000д��ǩ���ݵĽ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "WriteBlock")]
        public static extern int writeBlock(int device, ushort SrcAddr, ushort TargetAddr, Byte FstAddr, Byte[] UID,
                                           Byte[] dataStr, ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);

        ///<summary>
        ///������ǩ�������ݿ��ڵ����ݣ�ע�⣺�����������ݿ������Բ���д������
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="FstAddr", dir="����">���ݿ��ַ</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<param name="pGeneralCmd", dir="���">HR2000������ǩ���ݿ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "LockBlock")]
        public static extern int lockBlock(int device, ushort SrcAddr, ushort TargetAddr, Byte FstAddr, Byte[] UID,
                                           ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);


        ///<summary>
        ///��ȡ��ǩ������ݿ��ڵ�����
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="FstAddr", dir="����">���ݿ��ַ</param>
        ///<paam name="BlockSum", dir="����">���ݿ�����</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<param name="pReadMBlock", dir="���">HR2000��ȡ�ı�ǩ������ݿ��ڵ�������Ϣ</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "ReadMBlock")]
        public static extern int readMBlock(int device, ushort SrcAddr, ushort TargetAddr, Byte FstAddr, Byte BlockSum, 
                                            Byte[] UID, ref OReadMBlockType pReadMBlock, Byte[] pSendBuf, Byte[] pRecBuf);


        ///<summary>
        ///���ǩ������ݿ���д������
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="FstAddr", dir="����">������ݿ��׵�ַ</param>
        ///<paam name="BlockSum", dir="����">���ݿ�����</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<paam name="dataStr", dir="����">д������ݣ�ע�⣺������������ַ�����ʽ����001122AA556677BB����</param>
        ///<param name="pGeneralCmd", dir="���">HR2000д��ǩ������ݵĽ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "WriteMBlock")]
        public static extern int writeMBlock(int device, ushort SrcAddr, ushort TargetAddr, Byte FstAddr, Byte BlockSum, Byte[] UID,
                                             Byte[] dataStr, ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);

        ///<summary>
        ///��ȡ��ǩ��Ϣ
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<param name="pReadLblSysInfo", dir="���">HR2000��ȡ�ı�ǩ��Ϣ</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "ReadLblSysInfo")]
        public static extern int readLblSysInfo(int device, ushort SrcAddr, ushort TargetAddr, Byte[] UID,
                                                ref OReadLblSysInfoType pReadLblSysInfo, Byte[] pSendBuf, Byte[] pRecBuf);





        ///<summary>
        ///д��ǩAFIֵ
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="AFIVal", dir="����">AFIֵ</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<param name="pGeneralCmd", dir="���">HR2000д��ǩAFIֵ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "WriteAFI")]
        public static extern int writeAFI(int device, ushort SrcAddr, ushort TargetAddr, Byte AFIVal, Byte[] UID,
                                          ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);

        ///<summary>
        ///������ǩAFIֵ
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<param name="pGeneralCmd", dir="���">HR2000������ǩAFIֵ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "LockAFI")]
        public static extern int lockAFI(int device, ushort SrcAddr, ushort TargetAddr, Byte[] UID,
                                    ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);

        ///<summary>
        ///д��ǩDSFIDֵ
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="DSFIDVal", dir="����">DSFIDֵ</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<param name="pGeneralCmd", dir="���">HR2000д��ǩDSFIDֵ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "WriteDSFID")]
        public static extern int writeDSFID(int device, ushort SrcAddr, ushort TargetAddr, Byte DSFIDVal, Byte[] UID,
                                      ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);

        ///<summary>
        ///������ǩDSFIDֵ
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<param name="pGeneralCmd", dir="���">HR2000������ǩDSFIDֵ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "LockDSFID")]
        public static extern int lockDSFID(int device, ushort SrcAddr, ushort TargetAddr, Byte[] UID,
                                      ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);

        ///<summary>
        ///���ñ�ǩEASֵ
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="Cmd", dir="����">�����룺��λEAS��0x29������λEAS��0x2A��������EAS��0x2B��</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<param name="pGeneralCmd", dir="���">HR2000���ñ�ǩEAS�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "SetEAS")]
        public static extern int setEAS(int device, ushort SrcAddr, ushort TargetAddr, Byte Cmd, Byte[] UID,
                                      ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);


        ///<summary>
        ///��ȡM24��ǩ������ݿ��ڵ�����
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="FstAddr", dir="����">���ݿ��ַ</param>
        ///<paam name="BlockSum", dir="����">���ݿ�����</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<param name="pReadMBlock", dir="���">HR2000������ǩ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("STM24LRxxx.dll", EntryPoint = "m24ReadMBlock")]
        public static extern int m24ReadMBlock(int device, ushort SrcAddr, ushort TargetAddr, ushort FstAddr, Byte BlockSum,
                                               Byte[] UID, ref OReadMBlockType pReadMBlock, Byte[] pSendBuf, Byte[] pRecBuf);


        ///<summary>
        ///��M24��ǩ������ݿ���д������
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="FstAddr", dir="����">������ݿ��׵�ַ</param>
        ///<paam name="BlockSum", dir="����">���ݿ�����</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<paam name="dataStr", dir="����">д������ݣ�ע�⣺������������ַ�����ʽ����001122AA556677BB����</param>
        ///<param name="pGeneralCmd", dir="���">HR2000������ǩ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("STM24LRxxx.dll", EntryPoint = "m24WriteMBlock")]
        public static extern int m24WriteMBlock(int device, ushort SrcAddr, ushort TargetAddr, ushort FstAddr, Byte BlockSum, Byte[] UID,
                                                Byte[] dataStr, ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);


        ///<summary>
        ///��ȡM24��ǩ���ݶ�����״̬
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="pSectorStatus", dir="���">���ݶ�����״̬</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<paam name="sectorIndex", dir="����">���ݶ�����</param>
        ///<param name="pGeneralCmd", dir="���">HR2000������ǩ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("STM24LRxxx.dll", EntryPoint = "m24ReadSectorStatus")]
        public static extern int m24ReadSectorStatus(int device, ushort SrcAddr, ushort TargetAddr, Byte[] pSectorStatus, Byte[] UID, 
                                                     Byte sectorIndex, ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);


        ///<summary>
        ///����M24��ǩ���ݶ�
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<paam name="sectorIndex", dir="����">���ݶ�����</param>
        ///<paam name="sectorStatus", dir="����">���ݶ�����״̬</param>
        ///<param name="pGeneralCmd", dir="���">HR2000������ǩ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("STM24LRxxx.dll", EntryPoint = "m24LockSector")]
        public static extern int m24LockSector(int device, ushort SrcAddr, ushort TargetAddr, Byte[] UID, Byte sectorIndex, Byte sectorStatus, 
                                               ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);

        ///<summary>
        ///дM24��ǩ���ݶ�����
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<paam name="pwdIndex", dir="����">���ݶ�����������1/2/3</param>
        ///<paam name="pSectorPwd", dir="����">���ݶ����룺4�ֽ�</param>
        ///<param name="pGeneralCmd", dir="���">HR2000������ǩ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("STM24LRxxx.dll", EntryPoint = "m24WriteSectorPwd")]
        public static extern int m24WriteSectorPwd(int device, ushort SrcAddr, ushort TargetAddr, Byte[] UID, Byte pwdIndex, Byte[] pSectorPwd, 
                                                   ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);

        ///<summary>
        ///�ύM24��ǩ���ݶε�ǰ����
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<paam name="pwdIndex", dir="����">���ݶ�����������1/2/3</param>
        ///<paam name="pSectorPwd", dir="����">���ݶ����룺4�ֽ�</param>
        ///<param name="pGeneralCmd", dir="���">HR2000������ǩ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("STM24LRxxx.dll", EntryPoint = "m24PresentSectorPwd")]
        public static extern int m24PresentSectorPwd(int device, ushort SrcAddr, ushort TargetAddr, Byte[] UID, Byte pwdIndex, Byte[] pSectorPwd,
                                                     ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);


        ///<summary>
        ///��ȡM24��ǩ���üĴ���
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<paam name="pCfg", dir="���">���ò���</param>
        ///<param name="pGeneralCmd", dir="���">HR2000������ǩ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("STM24LRxxx.dll", EntryPoint = "m24ReadCfg")]
        public static extern int m24ReadCfg(int device, ushort SrcAddr, ushort TargetAddr, Byte[] UID, Byte[] pCfg,
                                            ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);

        ///<summary>
        ///дM24��ǩEH���ò���
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<paam name="eHCfg", dir="����">EH���ò���</param>
        ///<param name="pGeneralCmd", dir="���">HR2000������ǩ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("STM24LRxxx.dll", EntryPoint = "m24WriteEHCfg")]
        public static extern int m24WriteEHCfg(int device, ushort SrcAddr, ushort TargetAddr, Byte[] UID, Byte eHCfg,
                                               ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);


        ///<summary>
        ///дM24��ǩDO���ò���
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<paam name="dOCfg", dir="����">DO���ò���</param>
        ///<param name="pGeneralCmd", dir="���">HR2000������ǩ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("STM24LRxxx.dll", EntryPoint = "m24WriteDOCfg")]
        public static extern int m24WriteDOCfg(int device, ushort SrcAddr, ushort TargetAddr, Byte[] UID, Byte dOCfg,
                                               ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);


        ///<summary>
        ///���M24��ǩEHEn״̬
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<paam name="pEHEn", dir="���">EHEn״̬</param>
        ///<param name="pGeneralCmd", dir="���">HR2000������ǩ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("STM24LRxxx.dll", EntryPoint = "m24CheckEHEn")]
        public static extern int m24CheckEHEn(int device, ushort SrcAddr, ushort TargetAddr, Byte[] UID, Byte[] pEHEn,
                                              ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);


        ///<summary>
        ///����M24��ǩEHEn״̬
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<paam name="UID", dir="����">��ǩUID</param>
        ///<paam name="eHEn", dir="����">EHEn</param>
        ///<param name="pGeneralCmd", dir="���">HR2000������ǩ�Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("STM24LRxxx.dll", EntryPoint = "m24CtrlEHEn")]
        public static extern int m24CtrlEHEn(int device, ushort SrcAddr, ushort TargetAddr, Byte[] UID, Byte eHEn,
                                             ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);


        ///<summary>
        ///���ͺͽ�������֡
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<paam name="pSendBuf", dir="����">���͵�����֡</param>
        ///<param name="txLen", dir="����">��������֡����</param>
        ///<param name="pRecBuf", dir="���">��Ӧ���յ�����֡</param>
        ///<param name="timeout", dir="����">�ȴ���Ӧ��ʱʱ��</param>
        [DllImport("STM24LRxxx.dll", EntryPoint = "serialWriteFrame")]
        public static extern int serialWriteFrame(int hSerial, Byte[] pSendBuf, ushort txLen, Byte[] pRecBuf, int timeout);

        ///<summary>
        ///��ȡ�����¼�
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="pFindInterfaceEvent", dir="����">HR2000��ȡ�����¼��Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "FindInterfaceEvent")]
        public static extern int findInterfaceEvent(int device, ushort SrcAddr, ushort TargetAddr,
                                                    ref OFindInterfaceEventType pFindInterfaceEvent, Byte[] pSendBuf, Byte[] pRecBuf);

        ///<summary>
        ///��������¼�
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="Relay", dir="����">�̵������</param>
        ///<param name="OUT1", dir="����">OUT1���</param>
        ///<param name="OUT2", dir="����">OUT2���</param>
        ///<param name="pGeneralCmd", dir="����">HR2000��������¼��Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "setOutputIfcEvent")]
        public static extern int setOutputIfcEvent(int device, ushort SrcAddr, ushort TargetAddr, Byte Relay, Byte OUT1, Byte OUT2,
                                                   ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);


        ///<summary>
        ///����ӿ��¼�����
        ///</summary>
        ///<param name="device", dir="����">�򿪴���ʱ��ȡ���豸ID</param>
        ///<param name="SrcAddr", dir="����">Դ��ַ������ͨ�ŵ��豸��ַ��Ĭ����0x0000</param>
        ///<param name="TargetAddr", dir="����">Ŀ���ַ��������Ӧ���豸��ַ��Ĭ����0x0001</param>
        ///<param name="Relay", dir="����">�̵������</param>
        ///<param name="OUT1", dir="����">OUT1���</param>
        ///<param name="OUT2", dir="����">OUT2���</param>
        ///<param name="pGeneralCmd", dir="����">HR2000��������¼��Ľ��</param>
        ///<param name="pSendBuf", dir="���">���͵�HR2000������֡</param>
        ///<param name="pRecBuf", dir="���">HR2000��Ӧ������֡</param>
        [DllImport("HR2000.dll", EntryPoint = "OutputIfcEventConfig")]
        public static extern int outputIfcEventConfig(int device, ushort SrcAddr, ushort TargetAddr, 
                                                      Byte RelayFreq, Byte RelayCycle, Byte OUT1Freq, Byte OUT1Cycle, Byte OUT2Freq, Byte OUT2Cycle,
                                                      ref OGeneralCmdType pGeneralCmd, Byte[] pSendBuf, Byte[] pRecBuf);

        public const Byte HR2000_ISO14443ACMD_ACTIVE_SUID = 0x15;
        public const Byte HR2000_ISO14443ACMD_GET_UID = 0x16;
        public const Byte HR2000_ISO14443ACMD_AUTHRB = 0x70;
        public const Byte HR2000_ISO14443ACMD_AUTHWB = 0x71;
        public const Byte HR2000_ISO14443ACMD_AUTHRV = 0x72;
        public const Byte HR2000_ISO14443ACMD_AUTHWV = 0x73;
        public const Byte HR2000_ISO14443ACMD_AUTHV = 0x74;
        public const Byte HR2000_ISO14443ACMD_RBLOCK = 0x75;
        public const Byte HR2000_ISO14443ACMD_WBLOCK4 = 0x76;
        public const Byte HR2000_RISO14443A_UID_MAX = 15;
        public const Byte HR2000_RISO14443A_MOBLOCKNUM_MAX = 52;
        public const Byte HR2000_RISO14443A_SIG_LEN = 32;
        public const Byte HR2000_RISO14443A_CNT_LEN = 3;
        public const Byte HR2000_RISO14443A_PCK_LEN = 2;

        [DllImport("ISO14443A.dll", EntryPoint = "iso14443AGetUID")]
        public static extern int iso14443AGetUID(int device, ushort SrcAddr, ushort TargetAddr, Byte mode,
                                                 ref OGetISO14443AUID pGetUID, Byte[] pSendBuf, Byte[] pRecBuf);

        [DllImport("ISO14443A.dll", EntryPoint = "iso14443AAuthReadBlock")]
        public static extern int iso14443AAuthReadBlock(int device, ushort SrcAddr, ushort TargetAddr,
                                                        ref ORWISO14443ABlock pISO14443ABlock, Byte[] pSendBuf, Byte[] pRecBuf);

        [DllImport("ISO14443A.dll", EntryPoint = "iso14443AAuthWriteBlock")]
        public static extern int iso14443AAuthWriteBlock(int device, ushort SrcAddr, ushort TargetAddr,
                                                         ref ORWISO14443ABlock pISO14443ABlock, Byte[] pSendBuf, Byte[] pRecBuf);

        [DllImport("ISO14443A.dll", EntryPoint = "iso14443AAuthReadValue")]
        public static extern int iso14443AAuthReadValue(int device, ushort SrcAddr, ushort TargetAddr,
                                                        ref ORWISO14443AValue pISO14443AValue, Byte[] pSendBuf, Byte[] pRecBuf);

        [DllImport("ISO14443A.dll", EntryPoint = "iso14443AAuthWriteValue")]
        public static extern int iso14443AAuthWriteValue(int device, ushort SrcAddr, ushort TargetAddr,
                                                         ref ORWISO14443AValue pISO14443AValue, Byte[] pSendBuf, Byte[] pRecBuf);

        [DllImport("ISO14443A.dll", EntryPoint = "iso14443AAuthValue")]
        public static extern int iso14443AAuthValue(int device, ushort SrcAddr, ushort TargetAddr,
                                                    ref OOpISO14443AValue pOpISO14443AValue, Byte[] pSendBuf, Byte[] pRecBuf);

        [DllImport("ISO14443A.dll", EntryPoint = "iso14443AReadM0Block")]
        public static extern int iso14443AReadM0Block(int device, ushort SrcAddr, ushort TargetAddr,
                                                    ref ORWISO14443ABlock pOpISO14443ABlock, Byte[] pSendBuf, Byte[] pRecBuf);

        [DllImport("ISO14443A.dll", EntryPoint = "iso14443AWriteM0Block")]
        public static extern int iso14443AWriteM0Block(int device, ushort SrcAddr, ushort TargetAddr,
                                                    ref ORWISO14443ABlock pOpISO14443ABlock, Byte[] pSendBuf, Byte[] pRecBuf);

        [DllImport("ISO14443A.dll", EntryPoint = "iso14443AReadM0Sig")]
        public static extern int iso14443AReadM0Sig(int device, ushort SrcAddr, ushort TargetAddr,
                                                  ref ORISO14443AInfo pISO14443AInfo, Byte[] pSendBuf, Byte[] pRecBuf);

        [DllImport("ISO14443A.dll", EntryPoint = "iso14443AReadM0Cnt")]
        public static extern int iso14443AReadM0Cnt(int device, ushort SrcAddr, ushort TargetAddr,
                                                  ref ORISO14443AInfo pISO14443AInfo, Byte[] pSendBuf, Byte[] pRecBuf);

        [DllImport("ISO14443A.dll", EntryPoint = "iso14443AAuthM0")]
        public static extern int iso14443AAuthM0(int device, ushort SrcAddr, ushort TargetAddr,
                                                  ref ORISO14443AInfo pISO14443AInfo, Byte[] pSendBuf, Byte[] pRecBuf);
    }
}



