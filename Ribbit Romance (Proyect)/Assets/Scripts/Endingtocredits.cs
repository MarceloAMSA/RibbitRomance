using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Endingtocredits : MonoBehaviour
{
    private VideoPlayer video;
    public Button skip;


    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CambiarEscena;
        skip.onClick.AddListener(Skip);
    }

    private void CambiarEscena(VideoPlayer vp)
    {
        SceneManager.LoadScene("Credits");

    }

    void Skip()
    {
        SceneManager.LoadScene("Credits");

    }
}
