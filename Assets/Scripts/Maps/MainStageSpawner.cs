using UnityEngine;

namespace Maps
{
    public class MainStageSpawner : IStagePartSpawner
    {
        private Vector2 startPosition = new Vector2(-16.0f, -34.0f); //-16.0, -34.0
        private Vector2 endPosition = new Vector2(16.0f, -34.0f);

        public void SpawnParts(DifficultyType? difficulty)
        {
            //todo instantiate logic with addressable
            Debug.Log(startPosition);
        }
    }
}
