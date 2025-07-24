# 2D 파츠교체 with UI Interaction

## 🖥️ Project Introduction
파츠교환을 통해 플레이어의 능력치 변화등을 줄 수 있다.
가까운 파츠를 탐지 후, 인터랙션 키를 통해 현재 보유중인 파츠와 교체한다.
MVP 패턴을 통해 UI와 데이터 사이의 결합성을 낮춰주었다.
또한 데이터는 json파일에서 알맞는 데이터를 dto로 파싱한 후 일반 엔티티객체로 변환시켜줬다.

## 🕰️ Development Period
2025-06-22 ~ 2025-07-20
## ⚙️ Development Environment
Language : C#
Engine : Unity6
IDE : Rider

## What's important system?
- 파츠교환 시스템
    - 델리게이트와 MVP패턴을 사용한 이벤트 등록형 UI
- json to object (json -> DTO -> Object)
- Addressable
- 게임오브젝트 to png/jpg/jpeg 캡쳐 시스템

---
# Parts Change System
기본적으로 UI와 상호작용하는 파트이다

//todo

# Json to Object

유니티에서 json데이터를 다루기 위해 사용한 라이브러리 이다.

`JsonUtility`, `NewtonJson`.

[Weapon.json](https://github.com/rojae1339/2D_ChangeBody/blob/main/Assets/JsonDatas/Weapon.json)
[Body.json](https://github.com/rojae1339/2D_ChangeBody/blob/main/Assets/JsonDatas/Body.json)

## json parsing
[jsonUtil.cs](https://github.com/rojae1339/2D_ChangeBody/blob/main/Assets/Scripts/Utils/JsonUtil.cs)

```cs
//Json을 딕셔너리로 파싱
private static Dictionary<string, List<Dictionary<string, object>>> FromJsonToDictionaryList(string json)
{
    return JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, object>>>>(json);
}

//json파일 path가 주어지면, 해당 path에 존재하는 json을 읽어와서 딕셔너리로 파싱
public static Dictionary<string, List<Dictionary<string, object>>> LoadPartsWithPath(string partsPath)
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
```

파일 path를 이용해, 경로에 위치하는 json파일을 읽고, Dictionary형식으로 반환한다.
Dictionary형식은 Dictionary<partsname, List<Dictionary<attribute, value>>> 이다.



## 데이터 매니저

https://github.com/rojae1339/2D_ChangeBody/blob/main/Assets/Scripts/Managers/PartsDataManager.cs
```cs
//PartsDataManager
private const string _weaponJsonPath = "Assets/JsonDatas/Weapon.json";
private const string _bodyJsonPath = "Assets/JsonDatas/Body.json";

private Dictionary<string, List<Dictionary<string, object>>> _weaponData = new();
private Dictionary<string, List<Dictionary<string, object>>> _bodyData = new();
        
public Dictionary<string, List<Dictionary<string, object>>> WeaponData { get => _weaponData; }
public Dictionary<string, List<Dictionary<string, object>>> BodyData { get => _bodyData; }
```

위의 jsonUtil을 이용해 Weapon.json과 Body.json파일을 불러와서 
각각 weaponData와 bodyData라는 딕셔너리에 캐싱을 한다.

먼저 json을 DTO로 파싱한 후 엔티티객체(Base~~)로 변환
[WeaponDTO.cs](https://github.com/rojae1339/2D_ChangeBody/blob/main/Assets/Scripts/Player/Weapon/DTO/WeaponDTO.cs)
[BaseWeapon.cs](https://github.com/rojae1339/2D_ChangeBody/blob/main/Assets/Scripts/Player/Weapon/DTO/BaseWeapon.cs)
[BodyDTO.cs](https://github.com/rojae1339/2D_ChangeBody/blob/main/Assets/Scripts/Player/Body/DTO/BodyDTO.cs)
[BaseBody.cs](https://github.com/rojae1339/2D_ChangeBody/blob/main/Assets/Scripts/Player/Body/DTO/BaseBody.cs)

### DTO로 파싱하는 이유
클라이언트, 서버, 데이터베이스로 크게 분리가 가능한 생태계에서, 클라이언트측의 혹은 서버측의 정보 중, 서로에게 노출되면 안되는 정보들이 존재할 수 있다. 

예를들어, 서버에서는 유저 객체에 DB정보, 토큰 등의 보안과 연결된 문제가 있을수 있는데, 이러한 정보들이 단순한 데이터정보(공격력, 경험치등)과 연관이 없음에도 불구하고, 엮이게 된다.

따라서, 플레이어 단순 DTO에는 공격력, 경험치 등의 단순한 정보만 넣어두고, 데이터베이스에서 정보를 꺼내, DTO로 파싱한 후, 유저에게 주게되면, 불필요한 정보의 노출이나 엮임이 없이 보호, 수정, 획득 할 수 있게된다.

# Addressable시스템
리소스 폴더의 문제점으로 Addressable시스템으로 리소스를 관리한다.
그리고 로드된 게임오브젝트 객체들은 딕셔너리에 이름, 오브젝트 페어로 캐싱이 되어 추가적인 로드와 비동기 Instantiate호출을 막도록 하였다.

[AddressableManager](https://github.com/rojae1339/2D_ChangeBody/blob/main/Assets/Scripts/Managers/AddressableManager.cs)
```cs
//캐시용 dictionary
private Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, Object>();
private Dictionary<string, HashSet<string>> _labelToKeys = new Dictionary<string, HashSet<string>>();

//.....중략

//미리 로딩해서 dictionary에 캐싱하기
//키 값으로 로딩
public void LoadAsync<T>(string key, Action<T> callback = null) where T : Object
{
    if (_resources.TryGetValue(key, out Object resource))
    {
        callback?.Invoke(resource as T);
        return;
    }

    AsyncOperationHandle<T> asyncOperation = Addressables.LoadAssetAsync<T>(key);
    asyncOperation.Completed += oper =>
    {
        _resources.Add(key, oper.Result);
        callback?.Invoke(oper.Result);
    };
}
    
//label에 해당되는 모든 게임오브젝트 로딩(LoadAsync 래핑 메서드)
public void LoadAllAsync<T>(string label, Action<string, int, int> loadCallback) where T : Object
{
    AsyncOperationHandle<IList<IResourceLocation>> asyncOperation = Addressables.LoadResourceLocationsAsync(label,     typeof(T));

    asyncOperation.Completed += op =>
    {
        int loadCount = 0;
        int totalCount = op.Result.Count;

        foreach (IResourceLocation resourceLocation in op.Result)
        {
            LoadAsync<T>(resourceLocation.PrimaryKey, obj =>
            {
                //캐싱된 키 추가
                if (!_labelToKeys.ContainsKey(label))
                {
                    _labelToKeys[label] = new HashSet<string>();
                }
                    
                _labelToKeys[label].Add(resourceLocation.PrimaryKey);
                    
                loadCount++;
                loadCallback?.Invoke(resourceLocation.PrimaryKey, loadCount, totalCount);
            });
        }
    };
}
    
//그룹의 모든 라벨을 순회하며 로드 하기(LoadAllAsync래핑 메서드)
public void LoadAllByLabelsAsync<T>(string[] labels, Action onComplete) where T : Object
{
    int totalLabelCount = labels.Length;
    int finishedLabelCount = 0;

    foreach (string label in labels)
    {
        LoadAllAsync<T>(label, (key, count, totalCount) =>
        {
            // 한 라벨 로딩이 끝났을 때만 증가
            if (count == totalCount)
            {
                finishedLabelCount++;

                //Debug.Log($"label: {label}, {totalLabelCount} : total, {finishedLabelCount} : finished");

                if (finishedLabelCount == totalLabelCount)
                {
                    // 모든 라벨의 리소스 로딩 완료
                    onComplete?.Invoke();
                }
            }
        });
    }
}
```

# 게임오브젝트 캡쳐 시스템

부가적인 시스템이다.
현재 파츠 시스템은 4개 티어별로 총 10개 가량의 파츠가 존재한다.
따라서 이에 따른 무기의 썸네일을 구해야했는데, 직접 무기의 티어 색에 맞춰 사진을 찍어줬다.

[Capture.cs](https://github.com/rojae1339/2D_ChangeBody/blob/main/Assets/RenderTexture/Capture.cs)

유니티 UI의 RawImage를 이용해서 RenderTexture를 사용해 캡쳐시스템을 구축했다.

