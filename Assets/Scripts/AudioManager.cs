using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource[] audioSources;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySoundOnce(int i)
    {
        audioSources[i].Play();
    }
}