using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dodge: MonoBehaviour
{
    private float slowdown= 0.02f;
    private float Duration = 0.01f;
    private float _timeLeft = 0f;
    private Rigidbody rigidbody;
    public int force;
    private GameObject camera;
    private Vector3 MoveTo;
    private Animator ArmSwing;
    private float timer;
    public float MaxTime;
    [HideInInspector] public bool InAir;
    [HideInInspector] public bool dodge = false;
    [SerializeField] private Image DodgeTimer;
    [SerializeField] private float time;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = GetComponentsInChildren<Camera>()[0].gameObject;
        MoveTo = new Vector3(0f, 0f, 0f);
        ArmSwing = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && timer >= MaxTime)
        {
            
            switch (Random.Range(0, 4)){
                case 1:
                    {
                        ArmSwing.SetTrigger("Attack1");
                        timer = 0;
                        break;
                    } 
                case 2:
                    {
                        ArmSwing.SetTrigger("Attack2");
                        timer = 0;
                        break;
                    }
                case 3:
                    {
                        ArmSwing.SetTrigger("Attack3");
                        timer = 0;
                        break;
                    }
            }
        }
        if (_timeLeft < time)
        {
            _timeLeft += Time.deltaTime;
            DodgeTimer.fillAmount = Mathf.Clamp(_timeLeft/time, 0f, 1f);
        }
        else
        {
            if (Time.timeScale < 1f && Time.timeScale !=0)
            {
                camera.transform.position = Vector3.MoveTowards(camera.transform.position, MoveTo, 0.001f);
                rigidbody.drag = 0f;
                Time.timeScale += (1f / Duration) * Time.deltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
                if(Vector3.Distance(camera.transform.position, MoveTo) < 0.001f)
                {
                    MoveTo = new Vector3(0f, 0.6f, 0f);
                }
            }
            else if (Input.GetKeyDown(KeyCode.E) && dodge == false && InAir == false)
            {
                Time.timeScale = slowdown;
                Time.fixedDeltaTime = Time.timeScale * 0.2f;
                rigidbody.drag = 0f;
                rigidbody.AddForce(gameObject.transform.right*5, ForceMode.VelocityChange);
                rigidbody.AddForce(gameObject.transform.right * force, ForceMode.VelocityChange);
                dodge = true;
            }
            else if (Input.GetKeyDown(KeyCode.Q) && dodge == false && InAir == false)
            {
                Time.timeScale = slowdown;
                Time.fixedDeltaTime = Time.timeScale * 0.2f;
                rigidbody.drag = 0f;
                rigidbody.AddForce(-gameObject.transform.right*5, ForceMode.VelocityChange);
                rigidbody.AddForce(-gameObject.transform.right * force, ForceMode.VelocityChange);
                dodge = true;
            }
            else
            {
                if (dodge == true)
                {
                    _timeLeft = 0;
                }
                dodge = false;
                rigidbody.drag = 5f;
                
            }
        }
 
    }
}
