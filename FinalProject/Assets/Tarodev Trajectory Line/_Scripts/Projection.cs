using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour {
    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxPhysicsFrameIterations = 100;
    [SerializeField] private Transform _obstaclesParent;
    [SerializeField] private Animator _animator;

    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();

    private void Start() {
        //�Ыؤ@�ӥu�����z�Ҳժ�����
        CreatePhysicsScene();
    }

    private void CreatePhysicsScene() {
        //�Ыس���
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();

        //�N��e������_obstaclesParent��������v�@�ͦ���������V(�קK��O�Ӧh�į�)�A�M���J�Ыت����z�������A�p�G����Dstatic(�N��i�H���z�@��)�A�N���J�r�夤
        foreach (Transform obj in _obstaclesParent) {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            if(ghostObj.GetComponent<Renderer>() != null)
                ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            if (!ghostObj.isStatic) _spawnedObjects.Add(obj, ghostObj.transform);
        }
    }

    private void Update() {
        //�N�r�夤�������X�A�@�@�N���m��אּ���������m
        foreach (var item in _spawnedObjects) {
            item.Value.position = item.Key.position;
            item.Value.rotation = item.Key.rotation;
        }
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
            _line.textureScale = new Vector2(0.5f, 1);
        else
            _line.textureScale = new Vector2(0.3f, 1);

        //�N���Y���y��@�@�e�X
        for (var i = 0; i < _maxPhysicsFrameIterations; i++) {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, ghostObj.transform.position);

            //�p�G�I���h�������}(�N���u�@�J�쪫��N����)
            if (ghostObj.GetComponent<BirdCommonVar>().HasCollider)
            {
                _line.positionCount = i;
                break;
            }
        }

        Destroy(ghostObj.gameObject);
    }
}