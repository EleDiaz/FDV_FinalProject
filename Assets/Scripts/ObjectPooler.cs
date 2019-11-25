using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Create a pool for a specified object, initial pool size shouldn't grow significatively if it's set "willGrow"
public class ObjectPooler : MonoBehaviour
{
    // Size of pool
    public int poolSize = 10;
    // It will grow on filled;
    public bool willGrow = true;
    // Object to be pooled
    public GameObject objectProbe;

    // We use an array to get fastest access.
    private GameObject[] _poolObject;

    public void Start() {
        _poolObject = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            _poolObject[i] = (GameObject) Instantiate(objectProbe);
            _poolObject[i].SetActive(false);
        }
    }

    // Return an inactive instance from the pool, and enable the object. Return null if it cant grow and pool is filled. 
    public GameObject GetInstance() {
        // TODO: STUDY CASE: When the size is really big we should take care of this search method O(n)
        // possible solution is add bitset to get next O(n/word) or some other techniques to get
        // even a better performance
        for (int i = 0; i < _poolObject.Length; i++)
        {
            if (!_poolObject[i].activeInHierarchy) {
                _poolObject[i].SetActive(true); // TODO: is this fine?
                return _poolObject[i];
            }
        }
        if (willGrow) {
            // TODO: Maybe a we should use a chunk.
            Array.Resize(ref _poolObject, _poolObject.Length + 1);
            _poolObject[_poolObject.Length - 1] = (GameObject) Instantiate(objectProbe);
        }
        return null;
    }

}