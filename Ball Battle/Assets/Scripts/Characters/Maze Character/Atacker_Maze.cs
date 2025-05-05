using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacker_Maze : MonoBehaviour
{
    public bool HasBall = false;

    private Inputaction controls;
    private Vector2 moveInput;
    private Animator animator;

    private void Awake()
    {
        controls = new Inputaction();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    void OnDisable()
    {
        controls.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled -= ctx => moveInput = Vector2.zero;
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (moveInput != Vector2.zero)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        transform.Translate(new Vector3(moveInput.x, 0, moveInput.y) * 5 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            HasBall = true;
            other.transform.SetParent(transform);
            other.transform.localPosition = Vector3.forward * 0.5f;
        }
    }
}
