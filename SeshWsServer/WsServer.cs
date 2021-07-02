using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SeshWsServer.Model;
using Newtonsoft.Json;
using SeshWsServer.Model.Message;
using SeshWsServer.Model;
using SeshWsServer.Service;

namespace SeshWsServer
{
    class WsServer
    {
        private List<UserSocket> users;

        private readonly int port;
        private readonly IPAddress ip;

        private WebSocketServer server;

        private jwt _jwt;
        private Database _db;

        private Action<IWebSocketConnection> _config;

        public WsServer(IPAddress ip, int port, jwt jwt, Database db)
        {
            this.ip = ip;
            this.port = port;

            server = new WebSocketServer($"ws://{ip}:{port}");

            _jwt = jwt;

            _db = db;

            _config = config;

            users = new List<UserSocket>();
        }

        public void start()
        {
            server.Start(_config);

            consoleInfo.stdInfo($"Serevr started on -> {server.Location}");
        }

        public void stop()
        {
            users.ForEach(x => x.socket.Close());

            server.Dispose();

            consoleInfo.stdInfo($"Server {server.Location} stopped!");
        }

        private void config(IWebSocketConnection socket)
        {
            void onOpen(){
                string token = socket.ConnectionInfo.Params["token"];

                TokenUserPayload tokenPayload;

                switch (_jwt.validateTokenToObj(token,out tokenPayload))
                {
                    case JwtDecodeResult.ok:
                        if(tokenPayload == null)
                        {
                            consoleInfo.onError($"Invalide user token => {socket.ConnectionInfo.Path}");
                            socket.Close(1004);
                            break;
                        }
                        var user = _db.users.Find(tokenPayload.UserId);
                        if (user == null)
                        {
                            socket.Close(1004);
                            break;
                        }
                        users.Add(new UserSocket(user, socket));
                        consoleInfo.onOpen($"Client {user} connected!!!");
                        socket.Send("OK");

                        break;
                    case JwtDecodeResult.expError:
                        socket.Close(1004);
                        consoleInfo.onError($"Client {socket.ConnectionInfo.ClientIpAddress}:{socket.ConnectionInfo.ClientPort} unauthorize");
                        break;
                    case JwtDecodeResult.signError:
                        socket.Close(1005);
                        consoleInfo.onError($"Client {socket.ConnectionInfo.ClientIpAddress}:{socket.ConnectionInfo.ClientPort} unauthorize");
                        break;
                    default:
                        break;
                }
            }

            void onMessage(string msg)
            {
                consoleInfo.onMessage(msg);

                Message message = JsonConvert.DeserializeObject<Message>(msg);

                IMessage msgg;

                switch (message.type)
                {
                    case MessageType.stringMsg:
                        msgg = JsonConvert.DeserializeObject<StrMessage>(message.payload);
                        break;
                    case MessageType.photoMsg:
                        break;
                    case MessageType.photoMsgWithDesc:
                        break;
                    case MessageType.videoMsg:
                        break;
                    case MessageType.videMsgWithDesc:
                        break;
                    case MessageType.audioMsg:
                        break;
                    default:
                        break;
                }


            }

            void onClose()
            {
                consoleInfo.onClose($"Client {socket.ConnectionInfo.ClientIpAddress}:{socket.ConnectionInfo.ClientPort} disconnected");
            }

            void onError(Exception ex)
            {
                consoleInfo.onError(ex.Message);
            }

            socket.OnMessage = onMessage;
            socket.OnOpen = onOpen;
            socket.OnClose = onClose;
            socket.OnError = onError;
        }
    }
}
