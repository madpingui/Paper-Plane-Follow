using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    bool gameover = false;

    Rigidbody2D rb;

    Camera cam;

    public AudioClip audC;
    public AudioSource audS;

    [SerializeField] Text score;

    float cpt = 0;
    int scr = 0;

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    void SetScore()
    {
        cpt += Time.deltaTime;
        if (cpt >= 0.5f)
        {
            cpt = 0f;
            scr++;
            score.text = scr.ToString("000");
        }
    }

    private void Update()
    {
        if (!gameover)
        {
            SetScore();

            transform.Rotate(Vector3.forward * (Input.GetAxis("Horizontal")*-rotationSpeed) * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (!gameover)
        {
            rb.AddRelativeForce(new Vector3(moveSpeed * Time.fixedDeltaTime, 0f, 0f));
        }
    }

    private void LateUpdate()
    {
        if (!gameover)
        {
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameover)
        {
            gameover = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponentInChildren<ParticleSystem>().Play();
            audS.PlayOneShot(audC);

            Invoke("Restart", 2);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
