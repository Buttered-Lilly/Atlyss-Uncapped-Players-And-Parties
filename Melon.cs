using MelonLoader;
using System;
using UnityEngine;

[assembly: MelonInfo(typeof(Uncapped_Players_And_Parties.MelonLoad), "Lilly's Uncapped Players", "4.0.0", "Lilly", null)]
[assembly: MelonGame("KisSoft", "ATLYSS")]
[assembly: MelonOptionalDependencies("BepInEx")]

namespace Uncapped_Players_And_Parties
{
    public class MelonLoad : MelonMod
    {
        Uncapper core;
        public override void OnInitializeMelon()
        {
            if (Uncapper.Instance != null)
                return;
            GameObject g = GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));
            g.hideFlags = UnityEngine.HideFlags.HideAndDontSave;
            core = g.AddComponent<Uncapper>();
            core.Logger = logger;
        }

        public bool logger(string msg)
        {
            MelonLogger.Msg(msg);
            return true;
        }
    }
}
