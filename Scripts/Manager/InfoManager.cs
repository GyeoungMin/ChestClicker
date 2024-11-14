using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class InfoManager
{
    public static readonly InfoManager Instance = new InfoManager();
    private const string GAME_INFO = "game_info";
    private const string CLICK_INFO = "click_info";

    private Dictionary<string, object> infoDict = new Dictionary<string, object>();

    public GameInfo GameInfo { get { return infoDict[GAME_INFO] as GameInfo; } }
    public ClickInfo ClickInfo { get { return infoDict[CLICK_INFO] as ClickInfo; } }

    public bool Init()
    {
        return LoadInfo<GameInfo>(GAME_INFO) && LoadInfo<ClickInfo>(CLICK_INFO);
    }

    public bool Reset()
    {
        infoDict.Clear();
        return Init();
    }

    public void CreateInfos()
    {
        CreateGameInfo();
        CreateClickInfo();
    }

    public bool LoadInfo<T>(string infoName)
    {
        string path = Path.Combine(Application.persistentDataPath, infoName + ".json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            T info = JsonConvert.DeserializeObject<T>(json);
            infoDict.Add(infoName, info);
            return true;
        }
        return false;
    }

    public bool LoadInfoInCloud<T>(string infoName, byte[] data)
    {
        string json = Encoding.Default.GetString(data);
        T info = JsonConvert.DeserializeObject<T>(json);
        if (info != null)
        {
            infoDict.Add(infoName, info);
            return true;
        }
        return false;
    }

    public byte[] SavedInfoInCloud(string infoName)
    {
        string json = JsonConvert.SerializeObject(infoName);
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        if (bytes == null)
        {
            Debug.Log("Byte Array is null");
            throw new NullReferenceException();
        }
        return bytes;
    }

    public void CreateGameInfo(int major = 0, int minor = 0, int patch = 0, bool isLinkGPGS = false, TimeSpan totalPalyTime = default)
    {
        if (infoDict.ContainsKey(GAME_INFO)) return;
        Debug.Log(Application.version);
        var gameInfo = new GameInfo(major, minor, patch, isLinkGPGS, totalPalyTime);
        infoDict.Add(GAME_INFO, gameInfo);
    }

    public void CreateClickInfo(long totalCount = 0, int hitCount = 0, int gold = 0, int crystal = 0)
    {
        if (infoDict.ContainsKey(CLICK_INFO)) return;
        var clickInfo = new ClickInfo(totalCount, hitCount,gold, crystal);
        infoDict.Add(CLICK_INFO, clickInfo);
    }

    public bool SaveInfo(string infoName)
    {
        string path = Path.Combine(Application.persistentDataPath, infoName + ".json");
        string json = JsonConvert.SerializeObject(infoDict[infoName]);
        File.WriteAllText(path, json);
        Debug.Log($"Save Completed to {infoName}");
        return File.Exists(path);
    }

    public bool SaveInfos()
    {
        return SaveInfo(GAME_INFO) && SaveInfo(CLICK_INFO);
    }
}
