using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Object = UnityEngine.Object;

public class AddressableManager
{
    //캐시용 dictionary
    private Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, Object>();
    private Dictionary<string, HashSet<string>> _labelToKeys = new Dictionary<string, HashSet<string>>();

    public T Load<T>(string key) where T : Object
    {
        if (_resources.TryGetValue(key, out Object obj)) return obj as T;

        Debug.Log($"{key} is not founded");
        return null;
    }

    public GameObject Instantiate(string key, Transform parent = null)
    {
        GameObject gameObject = Load<GameObject>(key);
        if (gameObject == null)
        {
            Debug.Log($"어드레서블을 불러오지 못했습니다. Key: {key}");
            return null;
        }

        GameObject instantiate = Object.Instantiate(gameObject, parent);
        instantiate.name = gameObject.name;
        return instantiate;
    }

    public void Destroy(GameObject go)
    {
        if (go == null) return;
        
        Object.Destroy(go);
    }
    
    //라벨에 따른 모든 키(이름) 구하기
    public HashSet<string> GetKeysByLabel(string label)
    {
        return _labelToKeys.TryGetValue(label, out var list) ? list : new HashSet<string>();
    }

    
    //그룹에 따른 모든 키 구하기
    public List<string> GetKeysByGroup(string[] group)
    {
        var keys = new List<string>();
        foreach (string label in group)
        {
            keys.AddRange(GetKeysByLabel(label)); // 중첩 루프 제거
        }
        return keys;
    }
    


    #region 어드레서블

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
        AsyncOperationHandle<IList<IResourceLocation>> asyncOperation = Addressables.LoadResourceLocationsAsync(label, typeof(T));

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

                    Debug.Log($"label: {label}, {totalLabelCount} : total, {finishedLabelCount} : finished");

                    if (finishedLabelCount == totalLabelCount)
                    {
                        // 모든 라벨의 리소스 로딩 완료
                        onComplete?.Invoke();
                    }
                }
            });
        }
    }


    #endregion
}
