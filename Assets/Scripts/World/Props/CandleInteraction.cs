using UnityEngine;

public class CandleInteraction : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private AudioClip lighter;

    public void Execute()
    {
        if (objectToActivate.activeInHierarchy)
            return;

        objectToActivate.SetActive(true);
        AudioManager.Instance.PlaySfx3D(lighter, transform.position);
    }
}
