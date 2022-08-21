using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class RewindInTime : MonoBehaviour
{
    
    private bool isPlayer => gameObject.GetComponent<Dodge>() != null;
    private bool isSecondCamera => gameObject.GetComponent<Camera>() != null;

    private float _maxRecordSeconds = 8;
    private bool startRewind = false;
    private LinkedList<PointInTime> _pointsInTime;
    private Rigidbody rb;
    private RigidbodyFirstPersonController controller;
    private SecondCameraMove second;
    private float speedColor = 1.4f;
    private ColorGrading color;


    [HideInInspector] public bool isdead { get; set; }

    private void Awake()
    {
        gameObject.tag = "CanReturnInTime";
    }

    private void Start()
    {
        Time.timeScale = 1;
        if (isSecondCamera)
        GameObject.Find("PostProccesingVolumeForRewind").GetComponent<PostProcessVolume>().profile.TryGetSettings(out color);
        if (isSecondCamera) second = GetComponent<SecondCameraMove>();
        if (isPlayer) controller = GetComponent<RigidbodyFirstPersonController>();
        _pointsInTime = new LinkedList<PointInTime>();
        if(GetComponent<Rigidbody>() != null)
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (isdead && startRewind == false)
        {
            if (isSecondCamera)
            {
                Destroy(second.firstCamera.gameObject);
                Destroy(second);
                GetComponent<AudioListener>().enabled = true;
                GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.9f,1);
                GetComponent<AudioSource>().Play();
            }
            else if(GetComponent<Rigidbody>() != null) rb.isKinematic = true;
            
            if (isPlayer)
            {
                GetComponentInChildren<Animator>().gameObject.transform.parent = GetComponentInChildren<SecondCameraMove>().gameObject.transform;
                GetComponentInChildren<Animator>().enabled = true;
                GetComponentInChildren<RewindAnimation>().enabled = true;
                Destroy(controller);
                
            }

            startRewind = true;
        }
    }
    private void FixedUpdate()
    {
        if (startRewind) Rewind();
        else 
        {
            Record();
        }
    }

    private void Rewind()
    {
        if (isSecondCamera)
        {
            color.postExposure.value += Time.fixedDeltaTime / speedColor;
        }
            
        Time.timeScale = 2.58f;
        if(_pointsInTime.Count > 0)
        {
            PointInTime pointInTime = _pointsInTime.First.Value;
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            _pointsInTime.RemoveFirst();
        }
        else
        {
            startRewind = false;
            if (!isSecondCamera && GetComponent<Rigidbody>() != null) rb.isKinematic = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Record()
    {
        if(_pointsInTime.Count > Math.Round(_maxRecordSeconds / Time.fixedDeltaTime))
        {
            _pointsInTime.RemoveLast();
        }
        _pointsInTime.AddFirst(new PointInTime(transform.position, transform.rotation));
    }
}