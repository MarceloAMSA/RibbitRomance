using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Endingtocredits : MonoBehaviour
{
    private VideoPlayer video;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CambiarEscena;
    }

    private void CambiarEscena(VideoPlayer vp)
    {
        SceneManager.LoadScene("Credits");
    }
}
