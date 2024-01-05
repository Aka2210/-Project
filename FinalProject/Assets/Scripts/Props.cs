using UnityEngine;

//���ĳ]�w
public class Props : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    private void Awake()
    {
        //Awake���ɭԥ��������ġA0.5���A�}�ҡA�Ψ��קK�j���������m�~�t�ɭP������p�d��L���y���n������
        if (gameObject.GetComponent<AudioSource>() != null)
        {
            gameObject.GetComponent<AudioSource>().mute = true;
            Invoke("openMusic", 0.5f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�p�⪫��t�פ�
        if(gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.2)
            _gameManager.PropsScore(gameObject.GetComponent<Rigidbody>().velocity.magnitude);
    }

    //�I���o�X����
    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.GetComponent<AudioSource>() != null)
            gameObject.GetComponent<AudioSource>().Play();
    }

    void openMusic()
    {
        gameObject.GetComponent<AudioSource>().mute = false;
    }
}
