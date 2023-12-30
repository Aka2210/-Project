using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Vector3 _spawn;
    [SerializeField] GameObject _player;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_player.transform.position.y <= -10)
        {
            CharacterController controller = _player.GetComponent<CharacterController>();
            Vector3 currentPosition = _player.transform.position;
            // �q��e��m��s��m���V�q
            Vector3 offset = _spawn - currentPosition;
            // �ϥγo�ӦV�q����
            controller.Move(offset);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject, 1.0f);
    }
}
