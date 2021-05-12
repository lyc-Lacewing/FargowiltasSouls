using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using FargowiltasSouls.Toggler;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    [AutoloadEquip(EquipType.Shield)]
    public class IronEnchant : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Enchantment");

            string tooltip =
@"Grants immunity to knockback
Right Click to guard with your shield
You will totally block an attack if timed correctly
You attract items from a larger range
'Strike while the iron is hot'";

            string tooltip_ch =
@"允许使用者向敌人冲刺
右键用盾牌防御
拾取物品半径增大
'趁热打铁'";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "铁魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SafeModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(152, 142, 131);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = ItemRarityID.Green;
            item.value = 40000;
            //item.shieldSlot = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();

            //cobalt shield
            player.noKnockback = true;

            if (player.GetToggleValue("IronS"))
            {
                //shield
                modPlayer.IronEffect();
            }
            //magnet
            if (player.GetToggleValue("IronM", false))
            {
                modPlayer.IronEnchant = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronHelmet);
            recipe.AddIngredient(ItemID.IronChainmail);
            recipe.AddIngredient(ItemID.IronGreaves);
            recipe.AddIngredient(ItemID.CobaltShield);
            //treasure magnet
            //empty bucket
            recipe.AddIngredient(ItemID.IronBroadsword);
            //recipe.AddIngredient(ItemID.IronBow);
            //apricot (high in iron pog)
            recipe.AddIngredient(ItemID.ZebraSwallowtailButterfly);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}