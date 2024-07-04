using UnityEngine;

public class PipeDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Pipe Root"))
        {
            return;
        }
        
        Destroy(other.gameObject);

        PipeSpawner.Instance.SpawnPipe();
    }
}