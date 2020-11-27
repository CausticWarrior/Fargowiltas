﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Fargowiltas.Items.Summons
{
    public abstract class BaseSummon : ModItem
    {
        public abstract int Type { get; }

        public abstract string NPCName { get; }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 20;
            item.value = Item.sellPrice(0, 0, 2);
            item.rare = ItemRarityID.Blue;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.consumable = true;
            item.shoot = mod.ProjectileType("SpawnProj");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 pos = new Vector2((int)player.position.X + Main.rand.Next(-800, 800), (int)player.position.Y + Main.rand.Next(-800, -250));

            if (Type == NPCID.Golem)
            {
                pos = player.Center;
                for (int i = 0; i < 30; i++)
                {
                    pos.Y -= 16;

                    if (pos.Y <= 0 || WorldGen.SolidTile((int)pos.X / 16, (int)pos.Y / 16))
                    {
                        pos.Y += 16;
                        break;
                    }
                }
            }

            //if (Main.netMode != 1)
            //{
                Projectile.NewProjectile(pos, Vector2.Zero, mod.ProjectileType("SpawnProj"), 0, 0, Main.myPlayer, Type);
            //}

            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral($"{NPCName} has awoken!"), new Color(175, 75, 255));
            }
            else if (Type != NPCID.KingSlime)
            {
                Main.NewText($"{NPCName} has awoken!", new Color(175, 75, 255));
            }

            Main.PlaySound(SoundID.Roar, player.position, 0);

            return false;
        }
    }
}