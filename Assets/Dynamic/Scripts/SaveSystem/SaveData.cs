using System.Collections.Generic;
using UnityEngine;

namespace Save.Data
{
    [System.Serializable]
    public class SaveData
    {
        // Utiliser une structure simple pour éviter les problèmes de sérialisation avec UnityEngine.Vector3
        public SerializableVector3 playerPosition;

        public int totalFishCount;
        
        public Dictionary<int, int> countByLure;
        public Dictionary<int, int> countByFish;
        
        public bool isTutorialDialogueLaunched;
    }

    [System.Serializable]
    public struct SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        public SerializableVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public SerializableVector3(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }
}