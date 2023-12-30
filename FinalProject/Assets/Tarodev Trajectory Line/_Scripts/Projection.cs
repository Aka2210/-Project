using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour {
    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxPhysicsFrameIterations = 100; 
    [SerializeField] private Animator _animator;

    private Scene _simulationScene;
    private PhysicsScene _physicsScene;

    private void Start() {
        //�Ыؤ@�ӥu�����z�Ҳժ�����
        CreatePhysicsScene();
    }

    private void CreatePhysicsScene() {
        //�Ыس���
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();
    }

    private void Update() {

    }

    public void SimulateTrajectory(GameObject ThrowingObject, Vector3 pos, Vector3 velocity) {
        //�N�n��X������ƻs�쪫�z������
        var ghostObj = Instantiate(ThrowingObject, pos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);

        //�����@�����Y�y��
        ghostObj.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);

        _line.positionCount = _maxPhysicsFrameIterations;

        //�ھڧ��Y���ʵe����u�I���j�p
        if (_animator.GetBool("PowerThrow"))
        {
            _line.textureScale = new Vector2(0.166f, 0.33f);
        }
        else
            _line.textureScale = new Vector2(0.1f, 0.33f);

        //�N���Y���y��@�@�e�X
        for (var i = 0; i < _maxPhysicsFrameIterations; i++) {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, ghostObj.transform.position);

            //�p�G�I���h�������}(�N���u�@�J�쪫��N����)
            if (ghostObj.GetComponent<BirdCommonVar>().HasCollider || ghostObj.transform.position.y <= -1.5f)
            {
                _line.positionCount = i;
                break;
            }
        }

        Destroy(ghostObj.gameObject);
    }
}