using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Maps
{
    public class MainStageSpawner : IStagePartSpawner
    {
        private Vector2 leftTopPos = new Vector2(-16.0f, -28.0f);
        private Vector2 leftPos = new Vector2(-16.0f, -34.0f);
        private Vector2 rightPos = new Vector2(16.0f, -34.0f);
        private Vector2 rightTopPos = new Vector2(16.0f, -28.0f);
        
        private Vector2 currentSpawnPos;
        private SpawnDirection currentDirection;
        
        private enum SpawnDirection
        {
            Down,   // 아래로
            Right,  // 오른쪽으로
            Up      // 위로
        }
        
        public void SpawnParts(DifficultyType? difficulty)
        {
            // 스폰 위치 초기화 (왼쪽 상단부터 시작)
            currentSpawnPos = leftTopPos;
            currentDirection = SpawnDirection.Down;
            
            // 1. 파츠 이름 목록 예시 (실제 구현에서는 PartsDataManager에서 동적으로 가져올 수 있음)
            var weaponNames = Managers.Managers.PartsData.WeaponData.Keys;
            var bodyNames = Managers.Managers.PartsData.BodyData.Keys;
            
            // 2. 무기 파츠 Instantiate 및 데이터 바인딩
            foreach (var weaponName in weaponNames)
            {
                var weaponDTOs = Managers.Managers.PartsData.GetWeaponDTOListByName(weaponName);
                
                string upperName = weaponName[0].ToString().ToUpper() + weaponName[1..];
                foreach (var dto in weaponDTOs)
                {
                    GameObject go = Managers.Managers.Addressable.Instantiate(upperName);
                    var script = go.GetComponent<BaseWeapon>(); // 예: ShortSword, NoWeapon 등
                    if (script != null)
                    {
                        script.Init(dto);
                    }
                    //todo 오브젝트마다 붙일 것들 여기서 붙이기
                    go.transform.position = currentSpawnPos;
                    go.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    
                    TierType tier = script.Tier;
                    PartsTierFeatureSO so = Managers.Managers.Addressable.Load<PartsTierFeatureSO>($"{tier.ToString()}SO");
                        GameObject game = Managers.Managers.Addressable.Instantiate($"{tier.ToString()}Particle.prefab", go.transform);
                    var ps = game.GetComponent<ParticleSystem>();
                    if (ps != null)
                    {
                        ps.Play();
                    }
                    game.transform.position += new Vector3(0.3f, 0, 0);
                    script.so = so;
                    
                    // 다음 스폰 위치 계산
                    UpdateSpawnPosition();
                }
            }
            
            // 3. 바디 파츠 Instantiate 및 데이터 바인딩
            foreach (var bodyName in bodyNames)
            {
                var bodyDTOs = Managers.Managers.PartsData.GetBodyDTOListByName(bodyName);
                
                string upperName = bodyName[0].ToString().ToUpper() + bodyName[1..];
                foreach (var dto in bodyDTOs)
                {
                    GameObject go = Managers.Managers.Addressable.Instantiate(upperName);
                    var script = go.GetComponent<BaseBody>(); // 예: FatBody, NormalBody 등
                    if (script != null)
                    {
                        script.Init(dto);
                    }
                    //todo 오브젝트마다 붙일 것들 여기서 붙이기
                    go.transform.position = currentSpawnPos;
                    go.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    
                    TierType tier = script.Tier;
                    PartsTierFeatureSO so = Managers.Managers.Addressable.Load<PartsTierFeatureSO>($"{tier.ToString()}SO");
                        GameObject game = Managers.Managers.Addressable.Instantiate($"{tier.ToString()}Particle.prefab", go.transform);
                        var ps = game.GetComponent<ParticleSystem>();
                        if (ps != null)
                        {
                            ps.Play();
                        }

                        game.transform.position += new Vector3(0.3f, 0, 0);
                    script.so = so;
                    
                    // 다음 스폰 위치 계산
                    UpdateSpawnPosition();
                }
            }
        }
        
        private void UpdateSpawnPosition()
        {
            switch (currentDirection)
            {
                case SpawnDirection.Down:
                    currentSpawnPos += new Vector2(0, -1); // 아래로 이동
                    // 왼쪽 하단에 도달하면 오른쪽으로 방향 전환
                    if (currentSpawnPos.y <= leftPos.y)
                    {
                        currentDirection = SpawnDirection.Right;
                        currentSpawnPos = new Vector2(leftPos.x + 1, leftPos.y); // 오른쪽으로 한 칸 이동
                    }
                    break;
                    
                case SpawnDirection.Right:
                    currentSpawnPos += new Vector2(1, 0); // 오른쪽으로 이동
                    // 오른쪽 하단에 도달하면 위쪽으로 방향 전환
                    if (currentSpawnPos.x >= rightPos.x)
                    {
                        currentDirection = SpawnDirection.Up;
                        currentSpawnPos = new Vector2(rightPos.x, rightPos.y + 1); // 위로 한 칸 이동
                    }
                    break;
                    
                case SpawnDirection.Up:
                    currentSpawnPos += new Vector2(0, 1); // 위로 이동
                    // 오른쪽 상단에 도달하면 다시 아래로 방향 전환 (또는 스폰 종료)
                    if (currentSpawnPos.y >= rightTopPos.y)
                    {
                        // 필요에 따라 다시 시작하거나 다른 로직 구현
                        currentDirection = SpawnDirection.Down;
                        currentSpawnPos = new Vector2(rightTopPos.x + 1, rightTopPos.y); // 오른쪽으로 확장
                    }
                    break;
            }
        }
    }
}