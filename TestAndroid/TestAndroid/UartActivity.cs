using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware.Usb;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Hoho.Android.UsbSerial.Driver;
using Java.IO;
using System.Threading;


namespace TestAndroid
{
    [Activity(Label = "UartActivity", MainLauncher = true)]
    public class UartActivity : Activity, View.IOnClickListener
    {
        private TextView tv_Info;
        private EditText et_SendToServer;
        private Button btn_Connect;
        private Button btn_Send;
        private Button btn_Disconnect;
        private Button btn_clear;
        private UsbManager manager;
        private UsbDeviceConnection connection;
        private IUsbSerialDriver driver;
        private IUsbSerialPort port;

        public void OnClick(View v)
        {
            if (v.Id == btn_Connect.Id)
            {


                manager = (UsbManager)GetSystemService(UsbService);
                IList<IUsbSerialDriver> availableDrivers = UsbSerialProber.DefaultProber.FindAllDrivers(manager);
                if (availableDrivers.Count == 0)
                {
                    tv_Info.Append("No devices found");
                }
                else
                {
                    // Open a connection to the first available driver.

                    PendingIntent mPermissionIntent = PendingIntent.GetBroadcast(this, 0, Intent.SetAction("ACTION_USB_PERMISSION"), 0);
                    driver = availableDrivers[0];
                    manager.RequestPermission(driver.Device, mPermissionIntent);

                    connection = manager.OpenDevice(driver.Device);
                    if (connection == null)
                    {
                        tv_Info.Append("You probably need to call UsbManager.requestPermission(driver.getDevice(), ..)");
                    }
                    else
                    {
                        port = driver.Ports[0];
                        tv_Info.Append("Connected to USB");

                        port.Open(connection);
                        port.SetParameters(9600, 8, StopBits.One, Parity.None);
                        byte[] bytes = new byte[1024];
                        port.Read(bytes, 1000);

                    }
                }
                ThreadPool.QueueUserWorkItem(delegate (object state) {

                    while (connection != null)
                    {

                        try
                        {
                            byte[] readBuffer = new byte[1024];
                            int message = port.Read(readBuffer, readBuffer.Length);
                            string input = Encoding.ASCII.GetString(readBuffer, 0, message);
                            if (input != "")
                            {
                                RunOnUiThread(() => {
                                    tv_Info.Text = input;
                                });
                            }
                        }
                        catch (TimeoutException e)
                        {
                            //                Console.WriteLine(e.ToString());
                        }
                    }


                }, null);
            }
            else if (v.Id == btn_Send.Id)
            {
                try
                {
                    //port.SetParameters(115200, 8, StopBits.One, Parity.None);

                    byte[] buffer = Encoding.ASCII.GetBytes(et_SendToServer.Text);
                    int numBytesRead = port.Write(buffer, 1024);
                }
                catch (IOException e)
                {
                    tv_Info.Append(e.ToString());
                }
            }
            else if (v.Id == btn_Disconnect.Id)
            {
                port.Close();
            }
            else if (v.Id == btn_clear.Id)
            {
                tv_Info.Text = null;
            }
        }

        public void ReadPort()
        {
            while (connection != null)
            {

                try
                {
                    byte[] readBuffer = new byte[1024];
                    int message = port.Read(readBuffer, readBuffer.Length);
                    string inout = Encoding.ASCII.GetString(readBuffer, 0, message);
                    tv_Info.Text = inout;

                }
                catch (TimeoutException e)
                {
                    //                Console.WriteLine(e.ToString());
                }
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            // Sets
            tv_Info = FindViewById<TextView>(Resource.Id.textViewInfo);
            tv_Info.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            et_SendToServer = FindViewById<EditText>(Resource.Id.editTextSendServer);
            btn_Send = FindViewById<Button>(Resource.Id.btnSendToServer);
            btn_Send.SetOnClickListener(this);
            btn_Connect = FindViewById<Button>(Resource.Id.btnConnect);
            btn_Connect.SetOnClickListener(this);
            btn_Disconnect = FindViewById<Button>(Resource.Id.btnClear);
            btn_Disconnect.SetOnClickListener(this);
            btn_clear = FindViewById<Button>(Resource.Id.btnClearFromServer);
            btn_clear.SetOnClickListener(this);
        }
    }
}