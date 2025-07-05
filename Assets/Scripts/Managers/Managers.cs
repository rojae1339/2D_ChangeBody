using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public class Managers : MonoBehaviour
    {
        private static Managers s_Instance;

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
        private readonly UIManager _ui = new UIManager();

        public static MapManager Map { get => Instance._map; }

        public static PlayerManager Player { get => Instance._player; }

        public static AddressableManager Addressable { get => Instance._addressable; }
        
        public static PartsDataManager PartsData { get => Instance._partsData; }
        public static UIManager UI { get => Instance._ui; }

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

            _map.ChangeStage(StageType.Main);
        }
    }
}