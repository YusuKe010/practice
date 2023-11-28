using UnityEngine;

public class Deploy : MonoBehaviour
{
    [SerializeField] GameObject[] _character;
    GameObject _currentCharacter;
    void Update()
    {
        RayDeploy(_currentCharacter);
    }

    private void RayDeploy(GameObject gameObject)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            gameObject.transform.position = hit.point;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition));
    }
}
