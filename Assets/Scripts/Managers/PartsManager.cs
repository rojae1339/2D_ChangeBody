using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PartsManager
    {
        private WeaponDatabase WeaponDB { get; set; }

        public void Init() { LoadWeaponData(); }

        private void LoadWeaponData()
        {
            var rawData = JsonUtil.LoadWeaponRawJson();

            if (rawData == null)
            {
                Debug.LogError("무기 데이터를 불러오지 못했습니다.");
                return;
            }

            WeaponDB = new WeaponDatabase();
            List<NoWeaponDTO> noWeaponDtos = ParseWeapon(rawData, WeaponNames.NoWeapon,
                item => new NoWeaponDTO(Enum.Parse<TierType>(item[WeaponKeys.Tier].ToString()),
                    Convert.ToSingle(item[WeaponKeys.AttackDamage]), Convert.ToSingle(item[WeaponKeys.AttackSpeed]),
                    Convert.ToInt32(item[WeaponKeys.UpgradeFleshCount]),
                    Convert.ToSingle(item[WeaponKeys.PartDropProbability])));

            List<ShortSwordDTO> shortSwordDtos = ParseWeapon(rawData, WeaponNames.ShortSword,
                item => new ShortSwordDTO(Enum.Parse<TierType>(item[WeaponKeys.Tier].ToString()),
                    Convert.ToSingle(item[WeaponKeys.AttackDamage]), Convert.ToSingle(item[WeaponKeys.AttackSpeed]),
                    Convert.ToInt32(item[WeaponKeys.UpgradeFleshCount]),
                    Convert.ToSingle(item[WeaponKeys.PartDropProbability]),
                    Enum.Parse<AttackType>(item[WeaponKeys.AttackType].ToString())));

            List<LongSwordDTO> longSwordDtos = ParseWeapon(rawData, WeaponNames.LongSword,
                item => new LongSwordDTO(Enum.Parse<TierType>(item[WeaponKeys.Tier].ToString()),
                    Convert.ToSingle(item[WeaponKeys.AttackDamage]), Convert.ToSingle(item[WeaponKeys.AttackSpeed]),
                    Convert.ToInt32(item[WeaponKeys.UpgradeFleshCount]),
                    Convert.ToSingle(item[WeaponKeys.PartDropProbability]),
                    Enum.Parse<AttackType>(item[WeaponKeys.AttackType].ToString())));

            List<BowDTO> bowDtos = ParseWeapon(rawData, WeaponNames.Bow,
                item => new BowDTO(Enum.Parse<TierType>(item[WeaponKeys.Tier].ToString()),
                    Convert.ToSingle(item[WeaponKeys.AttackDamage]), Convert.ToSingle(item[WeaponKeys.AttackSpeed]),
                    Convert.ToInt32(item[WeaponKeys.UpgradeFleshCount]),
                    Convert.ToSingle(item[WeaponKeys.PartDropProbability]),
                    Convert.ToSingle(item[WeaponKeys.BulletSpeed])));

            List<PistolDTO> pistolDtos = ParseWeapon(rawData, WeaponNames.Pistol,
                item => new PistolDTO(Enum.Parse<TierType>(item[WeaponKeys.Tier].ToString()),
                    Convert.ToSingle(item[WeaponKeys.AttackDamage]), Convert.ToSingle(item[WeaponKeys.AttackSpeed]),
                    Convert.ToInt32(item[WeaponKeys.UpgradeFleshCount]),
                    Convert.ToSingle(item[WeaponKeys.PartDropProbability]),
                    Convert.ToSingle(item[WeaponKeys.BulletSpeed]),
                    Enum.Parse<AttackType>(item[WeaponKeys.AttackType].ToString()),
                    Convert.ToSingle(item[WeaponKeys.ReloadSpeed])));

            List<RifleDTO> rifleDtos = ParseWeapon(rawData, WeaponNames.Rifle,
                item => new RifleDTO(Enum.Parse<TierType>(item[WeaponKeys.Tier].ToString()),
                    Convert.ToSingle(item[WeaponKeys.AttackDamage]), Convert.ToSingle(item[WeaponKeys.AttackSpeed]),
                    Convert.ToInt32(item[WeaponKeys.UpgradeFleshCount]),
                    Convert.ToSingle(item[WeaponKeys.PartDropProbability]),
                    Convert.ToSingle(item[WeaponKeys.BulletSpeed]), Convert.ToSingle(item[WeaponKeys.ReloadSpeed]),
                    Convert.ToInt32(item[WeaponKeys.MaxBulletCount])));

            WeaponDB.Initialize(noWeaponDtos, shortSwordDtos, longSwordDtos, bowDtos, pistolDtos, rifleDtos);
        }


        #region Parsing Weapon

        private List<T> ParseWeapon<T>(Dictionary<string, List<Dictionary<string, object>>> rawData,
                                       string key,
                                       Func<Dictionary<string, object>, T> callback)
        {
            var list = new List<T>();

            if (!rawData.TryGetValue(key, out var items)) return list;

            foreach (var item in items) { list.Add(callback(item)); }

            return list;
        }

        #endregion
    }
}