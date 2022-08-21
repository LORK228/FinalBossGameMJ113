using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodShed : MonoBehaviour
{
    BoxCollider collider;
    public GameObject blood;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponentInParent<BossAIMover>() != null && other.transform.GetComponentInParent<BossAIMover>().GetComponentInChildren<DeadPArticle>() == null && GetComponentInParent<Dodge>().isAnim == true)
        {
            Instantiate(blood, transform.position,transform.rotation, other.transform.GetComponentInParent<BossAIMover>().transform);
        }
    }
}
