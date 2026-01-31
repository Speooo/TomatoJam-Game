using UnityEngine;

public class Mask : MonoBehaviour
{
    public bool permanentlyOwned;

    private void FixedUpdate()
    {
        transform.position = ActiveMask.Instance.CurrentMaskHolder.transform.position + Vector3.up * 3f;
    }
}
