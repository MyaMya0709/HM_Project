using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isGameOver = false;
    public BaseCore baseCore;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnManager.Instance.StartWaves();

        // ���� �ı� �� GameOver() ȣ��
        baseCore.OnBaseDestroy += GameOver;
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

