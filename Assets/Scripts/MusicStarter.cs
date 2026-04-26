using UnityEngine;

public class MusicStarter : MonoBehaviour
{
    [SerializeField] private string bgmName = "SpookyTheme"; 

    void Start()
    {
        AudioManager.Instance.PlayMusic(bgmName);
    }
}