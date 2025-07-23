using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public bool isGameOver = false;

    public void GameStart()
    {
        player = GameObject.FindWithTag("Player");
        PoolManager.Instance.LoadAllGroupPool();
        StageManager.Instance.StageSet();
        SpawnManager.Instance.StartWaves();
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}

