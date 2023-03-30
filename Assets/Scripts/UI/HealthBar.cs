using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        _slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + _offset);
    }

    public void SetHealthValue(int currentHealth, int maxHealth)
    {
        _slider.gameObject.SetActive(currentHealth < maxHealth);
        _slider.value = currentHealth;
        _slider.maxValue = maxHealth;
    }
}
