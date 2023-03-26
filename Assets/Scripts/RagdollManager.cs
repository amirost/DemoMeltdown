using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RagdollManager : MonoBehaviour
{


    Animator animator;

    Collider mainCollider;
    Rigidbody rigidbody;

    Collider[] ragdollColliders;
    Rigidbody[] ragdollRigidbodies;
    CharacterJoint[] characterJoints;
    //jointsscript[] jointsScripts;
    public bool ragdollisOn;
    // Start is called before the first frame update
    void Start()
    {

        mainCollider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        GetRagdollComponents();
        RagdollOff();
    }


    void GetRagdollComponents()
    {
        ragdollColliders = gameObject.GetComponentsInChildren<Collider>();
        ragdollRigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
        characterJoints = gameObject.GetComponentsInChildren<CharacterJoint>();
        //jointsScripts = gameObject.GetComponentsInChildren<jointsscript>();
    }
    public IEnumerator RagdollOn()
    {
        ragdollisOn = true;
        EnableAllColliders(1);

        for (int i = 0; i < ragdollRigidbodies.Length; i++)
        {
            ragdollRigidbodies[i].isKinematic = false;
        }
        mainCollider.enabled = false;
        animator.enabled = false;

        rigidbody.isKinematic = true;
        yield return new WaitForSeconds(2f);
        RagdollOff();
    }

    public void RagdollOff()
    {
        ragdollisOn = false;
        DisableAllColliders(1);

        for (int i = 0; i < ragdollRigidbodies.Length; i++)
        {
            ragdollRigidbodies[i].isKinematic = true;
        }
        mainCollider.enabled = true;
        animator.enabled = true;

        rigidbody.isKinematic = false;
    }




    public void RemoveCharcterJoints()
    {
        for (int i = 0; i < characterJoints.Length; i++)
        {
            Destroy(characterJoints[i]);
            //Destroy(jointsScripts[i]);
        }

    }

    public void RemoveRigidbodies()
    {
        for (int i = 0; i < ragdollRigidbodies.Length; i++)
        {
            Destroy(ragdollRigidbodies[i]);
        }
    }
    public void DisableAllColliders(int startingCollider)
    {
        for (int i = startingCollider; i < ragdollColliders.Length; i++)
        {
            ragdollColliders[i].enabled = false;
        }
    }

    public void EnableAllColliders(int startingCollider)
    {
        for (int i = startingCollider; i < ragdollColliders.Length; i++)
        {
            ragdollColliders[i].enabled = true;
        }
    }




}
