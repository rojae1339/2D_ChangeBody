using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Maps
{
    public class MainStageSpawner : IStagePartSpawner
    {
        private Vector2 startPosition = new Vector2(-16.0f, -34.0f); //-16.0, -34.0
        private Vector2 endPosition = new Vector2(16.0f, -34.0f);


        public void SpawnParts(DifficultyType? difficulty)
        {
            // 1. 파츠 이름 목록 예시 (실제 구현에서는 PartsDataManager에서 동적으로 가져올 수 있음)
            var weaponNames = Managers.Managers.PartsData.WeaponData.Keys;
            var bodyNames = Managers.Managers.PartsData.BodyDate.Keys;

            // 2. 무기 파츠 Instantiate 및 데이터 바인딩
            foreach (var weaponName in weaponNames)
            {
                var weaponDTOs = Managers.Managers.PartsData.GetWeaponDTOListByName(weaponName);
                foreach (var dto in weaponDTOs)
                {
                    GameObject go = Managers.Managers.Addressable.Instantiate(weaponName);
                    var script = go.GetComponent(weaponName); // 예: ShortSword, NoWeapon 등
                    if (script != null)
                    {
                        //script.Init(dto);
                    }
                    // 위치 지정 등 추가 작업
                }
            }

            // 3. 바디 파츠 Instantiate 및 데이터 바인딩
            foreach (var bodyName in bodyNames)
            {
                var bodyDTOs = Managers.Managers.PartsData.GetBodyDTOListByName(bodyName);
                foreach (var dto in bodyDTOs)
                {
                    GameObject go = Managers.Managers.Addressable.Instantiate(bodyName);
                    var script = go.GetComponent(bodyName); // 예: FatBody, NormalBody 등
                    if (script != null)
                    {
                        //script.Init(dto);
                    }
                    // 위치 지정 등 추가 작업
                }
            }
        }
    }
}