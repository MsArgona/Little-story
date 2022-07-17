using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpForce = 17.0f;

    [Header("Grabbing")]
    [SerializeField] private GameObject grabbedObj;
    private float grabbedObjYValue;
    [SerializeField] private Transform grabPoint;

    [Header("Effects")]
    [SerializeField] private ParticleSystem dust;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpClip;

    private bool isGrounded = false;
    private bool isGrabbing = false;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    void Update()
    {
        if (isGrounded) State = CharState.Idle;

        if (Input.GetButton("Horizontal")) Run();
        if (Input.GetButtonDown("Jump") && isGrounded) Jump();
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.E)) GrabDrop();
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        Vector3 characterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -1;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = 1;
        }

        transform.localScale = characterScale;

        if (isGrounded)
        {
            State = CharState.Run;
            CreateDust();
        }
    }

    private void Jump()
    {
        audioSource.PlayOneShot(jumpClip);
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.6f);
        isGrounded = colliders.Length > 1;

        if (!isGrounded) State = CharState.Jump;
    }

    //Можно ли поднять объект
    private bool CheckGrabObj()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(grabPoint.transform.position, 1f);

        foreach (var item in colliders)
        {
            if (item.CompareTag("Grabbable"))
            {
                grabbedObj = item.gameObject;
                return true;
            }
        }
        return false;
    }

    private void GrabDrop()
    {
        //если в руках что-то есть. то сбросить это      
        if (isGrabbing) 
        {
            grabbedObj.GetComponent<BoxCollider2D>().isTrigger = false;
            grabbedObj.GetComponent<Rigidbody2D>().simulated = true;
            isGrabbing = false;
            grabbedObj.transform.parent = null;     
            grabbedObj = null;
        }
        //если руки свободны, то поднять это      
        else
        {
            if (!CheckGrabObj()) return;

            grabbedObj.GetComponent<BoxCollider2D>().isTrigger = true;
            grabbedObj.GetComponent<Rigidbody2D>().simulated = false;
            isGrabbing = true;
            grabbedObj.transform.parent = transform;
            grabbedObjYValue = grabbedObj.transform.position.y;
            grabbedObj.transform.localPosition = grabPoint.localPosition;
        }
    }

    private void CreateDust()
    {
        dust.Play(); 
    }
}

public enum CharState
{
    Idle,
    Run,
    Jump
}
