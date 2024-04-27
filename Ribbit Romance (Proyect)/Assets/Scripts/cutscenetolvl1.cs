using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CambioDeEscenaDespuesDeVideo : MonoBehaviour
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
        // Cambia a la escena que desees después de que el video haya terminado
        SceneManager.LoadScene("Level1");
    }
}
