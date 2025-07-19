using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.IO;

public enum SavePathType
{
    Asset, // \Asset
    Resources, // \Asset\Resources
    PersistentPath, //C:\Users\[user name]\AppData\LocalLow\[company name]\[product name]
}

public enum Tier
{
    Common,
    Rare,
    Unique,
    Legendary
}

public enum RTSizeType
{
    POT64,
    POT128,
    POT256,
    POT512,
    POT1024
}

public class Capture : MonoBehaviour
{
    
    
    public Camera cam;
    public RenderTexture rt;
    public Image bg;
    public SavePathType saveSavePathType;
    public Tier tierType;
    public RTSizeType sizeType;

    public GameObject[] obj;
    private int count = 0;

    private void Start()
    {
        cam = Camera.main;
        SetColor();
        SetSize();
    }

    public void Create()
    {
        StartCoroutine(CaptureImage());
        
    }

    public void CreateAll()
    {
        StartCoroutine(CaptureAllImage());
    }

    //capture by 1 object
    IEnumerator CaptureImage()
    {
        yield return null;

        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false, true);
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);

        yield return null;

        var data = tex.EncodeToPNG();
        string name = $"Thumbnail";
        string extension = ".png";

        string basePath;
        switch (saveSavePathType)
        {
            case SavePathType.Asset:
                basePath = Application.dataPath;                 // Assets
                break;
            case SavePathType.Resources:
                basePath = Path.Combine(Application.dataPath, "Resources"); // Assets/Resources
                break;
            case SavePathType.PersistentPath:
                basePath = Application.persistentDataPath;
                break;
            default:
                basePath = Application.dataPath;
                break;
        }

        if (!Directory.Exists(basePath))
            Directory.CreateDirectory(basePath);

        string filePath = Path.Combine(basePath, name + extension);
        
        File.WriteAllBytes(filePath, data);
        
        Debug.Log("Saved: " + filePath);

        yield return null;
    }

    //capture by multiple objects
    IEnumerator CaptureAllImage()
    {
        while (count < obj.Length)
        {
            var nowObj = Instantiate(obj[count].gameObject);

            nowObj.transform.position = new Vector3(0, 0, 10);
            
            yield return null;
            yield return null;
            yield return null;
            
            Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false, true);
            RenderTexture.active = rt;
            tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);

            yield return null;

            var data = tex.EncodeToPNG();
            string name = $"Thumbnail_{tierType}_{obj[count].gameObject.name}";
            string extension = ".png";

            string basePath;
            switch (saveSavePathType)
            {
                case SavePathType.Asset:
                    basePath = Application.dataPath;                 // Assets
                    break;
                case SavePathType.Resources:
                    basePath = Path.Combine(Application.dataPath, "Resources"); // Assets/Resources
                    break;
                case SavePathType.PersistentPath:
                    basePath = Application.persistentDataPath;
                    break;
                default:
                    basePath = Application.dataPath;
                    break;
            }

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            string filePath = Path.Combine(basePath, name + extension);
        
            File.WriteAllBytes(filePath, data);
        
            Debug.Log("Saved: " + filePath);
            
            yield return null;
            
            DestroyImmediate(nowObj);
            count++;
            
            yield return null;
        }
    }
    
    void SetColor()
    {
        switch (tierType)
        {
            case Tier.Common: 
                cam.backgroundColor = Color.gray;
                bg.color = Color.gray;
                break;
            case Tier.Rare: 
                cam.backgroundColor = Color.skyBlue;
                bg.color = Color.skyBlue;
                break;
            case Tier.Unique: 
                cam.backgroundColor = Color.gold;
                bg.color = Color.gold;
                break;
            case Tier.Legendary: 
                cam.backgroundColor = Color.crimson;
                bg.color = Color.crimson;
                break;
            default: break;
        }
    }

    void SetSize()
    {
        int w, h;
        switch (sizeType)
        {
            case RTSizeType.POT64:   w = h = 64;   break;
            case RTSizeType.POT128:  w = h = 128;  break;
            case RTSizeType.POT256:  w = h = 256;  break;
            case RTSizeType.POT512:  w = h = 512;  break;
            case RTSizeType.POT1024: w = h = 1024; break;
            default:                 w = h = 256;  break;
        }

        // 이미 있고 같은 사이즈면 건드릴 필요 없음
        if (rt != null && rt.width == w && rt.height == h) return;

        // 기존 것 정리
        if (rt != null)
        {
            if (cam && cam.targetTexture == rt) cam.targetTexture = null;
            rt.Release();
            Destroy(rt);
        }

        rt = new RenderTexture(w, h, 24, RenderTextureFormat.ARGB32)
        {
            antiAliasing = 1
        };
        rt.Create();

        if (cam) cam.targetTexture = rt;
    }

}
