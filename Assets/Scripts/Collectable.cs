using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    [SerializeField] private UnityEvent OnPickUp;
    [SerializeField] private float sizeIncrease;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PowerUp(other.gameObject);
            Destroy(gameObject);
        }
    }

    public void PowerUp(GameObject player)
    {
        player.transform.localScale *= sizeIncrease;
    }
}
