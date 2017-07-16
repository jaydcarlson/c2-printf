using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace c2_printf
{
    public class C2Scanner : INotifyPropertyChanged
    {
        private string usbDllVersion;

        /// <summary>
        /// Create a new C2 debugger scanner
        /// </summary>
        public C2Scanner()
        {
            Debuggers.CollectionChanged += Debuggers_CollectionChanged;
            Scan();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Collection of currently-attached debuggers
        /// </summary>
        public ObservableCollection<C2Debugger> Debuggers { get; } = new ObservableCollection<C2Debugger>();

        /// <summary>
        /// Get the USB DLL version
        /// </summary>
        public string UsbDllVersion
        {
            get
            {
                if (usbDllVersion == null)
                    usbDllVersion = GetUsbDllVersion();

                return usbDllVersion;
            }
        }

        private void Debuggers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Debuggers)));
        }

        /// <summary>
        /// Scan for devices (which will internally update the Debuggers collection)
        /// </summary>
        public void Scan()
        {
            uint numDevices = 0;
            NativeMethods.USBDebugDevices(ref numDevices);

            string[] newDeviceList = new string[numDevices];

            for (uint i = 0; i < numDevices; i++)
            {
                //StringBuilder serial = new StringBuilder(256);
                IntPtr serial = new IntPtr();
                NativeMethods.GetUSBDeviceSN(i, ref serial);
                newDeviceList[i] = Marshal.PtrToStringAnsi(serial);
                
            }

            // add any new devices
            foreach(var newDevice in newDeviceList.Where(p => !Debuggers.Any(p2 => p2.SerialNumber == p)))
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Debug.WriteLine($"Adding device {newDevice}");
                    Debuggers.Add(new C2Debugger(newDevice));
                }));
            }

            // remove any old devices
            foreach (var removedDevice in Debuggers.Where(p => !newDeviceList.Any(p2 => p2 == p.SerialNumber)).ToList())
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Debug.WriteLine($"Removing device {removedDevice}");
                    Debuggers.Remove(removedDevice);
                }));
            }
        }

        private string GetUsbDllVersion()
        {
            IntPtr version = new IntPtr();
            NativeMethods.GetUSBDLLVersion(ref version);
            return Marshal.PtrToStringAnsi(version);
        }

    }
}
