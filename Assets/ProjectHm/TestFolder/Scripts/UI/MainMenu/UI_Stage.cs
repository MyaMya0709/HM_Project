using UnityEngine;
using UnityEngine.UI;

public class UI_Stage : MonoBehaviour
{
    public Button startBtn;

    public void Awake()
    {
        startBtn.onClick.AddListener(() =>
        {
            if (gameObject.activeSelf)
            {
                //게임 씬 로드 & 튜토리얼 UI On
                SceneLoader.Instance.LoadSceneAsync("InGame");
            }
        });
    }
}
