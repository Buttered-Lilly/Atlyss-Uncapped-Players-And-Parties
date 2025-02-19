using System;
using System.IO;
using HarmonyLib;
using MelonLoader;
using MelonLoader.TinyJSON;
using MelonLoader.Utils;
using Mirror;
using UnityEngine;
using static MelonLoader.MelonLogger;

namespace Uncapped_Players_And_Parties
{
    public class Uncapper : MonoBehaviour
    {
        public static Uncapper Instance;
        PartyObjectBehavior partyPre = null;
        public Func<string, bool> Log;

        public void Start()
        {
            Instance = this;
            uncapParties();
        }

        void uncapParties()
        {
            try
            {
                if (partyPre == null)
                {
                    foreach (PartyObjectBehavior go in Resources.FindObjectsOfTypeAll(typeof(PartyObjectBehavior)) as PartyObjectBehavior[])
                    {
                        go.Network_maxPartyLimit = 1000;
                        go._maxPartyLimit = 1000;
                    }
                }
            }
            catch (Exception e)
            {
                Log(e.ToString());
            }
        }

        [HarmonyPatch(typeof(LobbyListManager), "Awake")]
        public static class unCapSlider
        {
            private static void Postfix(ref LobbyListManager __instance)
            {
                try
                {
                    __instance._lobbyMaxConnectionSlider.maxValue = 250f;
                    __instance._lobbyMaxConnectionSlider.minValue = 2f;
                }
                catch (Exception obj)
                {
                    Uncapper.Instance.Log(obj.ToString());
                }
            }
        }

        [HarmonyPatch(typeof(ProfileDataManager), "Load_HostSettingsData")]
        public static class Inject
        {
            private static bool Prefix(ref string ____dataPath)
            {
                Uncapper.Inject.Load_HostSettingsData(____dataPath);
                return false;
            }

            private static void Load_HostSettingsData(string dataPath)
            {
                try
                {
                    if (File.Exists(dataPath + "/hostSettings.json"))
                    {
                        string json = File.ReadAllText(dataPath + "/hostSettings.json");
                        ProfileDataManager._current._hostSettingsProfile = JsonUtility.FromJson<ServerHostSettings_Profile>(json);
                        if (string.IsNullOrEmpty(ProfileDataManager._current._hostSettingsProfile._serverName) || ProfileDataManager._current._hostSettingsProfile._serverName.Contains("<") || ProfileDataManager._current._hostSettingsProfile._serverName.Contains(">"))
                        {
                            ProfileDataManager._current._hostSettingsProfile._serverName = "Atlyss Server";
                        }
                        if (ProfileDataManager._current._hostSettingsProfile._maxAllowedConnections < 2)
                        {
                            ProfileDataManager._current._hostSettingsProfile._maxAllowedConnections = 2;
                        }
                        else if (ProfileDataManager._current._hostSettingsProfile._maxAllowedConnections > 250)
                        {
                            ProfileDataManager._current._hostSettingsProfile._maxAllowedConnections = 250;
                        }
                        MainMenuManager._current.Load_HostSettings();
                        AtlyssNetworkManager._current._serverName = ProfileDataManager._current._hostSettingsProfile._serverName;
                        AtlyssNetworkManager._current._serverPassword = ProfileDataManager._current._hostSettingsProfile._serverPassword;
                        AtlyssNetworkManager._current._serverMotd = ProfileDataManager._current._hostSettingsProfile._serverMotd;
                        AtlyssNetworkManager._current.maxConnections = ProfileDataManager._current._hostSettingsProfile._maxAllowedConnections;
                    }
                }
                catch (Exception ex)
                {
                    Uncapper.Instance.Log(ex.ToString());
                }
            }
        }
    }
}
