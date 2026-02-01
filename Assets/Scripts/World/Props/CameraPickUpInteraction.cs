using UnityEngine;

public class CameraPickUpInteraction : MonoBehaviour, Interactable
{
    private PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void Execute()
    {
        player.PickUpCamera();
        Destroy(gameObject);
    }
}
