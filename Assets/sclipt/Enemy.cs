using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyAiState
    {
        MOVE,
        ATTACK,
        IDLE,
        REMOVE
    }
    public EnemyAiState aiState = EnemyAiState.IDLE;

    Rigidbody _rb;
    Transform _enemyTransform;
    [SerializeField] float speed = 3.0f;
    [SerializeField] float _searchRange = 10f;
    CharacterMoveAddForce player;
    private void Start()
    {
        player = FindObjectOfType<CharacterMoveAddForce>();
        _rb = GetComponent<Rigidbody>();
        _enemyTransform = this.transform;
    }

    private void Update()
    {
        setAi();
        UpdateAI();
    }

    void UpdateAI()
    {
        switch (aiState)
        {
            case EnemyAiState.MOVE:
                Move(); break;
            case EnemyAiState.ATTACK:
                Attack(); break;
            case EnemyAiState.IDLE:
                Idle(); break;
            case EnemyAiState.REMOVE:
                Remove(); break;
        }

        StartCoroutine("AiWaitTimer");
    }

    void setAi()
    {
        float dis = Vector3.Distance(this.transform.position, player.transform.position);
        if (dis <= _searchRange)
        {
            aiState = EnemyAiState.MOVE;
        }
        else
        {
            aiState = EnemyAiState.IDLE;
        }
    }

    void Move()
    {
        _rb.velocity = ( player.transform.position - transform.position).normalized * speed;
    }

    void Idle()
    {
        _rb.velocity = Vector3.zero;
    }

    void Attack()
    {

    }

    void Remove()
    {
        _rb.velocity = _enemyTransform.position - this.transform.position;
    }

    IEnumerator AiWaitTimer()
    {
        yield return null;
    }
}
