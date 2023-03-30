using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 1.5f);
        }
        if (other.gameObject.GetComponent<Plane>())
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 1.5f);
        }
        
    }
}