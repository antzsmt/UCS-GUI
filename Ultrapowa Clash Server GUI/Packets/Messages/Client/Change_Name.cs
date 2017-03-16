#region Usings



#endregion

namespace UCS.Packets.Messages.Client
{
    #region Usings

    using UCS.Core;
    using UCS.Core.Network;
    using UCS.Extensions.Binary;
    using UCS.Logic;
    using UCS.Packets.Commands.Server;
    using UCS.Packets.Messages.Server;

    #endregion

    internal class Change_Name : Message
    {
        public const ushort PacketID = 10212;

        public bool Bool = true;

        public string Name = string.Empty;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Change_Name" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Change_Name(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Set_Name.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Name = this.Reader.ReadString();
            this.Bool = this.Reader.ReadBoolean();
            Debug.Write("Name: " + this.Name);
            Debug.Write("Bool: " + this.Bool);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            this.Client.GetLevel().GetPlayerAvatar().SetName(this.Name);
            var p = new AvatarNameChangeOkMessage(this.Client);
            p.SetAvatarName(this.Client.GetLevel().GetPlayerAvatar().GetAvatarName());
            p.Send();

            new Server_Commands(this.Client) { _Command = new Name_Change_Callback(this.Client).Handle() }.Send();
        }
    }
}