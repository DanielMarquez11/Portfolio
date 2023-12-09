using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Daniel.Marquez.KeyCard.System;
using UnityEngine.InputSystem;

public class KeyHolder : MonoBehaviour
{
    public List<KeyCard.KeyType> keyList;

    private void Awake()
    {
        keyList = new List<KeyCard.KeyType>();
    }

    public void AddKey(KeyCard.KeyType keyType)
    {
        Debug.Log("Added key: " + keyType);
        keyList.Add(keyType);
    }

    public void RemoveKey(KeyCard.KeyType keyType)
    {
        keyList.Remove(keyType);
    }

    public bool ContainsKey(KeyCard.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }

    private void OnTriggerStay(Collider collider)
    {
        KeyCard key = collider.GetComponent<KeyCard>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }
    }
}