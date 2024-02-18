using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace YaoLu
{
    public class FSMCtrl : MonoBehaviour
    {
        public Animator animator;

        private StateMachine stateMachine;
        private PlayerInput playerInput;
        public CharacterController controller;
        public Transform mainCamera;
        public float speed = 6f; // walk speed
        public float crouchSpeed;
        public float runSpeed;
        public float jumpHeight = 2f;
        public float gravity = -9.81f;
        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask; // gorund layer

        public Vector3 velocity;

        public Slider lifeSlider;

        public int life;

        public Collider footCollider;
        public bool isGrounded;
        public bool isJumping;
        public bool isFalling;
        public bool isKicking;
        public bool isHurting;
        public bool isDead;

        private Vector3 input;

        private IdleState idleState;
        private WalkState walkState;
        private RunState runState;
        private JumpState jumpState;
        private CrouchState crouchState;
        private KickState kickState;
        private FallState fallState;
        private HurtState hurtState;

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // initial state machine
            stateMachine = new StateMachine();
            idleState = new IdleState(this, animator);
            walkState = new WalkState(this, animator);
            runState = new RunState(this, animator);
            jumpState = new JumpState(this, animator);
            crouchState = new CrouchState(this, animator);
           // basic attack
            fallState = new FallState(this, animator);
            hurtState = new HurtState(this, animator);
            stateMachine.ChangeState(idleState);

            crouchSpeed = speed / 2;
            runSpeed = speed * 2;

        }

        private void Awake()
        {
            playerInput = new PlayerInput();
            playerInput.Gameplay.Jump.performed += _ => OnEnable();
            playerInput.Gameplay.Sprint.started += _ => OnEnable();
            playerInput.Gameplay.Sprint.canceled += _ => OnEnable();
            playerInput.Gameplay.Crouch.performed += _ => OnEnable();
        }
        private void OnEnable()
        {
            playerInput.Gameplay.Jump.performed += OnJump;

            playerInput.Gameplay.Sprint.performed += OnRun;
            playerInput.Gameplay.Crouch.performed += OnCrouch;
            playerInput.Gameplay.Crouch.canceled += OnCrouchRelease;
            playerInput.Enable();

        }

        private void OnDisable()
        {
            playerInput.Gameplay.Jump.performed -= OnJump;
            playerInput.Gameplay.Sprint.performed -= OnRun;
            playerInput.Gameplay.Crouch.performed -= OnCrouch;
            playerInput.Gameplay.Crouch.canceled -= OnCrouchRelease;
        }
        private void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                stateMachine.ChangeState(crouchState);
            }
        }
        private void OnCrouchRelease(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                stateMachine.ChangeState(idleState);
            }
        }
        private void OnRun(InputAction.CallbackContext context)
        {
            if (isGrounded)
            {
                stateMachine.ChangeState(runState);
            }
        }

        // jump action
        private void OnJump(InputAction.CallbackContext context)
        {
            if (isGrounded)
            {
                stateMachine.ChangeState(jumpState);
                isJumping = true;
            }
        }

        private void Update()
        {
            stateMachine.Update();
            // check if player is on ground
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (isJumping) return;
            if (isFalling) return;
            if (isKicking) return;
            if (isHurting) return;
            if (isDead) return;

            //falling
            if (!isGrounded && velocity.y < 0)
            {
                isFalling = true;
                stateMachine.ChangeState(fallState);
                return;
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                stateMachine.ChangeState(crouchState);
                return;
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                stateMachine.ChangeState(idleState);
            }
            // check the speed and ground
            else if (isGrounded && (horizontal != 0 || vertical != 0))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    stateMachine.ChangeState(runState);
                }
                else
                {
                    stateMachine.ChangeState(walkState);
                }
            }
            else if (isGrounded)
            {
                stateMachine.ChangeState(idleState);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        public void ChangeState(IState newState)
        {
            stateMachine.ChangeState(newState);
        }

        // hurt life slide when be hurt
        public void Hurt()
        {
            life -= 10;
            lifeSlider.value = life;

            if (life <= 0)
            {
                ChangeState(new DieState(this, animator));
            }
            isHurting = true;
            stateMachine.ChangeState(hurtState);
        }

    }
}
