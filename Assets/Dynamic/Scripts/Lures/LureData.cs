using Fish.Spawner.Data;
using UnityEngine;

namespace Lure.Data
{
    [CreateAssetMenu(fileName = "LureData", menuName = "ScriptableObjects/Lure Data")]
    public class LureData : ScriptableObject
    {
        //public Lure LureType;
        public Sprite LureIcon;
        public int LurePrice;
        public FishSpawnData[] SpawnTable;
    }
}