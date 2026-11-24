using UnityEngine;

public class HurtSound : MonoBehaviour
{
    [SerializeField] AudioClip hitClip;
    Health health;

    void Awake() { health = GetComponent<Health>(); }
    void OnEnable() { health.Damaged += OnHit; }
    void OnDisable() { health.Damaged -= OnHit; }

    void OnHit(int current, int max)
    {
        SfxPlayer.Instance.Play(hitClip);
    }
}