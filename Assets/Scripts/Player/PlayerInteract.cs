using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Vector3 interactSpot;
    [SerializeField] private float interactRadius;

    public void HandleInteract(bool interact)
    {
        if (interact)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.TransformPoint(interactSpot), interactRadius, interactableLayer);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<Interactable>(out Interactable inter))
                {
                    inter.Execute(); Debug.Log($"executed interaction on {collider.name}");
                }
            }
        }
    }
}
