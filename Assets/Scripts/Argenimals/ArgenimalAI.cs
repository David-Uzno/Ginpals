using UnityEngine;

public class ArgenimalAI : MonoBehaviour
{
    [SerializeField] private float _attackRange = 1.5f;
    private float _speed;
    [SerializeField] private bool _isWild = false;
    private Transform _targetEnemy;

    [Header("Dependencies")]
    [SerializeField] private ArgenimalData _argenimalData;

    private void Awake()
    {
        if (_argenimalData != null)
        {
            _speed = _argenimalData.MoveSpeed;
        }
        else
        {
            _speed = 1f;
            Debug.LogError("ArgenimalData no está asignado en " + name);
        }
    }

    private void FixedUpdate()
    {
        FindClosestEnemy();

        if (_targetEnemy != null)
        {
            float enemyDistance = Vector3.Distance(transform.position, _targetEnemy.position);
            if (enemyDistance > _attackRange)
            {
                // Mover hacia el enemigo
                transform.position = Vector3.MoveTowards(transform.position, _targetEnemy.position, _speed * Time.fixedDeltaTime);
            }
            else
            {
                Debug.Log($"{name} ataca a {_targetEnemy.name}");
            }
        }
    }

    private void FindClosestEnemy()
    {
        string targetTag;
        if (_isWild)
        {
            targetTag = "PlayerTeam";
        }
        else
        {
            targetTag = "EnemyTeam";
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        float closestDistance = Mathf.Infinity;
        _targetEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                _targetEnemy = enemy.transform;
            }
        }
    }
}
