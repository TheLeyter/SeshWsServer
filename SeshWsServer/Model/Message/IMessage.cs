using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeshWsServer.Model.Message
{
    interface IMessage
    {
        public MessageType type { get; set; }

        public string message { get; set; }
    }
}
