using UCS.Logic;

namespace UCS.Packets.Messages.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using UCS.Core;
    using UCS.Extensions.List;
    using UCS.Helpers;
    using UCS.Packets;

    // Packet 20161
    class ShutdownStartedMessage : Message
    {
        private int m_vCode;

        public ShutdownStartedMessage(Device client)
            : base(client)
        {
            this.SetMessageType(20161);
        }

        public override void Encode()
        {
            this.Writer.AddInt(this.m_vCode);
        }

        public void SetCode(int code)
        {
            this.m_vCode = code;
        }
    }
}
