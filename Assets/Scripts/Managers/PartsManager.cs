using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PartsManager
{
    public WeaponDatabase WeaponDB { get; set; }
    
    public void LoadWeaponData()
    {
        var rawData = JsonUtil.LoadWeaponRawJson();

        if (rawData == null)
        {
            Debug.LogError("무기 데이터를 불러오지 못했습니다.");
            return;
        }

        WeaponDB = new WeaponDatabase();
        List<NoWeaponDTO> noWeaponDtos = ParseNoWeapons(rawData);
        
        WeaponDB.Initialize(noWeaponDtos); // 여기에 필요한 무기들 계속 추가
        
        FieldInfo[] fields = noWeaponDtos.GetType().GetFields();
        
        foreach (FieldInfo field in fields)
        {
            object value = field.GetValue(noWeaponDtos);
            Console.WriteLine($"{field.Name} = {value}");
        }
    }

    private List<NoWeaponDTO> ParseNoWeapons(Dictionary<string, List<Dictionary<string, object>>> rawData)
    {
        var list = new List<NoWeaponDTO>();

        if (!rawData.TryGetValue("noWeapon", out var weapons)) return list;

        foreach (var item in weapons)
        {
            list.Add(new NoWeaponDTO(
                Enum.Parse<TierType>(item["tier"].ToString()),
                Convert.ToSingle(item["attackDamage"]),
                Convert.ToSingle(item["attackSpeed"]),
                Convert.ToInt32(item["upgradeFleshCount"]),
                Convert.ToSingle(item["partDropProbability"])
            ));
        }

        return list;
    }
}
