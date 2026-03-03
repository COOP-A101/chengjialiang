using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Menu menu;

    public int minutes = 1;  // 倒计时分钟
    public int seconds = 0;   // 倒计时秒
    public Text timerText;     // 用于显示倒计时的文本
    public GameObject victoryUI; // 胜利 UI
    public GameObject gameOverUI; // 失败 UI

    private float totalTime; // 总时间（秒）
    private bool isGamePaused = false;
    public GameObject prefabs;
    public Transform content;
    public List<GameObject> MonsterList = new List<GameObject>();

    public int passNum; // 过关数
    public int BoxNum = 0; // 箱子数量
    public Text txtBoxNum;

    private void Start()
    {
        totalTime = minutes * 60 + seconds; // 将分钟和秒转换为总秒数
        victoryUI.SetActive(false); // 初始时隐藏胜利 UI
        SwapMonster();
        txtBoxNum.text = $"Box：{BoxNum}/{passNum}";
    }

    private void Update()
    {
        if (!isGamePaused)
        {
            // 每帧减少时间
            totalTime -= Time.deltaTime;

            // 更新显示的倒计时文本
            UpdateTimerText();

            // 检查时间是否到达
            if (totalTime <= 0)
            {
                HandleTimeUp();
            }
        }
    }

    public void AddBoxNum()
    {
        BoxNum++;
        txtBoxNum.text = $"Box：{BoxNum}/{passNum}";
        if (BoxNum >= passNum)
        {
            HandleTimeUp();
            BoxNum = 0;
        }
    }

    private void UpdateTimerText()
    {
        int minutesLeft = Mathf.FloorToInt(totalTime / 60);
        int secondsLeft = Mathf.FloorToInt(totalTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutesLeft, secondsLeft);
    }

    public void HandleTimeUp()
    {
        isGamePaused = true; // 暂停游戏
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // 检查失败 UI 是否存在并显示胜利 UI
        if (gameOverUI.activeSelf == false)
        {
            victoryUI.SetActive(true); // 显示胜利 UI
            timerText.text = "0:00";
        }
        else
        {
            // 如果失败 UI 存在，可以在这里处理其他逻辑
            Debug.Log("游戏失败，无法显示胜利 UI");
        }
    }

    public void SwapMonster()
    {
        if (minutes > 0)
        {
            for (int i = 0; i < content.childCount; i++)
            {
                GameObject go = Instantiate(prefabs, content.GetChild(i));
                MonsterList.Add(go);
            }
        }
    }
}