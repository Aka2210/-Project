using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//����C���}�l�B�����έ���

public class GameManager : MonoBehaviour
{
    [SerializeField] Vector3 _spawn;
    [SerializeField] GameObject _pigs, _player, starsDisplay, scoreDisplay, levelDisplay, clear, E;
    float score;
    float maxPigsScore;

    private void Awake()
    {
        //��������e��
        clear.gameObject.active = false;
        //��l�e����
        score = 0;
        //�����ƹ�
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //�p���e���d�Ҧ��p�ަ��`�᪺����
        for(int i = 0; i < _pigs.transform.childCount; i++)
        {
            GameObject _pig = _pigs.transform.GetChild(i).gameObject;
            maxPigsScore += _pig.GetComponent<Rigidbody>().mass * 100;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //�}�l��0.5���s���ơA�]�������m���a���~�t�A���~�t�|�ɭP���󲣥ͱ����t�צӨϪ��a�[��
        Invoke("scoreReset", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //���ⱼ���᭫�^�D�q��
        if(_player.transform.position.y <= -10)
        {
            CharacterController controller = _player.GetComponent<CharacterController>();
            Vector3 currentPosition = _player.transform.position;
            // �q��e��m��s��m���V�q
            Vector3 offset = _spawn - currentPosition;
            // �ϥγo�ӦV�q����
            controller.Move(offset);
        }

        if(_pigs.transform.childCount == 0)
        {
            //�p�ެҦ��`��T��}�l����
            Invoke("end", 3.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //�p�ޭY���}�ĤH�q���d��h��q�k�s�A�������`
        if (other.gameObject.GetComponent<Pig>() != null)
        {
            other.gameObject.GetComponent<Pig>().explosiveDamage(float.MaxValue);
            pigDie(other.gameObject.GetComponent<Rigidbody>().mass);
        }
        Destroy(other.gameObject, 1.0f);
    }

    //�p�⪫�󲣥ͪ�����
    public void PropsScore(float velocity)
    {
        score += velocity;
    }

    //�p��p�ަ��`���ͪ�����
    public void pigDie(float mass)
    {
        score += mass * 100;
    }

    void scoreReset()
    {
        score = 0;
    }

    public void reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    //�^�������d�e��
    public void check()
    {
        SceneManager.LoadScene("ChooseLevelScene");
        Time.timeScale = 1;
    }

    void end()
    {
        Debug.Log(maxPigsScore);
        //UI�����ζ}��
        E.active = false;
        clear.gameObject.active = true;
        //�Ȱ��C�����z����
        Time.timeScale = 0;
        //�Ȱ���������
        _player.GetComponent<ThirdPersonController>().Stop = true;
        //�}�ҷƹ�
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //�p��P�P��
        for (int i = 0; i < starsDisplay.transform.childCount; i++)
        {
            if (score >= (i+3) * maxPigsScore)
                starsDisplay.transform.GetChild(i).gameObject.active = true;
            else
                break;
        }
        //���UI���d�B�������
        scoreDisplay.GetComponent<TextMeshProUGUI>().text = $"Score: {(int)score}";
        levelDisplay.GetComponent<TextMeshProUGUI>().text = $"{SceneManager.GetActiveScene().name}";
    }
}
