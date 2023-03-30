using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private int currentEnemyDamage = 15;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().TakeDamage(currentEnemyDamage);
        }
    }
}
