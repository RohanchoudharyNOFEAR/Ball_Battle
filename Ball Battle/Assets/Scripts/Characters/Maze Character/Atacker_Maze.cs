using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacker_Maze : MonoBehaviour
{

    Animator Animator;

    public bool HasBall = false;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
      float x=  Input.GetAxis("Horizontal");
      float y = Input.GetAxis("Vertical");

        if (x != 0f || y != 0f)
        {
            Animator.SetBool("Run", true);
        }
        else {
            Animator.SetBool("Run", false);
        }

        transform.Translate(x * 5 * Time.deltaTime, 0, y * 5 * Time.deltaTime);
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
