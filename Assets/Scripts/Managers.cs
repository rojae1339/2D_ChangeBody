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
    
    public static MapManager Map {get { return Instance._map; }}
    
    void Start()
    {
        Init();
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
