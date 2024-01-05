using Cinemachine;
using UnityEngine;

//����O�_�e�X�ߪ��u
public class DrawParabola : MonoBehaviour
{
    //�ѼƧ���B�ŧi�A�N�q�P��W�١A�䤤ThrowingObject�N���e���Y�����BThrowingOrient�N���ecamera�����A_projection�N���g�p�⪺�禡
    LineRenderer lineRenderer;
    float ThrowPowerX, ThrowPowerY;
    public Animator animator;
    [SerializeField] private Projection _projection;
    GameObject egg, ThrowingObject;
    Transform ThrowingOrient;
    public Vector3 ThrowPoint;

    void Start()
    {
        //����U�Ѽ�
        lineRenderer = GetComponent<LineRenderer>();
        egg = GetComponentInParent<ThrowControllor>().egg;
        ThrowingObject = GetComponentInParent<ThrowControllor>().ThrowingObject;
        ThrowingOrient = GetComponentInParent<ThrowControllor>().ThrowingOrient;
        ThrowPowerX = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerX;
        ThrowPowerY = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerY;
    }

    private void LateUpdate()
    {
        //������e���A�A�Y�B����Y�e���é|���i�J���Y�ʵe�B���e���Y�����w�g�����h�]�w�ѼƨçQ��_projection����function�e�X�ߪ��u
        if (GetComponent<CinemachineVirtualCamera>().Priority == 100 && !animator.GetBool("Throw") && GetComponentInParent<ThrowControllor>().clonedObject == null)
        {
            ThrowPowerX = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerX;
            ThrowPowerY = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerY;

            //���Y��m�H��e�k�⤤�����J�����I���e���W�貾�ʨϨ���Y��m�]�m�������Y���e��
            ThrowPoint = egg.transform.position;
            ThrowPoint.x += 0.069f;
            ThrowPoint.y += 0.961f;
            ThrowPoint.z += 0.212f;

            ThrowingObject = GetComponentInParent<ThrowControllor>().ThrowingObject;
            _projection.SimulateTrajectory(ThrowingObject, ThrowPoint, ThrowingOrient.forward * ThrowPowerX + ThrowingOrient.up * ThrowPowerY);
        }
        else
        {
            //�Y���ŦX����h�����ߪ��u���
            lineRenderer.positionCount = 0;
        }
    }
}
