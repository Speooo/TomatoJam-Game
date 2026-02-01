using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 30;
    [SerializeField] private float healthRegenMultiplier;
    [SerializeField] private GameObject diedText;

    private float currentHealth;

    private PlayerInput input;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();

        currentHealth = maxHealth;
    }

    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth + Time.deltaTime * healthRegenMultiplier, 0f, maxHealth);
    }

    public void ReceiveHealthDamage(float damage)
    {
        if (currentHealth > 0f)
        {
            currentHealth -= damage;

            if (currentHealth <= 0f)
            {
                currentHealth = 0f;
                Die();
            }
        }
    }

    public void Die()
    {
        input.LockInput();
        StartCoroutine(TriggerDeath());
    }

    private IEnumerator TriggerDeath()
    {
        diedText.SetActive(true);

        yield return new WaitForSeconds(5f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(2);
    }
}
