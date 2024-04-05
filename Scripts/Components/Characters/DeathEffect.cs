using System.Collections;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    [SerializeField] private GameObject _deathParticle;

    private HealthSystem _healthSystem;
    private SpriteRenderer[] _sprites;

    public void Init()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _sprites = transform.GetComponentsInChildren<SpriteRenderer>();
        _healthSystem.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        StartCoroutine(COFadeOut());
        if (_deathParticle != null) _deathParticle.SetActive(true);
        Invoke("DestroyCharacter", 1f);
    }

    private IEnumerator COFadeOut()
    {
        WaitForSeconds interval = new WaitForSeconds(0.1f);
        float value = 1f;

        while (value > 0f)
        {
            value -= 0.1f;
            if (value < 0f) value = 0f;
            foreach (SpriteRenderer renderer in _sprites)
            {
                Color color = renderer.color;
                color.a = value;
                renderer.color = color;
            }
            yield return interval;
        }
    }

    private void DestroyCharacter()
    {
        ResourceManager.Instance.Destroy(gameObject);
    }

    private void OnDisable()
    {
        foreach (SpriteRenderer renderer in _sprites)
        {
            Color color = renderer.color;
            color.a = 1f;
            renderer.color = color;
        }
        if (_deathParticle != null) _deathParticle.SetActive(false);
    }
}
