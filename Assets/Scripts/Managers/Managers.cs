using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class Managers : MonoBehaviour
    {
        private static Managers s_Instance;
        
        //어드레서블 로드 완료됐을때 Invoke할 이벤트 추가용
        public static Action OnManagerLoadInitialized;

        private static Managers Instance {
            get
            {
                Init();
                return s_Instance;
            }
        }

        private readonly MapManager _map = new MapManager();
        private readonly PlayerManager _player = new PlayerManager();
        private readonly AddressableManager _addressable = new AddressableManager();
        private readonly PartsDataManager _partsData = new PartsDataManager();
        private readonly ResourcesFolderManager _resourcesFolder = new ResourcesFolderManager();

        public static MapManager Map { get => Instance._map; }

        public static PlayerManager Player { get => Instance._player; }

        public static AddressableManager Addressable { get => Instance._addressable; }
        
        public static PartsDataManager PartsData { get => Instance._partsData; }
        
        public static ResourcesFolderManager ResourcesFolder { get => Instance._resourcesFolder; }

        void Awake() { Init(); }

        void Start() { }

        private static void Init()
        {
            if (s_Instance != null) return;
            
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject() { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_Instance = go.GetComponent<Managers>();
            s_Instance._partsData.Init();   
            
            OnManagerLoadInitialized += OnInitComplete;
            s_Instance.StartCoroutine(s_Instance.WaitForManagersInit());
        }
        
        private IEnumerator WaitForManagersInit()
        {
            bool isPartsDataLoaded = false;
            bool isStartGroupLoaded = false;

            // 비동기 작업 요청
            _addressable.LoadAllAsync<PartsTierFeatureSO>("SO", (s, i, arg3) =>
            {
                isPartsDataLoaded = true;
            });

            _addressable.LoadAllByLabelsAsync<GameObject>(AddressableLabelGroup.StartGroup, () =>
            {
                isStartGroupLoaded = true;
            });

            // 둘 다 로딩될 때까지 기다림
            yield return new WaitUntil(() => isPartsDataLoaded && isStartGroupLoaded);
            
            
            OnManagerLoadInitialized?.Invoke();
            OnManagerLoadInitialized -= OnInitComplete;
        }
        
        //Load를 마친후 이벤트 등록되어 실행할 메서드들
        private static void OnInitComplete()
        {
            s_Instance._map.ChangeStage(StageType.Main);
        }

    }
}