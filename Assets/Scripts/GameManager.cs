using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyController enemy1Controller;
    [SerializeField] private EnemyController enemy2Controller;
    [SerializeField] private EnemyController enemy3Controller;

    [SerializeField] private EnemyAreaEnterTrigger enemy1Trigger;
    [SerializeField] private EnemyAreaEnterTrigger enemy2Trigger;
    [SerializeField] private EnemyAreaEnterTrigger enemy3Trigger;

    [SerializeField] private GameObject curtainClosed;
    [SerializeField] private GameObject doorClosed;

    public static int MasksCollected { get; private set; } = 0;

    private void Start()
    {
        ActiveMask.Instance.OnEnemyDied += OnEnemyDeath;

        enemy1Trigger.OnPlayerEnterTrigger += AreaEnterTrigger;
        enemy2Trigger.OnPlayerEnterTrigger += AreaEnterTrigger;
        enemy3Trigger.OnPlayerEnterTrigger += AreaEnterTrigger;
    }

    private void OnEnemyDeath()
    {
        MasksCollected++; Debug.Log("player defeated the enemy and collected the mask");

        switch (MasksCollected)
        {
            case 1:
                curtainClosed.SetActive(false);
                break;
            case 3:
                doorClosed.SetActive(false);
                break;
        }
    }

    private void AreaEnterTrigger(GameObject enemyPrefab, Vector3 spawnPoint)
    {
        switch (MasksCollected)
        {
            case 0:

                MaskHolder mask1 = enemy1Controller.GetComponent<MaskHolder>();
                ActiveMask.Instance.BeginNewEnemyCombat(mask1);
                enemy1Controller.InitialiseEnemy();

                break;
            case 1:

                MaskHolder mask2 = enemy2Controller.GetComponent<MaskHolder>();
                ActiveMask.Instance.BeginNewEnemyCombat(mask2);
                enemy2Controller.InitialiseEnemy();

                break;
            case 2:

                MaskHolder mask3 = enemy3Controller.GetComponent<MaskHolder>();
                ActiveMask.Instance.BeginNewEnemyCombat(mask3);
                enemy3Controller.InitialiseEnemy();

                break;
        }
    }
}
