using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isGameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnManager.Instance.StartWaves();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}

