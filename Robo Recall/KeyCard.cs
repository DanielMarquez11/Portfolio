using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Daniel.Marquez.KeyCard.System
{
    public class KeyCard : MonoBehaviour
    {
        [SerializeField] private KeyType keyType;
        public enum KeyType
        {
            Red,
            Blue,
            Green,
            Yellow,
            Purple,
            Orange,
            Pink,
            Gray,
            Turquoise,
            Maroon,
            None
        }

        public KeyType GetKeyType()
        {
            return keyType;
        }
    }
}