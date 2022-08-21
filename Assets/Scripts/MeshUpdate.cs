using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class MeshUpdate : MonoBehaviour
{
    SkinnedMeshRenderer renderer;
    MeshCollider meshrend;
    Mesh ab;
    private void Start()
    {
        ab = new Mesh();
        renderer = GetComponent<SkinnedMeshRenderer>();
        meshrend = GetComponent<MeshCollider>();
        renderer.BakeMesh(ab);
        meshrend.sharedMesh = ab;
    }
    private void Update()
    {
        renderer.BakeMesh(ab,true);
        meshrend.sharedMesh = ab;
    }
}