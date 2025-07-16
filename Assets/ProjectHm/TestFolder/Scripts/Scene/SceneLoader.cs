using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false; // 로드 완료 후 자동 전환 방지

        while (op.progress < 0.9f)
        {
            Debug.Log($"Loading: {op.progress * 100:F0}%");
            yield return null;
        }

        Debug.Log($"Loading: {op.progress * 100:F0}%");

        // 로드 완료 후 활성화
        op.allowSceneActivation = true;
    }
}
