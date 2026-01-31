using UnityEngine;

public class ActivateObjectInteraction : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject objectToActivate;

    public void Execute()
    {
        objectToActivate.SetActive(true);
    }
}
