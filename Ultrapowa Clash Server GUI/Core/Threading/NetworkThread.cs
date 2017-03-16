namespace UCS.Core.Threading
{
    #region Usings

    using System;
    using System.Threading;

    using Network;
    using Network.Ancient;

    using Sys;

    using UCS.Network;
    using UCS.Packets;

    #endregion

    internal static class NetworkThread
    {
        // private static string Author = "ExPl0itR";

        // public static string Description = "Includes the Core (PacketManager etc.)";

        // public static string Name = "Network Thread";

        // public static string Version = "1.0.0";
        private static Command_Factory _CommandFactory = null;

        private static MessageFactory _MessageFactory = null;

        private static ObjectManager _ObjectManager = null;

        private static ResourcesManager _ResourcesManager = null;

        /// <summary>
        /// Variable holding the thread itself
        /// </summary>
        private static Thread T { get; set; }

        /// <summary>
        /// Starts the Thread
        /// </summary>
        public static void Start()
        {
            T = new Thread(() =>
            {
                Gateway g = new Gateway();
                //PacketManager ph = new PacketManager();
                _CommandFactory = new Command_Factory();
                _MessageFactory = new MessageFactory();
                _ResourcesManager = new ResourcesManager();
                MessageManager dp = new MessageManager();

                _ObjectManager = new ObjectManager();
                dp.Start();
                //ph.Start();

                // ApiManager api = new ApiManager();
                ControlTimer.StopPerformanceCounter();
                ControlTimer.Setup();
                ConfUCS.IsServerOnline = true;
                Console.WriteLine("Server started, let's play Clash Royale!");
            });
            T.Start();
        }

        /// <summary>
        /// Stops the Thread
        /// </summary>
        public static void Stop()
        {
            if (T.ThreadState == ThreadState.Running)
                T.Abort();
        }
    }
}