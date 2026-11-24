using UnityEngine;

public class HealthBarListener : MonoBehaviour
{
    [SerializeField] HealthBar bar;
    Health health;

    void Awake()
    {
        health = FindAnyObjectByType<Health>();
    }

    void OnEnable()
    {
        health.Damaged += OnChanged;
        health.Healed += OnChanged;
    }

    void OnDisable()
    {
        health.Damaged -= OnChanged;
        health.Healed -= OnChanged;
    }

    void OnChanged(int current, int max)
    {
        bar.Set(current, max);
    }
}