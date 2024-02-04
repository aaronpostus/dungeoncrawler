using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YaoLu
{
    //idle state
    public class IdleState : BaseState
    {
        public IdleState(FSMCtrl controller, Animator animator) : base(controller, animator) { }

        public override void OnEnter()
        {
            // play idle
            animator.Play("Idle");
        }

        public override void Update()
        {
        }
    }
    // walk state
    public class WalkState : BaseState
    {
        public WalkState(FSMCtrl controller, Animator animator) : base(controller, animator) { }

        public override void OnEnter()
        {   // play walk animation
            animator.Play("Walk");
        }

        public override void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            // Combine the input into a 3D vector for direction
            Vector3 input = new Vector3(horizontal, 0, vertical);

            var forward = fsm.mainCamera.transform.TransformDirection(Vector3.forward);
            // only move on plane
            forward.y = 0;

            //get the right-facing direction of the referenceTransform
            var right = fsm.mainCamera.transform.TransformDirection(Vector3.right);

            // determine the direction the player will face based on input and the referenceTransform's right and forward directions
            Vector3 targetDirection = input.x * right + input.z * forward;
            Vector3 lookDirection = targetDirection.normalized;
            // Calculate the desired rotation for the character to face the look direction
            Quaternion freeRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            var diferenceRotation = freeRotation.eulerAngles.y - fsm.transform.eulerAngles.y;
            var eulerY = fsm.transform.eulerAngles.y;
            if (diferenceRotation < 0 || diferenceRotation > 0)
            {
                eulerY = freeRotation.eulerAngles.y;
            }
            var euler = new Vector3(0, eulerY, 0);

            if (input != Vector3.zero)
            {
                // Smoothly rotate the character towards the desired orientation
                fsm.transform.rotation = Quaternion.Slerp(fsm.transform.rotation, Quaternion.Euler(euler), 10 * Time.deltaTime);
                fsm.controller.Move(fsm.transform.forward * Time.deltaTime * fsm.speed);
            }
        }
    }
    // sprint state
    public class RunState : BaseState
    {
        public RunState(FSMCtrl controller, Animator animator) : base(controller, animator) { }

        public override void OnEnter()
        {
            // play running animation
            animator.Play("Run");
        }

        public override void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 input = new Vector3(horizontal, 0, vertical);

            var forward = fsm.mainCamera.transform.TransformDirection(Vector3.forward);
            forward.y = 0;

            //get the right-facing direction of the referenceTransform
            var right = fsm.mainCamera.transform.TransformDirection(Vector3.right);

            // determine the direction the player will face based on input and the referenceTransform's right and forward directions
            Vector3 targetDirection = input.x * right + input.z * forward;
            Vector3 lookDirection = targetDirection.normalized;
            Quaternion freeRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            var diferenceRotation = freeRotation.eulerAngles.y - fsm.transform.eulerAngles.y;
            var eulerY = fsm.transform.eulerAngles.y;
            if (diferenceRotation < 0 || diferenceRotation > 0)
            {
                eulerY = freeRotation.eulerAngles.y;
            }
            var euler = new Vector3(0, eulerY, 0);

            if (input != Vector3.zero)
            {
                fsm.transform.rotation = Quaternion.Slerp(fsm.transform.rotation, Quaternion.Euler(euler), 10 * Time.deltaTime);
                fsm.controller.Move(fsm.transform.forward * Time.deltaTime * fsm.runSpeed);
            }
        }
    }
    // jump state
    public class JumpState : BaseState
    {
        private bool startCheck;

        private float startFallHeight;
        public JumpState(FSMCtrl fsm, Animator animator) : base(fsm, animator)
        {
        }

        public override void OnEnter()
        {
            // play jump animation
            animator.Play("Jump");
            startCheck = false;
            fsm.StartCoroutine(WaitJump());
            startFallHeight = fsm.transform.position.y;
        }

        private IEnumerator WaitJump()
        {
            yield return new WaitForSeconds(0.5f);
            // Apply the initial speed (momentum from sprinting) to the jump's vertical velocity
            fsm.velocity.y = Mathf.Sqrt(fsm.jumpHeight * -2f * fsm.gravity);
            startCheck = true;
        }

        public override void Update()
        {
            if (!startCheck) return;

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 input = new Vector3(horizontal, 0, vertical);

            var forward = fsm.mainCamera.transform.TransformDirection(Vector3.forward);
            forward.y = 0;
            var right = fsm.mainCamera.transform.TransformDirection(Vector3.right);

            Vector3 targetDirection = input.x * right + input.z * forward;
            Vector3 lookDirection = targetDirection.normalized;
            Quaternion freeRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            var diferenceRotation = freeRotation.eulerAngles.y - fsm.transform.eulerAngles.y;
            var eulerY = fsm.transform.eulerAngles.y;

            if (diferenceRotation < 0 || diferenceRotation > 0)
            {
                eulerY = freeRotation.eulerAngles.y;
            }
            var euler = new Vector3(0, eulerY, 0);

            // While in the air, allow the player to move forward based on current orientation
            if (input != Vector3.zero)
            {
                fsm.transform.rotation = Quaternion.Slerp(fsm.transform.rotation, Quaternion.Euler(euler), 10 * Time.deltaTime);
                // This moves the character forward in the direction they are facing
                fsm.controller.Move(fsm.transform.forward * Time.deltaTime * fsm.speed);
            }

            fsm.velocity.y += fsm.gravity * Time.deltaTime; // Apply gravity
            fsm.controller.Move(fsm.velocity * Time.deltaTime); // Apply movement and gravity effect

            // Check for landing
            if (fsm.controller.isGrounded)
            {
                fsm.isJumping = false;
                // If landed from a height greater than 5 units, consider it a fall
                float currentHeight = fsm.transform.position.y;
                if (startFallHeight - currentHeight >= 5)
                {
                    fsm.isDead = true; // Or handle fall damage instead of immediate death
                    fsm.ChangeState(new DieState(fsm, animator));
                }
                else
                {
                    // Transition to an appropriate state, like Idle or Walk, depending on the player's input
                }
            }
        }
    }
    // crouch state
    public class CrouchState : BaseState
    {
        public CrouchState(FSMCtrl fsm, Animator animator) : base(fsm, animator) { }

        public override void OnEnter()
        {
            //play crouch animation
            animator.Play("Crouch");
        }

        public override void Update()
        {

        }
    }
    //kick state
    public class KickState : BaseState
    {
        public KickState(FSMCtrl fsm, Animator animator) : base(fsm, animator) { }

        public override void OnEnter()
        {
            // play attack animation
            animator.Play("Kick");
            fsm.StartCoroutine(WaitKick());
        }

        private IEnumerator WaitKick()
        {
            yield return new WaitForSeconds(1.2f);
            fsm.footCollider.enabled = false;
            fsm.isKicking = false;
        }

        public override void Update()
        {

        }
    }
    // falling state
    public class FallState : BaseState
    {
        private float startFallHeight;

        public FallState(FSMCtrl fsm, Animator animator) : base(fsm, animator)
        {
        }

        public override void OnEnter()
        {
            // play falling animation
            animator.Play("Fall");
            startFallHeight = fsm.transform.position.y;
        }

        public override void Update()
        {
            // state when in the air
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 input = new Vector3(horizontal, 0, vertical);

            var forward = fsm.mainCamera.transform.TransformDirection(Vector3.forward);
            forward.y = 0;
            var right = fsm.mainCamera.transform.TransformDirection(Vector3.right);

            Vector3 targetDirection = input.x * right + input.z * forward;
            Vector3 lookDirection = targetDirection.normalized;
            Quaternion freeRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            var diferenceRotation = freeRotation.eulerAngles.y - fsm.transform.eulerAngles.y;
            var eulerY = fsm.transform.eulerAngles.y;

            if (diferenceRotation < 0 || diferenceRotation > 0)
            {
                eulerY = freeRotation.eulerAngles.y;
            }
            var euler = new Vector3(0, eulerY, 0);

            if (input != Vector3.zero)
            {
                fsm.transform.rotation = Quaternion.Slerp(fsm.transform.rotation, Quaternion.Euler(euler), 10 * Time.deltaTime);
                fsm.controller.Move(fsm.transform.forward * Time.deltaTime * fsm.speed);
            }
            fsm.velocity.y += fsm.gravity * Time.deltaTime;

            // check if on the ground
            if (fsm.velocity.y <= 0)
            {
                if (fsm.controller.isGrounded)
                {
                    fsm.isFalling = false;
                    // check falling distance
                    float currentHeight = fsm.transform.position.y;
                    if (startFallHeight - currentHeight >= 5) // X ÊÇÄãµÄãÐÖµ
                    {
                        fsm.isDead = true;
                        fsm.ChangeState(new DieState(fsm, animator));
                    }
                }
            }
        }
    }
    // dead state
    public class DieState : BaseState
    {
        public DieState(FSMCtrl fsm, Animator animator) : base(fsm, animator) { }

        // clean health, refresh the scene
        public override void OnEnter()
        {
            fsm.life = 0;
            fsm.lifeSlider.value = fsm.life;
            fsm.isDead = true;
            animator.Play("Die");
            fsm.StartCoroutine(WaitDie());
        }

        private IEnumerator WaitDie()
        {
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene(0);
        }

        public override void Update()
        {

        }
    }
    // hurt state
    public class HurtState : BaseState
    {
        public HurtState(FSMCtrl fsm, Animator animator) : base(fsm, animator) { }

        // reduce 10 health
        public override void OnEnter()
        {
            fsm.StartCoroutine(WaitHurt());
            animator.Play("Hurt");
            fsm.life -= 10;
            fsm.lifeSlider.value = fsm.life;

            if (fsm.life <= 0)
            {
                fsm.ChangeState(new DieState(fsm, animator));
            }
        }

        private IEnumerator WaitHurt()
        {
            yield return new WaitForSeconds(1f);
            fsm.isHurting = false;
        }

        public override void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 input = new Vector3(horizontal, 0, vertical);

            var forward = fsm.mainCamera.transform.TransformDirection(Vector3.forward);
            forward.y = 0;

            //get the right-facing direction of the referenceTransform
            var right = fsm.mainCamera.transform.TransformDirection(Vector3.right);

            // determine the direction the player will face based on input and the referenceTransform's right and forward directions
            Vector3 targetDirection = input.x * right + input.z * forward;
            Vector3 lookDirection = targetDirection.normalized;
            Quaternion freeRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            var diferenceRotation = freeRotation.eulerAngles.y - fsm.transform.eulerAngles.y;
            var eulerY = fsm.transform.eulerAngles.y;
            if (diferenceRotation < 0 || diferenceRotation > 0)
            {
                eulerY = freeRotation.eulerAngles.y;
            }
            var euler = new Vector3(0, eulerY, 0);

            if (input != Vector3.zero)
            {
                fsm.transform.rotation = Quaternion.Slerp(fsm.transform.rotation, Quaternion.Euler(euler), 10 * Time.deltaTime);
                fsm.controller.Move(fsm.transform.forward * Time.deltaTime * fsm.speed);
            }
        }
    }
}
