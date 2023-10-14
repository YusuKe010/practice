using UnityEngine;

/// <summary>
/// ����I�ȃL�����N�^�[����X�L�[�}����������B
/// �u�J�������猩�������v�ɃL�����N�^�[�uRigidbody.AddForce ���g���āv�������B
/// �~�܂鎞�̌������ Rigidbody.Drag �� Physics Material �Œ�������B
/// </summary>
public class CharacterMoveAddForce : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3;
    Rigidbody _rb = default;
    RaycastHit hit;

    [SerializeField] float _jumpPower = 10;



    [SerializeField]
    float groundCheckRadius = 0.4f;
    [SerializeField]
    float groundCheckOffsetY = 0.45f;
    [SerializeField]
    float groundCheckDistance = 0.2f;
    [SerializeField]
    LayerMask groundLayers = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = Vector3.forward * v + Vector3.right * h;
        // �J�����̃��[�J�����W�n����� dir ��ϊ�����
        dir = Camera.main.transform.TransformDirection(dir);
        // �J�����͎΂߉��Ɍ����Ă���̂ŁAY ���̒l�� 0 �ɂ��āuXZ ���ʏ�̃x�N�g���v�ɂ���
        dir.y = 0;
        // �ړ��̓��͂��Ȃ����͉�]�����Ȃ��B���͂����鎞�͂��̕����ɃL�����N�^�[��������B
        if (dir != Vector3.zero) this.transform.forward = dir;
        _rb.velocity = dir.normalized * _moveSpeed;
        //�n�ʔ��� & �W�����v
        Ray ray = new Ray(transform.position, Vector3.down * 1.3f);
        Debug.DrawRay(transform.position, Vector3.down * 1.3f);
        if ( Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        _rb.AddForce(_jumpPower * Vector3.up, ForceMode.Impulse);
    }

    bool CheckGroundStatus()
    {
        return Physics.SphereCast(transform.position + groundCheckOffsetY * Vector3.down, groundCheckRadius, Vector3.down, out hit, groundCheckDistance, groundLayers, QueryTriggerInteraction.Ignore);
    }
}
