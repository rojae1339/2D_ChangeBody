using System;
using System.Collections.Generic;

namespace Managers
{
    public class PartsDataManager
    {
        private const string _weaponJsonPath = "Assets/JsonDatas/Weapon.json";
        private const string _bodyJsonPath = "Assets/JsonDatas/Body.json";

        private Dictionary<string, List<Dictionary<string, object>>> _weaponData = new();
        private Dictionary<string, List<Dictionary<string, object>>> _bodyData = new();
        
        public Dictionary<string, List<Dictionary<string, object>>> WeaponData { get => _weaponData; }
        public Dictionary<string, List<Dictionary<string, object>>> BodyData { get => _bodyData; }

        public void Init()
        {
            _weaponData = JsonUtil.LoadPartsWithPath(_weaponJsonPath);
            _bodyData = JsonUtil.LoadPartsWithPath(_bodyJsonPath);
        }

        #region 파츠 이름으로 데이터 리스트 가져오기
        
        private List<Dictionary<string, object>> GetWeaponInfoListByWeaponName(string name)
        {
            if (_weaponData.TryGetValue(name, out var list))
            {
                return list;
            }

            return new List<Dictionary<string, object>>();
        }
        
        private List<Dictionary<string, object>> GetBodyInfoListByBodyName(string name)
        {
            if (_bodyData.TryGetValue(name, out var list))
            {
                return list;
            }

            return new List<Dictionary<string, object>>();
        }

        #endregion

        #region 파츠 이름으로 DTO 리스트 가져오기

        public List<WeaponDTO> GetWeaponDTOListByName(string name)
        {
            var dictList = GetWeaponInfoListByWeaponName(name);
            var result = new List<WeaponDTO>();
            foreach (var dict in dictList)
            {
                result.Add(new WeaponDTO() {
                    WeaponName = name,
                    Tier = (TierType)Enum.Parse(typeof(TierType), dict[WeaponKeys.Tier].ToString()),
                    AttackDamage = Convert.ToDouble(dict[WeaponKeys.AttackDamage]),
                    AttackSpeed = Convert.ToDouble(dict[WeaponKeys.AttackSpeed]),
                    BulletSpeed = dict.TryGetValue(WeaponKeys.BulletSpeed, out var value) ? Convert.ToDouble(value) : (double?)null,
                    HandType = (WeaponHandType)Enum.Parse(typeof(WeaponHandType), dict[WeaponKeys.WeaponHandType].ToString()),
                    WeaponAttackType = (AttackType)Enum.Parse(typeof(AttackType), dict[WeaponKeys.AttackType].ToString()),
                    MaxBulletCount = dict.TryGetValue(WeaponKeys.MaxBulletCount, out var value1) ? Convert.ToInt32(value1) : (int?)null,
                    ReloadSpeed = dict.TryGetValue(WeaponKeys.ReloadSpeed, out var value2) ? Convert.ToDouble(value2) : (double?)null,
                    PartDropProbability = Convert.ToDouble(dict[WeaponKeys.PartDropProbability]),
                    UpgradeFleshCount = Convert.ToInt32(dict[WeaponKeys.UpgradeFleshCount])
                });
            }
            return result;
        }


        public List<BodyDTO> GetBodyDTOListByName(string name)
        {
            var dictList = GetBodyInfoListByBodyName(name);
            var result = new List<BodyDTO>();
            foreach (var dict in dictList)
            {
                result.Add(new BodyDTO {
                    BodyName = name,
                    Tier = (TierType)Enum.Parse(typeof(TierType), dict[BodyKeys.Tier].ToString()),
                    Hp = Convert.ToInt32(dict[BodyKeys.Hp]),
                    Shield = Convert.ToInt32(dict[BodyKeys.Shield]),
                    IsDead = Convert.ToBoolean(dict[BodyKeys.IsDead]),
                    IsDmgHalf = Convert.ToBoolean(dict[BodyKeys.IsDmgHalf]),
                    UpgradeFleshCount = Convert.ToInt32(dict[BodyKeys.UpgradeFleshCount]),
                    PartDropProbability = Convert.ToSingle(dict[BodyKeys.PartDropProbability])
                });
            }
            return result;
        }

        #endregion

    }
}