using StarterAssets;
using UnityEngine;

//�k���ӱ�
public class Stair : MonoBehaviour
{
    float horizontal, vertical;
    [SerializeField] float _upSpeed;
    [SerializeField] GameObject _player;
    [SerializeField] ThirdPersonController _controller;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�����O�_���U�e���V��
        vertical = Input.GetAxisRaw("Vertical");
        //�����O�_���U���k��V��
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player"  && (vertical != 0 || horizontal != 0))
        {
            //�H���a���_�l�I�V�e�g�X�g�u�ç�I���쪺�����x�s�bhit
            Physics.Raycast(new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z), _player.transform.forward, out hit);
            Debug.Log(hit.collider.gameObject.name);
            //�p�G�I���쪺����=�ӱ�A�N���a���ۼӱ�A�Ϫ��a�V�W��
            if (hit.collider.gameObject == gameObject)
                _controller._verticalVelocity = _upSpeed;
        }
    }
}
