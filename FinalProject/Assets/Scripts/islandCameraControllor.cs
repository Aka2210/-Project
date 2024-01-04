using Cinemachine;
using UnityEngine;

//�q���۾��A�H���b�S�w�ާ@������q���A�T�w�ާ@�ĪG
public class islandCameraControllor : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _islandCamera;
    [SerializeField] ThrowControllor _throwControllor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //��R��YclonedObject�s�b�h�N���e�����Q���Y�A�������e��R�|�ഫ����������
        if (Input.GetKeyUp(KeyCode.R) && _throwControllor != null && _throwControllor.clonedObject != null)
        {
            openCamera(_throwControllor.clonedObject, 5.0f, false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        openCamera(other.gameObject, 3f, false);
    }

    public void openCamera(GameObject collider, float delay, bool TNT)
    {
        //�p�Gcollider�O�p���άOTNT�۰�Ĳ�o���|�������ର��������
        if ((collider.tag == "Bird" || TNT))
        {
            _islandCamera.Priority = 10000;
            Invoke("closeCamera", delay);
        }
    }

    //���������q���۾�
    void closeCamera()
    {
        _islandCamera.Priority = 0;
    }
}
