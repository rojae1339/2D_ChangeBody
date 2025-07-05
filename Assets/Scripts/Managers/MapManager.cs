using System.Collections.Generic;
using Maps;
using UnityEngine;

namespace Managers
{
    public class MapManager
    {
        

        private class StageData
        {
            public StageType StageType;
            public DifficultyType? Difficulty; // Nullable

            public StageData(StageType stageType, DifficultyType? difficulty = null)
            {
                StageType = stageType;
                Difficulty = difficulty;
            }
        }
    
        private readonly Dictionary<StageType, IStagePartSpawner> _spawnerMap = new()
        {
            { StageType.Main, new MainStageSpawner() },
            { StageType.Stage1, new Stage1Spawner() },
            //todo 스테이지 마다 추가
        };


        #region 맵 변경시 오브젝트 생성 메서드

        private StageData _currentStage;

        public void ChangeStage(StageType stageType, DifficultyType? difficulty = null)
        {
            _currentStage = new StageData(stageType, difficulty);
            CreateParts(_currentStage);
        }
    
        private void CreateParts(StageData data)
        {
            if (_spawnerMap.TryGetValue(data.StageType, out var spawner))
            {
                spawner.SpawnParts(data.Difficulty);
            }
        }

        #endregion
    }
}
