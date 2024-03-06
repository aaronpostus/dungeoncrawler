using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DunGen
{
    public class DunGenData : MonoBehaviour, ISaveData
    {
        public void LoadData(GameData data)
        {
            if (data.seed != 0)
            {
                this.GetComponent<RuntimeDungeon>().Generator.Seed = data.seed;
                this.GetComponent<RuntimeDungeon>().Generator.ShouldRandomizeSeed = false;
            }
        }

        public void SaveData(GameData data)
        {
            //Debug.Log(this.GetComponent<RuntimeDungeon>().Generator.Seed);
            //Debug.Log(this.GetComponent<RuntimeDungeon>().Generator.ChosenSeed);
            data.seed = this.GetComponent<RuntimeDungeon>().Generator.ChosenSeed;
        }
    }
}
