using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Uncapped_Players_And_Parties
{
    [BepInPlugin("022aff51-6d76-45e7-b7ba-ef9e6f53cc53", "Lilly's Uncapped Players", "4.0.0")]
    public class Bepin : BaseUnityPlugin
    {
        Uncapper core;
        private void Awake()
        {
            if (Uncapper.Instance != null)
                return;
            GameObject g = GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));
            g.hideFlags = UnityEngine.HideFlags.HideAndDontSave;
            core = g.AddComponent<Uncapper>();
            core.Log = logger;
            var harmony = new HarmonyLib.Harmony("Lilly's Uncapped Players");
            harmony.PatchAll();
        }
        public bool logger(string mesg)
        {
            Logger.LogInfo($"{mesg}");
            return true;
        }
    }
}
