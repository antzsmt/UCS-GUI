namespace UCS.Core
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Linq;

    using Logic;

    using UCS.Database;

    #endregion

    internal class DatabaseManager
    {
        private static string m_vConnectionString;

        public DatabaseManager()
        {
            m_vConnectionString = "ucsdbEntities";
        }

        public void CreateAccount(Level l)
        {
            try
            {
                Debug.Write("Saving new account to database (player id: " + l.GetPlayerAvatar().GetId() + ")");
                using (var db = new ucsdbEntities(m_vConnectionString))
                {
                    db.Players.Add(new player {
                                                  PlayerId = l.GetPlayerAvatar().GetId(),
                                                  AccountStatus = l.GetAccountStatus(),
                                                  AccountPrivileges = l.GetAccountPrivileges(),
                                                  LastUpdateTime = l.GetTime(),
                                                  Avatar = l.GetPlayerAvatar().SaveToJSON(),
                                                  GameObjects = l.SaveToJSON()
                                              });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.Write("An exception occured during CreateAccount processing:" + Debug.FlattenException(ex));
            }
        }

        public void CreateAlliance(Clan a)
        {
            try
            {
                Debug.Write("Saving new Alliance to database (alliance id: " + a.GetAllianceId() + ")");
                using (var db = new ucsdbEntities(m_vConnectionString))
                {
                    db.Clans.Add(new clan {
                                              ClanId = a.GetAllianceId(),
                                              LastUpdateTime = DateTime.Now,
                                              Data = a.SaveToJSON()
                                          });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.Write("An exception occured during CreateAlliance processing:" + Debug.FlattenException(ex));
            }
        }

        public Level GetAccount(long playerId)
        {
            Level account = null;
            try
            {
                using (var db = new ucsdbEntities(m_vConnectionString))
                {
                    var p = db.Players.Find(playerId);

                    // Check if player exists
                    if (p != null)
                    {
                        account = new Level();
                        account.SetAccountStatus(p.AccountStatus);
                        account.SetAccountPrivileges(p.AccountPrivileges);
                        account.SetTime(p.LastUpdateTime);
                        account.GetPlayerAvatar().LoadFromJSON(p.Avatar);
                        account.LoadFromJSON(p.GameObjects);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("An exception occured during GetAccount processing:" + Debug.FlattenException(ex));
            }

            return account;
        }

        public Clan GetAlliance(long allianceId)
        {
            Clan alliance = null;
            try
            {
                using (var db = new ucsdbEntities(m_vConnectionString))
                {
                    var p = db.Clans.Find(allianceId);

                    // Check if player exists
                    if (p != null)
                    {
                        alliance = new Clan();
                        alliance.LoadFromJSON(p.Data);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("An exception occured during GetAlliance processing:" + Debug.FlattenException(ex));
            }

            return alliance;
        }

        public long GetMaxAllianceId()
        {
            long max = 0;
            using (var db = new ucsdbEntities(m_vConnectionString))
            {
                max = (from alliance in db.Clans select (long?)alliance.ClanId ?? 0).DefaultIfEmpty().Max();
            }

            return max;
        }

        public long GetMaxPlayerId()
        {
            long max = 0;
            using (var db = new ucsdbEntities(m_vConnectionString))
            {
                max = (from ep in db.Players select (long?)ep.PlayerId ?? 0).DefaultIfEmpty().Max();
            }

            return max;
        }

        public static void Save(List<Level> avatars)
        {
            Debug.Write("Starting saving players from memory to database at " + DateTime.Now.ToString());
            try
            {
                using (var context = new ucsdbEntities(m_vConnectionString))
                {
                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;
                    int transactionCount = 0;
                    foreach (Level pl in avatars)
                    {
                        lock (pl)
                        {
                            var p = context.Players.Find(pl.GetPlayerAvatar().GetId());
                            if (p != null)
                            {
                                p.LastUpdateTime = pl.GetTime();
                                p.AccountStatus = pl.GetAccountStatus();
                                p.AccountPrivileges = pl.GetAccountPrivileges();
                                p.Avatar = pl.GetPlayerAvatar().SaveToJSON();
                                p.GameObjects = pl.SaveToJSON();
                                context.Entry(p).State = EntityState.Modified;
                            }
                            else
                            {
                                context.Players.Add(new player {
                                                                   PlayerId = pl.GetPlayerAvatar().GetId(),
                                                                   AccountStatus = pl.GetAccountStatus(),
                                                                   AccountPrivileges = pl.GetAccountPrivileges(),
                                                                   LastUpdateTime = pl.GetTime(),
                                                                   Avatar = pl.GetPlayerAvatar().SaveToJSON(),
                                                                   GameObjects = pl.SaveToJSON()
                                                               });
                            }
                        }

                        transactionCount++;
                        if (transactionCount >= 500)
                        {
                            context.SaveChanges();
                            transactionCount = 0;
                        }
                    }

                    context.SaveChanges();
                }

                Debug.Write("Finished saving players from memory to database at " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Debug.Write("An exception occured during Save processing for avatars:" + Debug.FlattenException(ex));
            }
        }

        public void Save(List<Clan> alliances)
        {
            Debug.Write("Starting saving alliances from memory to database at " + DateTime.Now.ToString());
            try
            {
                using (var context = new ucsdbEntities(m_vConnectionString))
                {
                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;
                    int transactionCount = 0;
                    foreach (Clan alliance in alliances)
                    {
                        lock (alliance)
                        {
                            var c = context.Clans.Find((int)alliance.GetAllianceId());
                            if (c != null)
                            {
                                c.LastUpdateTime = DateTime.Now;
                                c.Data = alliance.SaveToJSON();
                                context.Entry(c).State = EntityState.Modified;
                            }
                            else
                            {
                                context.Clans.Add(new clan {
                                                               ClanId = alliance.GetAllianceId(),
                                                               LastUpdateTime = DateTime.Now,
                                                               Data = alliance.SaveToJSON()
                                                           });
                            }
                        }

                        transactionCount++;
                        if (transactionCount >= 500)
                        {
                            context.SaveChanges();
                            transactionCount = 0;
                        }
                    }

                    context.SaveChanges();
                }

                Debug.Write("Finished saving alliances from memory to database at " + DateTime.Now);
            }
            catch (Exception ex)
            {
                Debug.Write("An exception occured during Save processing for alliances:" + Debug.FlattenException(ex));
            }
        }
    }
}