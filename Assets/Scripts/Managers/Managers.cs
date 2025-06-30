using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Managers
{
    public class Managers : MonoBehaviour
    {

        private static Managers s_Instance;

        static Managers Instance {
            get
            {
                Init();
                return s_Instance;
            }
        }

        private MapManager _map = new MapManager();

        private PartsManager _parts = new PartsManager();

        private PlayerManager _player = new PlayerManager();

        public static MapManager Map { get => Instance._map; }

        public static PartsManager Parts { get => Instance._parts; }
        
        public static PlayerManager Player { get => Instance._player; }

        void Start() { Init(); }

        static void Init()
        {
            if (s_Instance == null)
            {
                GameObject go = GameObject.Find("@Managers");

                if (go == null)
                {
                    go = new GameObject() { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                s_Instance = go.GetComponent<Managers>();
            }

            s_Instance._parts.Init();
        }
    }
}

