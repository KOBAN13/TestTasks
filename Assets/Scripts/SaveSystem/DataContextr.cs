using System;
using Zenject;

namespace SaveSystem
{
    public class DataContext
    {
        protected GameData GameDataCurrent;
        
        public float Score
        {
            get => GameDataCurrent.score;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();
                GameDataCurrent.score = value;
            }
        }

        [Inject]
        private void Construct(GameData gameData) => GameDataCurrent = gameData;
    }
}