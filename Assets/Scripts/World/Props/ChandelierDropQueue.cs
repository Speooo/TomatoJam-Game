using UnityEngine;

public class ChandelierDropQueue : MonoBehaviour
{
    [SerializeField] private float fallSpeed;
    [SerializeField] private GameObject explosion;
    [SerializeField] private AudioClip explosionSound;

    private bool isInitialised = false;
    private bool hasSmashed = false;
    private float timer = 1f;
    [ContextMenu("Initialise")]
    public void Initialise()
    {
        isInitialised = true;
    }

    private void Update()
    {
        if (isInitialised)
        {
            transform.position += fallSpeed * Time.deltaTime * Vector3.down;

            timer -= Time.deltaTime;
            if (timer < 0f && !hasSmashed)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                AudioManager.Instance.PlaySfx3D(explosionSound, transform.position, 1f);
                hasSmashed = true;
            }
        }
    }
}
