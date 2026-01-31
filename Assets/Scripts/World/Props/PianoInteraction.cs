using UnityEngine;

public class PianoInteraction : MonoBehaviour, Interactable
{
    [SerializeField] private AudioClip piano1;
    [SerializeField] private AudioClip piano2;

    public void Execute()
    {
        int random = Random.Range(0, 2);

        if (random == 0)
            AudioManager.Instance.PlaySfx3D(piano1, transform.position, 0.5f);
        else
            AudioManager.Instance.PlaySfx3D(piano2, transform.position, 0.5f);
    }
}
