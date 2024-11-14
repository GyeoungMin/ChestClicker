using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMain : MonoBehaviour
{
    [SerializeField] UIMain ui;
    [SerializeField] ClickController click;


    ChestType type;
    ClickInfo clickInfo;

    public long TotalCount { get { return totalCount; } }
    public int HitCount { get { return hitCount; } }
    public int Gold {  get { return gold; } }
    public int Crystal { get { return crystal; } }

    long totalCount;
    int hitCount;
    int gold;
    int crystal;

    int lastCount;
    float lastClickTime;
    int clicksInLastSecond;


    void Start()
    {
        clickInfo = InfoManager.Instance.ClickInfo;
        totalCount = clickInfo.totalCount;
        hitCount = clickInfo.hitCount;
        gold = clickInfo.gold;
        crystal = clickInfo.crystal;

        ui.UpdateHitCountText(hitCount);
        ui.UpdateGoodsText(gold, crystal);

        InvokeRepeating("UpdatePerSecond", 1f, 1f);
        //GameManager.Instance.StartGame();
        ADMobManager.Instance.Banner.ShowAd();
    }

    // Update is called once per frame
    void Update()
    {
        InputSystem();
    }
    void InputSystem()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log(EventSystem.current.IsPointerOverGameObject());
            ScreenHit();
        }
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    Check if finger is over a UI element
        //    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        //    {
        //        Debug.Log("Touched the UI");
        //    }
        //    else
        //    {
        //        ScreenHit();
        //    }
        //}
    }

    void ScreenHit()
    {
        click.Hit();
        IncrementCount();
        GetReward();
        
        clicksInLastSecond++;
        lastClickTime = Time.time;
    }

    void IncrementCount()
    {
        totalCount++;
        ui.UpdateHitCountText(++hitCount);
        Debug.Log($"Total Count : {totalCount}, Current Count : {hitCount}");
    }

    void UpdatePerSecond()
    {
        float currentTime = Time.time;
        if (currentTime - lastClickTime > 1f)
        {
            // More than 1 second since last click
            clicksInLastSecond = 0;
        }
        ui.UpdatePerSecondText(clicksInLastSecond);
        clicksInLastSecond = 0; // Reset count for the next second
    }

    void GetReward()
    {
        int rand = Random.Range(0, 100);
        if (rand == 0)
        {
            crystal += click.GetCrystal();
            ui.UpdateCrystalText(crystal);
        }
        else
        {
            gold += click.GetGold();
            ui.UpdateGoldText(gold);
        }
    }
}
