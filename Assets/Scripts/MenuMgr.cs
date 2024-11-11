using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMgr : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject Menu;
    public GameObject LevelSelection;
    public GameObject GameSetting;
    public GameObject CharacterSelection;

    private GameObject[] Pages;
    private int PageNum;

    public static MenuMgr instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        PageNum = -1;
        Pages = new GameObject[4];
        Pages[0] = Menu;
        Pages[1] = LevelSelection;
        Pages[2] = GameSetting;
        Pages[3] = CharacterSelection;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (PageNum)
            {
                case -1:
                    OpenMenu();
                    break;
                case 0:
                    CloseMenu();
                    break;
                case 1:
                case 2:
                case 3:
                    MoveToPage(0);
                    break;
            }
        }
    }

    public void BackToMain()
    {
        if (PageNum == 0)
        {
            CloseMenu();
            SceneManager.LoadScene(0);
        }
    }

    public void CloseMenu()
    {
        if(PageNum == 0)
        {
            Pages[PageNum].SetActive(false);
            PageNum = -1;
            Canvas.SetActive(false);
        }
    }

    public void MoveToPage(int page)
    {
        if (PageNum != -1)
        {
            Pages[PageNum].SetActive(false);
            PageNum = page;
            Pages[PageNum].SetActive(true);
        }
    }

    public void OpenMenu()
    {
        if(PageNum == -1)
        {
            Canvas.SetActive(true);
            PageNum = 0;
            Pages[PageNum].SetActive(true);
        }
    }

    public void LevelChange(int level)
    {
        if (SceneManager.GetActiveScene().buildIndex != level)
        {
            MoveToPage(0);
            CloseMenu();
            SceneManager.LoadScene(level);
        }
    }
}
