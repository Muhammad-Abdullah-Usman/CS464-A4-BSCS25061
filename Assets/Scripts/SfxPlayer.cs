using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    public static SfxPlayer Instance { get; private set; }

    [SerializeField] AudioSource[] sources;
    [SerializeField] float pitchRange = 2f;
    [SerializeField] float minVolume = 0.85f;
    int next;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Play(AudioClip clip, float volume = 1f)
    {
        AudioSource source = sources[next];
        next = (next + 1) % sources.Length;
        source.pitch = Mathf.Pow(2f, Random.Range(-pitchRange, pitchRange) / 12f);
        source.PlayOneShot(clip, volume * Random.Range(minVolume, 1f));
    }
}