using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace c2_printf
{
    public class C2Debugger
    {
        private string serialString;

        /// <summary>
        /// Create a new C2 Debugger with the given serial number
        /// </summary>
        /// <param name="serialString">The serial number to use, or string.Emtpy for the default adapter.</param>
        public C2Debugger(string serialString = "")
        {
            this.serialString = serialString;
        }

        /// <summary>
        /// Get the serial number of this adapter
        /// </summary>
        public string SerialNumber => serialString;

        /// <summary>
        /// Connect to the debugger and attach to the target
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            var res = NativeMethods.ConnectUSB(serialString, 1);
            if (res != 0)
                Debug.WriteLine("Error with HaltTarget(): " + res);
            return NativeMethods.Connected();
        }

        /// <summary>
        /// Detach from the target and disconnect from the debugger.
        /// </summary>
        /// <returns></returns>
        public bool Disconnect()
        {
            var res = NativeMethods.DisconnectUSB();
            if (res != 0)
                Debug.WriteLine("Error with HaltTarget(): " + res);
            return res == 0;
        }

        /// <summary>
        /// Get the connected device name
        /// </summary>
        public string DeviceName
        {
            get
            {
                IntPtr devName = new IntPtr();
                var res = NativeMethods.GetDeviceName(ref devName);
                if (res != 0)
                    Debug.WriteLine("Error with HaltTarget(): " + res);
                return Marshal.PtrToStringAnsi(devName);
            }
        }

        /// <summary>
        /// Get contents of RAM at the specified address. Target must be halted for this to succeed.
        /// </summary>
        /// <param name="address">The address to read from</param>
        /// <param name="count">the number of bytes to read</param>
        /// <returns>The contents of the RAM</returns>
        public byte[] GetRAMMemory(int address, int count)
        {
            var retVal = new byte[count];
            var res = NativeMethods.GetRAMMemory(retVal, (uint)address, (uint)count);
            if (res != 0)
                Debug.WriteLine("Error with HaltTarget(): " + res);
            return retVal;
        }

        /// <summary>
        /// Get contents of XRAM at the specified address. Target must be halted for this to succeed.
        /// </summary>
        /// <param name="address">The address to read from</param>
        /// <param name="count">the number of bytes to read</param>
        /// <returns>The contents of the XRAM</returns>
        public byte[] GetXRAMMemory(int address, int count)
        {
            var retVal = new byte[count];
            var res = NativeMethods.GetXRAMMemory(retVal, (uint)address, (uint)count);
            if (res != 0)
                Debug.WriteLine("Error with GetXRAMMemory(): " + res);
            return retVal;
        }

        /// <summary>
        /// Get contents of code memory at the specified address. Target must be halted for this to succeed.
        /// </summary>
        /// <param name="address">The address to read from</param>
        /// <param name="count">the number of bytes to read</param>
        /// <returns>The contents of the code</returns>
        public byte[] GetCodeMemory(int address, int count)
        {
            var retVal = new byte[count];
            var res = NativeMethods.GetCodeMemory(retVal, (uint)address, (uint)count);
            if (res != 0)
                Debug.WriteLine("Error with HaltTarget(): " + res);
            return retVal;
        }

        /// <summary>
        /// Sets RAM contents to the given array
        /// </summary>
        /// <param name="address">the address to write the data to</param>
        /// <param name="data">the data to write</param>
        public void SetRAMMemory(int address, byte[] data)
        {
            var res = NativeMethods.SetRAMMemory(data, (uint)address, (uint)data.Length);
            if (res != 0)
                Debug.WriteLine("Error with HaltTarget(): " + res);
        }

        /// <summary>
        /// Sets XRAM contents to the given array
        /// </summary>
        /// <param name="address">the address to write the data to</param>
        /// <param name="data">the data to write</param>
        public void SetXRAMMemory(int address, byte[] data)
        {
            var res = NativeMethods.SetXRAMMemory(data, (uint)address, (uint)data.Length);
            if (res != 0)
                Debug.WriteLine("Error with SetXRAMMemory(): " + res);
        }

        /// <summary>
        /// Sets code memory contents to the given array
        /// </summary>
        /// <param name="address">the address to write the data to</param>
        /// <param name="data">the data to write</param>
        public void SetCodeMemory(int address, byte[] data)
        {
            var res = NativeMethods.SetCodeMemory(data, (uint)address, (uint)data.Length);
            if (res != 0)
                Debug.WriteLine("Error with HaltTarget(): " + res);
        }

        /// <summary>
        /// Gets a friendly name (the serial number) of this device
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return SerialNumber;
        }

        /// <summary>
        /// Halt the target
        /// </summary>
        public void HaltTarget()
        {
            var res = NativeMethods.SetTargetHalt();
            if (res != 0)
                Debug.WriteLine("Error with HaltTarget(): " + res);
        }

        /// <summary>
        /// Run the target
        /// </summary>
        public void RunTarget()
        {
            var res = NativeMethods.SetTargetGo();
            if (res != 0)
                Debug.WriteLine("Error with HaltTarget(): " + res);
        }
    }
}
