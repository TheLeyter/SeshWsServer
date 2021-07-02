using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SeshWsServer.Model.Message;

namespace SeshWsServer.Service
{
    static class MessageDeserializer
    {
        //public static Message deserialize(string msg)
        //{
        //    var obj = JsonConvert.DeserializeObject<Message>(msg);

        //    switch (obj.type)
        //    {
        //        case MessageType.stringMsg:
        //            JsonConvert.DeserializeObject<StrMessage>(Convert.ToString(obj.payload));
        //            break;
        //        case MessageType.photoMsg:
        //            break;
        //        case MessageType.photoMsgWithDesc:
        //            break;
        //        case MessageType.videoMsg:
        //            break;
        //        case MessageType.videMsgWithDesc:
        //            break;
        //        case MessageType.audioMsg:
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}
