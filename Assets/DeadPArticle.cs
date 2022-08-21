using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPArticle : MonoBehaviour
{
    IEnumerator dead()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    private void Start()
    {
        StartCoroutine(dead());
    }
}
