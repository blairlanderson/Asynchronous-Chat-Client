using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{
    //TODO RENAME TO CLASS NAME  - 
    public delegate void GetMessageHandler(GetMessageEventArgs msg);

    /// <summary>
    /// A custom EventArgs object that contains message data from an event
    /// </summary>
    public class GetMessageEventArgs : EventArgs
    {
        private string msg;
        /// <summary>
        /// return message information for use
        /// </summary>
        public string Msg
        {
            get 
            { 
                return msg; 
            }
        }
        /// <summary>
        /// Set message to data received from event
        /// </summary>
        /// <param name="msgIn">Message info received from event.</param>
        public GetMessageEventArgs(string msgIn)
        {
            msg = msgIn;
        }
    }
        
}
