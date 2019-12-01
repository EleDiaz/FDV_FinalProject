using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class FleeBehaviour : MonoBehaviour
{
    public delegate void Flee(int group, Vector3 fearPosition);

    // We should care of remove the listener from the event to allow them been garbage collected (It's a static event)
    static event Flee OnFlee;

    public GameObject scaryThing;
    public int belongGroup = 0;
    public bool isFleeing = false;
    public float fleeingTime = 3.0f;
    
    private Vector3 lastFearPosition;
    
    private Rigidbody2D _rb;

    void OnEnable() {
        OnFlee += Fleeing;
    }

    void OnDisable() {
        OnFlee += Fleeing;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFleeing) {
            float direction = transform.position.x - lastFearPosition.x > 0 ? 1 : -1;
            _rb.velocity = new Vector2(direction * 4.0f, 0);
        }
    }

    void Fleeing(int group, Vector3 fearPosition) {
        if (group == belongGroup) {
            isFleeing = true;
            lastFearPosition = fearPosition;
            StartCoroutine(StopFleeing());
        }
    }

    IEnumerator StopFleeing() {
        yield return new WaitForSeconds(fleeingTime);
        isFleeing = false;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (scaryThing == collision.gameObject) {
            OnFlee(belongGroup, collision.gameObject.transform.position);
        }
    }
}
