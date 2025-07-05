using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;    
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadAsynchronously(2));
    }

    IEnumerator LoadAsynchronously(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public void SceneryGames()
    {
        Application.OpenURL("https://scenery-games.itch.io/");
    }

    public void Sair()
    {
        Application.Quit();
    }
}
