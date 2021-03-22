using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using BloodToMana;

namespace BloodToMana
{
    class BloodToManaPlayer : ModPlayer
    {
        public SoundEffect SparkleSparkEffect;
        public SoundEffectInstance SparkleSparkInstance;
        //This in combination with it's counterpart in "Arcane Transfuser" make it to where you can only use the button press below it if you have the accessory on.
        public bool wearingAccessory;
        //float volumeFactor = Main.soundVolume;
        public override void ResetEffects()
        {
            wearingAccessory = false;
            //volumeFactor = Main.soundVolume;
        }
        public override void PreUpdate()
        {
            if (SparkleSparkInstance != null && SparkleSparkInstance.State == SoundState.Playing && SparkleSparkInstance.Volume != Main.soundVolume)
            {
                SparkleSparkInstance.Volume = Main.soundVolume;
            }
        }
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            MiscEffectsBack.visible = true;
            layers.Insert(0, MiscEffectsBack);
        }

        //The Below code calls a blue mana flame behind the player while the buff "Arcane Infusion is active
        public static readonly PlayerLayer MiscEffectsBack = new PlayerLayer("BloodToMana", "MiscEffectsBack", PlayerLayer.MiscEffectsBack, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("BloodToMana");
            BloodToManaPlayer modPlayer = drawPlayer.GetModPlayer<BloodToManaPlayer>();
            if (drawPlayer.HasBuff(mod.BuffType("ArcaneInfusion")))
                if (!drawPlayer.dead)
                    if (drawPlayer.statLife > 100)
                    {
                        Texture2D texture = mod.GetTexture("Buffs/BlueManaFlame");
                        Lighting.AddLight(drawPlayer.position, 0 / 255f, 255 / 255f, 255 / 255f);

                        //Note that the 8 is the number of frames the animation has
                        Rectangle frame = texture.Frame(1, 8, 0, (int)(Main.time / 3) % 8);

                        // Get the draw positions, centered on the player.
                        // These are typecast to integers, to avoid jittery drawing.
                        int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                        int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y);

                        SpriteEffects effects =
                         drawPlayer.direction == 1 ?
                         SpriteEffects.None :
                         SpriteEffects.FlipHorizontally;
                        if (drawPlayer.gravDir == -1)
                            effects |= SpriteEffects.FlipVertically;
                        // Creates a DrawData object and add it to the drawing cache for this player.
                        DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame, Color.White, 0f, frame.Size() / 2, 1f, effects, 0);

                        Main.playerDrawData.Add(data);
                    }
                    else
            if (drawPlayer.HasBuff(mod.BuffType("ArcaneInfusion")))
                        if (!drawPlayer.dead)
                            if (drawPlayer.statLife < 100)
                            {
                                Texture2D texture = mod.GetTexture("Buffs/DarkBlueManaFlame");
                                Lighting.AddLight(drawPlayer.position, 30 / 255f, 0 / 255f, 255 / 255f);
                                Rectangle frame = texture.Frame(1, 8, 0, (int)(Main.time / 3) % 8);
                                int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                                int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y);


                                SpriteEffects effects =
                                           drawPlayer.direction == 1 ?
                                           SpriteEffects.None :
                                           SpriteEffects.FlipHorizontally;
                                if (drawPlayer.gravDir == -1)
                                    effects |= SpriteEffects.FlipVertically;
                                // Creates a DrawData object and add it to the drawing cache for this player.
                                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), frame, Color.White, 0f, frame.Size() / 2, 1f, effects, 0);
                                Main.playerDrawData.Add(data);
                            }

        });
        //The below effects make you take more damage from enemies and their projectiles when under the effects of "Arcane Infusion/BloodyEquivalence"
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (player.HasBuff(mod.BuffType("ArcaneInfusion")))
                damage = (int)(damage * 1.25f);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if (player.HasBuff(mod.BuffType("ArcaneInfusion")))
                damage = (int)(damage * 1.25f);
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (BloodToMana.TickHotKey.JustPressed)
            {
                if (player.HasBuff(mod.BuffType("ArcaneInfusion")))
                {
                    player.ClearBuff(mod.BuffType("ArcaneInfusion"));
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/SnuffSpark"));
                    SparkleSparkInstance?.Stop(true);
                }
                else
                {
                    if (player.GetModPlayer<BloodToManaPlayer>().wearingAccessory == true)
                    {
                        player.AddBuff(mod.BuffType("ArcaneInfusion"), 18000);
                        player.statLife -= 50;
                        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.DamagedFriendly, 50);
                        Main.PlaySound(SoundID.NPCHit);
                        Init();
                        SparkleSparkInstance?.Play();
                        Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Ignite"));
                        //SparkleSparkInstance = Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/SparkleSpark"));
                    }
                }
            }
        }
        public override void Load(TagCompound tag)
        {
            Init();
        }
        public void Init()
        {
            SparkleSparkEffect = mod.GetSound("Sounds/Custom/SparkleSpark");

            SparkleSparkInstance = SparkleSparkEffect?.CreateInstance();

            SparkleSparkInstance.IsLooped = true;
        }
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            base.Hurt(pvp, quiet, damage, hitDirection, crit);
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (player.HasBuff(mod.BuffType("ArcaneInfusion")))
            {
                if (Main.rand.Next(2) == 0)
                {
                    damageSource = PlayerDeathReason.ByCustomReason($"{player.name} was converted into mana.");
                    SparkleSparkInstance?.Stop(true);
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/SnuffSpark"));
                }
                else
                {
                    damageSource = PlayerDeathReason.ByCustomReason($"{player.name} was completely drained.");
                    SparkleSparkInstance?.Stop(true);
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/SnuffSpark"));
                }
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }
    }
}