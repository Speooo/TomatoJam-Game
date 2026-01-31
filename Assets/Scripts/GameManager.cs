using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyAreaEnterTrigger enemy1Trigger;
    [SerializeField] private EnemyAreaEnterTrigger enemy2Trigger;
    [SerializeField] private EnemyAreaEnterTrigger enemy3Trigger;

    [SerializeField] private EnemyController enemy1Controller;
    [SerializeField] private EnemyController enemy2Controller;
    [SerializeField] private EnemyController enemy3Controller;

    private int masksCollected = 0;

    private void Start()
    {
        ActiveMask.Instance.OnEnemyDied += OnEnemyDeath;

        if (enemy1Controller != null)
            enemy1Trigger.OnPlayerEnterTrigger += AreaEnterTrigger;
    }

    private void OnEnemyDeath()
    {
        masksCollected++; Debug.Log("player defeated the enemy and collected the mask");
    }

    private void AreaEnterTrigger(int index)
    {
        switch (index)
        {
            case 0:

                enemy1Controller.InitialiseEnemy();
                ActiveMask.Instance.BeginNewEnemyCombat(enemy1Controller.GetComponent<MaskHolder>());

                break;
            case 1:

                enemy2Controller.InitialiseEnemy();
                ActiveMask.Instance.BeginNewEnemyCombat(enemy2Controller.GetComponent<MaskHolder>());

                break;
            case 2:

                enemy3Controller.InitialiseEnemy();
                ActiveMask.Instance.BeginNewEnemyCombat(enemy3Controller.GetComponent<MaskHolder>());

                break;
        }
    }
}
