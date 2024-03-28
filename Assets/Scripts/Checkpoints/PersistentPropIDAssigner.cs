using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Checkpoints
{
    internal class PersistentPropIDAssigner
    {
        public static void ReassignChestIDs() {
            Chest[] chests = GameObject.FindObjectsOfType<Chest>();
            Array.Sort(chests, (chest1, chest2) => chest1.chestNumber.CompareTo(chest2.chestNumber));
            for (int i = 0; i < chests.Length; i++)
            {
                chests[i].chestNumber = i + 1;
            }
        }
    }
}
