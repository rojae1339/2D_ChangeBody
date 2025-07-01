using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonUtil
{
    private static readonly string _weaponJsonPath = "Assets/JsonDatas/Weapon.json";
    private static readonly string _bodyJsonPath = "Assets/JsonDatas/Body.json";
    private static readonly string _legJsonPath = "Assets/JsonDatas/Leg.json";

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
        return LoadPartsWithPath(_weaponJsonPath);
    }
    
    public static Dictionary<string, List<Dictionary<string, object>>> LoadBodyRawJson()
    {
        return LoadPartsWithPath(_bodyJsonPath);
    }
    
    public static Dictionary<string, List<Dictionary<string, object>>> LoadLegRawJson()
    {
        return LoadPartsWithPath(_legJsonPath);
    }

    private static Dictionary<string, List<Dictionary<string, object>>> LoadPartsWithPath(string partsPath)
    {
        try
        {
            if (!File.Exists(partsPath))
            {
                Debug.LogError($"Weapon.json 파일을 찾을 수 없습니다. 경로: {partsPath}");
                return null;
            }

            string json = File.ReadAllText(partsPath);

            var fromJsonToDictionaryList = FromJsonToDictionaryList(json);
            
            return fromJsonToDictionaryList;
        }
        catch (Exception e)
        {
            Debug.LogError("JSON 파싱 중 예외 발생: " + e);
            return null;
        }
    }


    
}