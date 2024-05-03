using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class inbetweentolvl2 : MonoBehaviour
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
        SceneManager.LoadScene("Level2");
        FindObjectOfType<AudioManager>().Play("Lvl2 Theme");
    }

    void Skip()
    {
        SceneManager.LoadScene("Level2");
        FindObjectOfType<AudioManager>().Play("Lvl2 Theme");
    }
}
