using UnityEngine;

public class HitSparks : MonoBehaviour
{
    [SerializeField] ParticleSystem sparks;
    Health health;

    void Awake() { health = GetComponent<Health>(); }
    void OnEnable() { health.Damaged += Burst; }
    void OnDisable() { health.Damaged -= Burst; }

    void Burst(int current, int max)
    {
        sparks.Play();
    }
}