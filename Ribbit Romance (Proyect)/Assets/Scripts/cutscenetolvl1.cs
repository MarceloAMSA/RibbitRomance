using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using static UnityEditor.ShaderData;

public class CambioDeEscenaDespuesDeVideo : MonoBehaviour
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
        SceneManager.LoadScene("Level1");
        FindObjectOfType<AudioManager>().Play("Lvl1 Theme");
    }

    void Skip()
    {
        SceneManager.LoadScene("Level1");
        FindObjectOfType<AudioManager>().Play("Lvl1 Theme");
    }
}
