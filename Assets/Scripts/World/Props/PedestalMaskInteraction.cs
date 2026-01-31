using UnityEngine;

public class PedestalMaskInteraction : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private int requiredMasksToActivate;
    [SerializeField] private AudioClip collectSound;

    public void Execute()
    {
        if (objectToActivate.activeInHierarchy)
            return;

        if (GameManager.MasksCollected >= requiredMasksToActivate)
        {
            objectToActivate.SetActive(true);
            AudioManager.Instance.PlaySfx3D(collectSound, transform.position + Vector3.up * 3f, 0.25f);
        }
    }
}
