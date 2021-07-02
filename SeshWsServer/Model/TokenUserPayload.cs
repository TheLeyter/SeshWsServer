using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeshWsServer.Model
{
    class TokenUserPayload
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public long nbf { get; set; }
        public long exp { get; set; }
        public string iss { get; set; }
        public string aud { get; set; }
        public TokenUserPayload(){}
    }
}
