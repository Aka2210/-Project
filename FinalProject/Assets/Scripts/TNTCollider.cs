using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTCollider : MonoBehaviour
{
    [SerializeField] float triggerForce;
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionForce;
    [SerializeField] GameObject particles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�p�G�I�����O>=Ĳ�oTNT�һݪ��O�h�i��TNT�z���{���X
        if(collision.relativeVelocity.magnitude >= triggerForce)
        {
            //�Q��Physics����Htransform.position������, explosionRadius���b�|�����d�򤺩Ҧ�������
            var surroundingObject = Physics.OverlapSphere(transform.position, explosionRadius);
            
            foreach(var obj in surroundingObject)
            {
                var rb = obj.GetComponent<Rigidbody>();
                if (rb == null) continue;

                //�����Ҧ����񪫥�@�z�}�O(�����z�����O)�A�ѼƤ��O��(�z���O�q, �z�������I, �z���b�|)
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            //�Ыئ������z���ɤl�ĪG
            GameObject explosive = Instantiate(particles, transform.position, Quaternion.identity);

            //�R��TNT����(�ϥ�gameObject�s�P���B�l����@�_�R��)
            Destroy(gameObject);
            //����R���z���Ϊ��ɤl�ĪG�A�קK���|�Ӧh�ɭP�d�y
            Destroy(explosive, 2);
        }
    }
}
