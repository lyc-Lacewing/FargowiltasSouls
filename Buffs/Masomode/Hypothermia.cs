using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class Hypothermia : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hypothermia");
            Description.SetDefault("Increased damage taken from cold attacks");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FargoSoulsPlayer>().Hypothermia = true;
        }
    }
}