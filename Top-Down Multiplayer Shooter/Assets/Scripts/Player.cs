using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Handles all player inputs and score.
    /// </summary>
    
    [SerializeField] KeyCode front;
    [SerializeField] KeyCode back;
    [SerializeField] KeyCode left;
    [SerializeField] KeyCode right;
    [SerializeField] KeyCode shoot;
    [SerializeField] KeyCode melee;
    [SerializeField] float speed;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject meleeWeapon;
    [SerializeField] GameObject winPanel;
    [SerializeField] string playerNumber;
    [SerializeField] TextMeshProUGUI myScore;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] Animator meleeAttack;
    public int score;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meleeWeapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!winPanel.activeSelf)
        {
            Move();
            Shoot();
            if (Input.GetKeyDown(melee))
            {
                StartCoroutine(Melee());
            }
        }
    }

    private void Move()
    {
        if (Input.GetKey(front))
        {
            rb.velocity += new Vector3(0, 0, speed);
            transform.forward = rb.velocity;
        }
        if (Input.GetKey(back))
        {
            rb.velocity += new Vector3(0, 0, -speed);
            transform.forward = rb.velocity;
        }
        if (Input.GetKey(left))
        {
            rb.velocity += new Vector3(-speed, 0, 0);
            transform.forward = rb.velocity;
        }
        if (Input.GetKey(right))
        {
            rb.velocity += new Vector3(speed, 0, 0);
            transform.forward = rb.velocity;
        }
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, 15);
        if (!Input.GetKey(front) && !Input.GetKey(back) && !Input.GetKey(left) && !Input.GetKey(right))
        {
            rb.velocity *= 0.95f;
            if (rb.velocity.magnitude < 0.5f)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(shoot))
        {
            GameObject newBullet = Instantiate(bullet, transform.position + transform.forward * 1.5f, transform.rotation);
            newBullet.GetComponent<Bullet>().myPlayer = this;
            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 5000);
        }
    }

    IEnumerator Melee()
    {
        meleeWeapon.SetActive(true);
        meleeAttack.SetBool("Swing!", true);
        yield return new WaitForSeconds(0.34f);
        meleeAttack.SetBool("Swing!", false);
        meleeWeapon.SetActive(false);
    }

    void UpdateScore()
    {
        myScore.text = $"P{playerNumber}: {score}";
        if (score >= 20)
        {
            winPanel.SetActive(true);
            winText.text = $"Player {playerNumber} wins!";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            other.GetComponent<Bullet>().myPlayer.score++;
            other.GetComponent<Bullet>().myPlayer.UpdateScore();
        }
        else if (other.CompareTag("Melee"))
        {
            other.GetComponent<Melee>().myPlayer.score+= 5;
            other.GetComponent<Melee>().myPlayer.UpdateScore();
        }
    }
}
