namespace UCS.Packets
{
    #region Usings

    using System;
    using System.Collections.Generic;

    using UCS.Packets.Messages.Client;

    #endregion

    internal class MessageFactory : IDisposable
    {
        /// <summary>
        ///     A list of all available <see cref="Message" /> s.
        /// </summary>
        internal static Dictionary<int, Type> m_vMessages;

        /// <summary>
        ///     Initialize a new instance of the <see cref="MessageFactory" />
        ///     class. When initialized, it will fill the list with all available
        ///     <see cref="Message" /> s.
        /// </summary>
        static MessageFactory()
        {
            m_vMessages = new Dictionary<int, Type>();

            m_vMessages.Add(10100, typeof(Pre_Authentification));
            m_vMessages.Add(10101, typeof(Authentification));
            m_vMessages.Add(10107, typeof(Client_Capabilities));
            m_vMessages.Add(10108, typeof(Keep_Alive));
            m_vMessages.Add(10113, typeof(Get_Device_Token));

            // m_vMessages.Add(10151, typeof(Google_Billing_Request));
            m_vMessages.Add(10212, typeof(Change_Name));

            // m_vMessages.Add(10159, typeof(Kunlub_Billing_Request));
            // m_vMessages.Add(10511, typeof(Apple_Billing_Request));
            m_vMessages.Add(10513, typeof(Social_Connection));

            m_vMessages.Add(10905, typeof(Check_Inbox));

            // m_vMessages.Add(12903, typeof(Request_Sector_State));
            m_vMessages.Add(12904, typeof(Sector_Command));

            // m_vMessages.Add(12906, typeof(Request_Spectator_State));
            m_vMessages.Add(12951, typeof(Battle_Event));

            // m_vMessages.Add(12952, typeof(Battle_Event_Spectate));
            m_vMessages.Add(14102, typeof(Execute_Commands));
            m_vMessages.Add(14101, typeof(Go_Home));
            m_vMessages.Add(14104, typeof(Battle_NPC));
            m_vMessages.Add(14107, typeof(Cancel_Battle));
            m_vMessages.Add(14111, typeof(Cancel_Challenge));
            m_vMessages.Add(14113, typeof(Ask_Profile));

            m_vMessages.Add(14201, typeof(Facebook_Connect));
            m_vMessages.Add(14123, typeof(Cancel_Challenge));

            // m_vMessages.Add(14212, typeof(Bind_Gamecenter_Account));
            m_vMessages.Add(14262, typeof(Bind_Google_Account));

            m_vMessages.Add(14301, typeof(Create_Clan));
            m_vMessages.Add(14302, typeof(Ask_Clan));
            m_vMessages.Add(14303, typeof(Joinable_Clans));
            m_vMessages.Add(14305, typeof(Join_Clan));
            m_vMessages.Add(14307, typeof(Kick_Clan_Member));
            m_vMessages.Add(14324, typeof(Search_Clans));
            m_vMessages.Add(14315, typeof(Chat_Alliance));
            m_vMessages.Add(14402, typeof(Royale_TV));
            m_vMessages.Add(14403, typeof(Top_Global_Players));
            m_vMessages.Add(14405, typeof(Avatar_Stream));
            m_vMessages.Add(14406, typeof(Battle_Stream));
            m_vMessages.Add(14423, typeof(Cancel_Friendly_Battle));

            // m_vMessages.Add(14600, typeof(Name_Check_Request));
            m_vMessages.Add(16000, typeof(Device_Link_Code));
            m_vMessages.Add(16002, typeof(Link_Device));
            m_vMessages.Add(16103, typeof(Joinable_Tournaments));
            m_vMessages.Add(16113, typeof(Search_Tournaments));
        }

        //public static object Read(Device c, BinaryReader br, int packetType)
        //{
        //    if (m_vMessages.ContainsKey(packetType))
        //    {
        //        return Activator.CreateInstance(m_vMessages[packetType], c, br);
        //    }
        //    else
        //    {
        //        Console.Write("[");
        //        Console.ForegroundColor = ConsoleColor.Magenta;
        //        Console.Write("U");
        //        Console.ResetColor();
        //        Debug.Write("] " + packetType.ToString() + " Mensaje no registado (ignorado)");
        //        return null;
        //    }
        //}

        /// <summary>
        ///     <see cref="Dispose" /> the class.
        /// </summary>
        public void Dispose()
        {
            m_vMessages.Clear();
        }
    }
}