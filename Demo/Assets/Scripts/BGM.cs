using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip start;
    public AudioClip siren0Start;
    public AudioClip siren0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.clip == start && !audioSource.isPlaying) {
            GameManager.Instance.StartGame();
            audioSource.clip = siren0Start;
            audioSource.Play();
        } else if (audioSource.clip == siren0Start && !audioSource.isPlaying) {
            audioSource.clip = siren0;
            audioSource.Play();
            audioSource.loop = true;
        }
    }
}
