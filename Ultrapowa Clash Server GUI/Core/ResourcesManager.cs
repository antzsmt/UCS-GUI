namespace UCS.Core
{
    #region Usings

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Threading;

    using UCS.Core.Database;
    using UCS.Files;
    using UCS.Helpers;
    using UCS.Logic;
    using UCS.Logic.Slots;

    #endregion

    internal class ResourcesManager : IDisposable
    {
        /// <summary>
        ///     The list of all in memory
        ///     <see cref="UCS.Core.ResourcesManager.Battles" /> s.
        /// </summary>
        public static Battles Battles;

        //private static ConcurrentDictionary<long, Device> m_vClients;
        public static Devices Devices;

        //public static Players Players = null;

        public static Random Random;

        /// <summary>
        ///     The list of all in memory
        ///     <see cref="UCS.Core.ResourcesManager.Clans" /> s.
        /// </summary>
        private static Clans Clans;

        /// <summary>
        ///     The current class logger, used to write logs using
        ///     <see cref="ResourcesManager" /> tag.
        /// </summary>
        private static DatabaseManager m_vDatabase;

        private static ConcurrentDictionary<long, Level> m_vInMemoryLevels;
        private Home _Home = null;
        private static List<Level> m_vOnlinePlayers;

        private static object m_vOnlinePlayersLock = new object();

        /// <summary>
        ///     The list of all in memory <see cref="UCS.Logic.Tournament" /> s.
        /// </summary>
        private static Tournaments Tournaments;

        private bool m_vTimerCanceled;

        private Timer TimerReference;

        /// <summary>
        ///     <para>
        ///         Initialize a new instance of the <see cref="ResourcesManager" />
        ///     </para>
        ///     <para>class.</para>
        /// </summary>
        public ResourcesManager()
        {
            m_vDatabase = new DatabaseManager();
            //m_vClients = new ConcurrentDictionary<long, Device>();
            m_vOnlinePlayers = new List<Level>();
            this._Home = new Home();
            Devices = new Devices();
            //Players = new Players();
            Clans = new Clans();
            Battles = new Battles();
            Tournaments = new Tournaments();
            Random = new Random();
            m_vInMemoryLevels = new ConcurrentDictionary<long, Level>();
            this.m_vTimerCanceled = false;
            TimerCallback TimerDelegate = this.ReleaseOrphans;
            Timer TimerItem = new Timer(TimerDelegate, null, 60000, 60000);
            this.TimerReference = TimerItem;
            Console.WriteLine("The Resources Manager class has been initialized.");
        }

        /// <summary>
        ///     <see cref="ResourcesManager.Add(UCS.Logic.Clan)" /> the specified
        ///     clan to the in memory <see cref="UCS.Logic.Clan" /> s list.
        /// </summary>
        /// <param name="_Clan">The clan.</param>
        public static void Add(Clan _Clan)
        {
            if (Clans.ContainsKey(_Clan.ClanID))
            {
                Clans[_Clan.ClanID] = _Clan;
            }
            else
            {
                Clans.Add(_Clan.ClanID, _Clan);
            }
        }

        public static void Add(Socket _Socket)
        {
            Device _Device = new Device(_Socket);

            if (Devices.ContainsKey(_Socket.Handle))
            {
                Devices[_Socket.Handle].Connection = _Socket;
            }
            else
            {
                Devices.Add(_Socket.Handle, _Device);
            }
        }

        public static void Add(Device _Device)
        {
            if (Devices.ContainsKey(_Device.Connection.Handle))
            {
                Devices[_Device.Connection.Handle] = _Device;
            }
            else
            {
                Devices.Add(_Device.Connection.Handle, _Device);
            }
        }

        /// <summary>
        ///     <see cref="Cache" /> the specified player to the redis database.
        /// </summary>
        /// <param name="_Player">The player.</param>
        public static void Cache(Level _Player)
        {
            var id = _Player.GetPlayerAvatar().GetId();
            Redis.Players.StringSet(id.ToString(), _Player.SaveToJSON());
        }

        /// <summary>
        ///     <see cref="Cache" /> the specified Clan to the redis database.
        /// </summary>
        /// <param name="_Clan">The Clan.</param>
        public static void Cache(Clan _Clan)
        {
            Redis.Clans.StringSet(_Clan.ClanID.ToString(), _Clan.SaveToJSON());
        }

        /// <summary>
        ///     <see cref="Cache" /> the specified Tournament to the redis database.
        /// </summary>
        /// <param name="_Tournament">The Tournament.</param>
        public static void Cache(Tournament _Tournament)
        {
            Redis.Tournaments.StringSet(_Tournament.TournamentID.ToString(), _Tournament.Serialize().ToString());
        }

        //public static void DropClient(long socketHandle)
        //{
        //    Device c;
        //    m_vClients.TryRemove(socketHandle, out c);
        //    if (c.GetLevel() != null)
        //        LogPlayerOut(c.GetLevel());
        //}

        //public static Device GetClient(long socketHandle)
        //{
        //    return Devices[socketHandle];
        //}

        public static List<Device> GetConnectedClients()
        {
            List<Device> clients = new List<Device>();
            clients.AddRange(Devices.Values);
            return clients;
        }

        public static List<Level> GetInMemoryLevels()
        {
            List<Level> levels = new List<Level>();
            lock (m_vOnlinePlayersLock)
            {
                levels.AddRange(m_vInMemoryLevels.Values);
            }
            return levels;
        }

        public static List<Level> GetOnlinePlayers()
        {
            List<Level> onlinePlayers = new List<Level>();
            lock (m_vOnlinePlayersLock)
            {
                onlinePlayers = m_vOnlinePlayers.ToList();
            }

            return onlinePlayers;
        }

        //public static Level GetPlayer(long id, bool persistent = false)
        //{
        //    Level result = GetInMemoryPlayer(id);
        //    if (result == null)
        //    {
        //        result = m_vDatabase.GetAccount(id);
        //        if (persistent)
        //            LoadLevel(result);
        //    }

        //    return result;
        //}

        //public static bool IsClientConnected(long socketHandle)
        //{
        //    return Devices.ContainsKey(socketHandle);
        //}

        public static bool IsPlayerOnline(Level l)
        {
            return m_vOnlinePlayers.Contains(l);
        }

        public static void LoadLevel(Level level)
        {
            var id = level.GetPlayerAvatar().GetId();
            if (!m_vInMemoryLevels.ContainsKey(id)) m_vInMemoryLevels.TryAdd(id, level);
        }

        public static void LogPlayerOut(Level level)
        {
            lock (m_vOnlinePlayersLock)
            {
                m_vOnlinePlayers.Remove(level);
            }
            m_vInMemoryLevels.TryRemove(level.GetPlayerAvatar().GetId());
        }

        public static void Remove(Socket _Socket)
        {
            if (Devices.ContainsKey(_Socket.Handle))
            {
                Level _Player = Devices[_Socket.Handle].GetLevel();
                Devices.Remove(_Socket.Handle);

                if (m_vInMemoryLevels.ContainsKey(_Player.GetPlayerAvatar().GetId()))
                {
                    m_vInMemoryLevels.TryRemove(_Player.GetPlayerAvatar().GetId());
                }
            }
        }

        public static void Remove(Device _Device)
        {
            if (Devices.ContainsKey(_Device.Connection.Handle))
            {
                Devices.Remove(_Device.Connection.Handle);

                if (m_vInMemoryLevels.ContainsKey(_Device.GetLevel().GetPlayerAvatar().GetId()))
                {
                    m_vInMemoryLevels.TryRemove(_Device.GetLevel().GetPlayerAvatar().GetId());
                }
            }
        }

        public static void RemoveClan(Clan _Clan)
        {
            if (Clans.ContainsKey(_Clan.ClanID))
            {
                Clans.Remove(_Clan.ClanID);
            }
        }

        /// <summary>
        ///     Forget the specified player from the redis database.
        /// </summary>
        /// <param name="_Player">The player.</param>
        public static void Uncache(Level _Player)
        {
            if (Redis.Players.KeyExists(_Player.GetPlayerAvatar().GetId().ToString()))
            {
                Redis.Players.KeyDelete(_Player.GetPlayerAvatar().GetId().ToString());
            }
        }

        /// <summary>
        ///     Forget the specified Clan from the redis database.
        /// </summary>
        /// <param name="_Clan">The Clan.</param>
        public static void Uncache(Clan _Clan)
        {
            if (Redis.Clans.KeyExists(_Clan.ClanID.ToString()))
            {
                Redis.Clans.KeyDelete(_Clan.ClanID.ToString());
            }
        }

        /// <summary>
        ///     Forget the specified Tournament from the redis database.
        /// </summary>
        /// <param name="_Tournament">The Tournament.</param>
        public static void Uncache(Tournament _Tournament)
        {
            if (Redis.Tournaments.KeyExists(_Tournament.TournamentID.ToString()))
            {
                Redis.Tournaments.KeyDelete(_Tournament.TournamentID.ToString());
            }
        }

        public void Dispose()
        {
            if (this.TimerReference != null)
            {
                this.TimerReference.Dispose();
                this.TimerReference = null;
                Clans.Clear();
                Devices.Clear();
                m_vInMemoryLevels.Clear();
                Debug.Write("The class has been reset / cleared.");

                // this.Logger.Info("The class has been reset / cleared.");
            }
        }

        private static Level GetInMemoryPlayer(long id)
        {
            Level result = null;
            lock (m_vOnlinePlayersLock)
            {
                if (m_vInMemoryLevels.ContainsKey(id))
                {
                    result = m_vInMemoryLevels[id];
                }
            }
            return result;
        }

        public static void LogPlayerIn(Level level, Device client)
        {
            level.SetClient(client);
            client.SetLevel(level);
            lock (m_vOnlinePlayersLock)
            {
                if (!m_vOnlinePlayers.Contains(level))
                {
                    m_vOnlinePlayers.Add(level);
                    LoadLevel(level);
                }
            }
        }

        public static Level GetPlayer(long id, bool persistent = false)
        {
            Level result = GetInMemoryPlayer(id);
            if (result == null)
            {
                result = m_vDatabase.GetAccount(id);
                if (persistent)
                {
                    LoadLevel(result);
                }
            }
            return result;
        }

        private void ReleaseOrphans(object state)
        {
            if (this.m_vTimerCanceled)
            {
                this.TimerReference.Dispose();
            }
        }
    }
}