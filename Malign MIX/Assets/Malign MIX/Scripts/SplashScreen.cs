using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    VideoPlayer videoSplashScreen;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        videoSplashScreen = GetComponent<VideoPlayer>();
        videoSplashScreen.loopPointReached += OnEndSplash;
    }

    void OnEndSplash(VideoPlayer video)
    {
        SceneManager.LoadScene(1);
    }
}
