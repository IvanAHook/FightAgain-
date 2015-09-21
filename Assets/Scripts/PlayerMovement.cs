using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

    public float Speed;

    private Rigidbody2D rb2D;

    private Animator anim;
    private BoxCollider2D swordCollider;
    private bool facingRight = true;

    public GameObject explosion;

	void Awake () {
        anim = GetComponentInChildren<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        swordCollider = GetComponent<BoxCollider2D>();
	}
	

	void Update () {

        transform.Translate(Vector3.up * CrossPlatformInputManager.GetAxis("Vertical") * Speed * Time.deltaTime);
        transform.Translate(Vector3.right * CrossPlatformInputManager.GetAxis("Horizontal") * Speed * Time.deltaTime);

        if (CrossPlatformInputManager.GetAxis("Horizontal") < 0 && facingRight) {
            Flip();
        } else if (CrossPlatformInputManager.GetAxis("Horizontal") > 0 && !facingRight) {
            Flip();
        }

        if (CrossPlatformInputManager.GetAxis("Vertical") != 0 || CrossPlatformInputManager.GetAxis("Horizontal") != 0) {
            anim.SetFloat("Speed", 1f);
        } else {
            anim.SetFloat("Speed", 0f);
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump")) 
            StartCoroutine(Attack("Attack"));
        if (CrossPlatformInputManager.GetButtonDown("Jump2")) 
            StartCoroutine(Attack("Attack2"));
        if (Input.GetKeyDown(KeyCode.D))
            anim.SetTrigger("Die");
	}

    void Flip() {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    IEnumerator Attack(string attack) {
        anim.SetTrigger(attack);
        yield return new WaitForSeconds(float.Epsilon);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log(other.gameObject.name + " hit");
        explosion.transform.position = other.transform.position;
        explosion.GetComponent<Animator>().SetTrigger("Play");
    }

}
