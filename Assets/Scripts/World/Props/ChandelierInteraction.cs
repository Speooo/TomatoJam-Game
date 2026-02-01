using UnityEngine;

public class ChandelierInteraction : MonoBehaviour, Interactable
{
    [SerializeField] private ChandelierDropQueue chandelier;
    public void Execute()
    {
        chandelier.Initialise();
    }
}
