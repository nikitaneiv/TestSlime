using System;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private CountDamage _countDamage;
    
    [SerializeField] private PlayerShoot _playerShoot;
    
    [SerializeField] private TextMeshProUGUI _dynamicScoreText;
    [SerializeField] private TextMeshProUGUI _dynamicAttackText;
    [SerializeField] private TextMeshProUGUI _dynamicHealthText;
    [SerializeField] private TextMeshProUGUI _dynamicRestoreText;
    [SerializeField] private TextMeshProUGUI _dynamicSpeedAttackText;

    [SerializeField] private UIManager _uiManager;

    private int score = 100;
    private int maxHealth = 100;
    private int currentHealth;
    private int currentPlayerDamage = 30;
    private int currentAttackSpeed = 10;
    
    public int CurrentPlayerDamage => currentPlayerDamage;
    
    private void Start()
    {
        currentHealth = maxHealth;
        _dynamicScoreText.text = score.ToString();
        _dynamicAttackText.text = currentPlayerDamage.ToString();
        _dynamicHealthText.text = maxHealth.ToString();
        _dynamicRestoreText.text = currentHealth.ToString();
        _dynamicSpeedAttackText.text = currentAttackSpeed.ToString();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        _dynamicRestoreText.text = currentHealth.ToString();
        _countDamage.SetDamageValue(damage);
        _healthBar.SetHealthValue(currentHealth,maxHealth);
        if (currentHealth <= 0)
        {
            _uiManager.LoseScreen();
        }
    }

    public void AddScore(int count)
    {
        score += count;
        _dynamicScoreText.text = score.ToString();
    }

    public void AddHealth()
    {
        if (score >= 50)
        {
            maxHealth += 50;
            score -= 50;
            _dynamicHealthText.text = maxHealth.ToString();
            _dynamicScoreText.text = score.ToString();
        }
    }

    public void AddSpeedAttack()
    {
        if (score >= 100)
        {
            currentAttackSpeed += 10;
            _playerShoot.FireRate -= 0.5f;
            score -= 100;
            _dynamicSpeedAttackText.text = currentAttackSpeed.ToString();
            _dynamicScoreText.text = score.ToString();
        }
    }

    public void AddAttack()
    {
        if (score >= 50)
        {
            currentPlayerDamage += 20;
            score -= 50;
            _dynamicAttackText.text = currentPlayerDamage.ToString();
            _dynamicScoreText.text = score.ToString();
        }
    }

    public void RestoreHealth()
    {
        if (score >= 100)
        {
            currentHealth = maxHealth;
            score -= 100;
            _healthBar.SetHealthValue(currentHealth,maxHealth);
            _dynamicRestoreText.text = currentHealth.ToString();
            _dynamicScoreText.text = score.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Finish>())
        {
            _uiManager.WinScreen();
        }
    }
}
