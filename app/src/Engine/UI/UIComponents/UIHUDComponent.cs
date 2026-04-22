using Entities;
using Myra.Graphics2D.UI;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIHUDComponent : UIComponent
    {

        public HorizontalProgressBar healthBar;
        public HorizontalProgressBar manaBar;
        public HorizontalProgressBar staminaBar;

        public Label healthLabel;
        public Label manaLabel;
        public Label staminaLabel;

        public UIHUDComponent()
        {
            SetTemplate(UITemplates.HUD);
        }


        public override void Init()
        {
            healthBar = UI.UIManager.UIDesktop.FindById("healthBar") as HorizontalProgressBar;
            manaBar = UI.UIManager.UIDesktop.FindById("manaBar") as HorizontalProgressBar;
            staminaBar = UI.UIManager.UIDesktop.FindById("staminaBar") as HorizontalProgressBar;

            healthLabel = UI.UIManager.UIDesktop.FindById("lblHealth") as Label;
            manaLabel = UI.UIManager.UIDesktop.FindById("lblMana") as Label;
            staminaLabel = UI.UIManager.UIDesktop.FindById("lblStamina") as Label;

            base.Init();
        }

        public override void Update()
        {
            var hp = Entities.Entities.Player.StatsManager.GetStat(EntityStats.HP).CurrentValue;
            var maxHP = Entities.Entities.Player.StatsManager.GetStat(EntityStats.HP).MaximumValue;
            var mana = Entities.Entities.Player.StatsManager.GetStat(EntityStats.MANA).CurrentValue;
            var maxMana = Entities.Entities.Player.StatsManager.GetStat(EntityStats.MANA).MaximumValue;
            var stamina = Entities.Entities.Player.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue;
            var maxStamina = Entities.Entities.Player.StatsManager.GetStat(EntityStats.STAMINA).MaximumValue;

            healthBar.Value = hp;
            healthBar.Maximum = maxHP;
            manaBar.Value = mana;
            manaBar.Maximum = maxMana;
            staminaBar.Value = stamina;
            staminaBar.Maximum = maxStamina;

            healthLabel.Text = Math.Round(hp) + "/" + Math.Round(maxHP);
            manaLabel.Text = Math.Round(mana) + "/" + Math.Round(maxMana);
            staminaLabel.Text = Math.Round(stamina) + "/" + Math.Round(maxStamina);

            base.Update();
        }
    }
}
