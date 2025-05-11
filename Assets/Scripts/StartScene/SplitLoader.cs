using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 分割処理でシーンをロード
/// </summary>
public class SplitLoader : MonoBehaviour
{
    [SerializeField] private Slider progressBar;

    public void StartLoading(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        var loadOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!loadOperation.isDone)
        {
            // シーンの読み込みが 90% でアクティブになるらしいです
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            progressBar.value = progress;
            yield return null;
        }
    }
}
