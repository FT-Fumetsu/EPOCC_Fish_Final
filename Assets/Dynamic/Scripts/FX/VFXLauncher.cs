using UnityEngine.VFX;

namespace FX
{
    public abstract class VFXLauncher : FXLauncher<VisualEffect>
    {
        protected override void PlayFX()
        {
            _fx.Play();
        }
    }
}