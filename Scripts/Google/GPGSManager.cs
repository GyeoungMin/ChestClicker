using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.SavedGame;
using System;

public class GPGSManager
{
    public static readonly GPGSManager Instance = new GPGSManager();
    //private static GPGSManager instance;
    private const string FILE_NAME = "game_info";
    public bool IsAuthenticated { get { return PlayGamesPlatform.Instance.IsAuthenticated(); } }
    public ILocalUser localUser { get { return PlayGamesPlatform.Instance.localUser; } }
    private ISavedGameMetadata metadata;
    //public static GPGSManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = FindAnyObjectByType<GPGSManager>();
    //            if (instance == null)
    //            {
    //                GameObject go = new GameObject("GPGSManager");
    //                go.AddComponent<GPGSManager>();
    //                DontDestroyOnLoad(go);
    //            }
    //        }
    //        return instance;
    //    }
    //}

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public void Authenticate()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    public void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
            Debug.Log("인증 성공");
            Debug.Log(PlayGamesPlatform.Instance.localUser.id);
            Debug.Log(PlayGamesPlatform.Instance.localUser.userName);
            OpenSavedGame();
        }
        else
        {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
            Debug.Log($"인증 실패 : {status}");
        }
    }


    public void PostLeaderboard(long totalCount, string leaderboardID)
    {
        if (!IsAuthenticated) return;
        PlayGamesPlatform.Instance.ReportScore(totalCount, leaderboardID, (bool success) =>
        {
            Debug.Log($"Saved the LeaderBoard(ID_{leaderboardID}) is {success}");
        });
    }

    public void ShowLeaderboard(string leaderboardID)
    {
        if (!IsAuthenticated) return;
        PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardID);
    }

    public void UnlockAchievement(float progress, string achievementID)
    {
        if (!IsAuthenticated) return;
        PlayGamesPlatform.Instance.ReportProgress(achievementID, progress, (bool success) =>
        {
            Debug.Log($"Unlocking the Achievement(ID_{achievementID}) is {success}");
        });
    }

    public void IncrementAchievement(int step, string achievementID)
    {
        if (!IsAuthenticated) return;
        PlayGamesPlatform.Instance.IncrementAchievement(achievementID, step, (bool success) =>
        {
            Debug.Log($"Incremented the Achievement(ID_{achievementID}) is {success}");
        });
    }

    public void ShowAchievement()
    {
        if (!IsAuthenticated) return;
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

    public void ShowSelectUI()
    {
        if (!IsAuthenticated) return;
        uint maxNumToDisplay = 5;
        bool allowCreateNew = false;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            OnSavedGameSelected);
    }


    public void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {
        
        if (status == SelectUIStatus.SavedGameSelected)
        {
            // handle selected game save
            Debug.Log("Open Saved Game UI");
        }
        else
        {
            // handle cancel or error
            Debug.Log($"Faild Open UI Saved Game Data : {status}");
        }
    }

    public void OpenSavedGame()
    {
        if (!IsAuthenticated) return;
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        Debug.Log(savedGameClient);
        savedGameClient.OpenWithAutomaticConflictResolution(FILE_NAME, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseMostRecentlySaved, OnSavedGameOpened);
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata metadata)
    {
        this.metadata = metadata;
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            LoadGameData(metadata);
        }
        else
        {
            // handle error
            Debug.Log($"Faild Open Saved Game Data : {status}");
            SaveGameToCloud();
        }
    }

    void LoadGameData(ISavedGameMetadata metadata)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(metadata, OnSavedGameDataRead);
    }

    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle processing the byte array data
            InfoManager.Instance.LoadInfoInCloud<ClickInfo>("click_info", data);
        }
        else
        {
            // handle error
            Debug.Log($"Faild Read Saved Game Data : {status}");
        }
    }

    public void SaveGameToCloud()
    {
        if (!IsAuthenticated) return;
        byte[] savedData = InfoManager.Instance.SavedInfoInCloud("click_info");
        TimeSpan totalPlaytime = GameManager.Instance.totalPlayTime;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + System.DateTime.Now);
        SavedGameMetadataUpdate updatedMetadata = builder.Build();

        savedGameClient.CommitUpdate(metadata, updatedMetadata, savedData, OnSavedGameWritten);
    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata metadata)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Complete Save Game in Cloud");
        }
        else
        {
            Debug.Log($"Faild Save Game Data : {status}");
        }
    }

    public void DeleteGameData()
    {
        if (!IsAuthenticated) return;
        // Open the file to get the metadata.
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(FILE_NAME, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseMostRecentlySaved, DeleteSavedGame);
    }

    public void DeleteSavedGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.Delete(game);
        }
        else
        {
            // handle error
            Debug.Log($"Faild Delete Saved Game Data : {status}");
        }
    }
}
