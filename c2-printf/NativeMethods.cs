using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace c2_printf
{
    public static unsafe class NativeMethods
    {
        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint Connect(int nComPort = 1, int nDisableDialogBoxes = 0, int nECprotocol = 0, int nBaudRateIndex = 0);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint ConnectUSB([MarshalAs(UnmanagedType.LPStr)] string sSerialNum, int nECprotocol = 0, int nPowerTarget = 0, int nDisableDialogBoxes = 0);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint Download([MarshalAs(UnmanagedType.LPStr)] string sDownloadFile,
                                                                int nDeviceErase = 0,
                                                                int nDisableDialogBoxes = 0,
                                                                int nDownloadScratchPadSFLE = 0,
                                                                int nBankSelect = -1,
                                                                int nLockFlash = 0,
                                                                bool bPersistFlash = true);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint DownloadCS([MarshalAs(UnmanagedType.LPStr)] string sDownloadFile,
                                                                int nDeviceErase = 0,
                                                                int nDisableDialogBoxes = 0,
                                                                int nDownloadScratchPadSFLE = 0,
                                                                int nBankSelect = -1,
                                                                int nLockFlash = 0,
                                                                bool bPersistFlash = true,
                                                                ushort* usChecksum = null);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint DownloadTxt([MarshalAs(UnmanagedType.LPStr)] string sDownloadFile,
                                                                uint wStartAddress,
                                                                int nDeviceErase = 0,
                                                                int nDisableDialogBoxes = 0,
                                                                int nLockFlash = 0,
                                                                bool bPersistFlash = true,
                                                                ushort* usChecksum = null);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VerifyFlashDownload([MarshalAs(UnmanagedType.LPStr)] string sDownloadFile, int nDisableDialogBoxes = 0, int nBankSelect = -1);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint Disconnect(int nComPort = 1);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint DisconnectUSB();

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint GetRAMMemory(byte[] ptrMem, uint wStartAddress, uint nLength);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint SetRAMMemory(byte[] ptrMem, uint wStartAddress, uint nLength);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint GetXRAMMemory(Byte[] ptrMem, uint wStartAddress, uint nLength);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint SetXRAMMemory(byte[] ptrMem, uint wStartAddress, uint nLength);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint GetCodeMemory(byte[] ptrMem, uint wStartAddress, uint nLength);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint SetCodeMemory(byte[] ptrMem, uint wStartAddress, uint nLength,
                                                           bool bDisableDialogs = false);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool Connected();

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint SetTargetGo();

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint SetTargetHalt();

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint FLASHErase(
                                       int nComPort = 1,
                                       int nDisableDialogBoxes = 0,
                                       int nECprotocol = 0);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint FLASHEraseUSB(
                                  [MarshalAs(UnmanagedType.LPStr)] string sSerialNum,
                                  int nDisableDialogBoxes = 0,
                                  int nECprotocol = 0);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint SetJTAGDeviceAndConnect(int nComPort = 1, int nDisableDialogBoxes = 0,
                                                        byte DevicesBeforeTarget = 0, byte DevicesAfterTarget = 0,
                                                        ushort IRBitsBeforeTarget = 0, ushort IRBitsAfterTarget = 0);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint SetJTAGDeviceAndConnectUSB([MarshalAs(UnmanagedType.LPStr)] string sSerialNum, int nPowerTarget = 0, int nDisableDialogBoxes = 0,
                                                          byte DevicesBeforeTarget = 0, byte DevicesAfterTarget = 0,
                                                          ushort IRBitsBeforeTarget = 0, ushort IRBitsAfterTarget = 0);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint ISupportBanking(int* nSupportedBanks);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint GetSAFirmwareVersion();

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern char* GetDLLVersion();

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint USBDebugDevices(ref uint dwDevices);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint GetUSBDeviceSN(uint dwDeviceNum, ref IntPtr psSerialNum);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint GetUSBDLLVersion(ref IntPtr pVersionString);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint GetUSBFirmwareVersion();

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint GetDeviceName(ref IntPtr psDeviceName);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint SetScratchPadMemory(byte[] ptrMem,
                                                uint wStartAddress,
                                                uint nLength,
                                                int nDisableDialogs = 0);

        [DllImport("SiUtil.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint GetScratchPadMemory(byte[] ptrMem,
                                                uint wStartAddress,
                                                uint nLength);
    }
}
