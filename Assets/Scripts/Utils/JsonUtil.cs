using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonUtil
{
    private static readonly string _weaponJsonPath = "Assets/JsonData/Weapon.json";
    private static readonly string _bodyJsonPath = "Assets/JsonData/Body.json";
    private static readonly string _legJsonPath = "Assets/JsonData/Leg.json";

    public enum WeaponCategory
    {
        NoWeapon,
        ShortSword,
        LongSword,
        Bow,
        Pistol,
        Rifle
    }

    private static Dictionary<string, List<Dictionary<string, object>>> FromJsonToDictionaryList(string json)
    {
        return JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, object>>>>(json);
    }

    public static Dictionary<string, List<Dictionary<string, object>>> LoadWeaponRawJson()
    {
        if (!File.Exists(_weaponJsonPath))
        {
            Debug.LogError($"Weapon.json 파일을 찾을 수 없습니다. 경로: {_weaponJsonPath}");
            return null;
        }

        string json = File.ReadAllText(_weaponJsonPath);
        return FromJsonToDictionaryList(json);
    }

    
}