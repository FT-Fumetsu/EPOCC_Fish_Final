using Fish.Spawner.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lure.Data
{
    [CreateAssetMenu(fileName = "LureData", menuName = "ScriptableObjects/Lure Data")]
    public class LureData : ScriptableObject
    {
        public Sprite LureIconWithoutPrice;
        public Sprite LureIconWithPrice;
        public int LurePrice;
        public FishSpawnData[] SpawnTable;

        //Only for debug
        public string LureName;
    }
}