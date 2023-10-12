using UnityEngine;

/// <summary>
/// 現代的なキャラクター操作スキーマを実現する。
/// 「カメラから見た方向」にキャラクター「Rigidbody.AddForce を使って」動かす。
/// 止まる時の減速具合は Rigidbody.Drag か Physics Material で調整する。
/// </summary>
public class CharacterMoveAddForce : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3;
    Rigidbody _rb = default;
    RaycastHit hit;

    [SerializeField] float _jumpPower = 10;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = Vector3.forward * v + Vector3.right * h;
        // カメラのローカル座標系を基準に dir を変換する
        dir = Camera.main.transform.TransformDirection(dir);
        // カメラは斜め下に向いているので、Y 軸の値を 0 にして「XZ 平面上のベクトル」にする
        dir.y = 0;
        // 移動の入力がない時は回転させない。入力がある時はその方向にキャラクターを向ける。
        if (dir != Vector3.zero) this.transform.forward = dir;
        _rb.velocity = dir.normalized * _moveSpeed;


        //地面判定 & ジャンプ
        Ray ray = new Ray(transform.position, Vector3.down * 1.3f);
        Debug.DrawRay(transform.position, Vector3.down * 1.3f);
        if (Physics.SphereCast(ray, 0.3f, out hit) && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        _rb.AddForce(_jumpPower * Vector3.up, ForceMode.Impulse);
    }

}
