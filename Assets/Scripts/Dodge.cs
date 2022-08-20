using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dodge: MonoBehaviour
{
    private float slowdown= 0.02f;
    private float Duration = 0.01f;
    [HideInInspector] public bool dodge = false;
    [SerializeField] private Image DodgeTimer;
    private Rigidbody rigidbody;
    public int force;
    [SerializeField] private float time;
    private float _timeLeft = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeLeft < time)
        {
            _timeLeft += Time.deltaTime;
            DodgeTimer.fillAmount = Mathf.Clamp(_timeLeft/time, 0f, 1f);
            print(Mathf.Clamp(time / _timeLeft, 0f, 1f));
        }
        else
        {
            if (Time.timeScale < 1f && Time.timeScale !=0)
            {
                rigidbody.drag = 0f;
                Time.timeScale += (1f / Duration) * Time.deltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            }
            else if (Input.GetKeyDown(KeyCode.E) && dodge == false)
            {
                Time.timeScale = slowdown;
                Time.fixedDeltaTime = Time.timeScale * 0.2f;
                rigidbody.drag = 0f;
                rigidbody.velocity = gameObject.transform.right;
                rigidbody.AddForce(gameObject.transform.right * force, ForceMode.VelocityChange);
                dodge = true;
            }
            else if (Input.GetKeyDown(KeyCode.Q) && dodge == false)
            {
                Time.timeScale = slowdown;
                Time.fixedDeltaTime = Time.timeScale * 0.2f;
                rigidbody.drag = 0f;
                rigidbody.velocity = -gameObject.transform.right;
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
