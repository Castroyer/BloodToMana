using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace BloodToMana
{
    public class SparkleSpark : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
            if (soundInstance == null)
            {
                soundInstance = sound.CreateInstance();
            }
            if (soundInstance.IsLooped == false && soundInstance.State != SoundState.Playing)
            {
                soundInstance.IsLooped = true;
            }
            soundInstance.Volume = volume;
            soundInstance.Pan = pan;
            soundInstance.Pitch = 0f;
            return soundInstance;
        }
    }
}