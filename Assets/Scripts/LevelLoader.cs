using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] int lastInPlayOrder = 6;   // the credits
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Slider progressBar;

    public void LoadNext()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next > lastInPlayOrder) next = 0;
        Load(next);
    }

    public void Reload() => Load(SceneManager.GetActiveScene().buildIndex);
    public void LoadMenu() => Load(0);

    public void Load(int buildIndex)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadAsync(buildIndex));
    }

    IEnumerator LoadAsync(int buildIndex)
    {
        loadingPanel.SetActive(true);
        progressBar.value = 0f;
        AsyncOperation op = SceneManager.LoadSceneAsync(buildIndex);
        while (!op.isDone)
        {
            progressBar.value = Mathf.Clamp01(op.progress / 0.9f);
            yield return null;
        }
    }
}