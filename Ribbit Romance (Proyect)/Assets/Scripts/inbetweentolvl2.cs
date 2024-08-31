using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class inbetweentolvl2 : MonoBehaviour
{
    [SerializeField] string videoFileName;
    public Button skip;

    void Start()
    {
        PlayVideo();
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

    public void PlayVideo()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            Debug.Log(videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
            videoPlayer.loopPointReached += CambiarEscena;
        }
    }
}
