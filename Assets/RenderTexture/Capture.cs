using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.IO;
using TMPro;

public enum SavePathType
{
    Asset, // \Asset
    Resources, // \Asset\Resources
    PersistentPath, //C:\Users\[user name]\AppData\LocalLow\[company name]\[product name]
}

public enum ExtensionType
{
    png,
    jpg,
    jpeg
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

    public TMP_InputField inputField;
    private string captureName = "";

    public TMP_Dropdown dropdown;
    public Image dropdownBG;
    public TMP_Dropdown extensionDropdown;
    private ExtensionType extension;

    public GameObject[] obj;
    private int count = 0;

    private void Start()
    {
        cam = Camera.main;
        SetColor();
        SetSize();
        PopulateTierDropdown();
        inputField.onValueChanged.AddListener(ChangeInput);
        dropdown.onValueChanged.AddListener(ChangeTier);
        extensionDropdown.onValueChanged.AddListener(ChangeExtension);
    }

    public void Create()
    {
        StartCoroutine(CaptureImage());
    }
    
    public void CreateAll()
    {
        StartCoroutine(CaptureAllImage());
    }

    void ChangeInput(string text)
    {
        captureName = text;
    }

    void PopulateTierDropdown()
    {
        if (dropdown == null) return;

        dropdown.ClearOptions();
        extensionDropdown.ClearOptions();

        var tierNames = Enum.GetNames(typeof(Tier)); // ["Common","Rare","Unique","Legendary"]
        var extenstionNames = Enum.GetNames(typeof(ExtensionType));
        
        var optionDataList = new List<TMP_Dropdown.OptionData>(tierNames.Length);
        var extensionList = new List<TMP_Dropdown.OptionData>(extenstionNames.Length);
        foreach (var n in tierNames)
            optionDataList.Add(new TMP_Dropdown.OptionData(n));

        foreach (var n in extenstionNames)
            extensionList.Add(new TMP_Dropdown.OptionData(n));
        
        dropdown.AddOptions(optionDataList);
        extensionDropdown.AddOptions(extensionList);
        
        dropdown.SetValueWithoutNotify((int)tierType);
        extensionDropdown.SetValueWithoutNotify((int)extension);
    }
    
    void ChangeTier(int index)
    {
        tierType = (Tier)index;
        SetColor();
        Debug.Log($"Tier changed to: {tierType}");
    }
    
    void ChangeExtension(int index)
    {
        extension = (ExtensionType)index;
        Debug.Log($"Extension changed to: {extension}");
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
        string name = $"Thumbnail_{tierType}_{captureName}";
        string ex = "." + extension.ToString();

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

        string filePath = Path.Combine(basePath, name + ex);
        
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
            string ex = "." + extension.ToString();

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

            string filePath = Path.Combine(basePath, name + ex);
        
            File.WriteAllBytes(filePath, data);
        
            Debug.Log("Saved: " + filePath);
            
            yield return null;
            
            DestroyImmediate(nowObj);
            count++;
            
            yield return null;
        }
    }
    
    // --- Tier 컬러 매핑 ---------------------------------------
    Color GetTierColor(Tier t)
    {
        switch (t)
        {
            case Tier.Common:    return Color.gray;
            case Tier.Rare:      return new Color32(110, 180, 255, 255);
            case Tier.Unique:    return new Color32(255, 215,   0, 255);
            case Tier.Legendary: return new Color32(220,  20,  60, 255);
            default:             return Color.white;
        }
    }

// --- SetColor 사용 ----------------------------------------
    void SetColor()
    {
        var c = GetTierColor(tierType);
        if (cam != null) cam.backgroundColor = c;
        if (bg != null)  bg.color = c;
        if (dropdownBG != null)  dropdownBG.color = c;
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
