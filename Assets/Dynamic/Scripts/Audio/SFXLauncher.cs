using FX;

namespace Audio
{
    public abstract class SFXLauncher : FXLauncherAsset<SoundData>
    {
        protected override void PlayFX()
        {
            AudioHandler.Instance.PlaySound(_fx);
        }

        protected void StopSFX()
        {
            AudioHandler.Instance.StopSound();
        }
    }
}