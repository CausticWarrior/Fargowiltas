﻿using System.Collections.Generic;
using Fargowiltas.Items.Summons.Deviantt;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Fargowiltas.NPCs
{
    [AutoloadHead]
    public class Deviantt : ModNPC
    {
        public override bool Autoload(ref string name)
        {
            name = "Deviantt";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deviantt");
            Main.npcFrameCount[npc.type] = 23;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;


        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = NPC.downedMoonlord ? 50 : 15;
            npc.lifeMax = NPC.downedMoonlord ? 2500 : 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Angler;

            if (GetInstance<FargoConfig>().CatchNPCs)
            {
                Main.npcCatchable[npc.type] = true;
                npc.catchItem = (short)mod.ItemType("Deviantt");
            }
                
            npc.buffImmune[BuffID.Suffocation] = true;
        }

        public override bool CanTownNPCSpawn(int numTownnpcs, int money)
        {
            if (Fargowiltas.ModLoaded["FargowiltasSouls"] && (bool)ModLoader.GetMod("FargowiltasSouls").Call("DevianttAlive"))
                return false;

            return GetInstance<FargoConfig>().Devi && !FargoGlobalNPC.AnyBossAlive() && (FargoWorld.DownedBools["rareEnemy"] || (Fargowiltas.ModLoaded["FargowiltasSouls"] && (bool)ModLoader.GetMod("FargowiltasSouls").Call("Masomode")));
        }

        public override bool CanGoToStatue(bool toKingStatue) => !toKingStatue;

        public override void AI()
        {
            npc.breath = 200;
        }

        public override string TownNPCName()
        {
            string[] names = { "Akira", "Remi", "Bloom", "Yuuki", "Seira", "Koi", "Elly", "Lori", "Calius", "Teri", "Artt" };
            return Main.rand.Next(names);
        }

        public override string GetChat()
        {
            if (Main.bloodMoon && Main.rand.NextBool(2))
            {
                return "The blood moon's effects? I'm not human anymore, so nope!";
            }

            List<string> dialogue = new List<string>
            {
                "Did you know? The only real music genres are death metal and artcore.",
                "I'll have you know I'm over a hundred Fargo years old! Don't ask me how long a Fargo year is.",
                "I might be able to afford a taller body if you keep buying!",
                "Where's that screm cat?",
                $"{Main.LocalPlayer.name}! I saw something rodent-y just now! You don't have a hamster infestation, right? Right!?",
                "You're the Terrarian? Honestly, I was expecting someone a little... taller.",
                "Don't look at me like that! The only thing I've deviated from is my humanity.",
                "Rip and tear and buy from me for more things to rip and tear!",
                "What's a chee-bee doe-goe?",
                "Wait a second. Are you sure this house isn't what they call 'prison?'",
                "Deviantt has awoken! Quick, give her all your money to defeat her!",
                "One day, I'll sell a summon for myself! ...Just kidding.",
                "Hmm, I can tell! You've killed a lot, but you haven't killed enough!",
                "Why the extra letter, you ask? Only the strongest sibling is allowed to remove their own!",
                "The more rare things you kill, the more stuff I sell! Simple, right?",
            };

            int mutant = NPC.FindFirstNPC(mod.NPCType("Mutant"));
            if (mutant != -1)
            {
                dialogue.Add($"Can you tell {Main.npc[mutant].GivenName} to put some clothes on?");
                dialogue.Add($"One day, I'll sell a summon for myself! ...Just kidding. That's {Main.npc[mutant].GivenName}'s job.");
                dialogue.Add($"{Main.npc[mutant].GivenName} is here! That's my big brother!");
            }

            int abom = NPC.FindFirstNPC(mod.NPCType("Abominationn"));
            if (abom != -1)
            {
                dialogue.Add($"{Main.npc[abom].GivenName} is here! That's my big-but-not-biggest brother!");
            }

            int lumberjack = NPC.FindFirstNPC(mod.NPCType("LumberJack"));
            if (lumberjack != -1)
            {
                dialogue.Add($"What's that? You want to fight {Main.npc[lumberjack].GivenName}? ...even I know better than to try.");
            }

            int angler = NPC.FindFirstNPC(NPCID.Angler);
            if (angler != -1)
            {
                dialogue.Add($"Have you ever considered throwing {Main.npc[angler].GivenName} back where you found him?");
            }

            if (Fargowiltas.ModLoaded["FargowiltasSouls"] && (bool)ModLoader.GetMod("FargowiltasSouls").Call("Masomode"))
            {
                dialogue.Add("Embrace suffering... and while you're at it, embrace another purchase!");
            }

            return Main.rand.Next(dialogue);
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            if (Fargowiltas.ModLoaded["FargowiltasSouls"] && (bool)ModLoader.GetMod("FargowiltasSouls").Call("Masomode"))
            {
                button2 = (bool)ModLoader.GetMod("FargowiltasSouls").Call("GiftsReceived") ? "Help" : "[c/" + Main.DiscoColor.Hex3() +":Receive Gift]";
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
            else if (Fargowiltas.ModLoaded["FargowiltasSouls"] && (bool)ModLoader.GetMod("FargowiltasSouls").Call("Masomode"))
            {
                /*if ((bool)ModLoader.GetMod("FargowiltasSouls").Call("DownedMutant"))
                    FargosLore();
                else*/
                    Fargos();
            }
        }

        public static void AddItem(bool check, int item, int price, ref Chest shop, ref int nextSlot)
        {
            if (check)
            {
                shop.item[nextSlot].SetDefaults(item);
                shop.item[nextSlot].shopCustomPrice = price;
                nextSlot++;
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            if (Fargowiltas.ModLoaded["FargowiltasSoulsDLC"])
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("FargowiltasSoulsDLC").ItemType("PandorasBox"));
                nextSlot++;
            }

            if (Fargowiltas.ModLoaded["FargowiltasSouls"])
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("FargowiltasSouls").ItemType("EurusSock"));
                nextSlot++;

                if ((bool)ModLoader.GetMod("FargowiltasSouls").Call("Masomode"))
                {
                    shop.item[nextSlot].SetDefaults(ItemType<Items.Misc.EternityAdvisor>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1);
                    nextSlot++;
                }
            }

            AddItem(FargoWorld.DownedBools["worm"], ItemType<WormSnack>(), Item.buyPrice(0, 2), ref shop, ref nextSlot);
            AddItem(FargoWorld.DownedBools["pinky"], ItemType<PinkSlimeCrown>(), Item.buyPrice(0, 5), ref shop, ref nextSlot);
            AddItem(FargoWorld.DownedBools["doctorBones"], ItemType<Eggplant>(), Item.buyPrice(0, 2), ref shop, ref nextSlot);
            AddItem(FargoWorld.DownedBools["undeadMiner"], ItemType<AttractiveOre>(), Item.buyPrice(0, 3), ref shop, ref nextSlot);
            AddItem(FargoWorld.DownedBools["tim"], ItemType<HolyGrail>(), Item.buyPrice(0, 5), ref shop, ref nextSlot);
            AddItem(NPC.downedBoss3 && FargoWorld.DownedBools["dungeonSlime"], ItemType<SlimyLockBox>(), Item.buyPrice(0, 10), ref shop, ref nextSlot);
            AddItem(Main.hardMode && FargoWorld.DownedBools["medusa"], ItemType<AthenianIdol>(), Item.buyPrice(0, 5), ref shop, ref nextSlot);
            AddItem(Main.hardMode && FargoWorld.DownedBools["clown"], ItemType<ClownLicense>(), Item.buyPrice(0, 5), ref shop, ref nextSlot);
            AddItem(FargoWorld.DownedBools["nymph"], ItemType<HeartChocolate>(), Item.buyPrice(0, 10), ref shop, ref nextSlot);
            //AddItem(FargoWorld.DownedBools["babyGuardian"], ItemType<InnocuousSkull>(), Item.buyPrice(0, 10), ref shop, ref nextSlot);
            AddItem(Main.hardMode && FargoWorld.DownedBools["moth"], ItemType<MothLamp>(), Item.buyPrice(0, 10), ref shop, ref nextSlot);
            AddItem(Main.hardMode && FargoWorld.DownedBools["rainbowSlime"], ItemType<DilutedRainbowMatter>(), Item.buyPrice(0, 10), ref shop, ref nextSlot);
            AddItem(Main.hardMode && FargoWorld.DownedBools["wyvern"], ItemType<CloudSnack>(), Item.buyPrice(0, 10), ref shop, ref nextSlot);
            AddItem(Main.hardMode && FargoWorld.DownedBools["runeWizard"], ItemType<RuneOrb>(), Item.buyPrice(0, 15), ref shop, ref nextSlot);
            AddItem(Main.hardMode && FargoWorld.DownedBools["mimic"], ItemType<SuspiciousLookingChest>(), Item.buyPrice(0, 20), ref shop, ref nextSlot);
            AddItem(Main.hardMode && FargoWorld.DownedBools["mimicHallow"], ItemType<HallowChest>(), Item.buyPrice(0, 30), ref shop, ref nextSlot);
            AddItem(Main.hardMode && (FargoWorld.DownedBools["mimicCorrupt"] || FargoWorld.DownedBools["mimicCrimson"]), ItemType<CorruptChest>(), Item.buyPrice(0, 30), ref shop, ref nextSlot);
            AddItem(Main.hardMode && (FargoWorld.DownedBools["mimicCrimson"] || FargoWorld.DownedBools["mimicCorrupt"]), ItemType<CrimsonChest>(), Item.buyPrice(0, 30), ref shop, ref nextSlot);
            AddItem(Main.hardMode && FargoWorld.DownedBools["mimicJungle"], ItemType<JungleChest>(), Item.buyPrice(0, 30), ref shop, ref nextSlot);
            AddItem(Main.hardMode && FargoWorld.DownedBools["iceGolem"], ItemType<CoreoftheFrostCore>(), Item.buyPrice(0, 10), ref shop, ref nextSlot);
            AddItem(Main.hardMode && FargoWorld.DownedBools["sandElemental"], ItemType<ForbiddenForbiddenFragment>(), Item.buyPrice(0, 10), ref shop, ref nextSlot);
            AddItem(Main.hardMode && NPC.downedGoblins && FargoWorld.DownedBools["goblinSummoner"], ItemType<ShadowflameIcon>(), Item.buyPrice(0, 10), ref shop, ref nextSlot);
            AddItem(Main.hardMode && NPC.downedPirates && FargoWorld.DownedBools["pirateCaptain"], ItemType<PirateFlag>(), Item.buyPrice(0, 15), ref shop, ref nextSlot);
            AddItem(NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && FargoWorld.DownedBools["mothron"], ItemType<MothronEgg>(), Item.buyPrice(0, 15), ref shop, ref nextSlot);
            AddItem(NPC.downedPlantBoss && FargoWorld.DownedBools["boneLee"], ItemType<LeesHeadband>(), Item.buyPrice(0, 15), ref shop, ref nextSlot);
            AddItem(NPC.downedPlantBoss && FargoWorld.DownedBools["paladin"], ItemType<GrandCross>(), Item.buyPrice(0, 15), ref shop, ref nextSlot);
            AddItem(NPC.downedPlantBoss && FargoWorld.DownedBools["skeletonGun"], ItemType<AmalgamatedSkull>(), Item.buyPrice(0, 30), ref shop, ref nextSlot);
            AddItem(NPC.downedPlantBoss && FargoWorld.DownedBools["skeletonMage"], ItemType<AmalgamatedSpirit>(), Item.buyPrice(0, 30), ref shop, ref nextSlot);
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            if (NPC.downedMoonlord)
            {
                damage = 80;
                knockback = 4f;
            }
            else if (Main.hardMode)
            {
                damage = 40;
                knockback = 4f;
            }
            else
            {
                damage = 20;
                knockback = 2f;
            }
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = NPC.downedMoonlord ? 1 : 30;
            if (!NPC.downedMoonlord)
            {
                randExtraCooldown = 30;
            }
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = NPC.downedMoonlord ? mod.ProjectileType("FakeHeartMarkDeviantt") : mod.ProjectileType("FakeHeartDeviantt");
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 10f;
            randomOffset = 0f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 8; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default, 0.8f);
                }

                Vector2 pos = npc.position + new Vector2(Main.rand.Next(npc.width - 8), Main.rand.Next(npc.height / 2));
                Gore.NewGore(pos, npc.velocity, mod.GetGoreSlot("Gores/DevianttGore3"));

                pos = npc.position + new Vector2(Main.rand.Next(npc.width - 8), Main.rand.Next(npc.height / 2));
                Gore.NewGore(pos, npc.velocity, mod.GetGoreSlot("Gores/DevianttGore2"));

                pos = npc.position + new Vector2(Main.rand.Next(npc.width - 8), Main.rand.Next(npc.height / 2));
                Gore.NewGore(pos, npc.velocity, mod.GetGoreSlot("Gores/DevianttGore1"));
            }
            else
            {
                for (int k = 0; k < damage / npc.lifeMax * 50.0; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, hitDirection, -1f, 0, default, 0.6f);
                }
            }
        }

        private void Fargos()
        {
            Player player = Main.LocalPlayer;

            //devi gifts
            if (!(bool)ModLoader.GetMod("FargowiltasSouls").Call("GiftsReceived"))
            {
                ModLoader.GetMod("FargowiltasSouls").Call("GiveDevianttGifts");

                Main.npcChatText = "This world looks tougher than usual, so you can have these on the house just this once! Talk to me if you need any tips, yeah?";

                return;
            }

            Main.npcChatText = Fargowiltas.dialogueTracker.GetDialogue(npc.GivenName);
        }

        private void FargosLore()
        {
            IList<string> dialogue = new List<string>
            {
                "We all came from the end of time. This past world is a lot better than the timeless abyss of nothing!",
                "Lumberjack is 'the one who cuts.' That means trees, connections, and even severing alternate timelines.",
                "Who do you think we are? We're parts of you, a few hundred million years from now after you shed the need for a physical body.",
                "Mutant is inhabiting the physical shell of your future self, but we're all manifestations of your power and experience.",
                "In our first past, it took you eons to amass power. Since we happened to come back, we decided to help speed it up a little!",
                "Even if the three of us joined forces again, we still wouldn't regain the full power of our original self. You could probably still beat us!",
                "To accelerate your growth, Mutant released his powers to the rest of the world. Good work gathering it all back for yourself!",
                "Don't worry about our true names. We don't actually have any!",
                "No hard feelings about killing Abominationn, by the way. He comes back, right? Not that it won't miff Mutant if you do it again!",
                "We summon enemies and control events because we are them! Sort of. It's a long story.",
                "Take on a bigger form? I could do that, but I don't feel like it! Sorry!",
                "Why was Mutant in that big slime? It was the best way to power it up. Too bad he's too lazy to do that with the rest!",
                "Don't worry about the end of time, it's still billions of years away! I think. Dunno how this timey-wimey stuff works, really!",
                "There's no fighting Lumberjack at full power. He's already cut away every timeline in which you tried.",
                "Cthulhu? Hastur? All I know is where we came from, so your guess is as good as mine when it comes to them!",
                "I once heard Mutant mention a once cat-like being so far beyond us that its existence transcends cause and effect. It's more like a law of reality."
            };

            Main.npcChatText = Main.rand.Next(dialogue);
        }

        public override void NPCLoot()
        {
            if (Fargowiltas.ModLoaded["FargowiltasSouls"] && NPC.AnyNPCs(ModLoader.GetMod("FargowiltasSouls").NPCType("CosmosChampion")))
                Item.NewItem(npc.Hitbox, mod.ItemType("WalkingRick"));
        }
    }
}
