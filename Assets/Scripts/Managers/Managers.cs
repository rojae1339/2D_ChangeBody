using System;
using System.Runtime.Serialization;
using System.Threading;
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

        public static MapManager Map { get => Instance._map; }

        public static PlayerManager Player { get => Instance._player; }

        public static AddressableManager Addressable { get => Instance._addressable; }

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
                
            s_Instance._addressable.LoadAllByLabels<GameObject>(AddressableLabelGroup.PlayerGroup,
                () => {
                    // 모든 label의 로딩이 끝난 후에만 stage 변경
                    Managers.Map.ChangeStage(MapManager.StageType.Main);
                });
        }
    }
}