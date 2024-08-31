using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class BadEnding : MonoBehaviour
{
    [SerializeField] string videoFileName;
    public Button skip;
    void Start()
    {
        PlayVideo();
        skip.onClick.AddListener(Skip);
    }

    // Update is called once per frame
    void Skip()
    {
        SceneManager.LoadScene("Credits");
    }

    private void CambiarEscena(VideoPlayer vp)
    {
        SceneManager.LoadScene("Credits");
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
