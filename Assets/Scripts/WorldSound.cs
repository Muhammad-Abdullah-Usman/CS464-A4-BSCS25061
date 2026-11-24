using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WorldSound : MonoBehaviour
{
    [SerializeField] AudioRolloffMode rolloff = AudioRolloffMode.Logarithmic;
    [SerializeField] float minDistance = 1f;
    [SerializeField] float maxDistance = 25f;
    [SerializeField, Range(0f, 5f)] float doppler = 1f;

    void Awake()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.spatialBlend = 1f;
        source.rolloffMode = rolloff;
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;
        source.dopplerLevel = doppler;
    }
}