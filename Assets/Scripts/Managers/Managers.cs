using UnityEngine;

public class Managers : MonoBehaviour
{
    
    private static Managers s_Instance;

    static Managers Instance
    {
        get
        {
            Init();
            return s_Instance;
        }
    }

    private MapManager _map = new MapManager();

    private PartsManager _parts = new PartsManager();
    
    public static MapManager Map { get => Instance._map; }
    
    public static PartsManager Parts { get => Instance._parts; }
    
    void Start()
    {
        Init();
        _parts.LoadWeaponData();
    }

    void Update()
    {
        
    }
    
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
    }
}
