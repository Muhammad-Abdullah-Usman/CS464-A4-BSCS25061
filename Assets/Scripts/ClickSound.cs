using UnityEngine;
using UnityEngine.UI;

public class ClickSound : MonoBehaviour
{
    [SerializeField] AudioClip clickClip;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Play);
    }

    void Play()
    {
        SfxPlayer.Instance.Play(clickClip);
    }
}