using UCS.Logic;

namespace UCS.Packets.Messages.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Extensions.List;

    using Packets;

    // Packet 24111
    class AvatarNameChangeOkMessage : Message
    {
        private string m_vAvatarName;

        private int m_vServerCommandType;

        public AvatarNameChangeOkMessage(Device _Device)
            : base(_Device)
        {
            this.SetMessageType(24111);

            this.m_vServerCommandType = 0x03;
            this.m_vAvatarName = "JJBreaker";
        }

        public string GetAvatarName()
        {
            return this.m_vAvatarName;
        }

        public void SetAvatarName(string name)
        {
            this.m_vAvatarName = name;
        }

        public override void Encode()
        {
            this.Writer.AddInt(this.m_vServerCommandType);
            this.Writer.AddString(this.m_vAvatarName);
            this.Writer.AddInt(1);
            this.Writer.AddInt(-1);
        }
    }
}
