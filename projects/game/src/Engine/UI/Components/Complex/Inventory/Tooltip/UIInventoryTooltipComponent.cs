using Entities;
using Microsoft.Xna.Framework;
using Resources;
using static Resources.StaticSpriteFactory;

namespace UI
{
    public class UIInventoryItemTooltipComponent : UIComponent
    {
        private const float Pad = 8f;
        private const float IconSize = 48f;
        private const float LineH = 14f;
        private const float StatCellW = 95f;
        private const int StatCols = 3;
        private const int FontId = 0;

        private const float TooltipW = 310f;
        private const float TooltipH = 160f;

        public UIInventoryItemTooltipComponent(int id) : base(id)
        {
            type = UIComponentTypes.HUD;
        }

        public void Show(Item item)
        {
            if (item == null) { children = null; return; }

            Vector2 mouse = Inputs.Inputs.mouse.GetMouseScreenPosition();
            Vector2 o = new Vector2(mouse.X, Graphics.Graphics.ScreenResolution.Y - mouse.Y) + new Vector2(Pad, Pad);

            var parts = new System.Collections.Generic.List<UIComponent>();

            // Frame background — framePos is the bottom-left inner corner (same as o),
            // frameSize covers the full content area
            parts.Add(new UIFrameComponent(-1, new Vector2(o.X, o.Y - TooltipH), new Vector2(TooltipW, TooltipH)));

            // Icon
            parts.Add(new UIIconComponent(-1, GetItemUISprite(item),
                o, new Vector2(0.75f, 0.75f)));

            float tx = o.X + IconSize + Pad;
            float ty = o.Y;

            // Name
            parts.Add(T($"<colored_severity=\"{Sev(item.Rarity)}\">{item.Name}</colored>", new Vector2(tx, ty))); ty -= LineH + 2f;
            parts.Add(T($"<colored_severity=\"{Sev(item.Rarity)}\">{item.Rarity}</colored>", new Vector2(tx, ty))); ty -= LineH + 2f;
            parts.Add(T($"<colored_severity=\"none\">{item.Value}</colored>", new Vector2(tx, ty)));

            // Type line
            float sy = o.Y - IconSize - LineH;
            parts.Add(T($"<colored_severity=\"read\">{item.Type.ToString().Replace("_", " ")}</colored>", new Vector2(o.X, sy)));
            sy -= LineH + 2f;

            // Stats grid
            if (item is Equipment eq && eq.BattleItemStatsData != null)
            {
                var dmg = eq.BattleItemStatsData.DamageSet;
                var def = eq.BattleItemStatsData.DefenseSet;
                var cost = eq.BattleItemStatsData.StatsCostSet;

                (string text, string sev)[][] rows =
                {
                    new[] { ($"Phys Dmg {dmg.PhysDamage:F0}",  "none"),   ($"Magic Dmg {dmg.MagicDamage:F0}", "mystery"), ($"Poise {eq.BattleItemStatsData.PoiseDamage:F0}", "none") },
                    new[] { ($"Phys Def {def.PhysDef:F0}",      "danger"), ($"Magic Def {def.MagicDef:F0}",   "danger"),  ($"Knockback {eq.BattleItemStatsData.KnockbackPower:F1}", "none") },
                    new[] { ($"Stamina {cost.StaminaCost:F0}",  "danger"), ($"Mana {cost.ManaCost:F0}",       "mystery"), ("", "none") },
                };

                for (int row = 0; row < rows.Length; row++)
                    for (int col = 0; col < StatCols; col++)
                    {
                        if (string.IsNullOrEmpty(rows[row][col].text)) continue;
                        parts.Add(T(
                            $"<colored_severity=\"{rows[row][col].sev}\">{rows[row][col].text}</colored>",
                            new Vector2(o.X + col * StatCellW, sy - row * (LineH + 2f))));
                    }

                sy -= rows.Length * (LineH + 2f) + 4f;
            }

            // Description
            if (!string.IsNullOrEmpty(item.Description))
                parts.Add(T(item.Description, new Vector2(o.X, sy)));

            children = parts.ToArray();
        }

        public override void Update()
        {
            if (children == null) return;
            foreach (var c in children) c?.Update();
        }

        public override void Draw()
        {
            if (children == null) return;
            foreach (var c in children) c?.Draw();
        }

        private static UITextStringComponent T(string text, Vector2 pos) =>
            new UITextStringComponent(-1, pos, text, FontId, Vector2.One, Color.White);

        private static string Sev(ItemRarity r) => r switch
        {
            ItemRarity.TRASH or ItemRarity.COMMON => "read",
            ItemRarity.UNCOMMON => "none",
            ItemRarity.RARE => "mystery",
            _ => "danger"
        };
    }
}