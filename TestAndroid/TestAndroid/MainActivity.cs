using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using isflib;
using System;
using System.Timers;

namespace TestAndroid
{
    [Activity(Label = "Test TCP/IP client", MainLauncher = false)]
    public class MainActivity : Activity, View.IOnClickListener
    {
        // varriables
        private TextView tv_Info;
        private EditText et_IP;
        private EditText et_Port;
        private EditText et_SendToServer;
        private Button btn_Connect;
        private Button btn_Send;
        private SimpleTcpClient client;
        private bool bConnected = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Set textView for Info from Server and make it scrollable
            tv_Info = FindViewById<TextView>(Resource.Id.textViewInfo);
            tv_Info.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();

            // Set EditTexts
            et_IP = FindViewById<EditText>(Resource.Id.editTextIP);
            et_Port = FindViewById<EditText>(Resource.Id.editTextPort);
            et_SendToServer = FindViewById<EditText>(Resource.Id.editTextSendServer);

            // Set Buttons with OnClickListeners
            btn_Send = FindViewById<Button>(Resource.Id.btnSendToServer);
            btn_Send.SetOnClickListener(this);
            btn_Connect = FindViewById<Button>(Resource.Id.btnConnect);
            btn_Connect.SetOnClickListener(this);
            Button b = FindViewById<Button>(Resource.Id.btnClear);
            b.SetOnClickListener(this);
            b = FindViewById<Button>(Resource.Id.btnClearFromServer);
            b.SetOnClickListener(this);

            // Creating Server
            client = new SimpleTcpClient();

            // Setting GUI State
            SetGUIstate(bConnected);

            // Setting Defaults for system
            et_Port.Text = Convert.ToString(12345);
            et_IP.Text = "192.168.1.105";

            // Setting Timer, Event & Starting timer
            Timer timer = new Timer();
            timer.Elapsed += Timer_10ms_Tick;
            timer.Start();
        }

        public void OnClick(View v)
        {
            string err;
            if (v.Id == Resource.Id.btnClearFromServer)
            {
                et_SendToServer.Text = "";                
            }
            else if (v.Id == Resource.Id.btnClear)
            {
                tv_Info.Text = "";
            }
            else if (v.Id == btn_Connect.Id)
            {
                bool bError = true;
                string s = "Unknown error";

                if (!bConnected)
                {

                    // try to connect
                    if (client.Connect(et_IP.Text, Convert.ToInt16(et_Port.Text), out err))
                    {
                        s = "Successfuly connected to the server";
                        bConnected = true;
                        bError = false;
                    }
                    else
                    {
                        s = "Failed to connect to server: " + err;
                    }
                }
                else
                {
                    // always disconnect on error (the client object will be disconnected anyways after calling Disconnect()
                    bConnected = false;
                    // try to disconnect
                    if (client.Disconnect(out err))
                    {
                        s = "Successfuly disconnected from the server";
                        bError = false;
                    }
                    else
                    {
                        s = "Failed to disconnect from server: " + err;
                    }
                }

                if (bError)
                {
                    Toast.MakeText(this, s, ToastLength.Short).Show();
                }

                tv_Info.Text += "DEBUG > " + s + "\r\n";

                btn_Connect.Text = bConnected ? "DISCONNECT" : "CONNECT";

                SetGUIstate(bConnected);
            }
            else if (v.Id == btn_Send.Id)
            {
                err = "";

                if (bConnected)
                {
                    if (!client.Send(et_SendToServer.Text + GetEOL(), out err))
                    {
                        Toast.MakeText(this, "Error while sending to server: " + err, ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Not connected to the server!", ToastLength.Long).Show();
                }
            }
        }

        void SetGUIstate(bool bConnected)
        {
            if (bConnected)
            {
                btn_Connect.Text = "DISCONNECT";
                btn_Send.Enabled = true;
                et_IP.Enabled = false;
                et_Port.Enabled = false;
            }
            else
            {
                btn_Connect.Text = "CONNECT";
                btn_Send.Enabled = false;
                et_IP.Enabled = true;
                et_Port.Enabled = true;
            }
        }

        string GetEOL()
        {
            string s = "";

            // Getting radioGroup to return the current checked RadioButton
            RadioGroup radioGroup = FindViewById<RadioGroup>(Resource.Id.radioGroup);
            RadioButton rb_Checked = FindViewById<RadioButton>(radioGroup.CheckedRadioButtonId);

            if (rb_Checked.Id == Resource.Id.rB_NoEOL)
            { }
            else if (rb_Checked.Id == Resource.Id.rB_CR_LF)
            { s = "\r\n"; }
            else if (rb_Checked.Id == Resource.Id.rB_CR)
            { s = "\r"; }
            else if (rb_Checked.Id == Resource.Id.rB_LF)
            { s = "\n"; }

            return s;
        }

        uint nTimer_10ms = 0;
        private void Timer_10ms_Tick(object sender, EventArgs e)
        {
            string err = "";
            if (bConnected)
            {
                if (client.Receive(out string s, out err))
                {
                    if (s != "")
                    {
                        tv_Info.Text += s;
                    }
                }
                else
                {
                    // error handling, client socket seems to be disconnected
                    client.Disconnect(out err);
                    tv_Info.Text += err + "\r\n";
                }
            }

            // Check every 1 second (1000 ms) if the socket is still connected
            if (nTimer_10ms % 100 == 0)
            {
                if (bConnected)
                {
                    if (!client.IsStillConnected())
                    {
                        bConnected = false;
                        SetGUIstate(bConnected);
                        tv_Info.Text += "DEBUG > " + "Disconnected from server.\r\n";
                    }
                }
            }
            nTimer_10ms++;
        }
    }
}

