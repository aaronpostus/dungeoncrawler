using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Checkpoints
{
    internal class CurrentLevel : ISaveData
    {
        private int _level;
        public CurrentLevel(int levelNum) {
            _level = levelNum;
        }

        public void LoadData(GameData data)
        {
            _level = data.currentLevel;
        }

        public void SaveData(GameData data)
        {
            data.currentLevel = _level;
        }
    }
}
