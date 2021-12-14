using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;

public class Manager : MonoBehaviour
{

    public static GameObject _selectedTowerBuy;
    [SerializeField] public static int _selectedTowerCost;
    public static GameObject _selectedTowerCanvas;

    public static bool waveStarted = false;
    public static int enemiesAlive = 0;
    public int StartWave = 0;
    public static int startWave;

    public static bool finalWave = false;
    public GameObject gameButton;


    public static bool gameLost;
    public static bool gameWin;

    public void Awake()
    {
        startWave = StartWave;
    }

    public void SetTower(int index)
    {
        switch (index)
        {
            case 0:
                _selectedTowerBuy = Resources.Load<GameObject>("Prefabs/TB_ArcherTower_Lvl1");
                break;
            case 1:
                _selectedTowerBuy = Resources.Load<GameObject>("Prefabs/Crossbow");
                break;
            case 2:
                _selectedTowerBuy = Resources.Load<GameObject>("Prefabs/ExploshionGun");
                break;
            case 3:
                _selectedTowerBuy = Resources.Load<GameObject>("Prefabs/ExplosionGunSpeeds");
                break;
        }

        _selectedTowerCost = _selectedTowerBuy.GetComponent<TowerScript>()._myCost;
        if (PlayerStats.Money < _selectedTowerCost)
        {
            _selectedTowerBuy = null;
        }

        Debug.Log(_selectedTowerBuy);
        Debug.Log(_selectedTowerCost);
    }
    
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            _selectedTowerBuy = null;
    }

    public static void TowerMenu(GameObject canvas)
    {
        if (!_selectedTowerCanvas)
        {
            _selectedTowerCanvas = canvas;
            canvas.SetActive(true);
        }
        else
        {
            if (_selectedTowerCanvas == canvas)
            {
                _selectedTowerCanvas = null;
                canvas.SetActive(false);
            }
            else
            {
                _selectedTowerCanvas.SetActive(false);
                _selectedTowerCanvas = canvas;
                _selectedTowerCanvas.SetActive(true);
            }
        }
    }
    
    public static void PlayerDamage()
    {
        PlayerStats.Lifes--;
        if (PlayerStats.Lifes <= 0)
        {
            gameLost = true;
        };
    }
    
    public static void EnemieDiscount()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0)
        {
            if (finalWave)
            {
                gameWin = true;
            }
            waveStarted = false;
        }
    }

    public void GameLost()
    {
        Time.timeScale = 0;
        gameButton.transform.GetChild(0).GetComponent<Text>().text = "Вы проиграли, Нажмите, чтобы начать заново!";
    }
    
    public void GameWin()
    {
        Time.timeScale = 0;
        gameButton.transform.GetChild(0).GetComponent<Text>().text = "Вы выиграли, Нажмите, чтобы начать заново!";
    }
    
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        
        if(gameWin || gameLost) gameButton.SetActive(true);
        else gameButton.SetActive(!waveStarted);
        
        if(gameLost) GameLost();
        if(gameWin) GameWin();
    }

    public static void Restart()
    {
        gameLost = false;
        gameWin = false;
        finalWave = false;
        Time.timeScale = 1;
        waveStarted = false;
        SceneManager.LoadScene(0);
    }
}


