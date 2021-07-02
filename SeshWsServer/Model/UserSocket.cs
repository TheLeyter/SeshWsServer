using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeshWsServer
{
    class UserSocket
    {
        public long id { get; }
        public User User { get; private set; }
        public IWebSocketConnection socket { get; private set; }

        public UserSocket( User user , IWebSocketConnection socket)
        {
            this.id = user.id;
            this.User = user;
            this.socket = socket;
        }

        public UserSocket(IWebSocketConnection socket)
        {
            this.socket = socket;
        }
    }
}
