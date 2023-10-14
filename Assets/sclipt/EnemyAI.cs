using System.Collections;
using Unity.VisualScripting;
using UnityEngine;



public class EnemyAI : MonoBehaviour
{
    Rigidbody _rb;

    public enum EnemyAiState
    {
        MOVE,
        ATTACK,
        IDLE,
        REMOVE
    }
    public EnemyAiState aiState = EnemyAiState.IDLE;


}
