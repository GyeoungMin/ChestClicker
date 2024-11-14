using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Text;
using UnityEngine.XR;
using GooglePlayGames;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private DateTime startTime;
    public TimeSpan totalPlayTime;


    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                PauseOrEndGame();
            }
        }
    }

    public void StartGame()
    {
        startTime = DateTime.Now;

        if (InfoManager.Instance.Init())
        {
            Debug.Log("Saved User");
        }
        else
        {
            Debug.Log("New User");
            InfoManager.Instance.CreateInfos();
        }

        ADMobManager.Instance.Init();
    }

    public void PauseOrEndGame()
    {
        DateTime endTime = DateTime.Now;
        TimeSpan playTime = endTime - startTime;

        totalPlayTime += playTime;

        bool saved = SaveGame();
        Debug.Log($"Saved the Game is {saved}");
    }

    public bool SaveGame()
    {
        var main = FindAnyObjectByType<GameMain>();
        if (main != null)
        {
            InfoManager.Instance.CreateClickInfo(main.TotalCount, main.HitCount, main.Gold, main.Crystal);
            InfoManager.Instance.SaveInfos();
            if (PlayGamesPlatform.Instance.IsAuthenticated())
            {
                GPGSManager.Instance.PostLeaderboard(main.TotalCount, GPGSIds.leaderboard_click_champion);
                GPGSManager.Instance.SaveGameToCloud();
            }
            return true;
        }

        return false;
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            PauseOrEndGame();
        }
    }

    public void OnApplicationQuit()
    {
        PauseOrEndGame();
    }

    private void OnDestroy()
    {
        ADMobManager.Instance.DestroyAds();
    }
}