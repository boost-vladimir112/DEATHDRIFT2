using System.Collections;
using System.Collections.Generic;
using UI_Inputs;
using UI_InputSystem.Base;
using UnityEngine;
using YG;
using PG;


namespace EvolveGames
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public Transform CameraParent;
        public Transform CameraParentInCar;
        public PG.PlayerController PlayerControllerForCar;
        public System.Action<CarController> OnExitAction;

        [Header("PlayerController")]
        [SerializeField] public Transform Camera;
      //  [SerializeField] public ItemChange Items;
        [SerializeField, Range(1, 10)] float walkingSpeed = 3.0f;
        [Range(0.1f, 5)] public float CroughSpeed = 1.0f;
        [SerializeField, Range(2, 20)] float RuningSpeed = 4.0f;
        [SerializeField, Range(0, 20)] float jumpSpeed = 6.0f;
        [SerializeField, Range(0.5f, 10)] public float lookSpeed = 2.0f;
        [SerializeField, Range(10, 120)] float lookXLimit = 80.0f;
        [Space(20)]
        [Header("Advance")]
        [SerializeField] float RunningFOV = 65.0f;
        [SerializeField] float SpeedToFOV = 4.0f;
        [SerializeField] float CroughHeight = 1.0f;
        [SerializeField] float gravity = 20.0f;
        [SerializeField] float timeToRunning = 2.0f;
        [HideInInspector] public bool canMove = true;
        [HideInInspector] public bool CanRunning = true;

        [Space(20)]
        [Header("Climbing")]
        [SerializeField] bool CanClimbing = true;
        [SerializeField, Range(1, 25)] float Speed = 2f;
        bool isClimbing = false;

        [Space(20)]
        [Header("HandsHide")]
        [SerializeField] bool CanHideDistanceWall = true;
        [SerializeField, Range(0.1f, 5)] float HideDistance = 1.5f;
        [SerializeField] int LayerMaskInt = 1;

        [Space(20)]
        [Header("Input")]
        [SerializeField] KeyCode CroughKey = KeyCode.LeftControl;


        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Vector3 moveDirection = Vector3.zero;
        bool isCrough = false;
        float InstallCroughHeight;
        float rotationX = 0;
        [HideInInspector] public bool isRunning = false;
        Vector3 InstallCameraMovement;
        float InstallFOV;
        Camera cam;
        [HideInInspector] public bool Moving;
        [HideInInspector] public float vertical;
        [HideInInspector] public float horizontal;
        [HideInInspector] public float Lookvertical;
        [HideInInspector] public float Lookhorizontal;
        float RunningValue;
        float installGravity;
        bool WallDistance;
        [HideInInspector] public float WalkingValue;
        void Start()
        {
     
            characterController = GetComponent<CharacterController>();
           // if (Items == null && GetComponent<ItemChange>()) Items = GetComponent<ItemChange>();
            cam = GetComponentInChildren<Camera>();
         
            InstallCroughHeight = characterController.height;
            InstallCameraMovement = Camera.localPosition;
            InstallFOV = cam.fieldOfView;
            RunningValue = RuningSpeed;
            installGravity = gravity;
            WalkingValue = walkingSpeed;
        }

        void Update()
        {
            RaycastHit CroughCheck;
            RaycastHit ObjectCheck;

            if (!characterController.isGrounded && !isClimbing)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            var joystickMovement = UIInputSystem.ME.GetAxis(JoyStickAction.Movement);

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            isRunning = !isCrough ? CanRunning ? Input.GetKey(KeyCode.LeftShift) : false : false;
            vertical = canMove ? (isRunning ? RunningValue : WalkingValue) * (Input.GetAxis("Vertical") + joystickMovement.y) : 0;
            horizontal = canMove ? (isRunning ? RunningValue : WalkingValue) * (Input.GetAxis("Horizontal") + joystickMovement.x) : 0;
            if (isRunning) RunningValue = Mathf.Lerp(RunningValue, RuningSpeed, timeToRunning * Time.deltaTime);
            else RunningValue = WalkingValue;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * vertical) + (right * horizontal);

            if ((Input.GetButton("Jump") || UIInputSystem.ME.GetButton(ButtonAction.Jump)) && canMove && characterController.isGrounded && !isClimbing)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }
            characterController.Move(moveDirection * Time.deltaTime);
            Moving = horizontal < 0 || vertical < 0 || horizontal > 0 || vertical > 0 ? true : false;

            if (YG2.envir.isDesktop && Cursor.lockState == CursorLockMode.Locked && canMove ||
                !YG2.envir.isDesktop && canMove)
            {
                var joystickLook = UIInputSystem.ME.GetAxis(JoyStickAction.CameraLook);
                Lookvertical = -joystickLook.y - (YG2.envir.isDesktop ? Input.GetAxis("Mouse Y") : 0);
                Lookhorizontal = joystickLook.x + (YG2.envir.isDesktop ? Input.GetAxis("Mouse X") : 0);

                rotationX += Lookvertical * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                Camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Lookhorizontal * lookSpeed, 0);

                if (isRunning && Moving) cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, RunningFOV, SpeedToFOV * Time.deltaTime);
                else cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, InstallFOV, SpeedToFOV * Time.deltaTime);
            }

            if (Input.GetKey(CroughKey) || UIInputSystem.ME.GetButton(ButtonAction.Crouch))
            {
                isCrough = true;
                float Height = Mathf.Lerp(characterController.height, CroughHeight, 5 * Time.deltaTime);
                characterController.height = Height;
                WalkingValue = Mathf.Lerp(WalkingValue, CroughSpeed, 6 * Time.deltaTime);

            }
            else if (!Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.up), out CroughCheck, 0.8f, 1))
            {
                if (characterController.height != InstallCroughHeight)
                {
                    isCrough = false;
                    float Height = Mathf.Lerp(characterController.height, InstallCroughHeight, 6 * Time.deltaTime);
                    characterController.height = Height;
                    WalkingValue = Mathf.Lerp(WalkingValue, walkingSpeed, 4 * Time.deltaTime);
                }
            }

            if(WallDistance != Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.forward), out ObjectCheck, HideDistance, LayerMaskInt) && CanHideDistanceWall)
            {
                WallDistance = Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.forward), out ObjectCheck, HideDistance, LayerMaskInt);
                //Items.ani.SetBool("Hide", WallDistance);
                //Items.DefiniteHide = WallDistance;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryEnterCar();
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Ladder" && CanClimbing)
            { 
                CanRunning = false;
                isClimbing = true;
                WalkingValue /= 2;
                //Items.Hide(true);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Ladder" && CanClimbing)
            {
                moveDirection = new Vector3(0, Input.GetAxis("Vertical") * Speed * (-Camera.localRotation.x / 1.7f), 0);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Ladder" && CanClimbing)
            {
                CanRunning = true;
                isClimbing = false;
                WalkingValue *= 2;
                //Items.ani.SetBool("Hide", false);
                //Items.Hide(false);
            }
        }

        public void TryEnterCar()
        {
            if (PlayerControllerForCar == null)
            {
                PlayerControllerForCar = PG.PlayerController.GetOrCreatePlayerController(); // если GetOrCreatePlayerController() статический метод
            }

            if (CameraParentInCar == null && PlayerControllerForCar != null)
            {
                var carCameraController = PlayerControllerForCar.GetComponentInChildren<CameraController>(true);
                if (carCameraController != null)
                    CameraParentInCar = carCameraController.CameraParentTransform;
            }

            if (Camera == null)
                Camera = GetComponentInChildren<Camera>().transform;

            if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, 2))
            {
                CarController car = hit.collider.GetComponentInParent<CarController>();
                if (car != null)
                {
                    gameObject.SetActive(false);
                    PlayerControllerForCar.EnterInCar(car);
                    PlayerControllerForCar.OnExitAction += OnExitFromCar;

                    if (CameraParentInCar != null)
                    {
                        Camera.SetParent(CameraParentInCar);
                        Camera.localPosition = Vector3.zero;
                        Camera.localRotation = Quaternion.identity;
                    }
                }
            }
        }

        public void OnExitFromCar(CarController car)
        {
            PlayerControllerForCar.OnExitAction -= OnExitFromCar;

            Vector3 offsetPos = car.transform.right * (car.Bounds.size.x / 2 + characterController.radius);
            if (car.SteerWheel != null)
            {
                offsetPos *= Mathf.Sign(car.SteerWheel.localPosition.x);
            }

            offsetPos += Vector3.up * characterController.height * 0.5f;
            transform.position = car.transform.position + offsetPos;
            transform.rotation = Quaternion.LookRotation(new Vector3(car.transform.forward.x, 0, car.transform.forward.z), Vector3.up);

            gameObject.SetActive(true);

            if (Camera == null)
                Camera = GetComponentInChildren<Camera>().transform;

            if (Camera != null && CameraParent != null)
            {
                Camera.SetParent(CameraParent);
                Camera.localPosition = Vector3.zero;
                Camera.localRotation = Quaternion.identity;
            }
        }

    }
}