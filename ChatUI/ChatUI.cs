using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//project based includes
using System.Threading;
using ChatLib;
using ChatLogger;
using Interfaces;

namespace ChatUI
{
    public partial class ChatUI : Form
    {
        //Form Properties
        Client client;
        ILoggingService logger;

//Thread      
        Thread msgThread;
        bool flag = true;
        delegate void SetConversationTextCallback(string msgIn);

        /// <summary>
        /// Basic constructor
        /// </summary>
        public ChatUI()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Cnostructor using Dependency Injection
        /// </summary>
        /// <param name="logger">ILoggingService compliant logger 
        /// class injected through constructor</param>
        public ChatUI(Client client) : this()
        {
            //this.logger = logger;
            this.client = client;
        }

        /// <summary>
        /// Shutdown client and thread, and exit program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShutDownTime();
            Application.Exit();
        }

        /// <summary>
        /// Start new tcpclient and begin message collection routine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //connect to chat server
                //client = new Client("127.0.0.1", 1337, logger);
                client.Start();

                //start the listening loop
                SetConversationText("New Conversation Started: ");
                client.GetMsg += new GetMessageHandler(GetMessage);
            }
            catch(System.Exception)
            { }
            finally
            {
                if (client.Flag == true)
                {
                    //enable relevant form controls
                    txtMessage.Enabled = true;
                    btnSend.Enabled = true;

//Thread            //Start second thread
                    msgThread = new Thread(new ThreadStart(client.Receive));
                    msgThread.Start();
                }
                else
                {
                    //TODO ERROR MSG
                }
            }
        }//end connect menu

        /// <summary>
        /// Disconnect from server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetConversationText("END TRANSMISSION: ");
            ShutDownTime();   
        }//end disconnect

        /// <summary>
        /// User initiates a message send to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            //mostly for debugging purposes
            string msg;
            msg = txtMessage.Text;
            
            //send along the message via the Client Obj
            client.Send(/*"Player: " + */msg /*+ "\n"*/);

            //display in convo box, and clear message box for new messge input
            txtConversation.Text += "Player: " + txtMessage.Text + Environment.NewLine;
            txtMessage.Text = "";

        }
        
        /// <summary>
        /// Event based message handling. Gets message from event and 
        /// invokes a delegate method, for cross thread updating of Form UI.
        /// </summary>
        /// <param name="msg">Message received from event.</param>
        public void GetMessage(GetMessageEventArgs msg) //TODO Rename: MessageReceived 
        {
            if(txtConversation.InvokeRequired)
            {
                MethodInvoker mi = new MethodInvoker(delegate{SetConversationText(msg.Msg);});
                txtConversation.Invoke(mi);
            }
            else
            {
                SetConversationText(msg.Msg);
            }
        }

        /// <summary>
        /// Method to update Forms Conversation textbox
        /// </summary>
        /// <param name="msgIn">Message to be displayed.</param>
        public void SetConversationText(string msgIn)
        {
            txtConversation.Text += msgIn + Environment.NewLine;
        }

        /// <summary>
        /// Prep client, thread, and ui to prevent exceptions during shutdowns
        /// </summary>
        public void ShutDownTime()
        {
            if(client != null)
            {
                if (client.Flag == true)
                {
                    try
                    {
                        //msgThread.Abort();
                        client.Flag = false;

                        //disonnect client from server
                        client.Close();
                        client = null;
                    }
                    catch (SystemException)
                    {

                    }
                    finally
                    {
                        //disable form controls
                        txtMessage.Enabled = false;
                        btnSend.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Accident that deleting breaks the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatUI_Load(object sender, EventArgs e)
        {

        }

        private void ChatUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShutDownTime();
        }

    }//end class
}//end namespace
