using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeshWsServer.Model.Message
{
    class Message<T> where T : Enum
    {
        public MessageType type { get; private set; }

        public long recipientId { get; private set; }

        public long chatId { get; private set; }

        public T payload { get; private set; }

        public Message(MessageType type , T payload, long recipientId, long chatId)
        {
            this.type = type;

            this.payload = payload;

            this.chatId = chatId;

            this.recipientId = recipientId;
        }
        public Message(){}
    }

    class Message
    {
        public MessageType type { get; private set; }

        public long recipientId { get; private set; }

        public long senderId { get; private set; }

        public long chatId { get; private set; }

        public string payload { get; private set; }

        public Message(MessageType type, string payload, long recipientId, long senderId, long chatId)
        {
            this.type = type;

            this.payload = payload;

            this.chatId = chatId;

            this.recipientId = recipientId;

            this.senderId = senderId;
        }
        public Message() { }
    }
}
