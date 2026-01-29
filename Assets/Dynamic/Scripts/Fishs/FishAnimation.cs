using UnityEngine;

namespace Animation.Fishs
{
    public class FishAnimation : MonoBehaviour
    {
        void Update()
        {
            transform.Rotate(0, 0.3f, 0);
        }
    }
}