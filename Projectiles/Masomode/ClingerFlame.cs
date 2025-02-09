﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class ClingerFlame : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_482";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Flames");
            Main.projFrames[Projectile.type] = Main.projFrames[ProjectileID.ClingerStaff];
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ClingerStaff);
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.timeLeft = 10 * 60;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 1;
        }

        public override bool? CanDamage()
        {
            return Projectile.localAI[0] >= 60;
        }

        public override void AI()
        {
            if (++Projectile.localAI[0] < 60)
            {
                int dust = Dust.NewDust(Projectile.Bottom, 0, 0, 75, 0f, 0f, 100, default, 2f);
                Main.dust[dust].velocity.Y -= 2f;
                Main.dust[dust].velocity *= 2f;
                if (Main.rand.NextBool(4))
                {
                    Main.dust[dust].scale += 0.5f;
                    Main.dust[dust].noGravity = true;
                }

                Projectile.velocity = Vector2.Zero;

                for (int i = 0; i < 2; i++)
                {
                    Tile tile = Framing.GetTileSafely(Projectile.Bottom); //zip to an acceptable location
                    Projectile.position.Y += tile.HasUnactuatedTile && (Main.tileSolid[tile.TileType] || Main.tileSolidTop[tile.TileType]) ? -16 : 16;
                }
            }
            else
            {
                int max = (int)(Projectile.width * Projectile.height * 0.0045f);
                for (int i = 0; i < max; i++)
                {
                    int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 75, 0.0f, 0.0f, 100, default, 1f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 0.5f;
                    Main.dust[d].velocity.Y -= 0.5f;
                    Main.dust[d].scale = 1.4f;
                    Main.dust[d].position.X += 6f;
                    Main.dust[d].position.Y -= 2f;
                }
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 180);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
    }
}