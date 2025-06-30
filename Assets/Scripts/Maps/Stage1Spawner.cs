using Managers;
using UnityEngine;

namespace Maps
{
    public class Stage1Spawner : IStagePartSpawner
    {
        public void SpawnParts(DifficultyType? difficulty)
        {
            if (!difficulty.HasValue)
            {
                Debug.LogError("Stage1에는 난이도가 필요합니다.");
                return;
            }

            switch (difficulty)
            {
                //todo
                case DifficultyType.Easy:
                    Debug.Log("MainStage - Easy 난이도: 쉬운 파츠 배치");
                    // Instantiate(easy prefab);
                    break;
                case DifficultyType.Normal:
                    Debug.Log("MainStage - Normal 난이도: 일반 파츠 배치");
                    break;
                case DifficultyType.Hard:
                    Debug.Log("MainStage - Hard 난이도: 어려운 파츠 배치");
                    break;
            }
        }
    }
}