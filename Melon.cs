using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uncapped_Players_And_Parties;
using UnityEngine;
using static MelonLoader.MelonLaunchOptions;

[assembly: MelonInfo(typeof(Uncapper), "Lilly's Uncapped Players", "2.0.0", "Lilly")]
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
            core.Log = logger;
        }

        bool logger(string msg)
        {
            MelonLogger.Msg(msg);
            return true;
        }
    }
}
