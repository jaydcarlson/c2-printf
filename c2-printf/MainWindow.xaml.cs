using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace c2_printf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        private C2Debugger selectedDebugger;
        private bool isConnected = false;
        System.Timers.Timer timer;
        CancellationTokenSource cts;
        CancellationToken token;
        Task consoleTask;

        /// <summary>
        /// Main entrypoint
        /// </summary>
        public MainWindow()
        {
            this.DataContext = this; // for databinding
            InitializeComponent();

            // kick off a timer to check for new C2 debuggers periodically
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed; ;
            timer.Start();

            // Set the default debugger if none is selected
            if (SelectedDebugger == null && Scanner.Debuggers.Count > 0)
            {
                SelectedDebugger = Scanner.Debuggers[0];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The scanner used to detect attached C2 debuggers
        /// </summary>
        public C2Scanner Scanner { get; set; } = new C2Scanner();

        /// <summary>
        /// The selected debugger
        /// </summary>
        public C2Debugger SelectedDebugger
        {
            get { return selectedDebugger; }
            set {
                selectedDebugger = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedDebugger)));
            }
        }

        /// <summary>
        /// Gets or sets whether the debugger is connected or not.
        /// </summary>
        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                isConnected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsConnected)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsNotConnected)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConnectButtonText)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConnectedText)));
            }
        }

        /// <summary>
        /// The text for the connect/disconnect button
        /// </summary>
        public string ConnectButtonText => IsConnected ? "Disconnect" : "Connect";

        public bool IsNotConnected => !IsConnected;

        /// <summary>
        /// The read address; in hex
        /// </summary>
        public string ReadAddress { get; set; } = "0x00";

        /// <summary>
        /// Number of bytes to read. It is assumed the last byte is the "flag" byte
        /// </summary>
        public int ReadLength { get; set; } = 255;

        public string ConnectedText => IsConnected ? $"Connected to {SelectedDebugger.DeviceName}" : "Not Connected";

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // strangely, if we scan for devices while the debugger is attached to the target, it disconnects.
            // So only scan when we're not connected.
            if (IsConnected) return;

            Scanner.Scan();
        }

        /// <summary>
        /// Responds when the user clicks the Connect/Disconnect button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectDisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedDebugger == null)
            {
                IsConnected = false;
                return;
            }

            if(!IsConnected)
            {
                IsConnected = SelectedDebugger.Connect();
                if (IsConnected)
                    StartConsole();
            }
            else
            {
                cts.Cancel(); // causes the task to finish up and disconnect
            }
        }

        public void StartConsole()
        {
            SelectedDebugger.RunTarget();
            cts = new CancellationTokenSource();
            token = cts.Token;

            consoleTask = Task.Factory.StartNew(() =>
            {
            int readAddress = int.Parse(ReadAddress.Replace("0x", ""), System.Globalization.NumberStyles.AllowHexSpecifier);
                while (SelectedDebugger != null)
                {
                    if (token.IsCancellationRequested)
                    {
                        SelectedDebugger?.Disconnect();
                        IsConnected = false;
                        return;
                    }

                    // we have to halt the target to read RAM
                    SelectedDebugger?.HaltTarget();
                    var data = SelectedDebugger?.GetXRAMMemory(readAddress, ReadLength);
                    if (data[ReadLength-1] == 0x01)
                    {
                        // we have a new message!
                        var text = Encoding.ASCII.GetString(data.Take(ReadLength - 1).ToArray()).TrimEnd((Char)0);
                        Application.Current.Dispatcher.Invoke(new Action(() => { ConsoleTextBox.AppendText(text); ConsoleTextBox.ScrollToEnd(); }));
                        SelectedDebugger?.SetXRAMMemory(readAddress + ReadLength - 1, new byte[1] { 0x00 });
                    }
                    // resume target
                    SelectedDebugger?.RunTarget();
                }
            }, token);
        }
    }
}
