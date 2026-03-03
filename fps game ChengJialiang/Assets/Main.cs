using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{ public Menu menu;
    public GameObject StartPanel;
    public GameObject SelePanel;
    public GameObject EndPanel;
    public Button[] menuButtons;
    public Button[] seleButtons;
    public Image previewImage;
    public Type type;
    public Tupian[] tupian;
    public Button startButton;
    public GameObject DescPanel;
    void Start()
    {
        Time.timeScale = 1;
        StartPanel.SetActive(true);

        menuButtons[0].onClick.AddListener(() =>
            {
              
                type = Type.人物;
                UpdateSeleButtonsAndPreview();

            });
        menuButtons[1].onClick.AddListener(() =>
        {
           
            type = Type.武器;
            UpdateSeleButtonsAndPreview();
        });
        menuButtons[2].onClick.AddListener(() =>
        {
            
            type = Type.地图;
            UpdateSeleButtonsAndPreview();
        });
        startButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(menu.scene);
        });
    }
    private void UpdateSeleButtonsAndPreview()
    {
        // 根据 type 更新 seleButtons 的图片
        for (int i = 0; i < seleButtons.Length; i++)
        {
            int currentIndex = i;
            seleButtons[i].GetComponent<Image>().sprite = tupian[(int)type].previewSprite[i];

            // 为 seleButtons 设置点击事件
            seleButtons[i].onClick.AddListener(() =>
            {
                previewImage.sprite = seleButtons[currentIndex].GetComponent<Image>().sprite;

                // 更新 menu 的相关信息
                if (type == Type.人物)
                {
                    menu.name1 = menu.names[currentIndex];
                }
                else if (type == Type.武器)
                {
                    menu.wuqi = menu.wuqis[currentIndex];
                }
                else if (type == Type.地图)
                {
                    menu.scene = menu.scenes[currentIndex];
                }
            });
        }

        // 注意：这里不再设置 previewImage.sprite，因为它将在 seleButtons 的点击事件中设置
    }
    public void TruePanel(GameObject go)
    {
        go.SetActive(!go.activeSelf);   
    }
    public void SelePanelButton()
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
        }
    }
    
    private void Update()
    {
        switch (type)
        {
            case Type.人物:

                

                break;
            case Type.武器:
               
                break;
            case Type.地图:
                
                break;
            default:
                break;
        }

    }
}
public enum Type { 人物,武器,地图}
[Serializable]
public struct Tupian
{
    public string name;
    public Sprite[] previewSprite;
}