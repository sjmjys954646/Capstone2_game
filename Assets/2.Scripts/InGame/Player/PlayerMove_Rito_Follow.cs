using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_Rito_Follow : MonoBehaviour
{
    //https://rito15.github.io/posts/unity-fps-tps-character/#source-code
    /***********************************************************************
    *                               Definitions
    ***********************************************************************/
    #region .
    public enum CameraType { FpCamera, TpCamera };

    [Serializable]
    public class Components
    {
        public Camera tpCamera;
        public Camera fpCamera;

        [HideInInspector] public Transform tpRig;
        [HideInInspector] public Transform fpRoot;
        [HideInInspector] public Transform fpRig;

        [HideInInspector] public GameObject tpCamObject;
        [HideInInspector] public GameObject fpCamObject;

        [HideInInspector] public Rigidbody rBody;
        [HideInInspector] public Animator anim;
    }
    [Serializable]
    public class KeyOption
    {
        public KeyCode moveForward = KeyCode.W;
        public KeyCode moveBackward = KeyCode.S;
        public KeyCode moveLeft = KeyCode.A;
        public KeyCode moveRight = KeyCode.D;
        public KeyCode run = KeyCode.LeftShift;
        public KeyCode jump = KeyCode.Space;
        public KeyCode switchCamera = KeyCode.Z;
        public KeyCode showCursor = KeyCode.LeftAlt;
    }
    [Serializable]
    public class MovementOption
    {
        [Range(1f, 10f), Tooltip("�̵��ӵ�")]
        public float speed = 3f;
        [Range(1f, 3f), Tooltip("�޸��� �̵��ӵ� ���� ���")]
        public float runningCoef = 1.5f;
        [Range(1f, 10f), Tooltip("���� ����")]
        public float jumpForce = 5.5f;
        [Range(1f, 10f), Tooltip("���ӵ�")]
        public float acceleration = 2f;
        [Tooltip("�������� üũ�� ���̾� ����")]
        public LayerMask groundLayerMask = 6;
    }
    [Serializable]
    public class CameraOption
    {
        [Tooltip("���� ���� �� ī�޶�")]
        public CameraType initialCamera;
        [Range(1f, 10f), Tooltip("ī�޶� �����¿� ȸ�� �ӵ�")]
        public float rotationSpeed = 2f;
        [Range(-90f, 0f), Tooltip("�÷��ٺ��� ���� ����")]
        public float lookUpDegree = -60f;
        [Range(0f, 75f), Tooltip("�����ٺ��� ���� ����")]
        public float lookDownDegree = 75f;
    }
    [Serializable]
    public class AnimatorOption
    {
        public string paramMoveX = "Move X";
        public string paramMoveY = "Move Y";
        public string paramMoveZ = "Move Z";
    }
    [Serializable]
    public class CharacterState
    {
        public bool isCurrentFp;
        public bool isMoving;
        public bool isRunning;
        public bool isGrounded;
        public bool isCursorActive;
    }

    #endregion
    /***********************************************************************
    *                               Fields, Properties
    ***********************************************************************/
    #region .
    public Components Com => _components;
    public KeyOption Key => _keyOption;
    public MovementOption MoveOption => _movementOption;
    public CameraOption CamOption => _cameraOption;
    public AnimatorOption AnimOption => _animatorOption;
    public CharacterState State => _state;

    [SerializeField] private Components _components = new Components();
    [Space]
    [SerializeField] private KeyOption _keyOption = new KeyOption();
    [Space]
    [SerializeField] private MovementOption _movementOption = new MovementOption();
    [Space]
    [SerializeField] private CameraOption _cameraOption = new CameraOption();
    [Space]
    [SerializeField] private AnimatorOption _animatorOption = new AnimatorOption();
    [Space]
    [SerializeField] private CharacterState _state = new CharacterState();

    private Vector3 _moveDir;
    private Vector3 _worldMove;
    private Vector2 _rotation;

    [SerializeField]
    private float _distFromGround;

    #endregion

    /***********************************************************************
    *                               Unity Events
    ***********************************************************************/
    #region .
    private void Awake()
    {
        InitComponents();
        InitSettings();
    }

    private void Update()
    {
        //CameraViewToggle();
        //SetValuesByKeyInput();
        //ShowCursorToggle();
        //CheckDistanceFromGround();
        //Rotate();
        //Move();
        //Jump();
    }

    #endregion
    /***********************************************************************
    *                               Init Methods
    ***********************************************************************/
    #region .
    private void InitComponents()
    {
        LogNotInitializedComponentError(Com.tpCamera, "TP Camera");
        LogNotInitializedComponentError(Com.fpCamera, "FP Camera");
        TryGetComponent(out Com.rBody);
        Com.anim = GetComponentInChildren<Animator>();

        Com.tpCamObject = Com.tpCamera.gameObject;
        Com.tpRig = Com.tpCamera.transform.parent;
        Com.fpCamObject = Com.fpCamera.gameObject;
        Com.fpRig = Com.fpCamera.transform.parent;
        Com.fpRoot = Com.fpRig.parent;
    }
    private float _groundCheckRadius;
    private void InitSettings()
    {
        // Rigidbody
        if (Com.rBody)
        {
            // ȸ���� Ʈ�������� ���� ���� ������ ���̱� ������ ������ٵ� ȸ���� ����
            Com.rBody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        // Camera
        var allCams = FindObjectsOfType<Camera>();
        foreach (var cam in allCams)
        {
            cam.gameObject.SetActive(false);
        }
        // ������ ī�޶� �ϳ��� Ȱ��ȭ
        State.isCurrentFp = (CamOption.initialCamera == CameraType.FpCamera);
        Com.fpCamObject.SetActive(State.isCurrentFp);
        Com.tpCamObject.SetActive(!State.isCurrentFp);

        TryGetComponent(out CapsuleCollider cCol);
        _groundCheckRadius = cCol ? cCol.radius : 0.1f;
    }

    #endregion
    /***********************************************************************
    *                               Checker Methods
    ***********************************************************************/
    #region .
    private void LogNotInitializedComponentError<T>(T component, string componentName) where T : Component
    {
        if (component == null)
            Debug.LogError($"{componentName} ������Ʈ�� �ν����Ϳ� �־��ּ���");
    }

    #endregion
    /***********************************************************************
    *                               Methods
    ***********************************************************************/
    #region .
    public void CameraViewToggle()
    {
        if (Input.GetKeyDown(Key.switchCamera))
        {
            CameraViewToggleInside();
        }
    }

    public void CameraViewToggleInside()
    {
        State.isCurrentFp = !State.isCurrentFp;
        Com.fpCamObject.SetActive(State.isCurrentFp);
        Com.tpCamObject.SetActive(!State.isCurrentFp);

        // TP -> FP
        if (State.isCurrentFp)
        {
            Vector3 tpEulerAngle = Com.tpRig.eulerAngles;
            Com.fpRoot.eulerAngles = Vector3.right * tpEulerAngle.x;
            Com.fpRoot.eulerAngles = Vector3.up * tpEulerAngle.y;
        }
        // FP -> TP
        else
        {
            Vector3 newRot = default;
            newRot.x = Com.fpRoot.eulerAngles.x;
            newRot.y = Com.fpRoot.eulerAngles.y;
            Com.tpRig.eulerAngles = newRot;
        }
    }

    public void SetValuesByKeyInput()
    {
        float h = 0f, v = 0f;

        if (Input.GetKey(Key.moveForward)) v += 1.0f;
        if (Input.GetKey(Key.moveBackward)) v -= 1.0f;
        if (Input.GetKey(Key.moveLeft)) h -= 1.0f;
        if (Input.GetKey(Key.moveRight)) h += 1.0f;

        Vector3 moveInput = new Vector3(h, 0f, v).normalized;
        _moveDir = Vector3.Lerp(_moveDir, moveInput, MoveOption.acceleration); // ����, ����
        _rotation = new Vector2(Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y"));

        State.isMoving = _moveDir.sqrMagnitude > 0.01f;
        State.isRunning = Input.GetKey(Key.run);
    }

    public void Rotate()
    {
        if (State.isCurrentFp)
        {
            if (!State.isCursorActive)
                RotateFP();
        }
        else
        {
            if (!State.isCursorActive)
                RotateTP();
            //RotateFPRoot();
        }
    }

    /// <summary> 1��Ī ȸ�� </summary>
    public void RotateFP()
    {
        float deltaCoef = Time.deltaTime * 50f;

        // ���� : FP Rig ȸ��
        float xRotPrev = Com.fpRig.localEulerAngles.x;
        float xRotNext = xRotPrev + _rotation.y
            * CamOption.rotationSpeed * deltaCoef;

        if (xRotNext > 180f)
            xRotNext -= 360f;

        // �¿� : FP Root ȸ��
        float yRotPrev = Com.fpRoot.localEulerAngles.y;
        float yRotNext =
            yRotPrev + _rotation.x
            * CamOption.rotationSpeed * deltaCoef;

        // ���� ȸ�� ���� ����
        bool xRotatable =
            CamOption.lookUpDegree < xRotNext &&
            CamOption.lookDownDegree > xRotNext;

        // FP Rig ���� ȸ�� ����
        Com.fpRig.localEulerAngles = Vector3.right * (xRotatable ? xRotNext : xRotPrev);

        // FP Root �¿� ȸ�� ����
        Com.fpRoot.localEulerAngles = Vector3.up * yRotNext;
    }

    /// <summary> 3��Ī ȸ�� </summary>
    public void RotateTP()
    {
        float deltaCoef = Time.deltaTime * 50f;

        // ���� : TP Rig ȸ��
        float xRotPrev = Com.tpRig.localEulerAngles.x;
        float xRotNext = xRotPrev + _rotation.y
            * CamOption.rotationSpeed * deltaCoef;

        if (xRotNext > 180f)
            xRotNext -= 360f;

        // �¿� : TP Rig ȸ��
        float yRotPrev = Com.tpRig.localEulerAngles.y;
        float yRotNext =
            yRotPrev + _rotation.x
            * CamOption.rotationSpeed * deltaCoef;

        // ���� ȸ�� ���� ����
        bool xRotatable =
            CamOption.lookUpDegree < xRotNext &&
            CamOption.lookDownDegree > xRotNext;

        Vector3 nextRot = new Vector3
        (
            xRotatable ? xRotNext : xRotPrev,
            yRotNext,
            0f
        );

        // TP Rig ȸ�� ����
        Com.tpRig.localEulerAngles = nextRot;
    }

    /// <summary> 3��Ī�� ��� FP Root ȸ�� </summary>
    public void RotateFPRoot()
    {
        if (State.isMoving == false) return;

        Vector3 dir = Com.tpRig.TransformDirection(_moveDir);
        float currentY = Com.fpRoot.localEulerAngles.y;
        float nextY = Quaternion.LookRotation(dir, Vector3.up).eulerAngles.y;

        if (nextY - currentY > 180f) nextY -= 360f;
        else if (currentY - nextY > 180f) nextY += 360f;

        Com.fpRoot.eulerAngles = Vector3.up * Mathf.Lerp(currentY, nextY, 0.1f);
    }

    public void Move()
    {
        // �̵����� �ʴ� ���, �̲��� ����
        if (State.isMoving == false)
        {
            Com.rBody.velocity = new Vector3(0f, Com.rBody.velocity.y, 0f);
            return;
        }

        // ���� �̵� ���� ���
        // 1��Ī
        if (State.isCurrentFp)
        {
            _worldMove = Com.fpRoot.TransformDirection(_moveDir);
        }
        // 3��Ī
        else
        {
            _worldMove = Com.tpRig.TransformDirection(_moveDir);
        }

        _worldMove *= (MoveOption.speed) * (State.isRunning ? MoveOption.runningCoef : 1f);

        // Y�� �ӵ��� �����ϸ鼭 XZ��� �̵�
        Com.rBody.velocity = new Vector3(_worldMove.x, Com.rBody.velocity.y, _worldMove.z);
    }

    public void ShowCursorToggle()
    {
        if (Input.GetKeyDown(Key.showCursor))
            State.isCursorActive = !State.isCursorActive;

        ShowCursor(State.isCursorActive);
    }

    public void ShowCursor(bool value)
    {
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }
    public void CheckDistanceFromGround()
    {
        Vector3 ro = transform.position + Vector3.up;
        Vector3 rd = Vector3.down;
        Ray ray = new Ray(ro, rd);

        const float rayDist = 500f;
        const float threshold = 0.01f;

        bool cast =
            Physics.SphereCast(ray, _groundCheckRadius, out var hit, rayDist, MoveOption.groundLayerMask);


        _distFromGround = cast ? (hit.distance - 2f + _groundCheckRadius) : float.MaxValue;
        State.isGrounded = _distFromGround <= _groundCheckRadius + threshold;
    }

    public void Jump()
    {
        if (!State.isGrounded) return;

        if (Input.GetKeyDown(Key.jump))
        {
            Com.rBody.AddForce(Vector3.up * MoveOption.jumpForce, ForceMode.VelocityChange);
        }
    }

    #endregion
}