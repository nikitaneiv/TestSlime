using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Transform _target;
    [SerializeField] private CountDamage _countDamage;
    [SerializeField] private GameObject _weapon;
    
    private Animator animator;
    private NavMeshAgent myAgent;
    private PlayerController playerController;
    
    private int maxHealth = 100;
    private int currentHealth;
    private int scoreAfterDead = 100;
    
    private float distance;

    private const string Attack = "EnemyAnimation";
    
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        myAgent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        distance = Vector3.Distance(_target.position, transform.position);
        if (distance > 20f)
        {
            myAgent.enabled = false;
        }

        if (distance < 20f && distance > 1.5f)
        {
            myAgent.enabled = true;
            myAgent.SetDestination(_target.position);
            transform.LookAt(_target);
        }

        if (distance <= 1.5f)
        {
            myAgent.enabled = false;
            _weapon.SetActive(true);
            animator.Play(Attack);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Bullet>())
        {
            _countDamage.SetDamageValue(playerController.CurrentPlayerDamage);
            currentHealth -= playerController.CurrentPlayerDamage;
            _healthBar.SetHealthValue(currentHealth,maxHealth);
            if (currentHealth <= 0)
            {
                playerController.AddScore(scoreAfterDead);
                gameObject.SetActive(false);
            }
        }
    }
}
