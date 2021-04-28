﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Fargowiltas.Items.Tiles
{
    public class MultitaskCenterSheet : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Width = 4;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Multitask Center");
            AddMapEntry(new Color(200, 200, 200), name);
            disableSmartCursor = true;
            //counts as
            adjTiles = new int[] { TileID.WorkBenches, TileID.HeavyWorkBench, TileID.Furnaces,  TileID.Anvils,  TileID.Bottles, TileID.Sawmill, TileID.Loom, TileID.Tables, TileID.Chairs, TileID.CookingPots, TileID.Sinks, TileID.Kegs };
            
            animationFrameHeight = 54;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.93f;
            g = 0.11f;
            b = 0.12f;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("MultitaskCenter"));
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter >= 10) //replace with duration of frame in ticks
            {
                frameCounter = 0;
                frame++;
                frame %= 7;
            }
        }
    }
}