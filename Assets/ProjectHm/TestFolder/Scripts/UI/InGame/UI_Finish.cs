using TMPro;
using UnityEngine;

public class UI_Finish : MonoBehaviour
{
    public UIManager uiManager;
    [Header("Finish")]
    [SerializeField] private GameObject clearTitle;
    [SerializeField] private GameObject gameOverTitle;

    [SerializeField] private GameObject clearStage;
    [SerializeField] private GameObject bestLevel;
    [SerializeField] private GameObject curLevel;
    [SerializeField] private GameObject enemyKill;
    [SerializeField] private GameObject getGold;
    [SerializeField] private TMP_Text stageTMP;
    [SerializeField] private TMP_Text bestLevelTMP;
    [SerializeField] private TMP_Text curLevelTMP;
    [SerializeField] private TMP_Text enemyKillTMP;
    [SerializeField] private TMP_Text getGoldTMP;


    // 활성화/비활성화 시 함수 추가/제거
    private void OnEnable()
    {
        ResultSet();
    }

    public void ResultSet()
    {
        stageTMP.text = $"{GameManager.Instance.selecStageID}";
        bestLevelTMP.text = $"{uiManager.player.playerLevel}";
        curLevelTMP.text = $"{uiManager.player.playerLevel}";
        enemyKillTMP.text = $"{uiManager.spawnManager.killEnemies}";
        getGoldTMP.text = $"{uiManager.player.curGold}";
    }

    public void GameClearResult()
    {
        clearTitle.SetActive(true);
        gameOverTitle.SetActive(false);

        clearStage.SetActive(true);
        bestLevel.SetActive(true);
        curLevel.SetActive(true);
        enemyKill.SetActive(true);
        getGold.SetActive(true);
    }

    public void GameOverResult()
    {
        clearTitle.SetActive(false);
        gameOverTitle.SetActive(true);

        clearStage.SetActive(true);
        bestLevel.SetActive(true);
        curLevel.SetActive(true);
        enemyKill.SetActive(true);
        getGold.SetActive(true);
    }

    public void OnEnableFinshUI(bool isGameClear)
    {
        gameObject.SetActive(true);
        if (isGameClear) GameClearResult();
        else GameOverResult();
    }

    public void OnExit()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);

        SceneLoader.Instance.LoadSceneAsync("MainMenu");
    }
}
