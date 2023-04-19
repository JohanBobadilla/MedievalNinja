using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingPanel;
    public string scene;
    private bool loading;

    public void LoadSceneAsync()
    {
        loadingPanel.SetActive(true);
        StartCoroutine(StartLoadScene(scene));
    }

    IEnumerator StartLoadScene(string scene)
    {
        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(scene);

        while (!sceneLoading.isDone)
        {
            yield return null;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return) && !loading)
        {
            loading = true;
            LoadSceneAsync();
        }
    }
}
