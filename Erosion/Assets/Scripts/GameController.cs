﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Doozy.Engine.UI;
using UnityEngine.SceneManagement;

public class GameController : SingletonMonoBehaviour<GameController>
{

    public int currentLevel;
    public float waitTime;
    public int rocksDestroyed;
    public int rocksInLevel;

    public Coroutine levelCoroutine;

    public GameObject rock1;
    public GameObject rock2;
    public GameObject rock3;
    public GameObject rock4;
    public GameObject rock5;

    public SpriteRenderer background;
    public Sprite[] backgroundSprites;
    public GameObject player;
    public GameObject currentPlayer;
    public GameObject[] playerList;

    public List<GameObject[]> LevelList = new List<GameObject[]>();
    public GameObject[] currentLevelRocks;
    public GameObject[] spawnPositions;

    public Vector2 playerSpawnPos = new Vector2(0f, -3.693279f);
    public UIView nextLevelView;
    public UIView gameOverView;
    public TMP_InputField levelInput;

    public override void Start()
    {
        base.Start();

        currentLevel = PlayerPrefs.GetInt("levelNumber", 0);
        FillLevels();
        MakeNewPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FillLevels()
    {
        GameObject[] level0 = new GameObject[] { rock3 };
        LevelList.Add(level0);

        GameObject[] level1 = new GameObject[] { rock1 };
        LevelList.Add(level1);

        GameObject[] level2 = new GameObject[] { rock2, rock2 };
        LevelList.Add(level2);

        GameObject[] level3 = new GameObject[] { rock4 };
        LevelList.Add(level3);

        GameObject[] level4 = new GameObject[] { rock5 };
        LevelList.Add(level4);

        GameObject[] level5 = new GameObject[] { rock1 , rock2 };
        LevelList.Add(level5);

        GameObject[] level6 = new GameObject[] { rock5, rock3 };
        LevelList.Add(level6);

        GameObject[] level7 = new GameObject[] { rock3, rock1, rock3 };
        LevelList.Add(level7);

        GameObject[] level8 = new GameObject[] { rock4, rock5 };
        LevelList.Add(level8);

        GameObject[] level9 = new GameObject[] { rock1, rock5, rock2 };
        LevelList.Add(level9);

        GameObject[] level10 = new GameObject[] { rock2, rock1, rock2, rock1 };
        LevelList.Add(level10);

        GameObject[] level11 = new GameObject[] { rock4, rock1, rock3,rock2 };
        LevelList.Add(level11);

        GameObject[] level12 = new GameObject[] { rock2, rock2, rock2, rock2, rock2 };
        LevelList.Add(level12);

        GameObject[] level13 = new GameObject[] { rock1, rock3, rock3,rock1 };
        LevelList.Add(level13);

    }
    
    public void SpawnRock(GameObject rock)
    {
        int temp = Random.Range(0, 6);
        Instantiate(rock, spawnPositions[temp].transform.position, Quaternion.identity);
    }

    public void NextLevelCheck()
    {
        Debug.Log("rock destoryed");
        rocksDestroyed++;
        if(rocksDestroyed == rocksInLevel)
        {
            player.SetActive(false);
            currentLevel++;
            rocksInLevel = 0;
            rocksDestroyed = 0;
            StopCoroutine(RunLevel(currentLevel));
            nextLevelView.Show();
        }

    }
    public void ChangBackground(int num)
    {
        background.sprite = backgroundSprites[num];
    }

    public void GameOver()
    {
        // currentLevel = 0;
        StopCoroutine(levelCoroutine);
        rocksInLevel = 0;
        rocksDestroyed = 0;
        gameOverView.Show();
    }
    private GameObject[] eraseRocks;
    private GameObject[] eraseRocksText;

    public void EraseRocks()
    {
        eraseRocks = GameObject.FindGameObjectsWithTag("rock");
        eraseRocksText = GameObject.FindGameObjectsWithTag("life text");

        for (int i = 0; i< eraseRocks.Length; i++)
        {
            Destroy(eraseRocks[i]);
        }
        for (int i = 0; i < eraseRocksText.Length; i++)
        {
            Destroy(eraseRocksText[i]);
        }
    }


    public float GetWaitTimeRegular(GameObject rock)
    {
        if (GameObject.ReferenceEquals(rock, rock1))
        {
            waitTime = 20f;
        }
        if (GameObject.ReferenceEquals(rock, rock2))
        {
            waitTime = 4f;
        }
        if (GameObject.ReferenceEquals(rock, rock3))
        {
            waitTime = 10f;
        }
        if (GameObject.ReferenceEquals(rock, rock4))
        {
            waitTime = 20f;
        }
        if (GameObject.ReferenceEquals(rock, rock5))
        {
            waitTime = 10f;
        }

        return waitTime;

    }

    public int CalculateRockTotal(GameObject[] rocks)
    {
        for(int i = 0; i< currentLevelRocks.Length; i++)
        {
            if (GameObject.ReferenceEquals(rocks[i], rock1))
            {
                rocksInLevel += 1;
            }
            if (GameObject.ReferenceEquals(rocks[i], rock2))
            {
                rocksInLevel += 1;
            }
            if (GameObject.ReferenceEquals(rocks[i], rock3))
            {
                rocksInLevel += 1;
            }
            if (GameObject.ReferenceEquals(rocks[i], rock4))
            {
                rocksInLevel += 3;
            }
            if (GameObject.ReferenceEquals(rocks[i], rock5))
            {
                rocksInLevel += 2;
            }
        }
        return rocksInLevel;
    }

    public void StartLevel()
    {
        levelCoroutine =  StartCoroutine(RunLevel(currentLevel));
    }
    public void StartGame()
    {
        int temp = int.Parse(levelInput.text);
        currentLevel = temp-1;
       levelCoroutine = StartCoroutine(RunLevel(currentLevel));

    }

    public void MakeNewPlayer()
    {
        player = Instantiate(player, playerSpawnPos, Quaternion.identity);
        player.gameObject.GetComponent<Animator>().enabled = false;

        //player.SetActive(false);
    }
    public void ChangePlayer(int num)
    {
        Destroy(player);
        player = Instantiate(playerList[num], playerSpawnPos, Quaternion.identity);
        player.gameObject.GetComponent<Animator>().enabled = false;
        //player.SetActive(false);

    }

    IEnumerator RunLevel(int level)
    {
        yield return new WaitForSeconds(.5f);
        player.SetActive(true);
        player.gameObject.GetComponent<Animator>().enabled = true;
        PlayerController.fireReady = true;
        PlayerController.fireReady = true;
        currentLevelRocks = LevelList[level];
        rocksInLevel = CalculateRockTotal(currentLevelRocks);

        for (int i = 0; i < currentLevelRocks.Length; i++)
        {
            SpawnRock(currentLevelRocks[i]);
            GetWaitTimeRegular(currentLevelRocks[i]);
            yield return new WaitForSeconds(waitTime);

        }
        yield return null;
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void QuitScene()
    {
        Application.Quit();
    }
}

