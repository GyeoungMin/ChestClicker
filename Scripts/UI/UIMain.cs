using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    [SerializeField] private Button lederboardBtn;
    [SerializeField] private Image userImage;
    [SerializeField] private TMP_Text userName;
    [SerializeField] TMP_Text hitCountText;
    [SerializeField] TMP_Text perSecondText;
    [SerializeField] TMP_Text goldText;
    [SerializeField] TMP_Text crystalText;
    //[SerializeField] private Button saveToCloudBtn;
    //[SerializeField] private Button deleteToCloudBtn;
    //[SerializeField] private Button showSavedCloudUIBtn;

    [SerializeField] private UIOpenedChest uiOpenedChest;
    void Start()
    {
        lederboardBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.PauseOrEndGame();
            GPGSManager.Instance.ShowLeaderboard(GPGSIds.leaderboard_click_champion);
        });

        //saveToCloudBtn.onClick.AddListener(() => { GPGSManager.Instance.SaveGameToCloud(); });

        //deleteToCloudBtn.onClick.AddListener(() => { GPGSManager.Instance.DeleteGameData(); });

        //showSavedCloudUIBtn.onClick.AddListener(() => { GPGSManager.Instance.ShowSelectUI(); });

        if (GPGSManager.Instance.IsAuthenticated)
        {
            InitializeUserProfile();
        }


    }

    void InitializeUserProfile()
    {
        ILocalUser userProfile = GPGSManager.Instance.localUser;
        userName.text = userProfile.userName;
        if (userProfile.image != null)
        {
            Texture2D texture = userProfile.image;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            userImage.sprite = sprite;
        }
    }

    public void OpenChest()
    {
        uiOpenedChest.OpenPopup();
    }

    public void UpdateHitCountText(int hitCount)
    {
        hitCountText.text = hitCount.ToString();
    }

    public void UpdatePerSecondText(int clicksInLastSecond)
    {
        perSecondText.text = clicksInLastSecond.ToString();
    }

    public void UpdateGoodsText(int gold, int crystal)
    {
        UpdateGoldText(gold);
        UpdateCrystalText(crystal);
    }

    public void UpdateGoldText(int gold)
    {
        goldText.text = gold.ToString();
    }

    public void UpdateCrystalText(int crystal)
    {
        crystalText.text = crystal.ToString();
    }
}
