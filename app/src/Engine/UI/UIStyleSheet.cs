using FontStashSharp;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI.Styles;
using System.Collections.Generic;

namespace UI
{
    public static class UIStylesheet
    {
        public static void Apply()
        {
            var stylesheet = Stylesheet.Current.Clone();

            ApplyProgressBarStyles(stylesheet);
            ApplyButtonStyles(stylesheet);
            ApplyLabelStyles(stylesheet);

            Stylesheet.Current = stylesheet;
        }

        private static void ApplyLabelStyles(Stylesheet s)
        {
            s.LabelStyle.TextColor = Color.White;

            var title = (LabelStyle)s.LabelStyle.Clone();
            title.TextColor = Color.Red;
            title.Border = new SolidBrush(Color.Red);
            title.BorderThickness = new Thickness(0, 0, 0, 1);
            s.LabelStyles["title"] = title;

            var muted = (LabelStyle)s.LabelStyle.Clone();
            muted.TextColor = new Color(180, 180, 180, 255);
            s.LabelStyles["muted"] = muted;

            var danger = (LabelStyle)s.LabelStyle.Clone();
            danger.TextColor = new Color(255, 80, 80, 255);
            s.LabelStyles["danger"] = danger;

            var success = (LabelStyle)s.LabelStyle.Clone();
            success.TextColor = new Color(80, 220, 80, 255);
            s.LabelStyles["success"] = success;

            var questTitle = (LabelStyle)s.LabelStyle.Clone();
            questTitle.TextColor = new Color(255, 220, 100, 255);
            s.LabelStyles["questTitle"] = questTitle;

            var hud = (LabelStyle)s.LabelStyle.Clone();
            hud.TextColor = Color.White;
            s.LabelStyles["hud"] = hud;
        }

        private static void ApplyButtonStyles(Stylesheet s)
        {
            s.ButtonStyle.Background = new SolidBrush(new Color(40, 40, 40, 200));
            s.ButtonStyle.OverBackground = new SolidBrush(new Color(70, 70, 70, 220));
            s.ButtonStyle.PressedBackground = new SolidBrush(new Color(20, 20, 20, 220));

            var slot = (ButtonStyle)s.ButtonStyle.Clone();
            slot.Background = new SolidBrush(new Color(17, 17, 17, 136));
            slot.OverBackground = new SolidBrush(new Color(170, 170, 68, 170));
            slot.PressedBackground = new SolidBrush(new Color(34, 34, 34, 204));
            s.ButtonStyles["slot"] = slot;

            var menuButton = (ButtonStyle)s.ButtonStyle.Clone();
            menuButton.Background = new SolidBrush(new Color(30, 30, 30, 210));
            menuButton.OverBackground = new SolidBrush(new Color(60, 90, 60, 230));
            menuButton.PressedBackground = new SolidBrush(new Color(20, 50, 20, 230));
            s.ButtonStyles["menuButton"] = menuButton;

            var dangerButton = (ButtonStyle)s.ButtonStyle.Clone();
            dangerButton.Background = new SolidBrush(new Color(80, 20, 20, 210));
            dangerButton.OverBackground = new SolidBrush(new Color(120, 30, 30, 230));
            dangerButton.PressedBackground = new SolidBrush(new Color(60, 10, 10, 230));
            s.ButtonStyles["dangerButton"] = dangerButton;

            var questItem = (ButtonStyle)s.ButtonStyle.Clone();
            questItem.Background = new SolidBrush(new Color(30, 30, 40, 180));
            questItem.OverBackground = new SolidBrush(new Color(50, 50, 80, 210));
            questItem.PressedBackground = new SolidBrush(new Color(20, 20, 60, 230));
            s.ButtonStyles["questItem"] = questItem;

            var ingameMenuButton = (ButtonStyle)s.ButtonStyle.Clone();
            ingameMenuButton.Background = null;
            ingameMenuButton.OverBackground = new SolidBrush(new Color(255, 255, 255, 30));
            ingameMenuButton.PressedBackground = new SolidBrush(new Color(255, 255, 255, 60));
            ingameMenuButton.BorderThickness = new Thickness(0);
            s.ButtonStyles["ingameMenuButton"] = ingameMenuButton;
        }

        private static void ApplyProgressBarStyles(Stylesheet s)
        {
            var healthBar = (ProgressBarStyle)s.HorizontalProgressBarStyle.Clone();
            healthBar.Background = new SolidBrush(new Color(0, 0, 0, 136));
            healthBar.Filler = new SolidBrush(new Color(221, 51, 51, 204));
            s.HorizontalProgressBarStyles["healthBar"] = healthBar;

            var manaBar = (ProgressBarStyle)s.HorizontalProgressBarStyle.Clone();
            manaBar.Background = new SolidBrush(new Color(0, 0, 0, 136));
            manaBar.Filler = new SolidBrush(new Color(51, 170, 221, 204));
            s.HorizontalProgressBarStyles["manaBar"] = manaBar;

            var staminaBar = (ProgressBarStyle)s.HorizontalProgressBarStyle.Clone();
            staminaBar.Background = new SolidBrush(new Color(0, 0, 0, 136));
            staminaBar.Filler = new SolidBrush(new Color(21, 220, 10, 204));
            s.HorizontalProgressBarStyles["staminaBar"] = staminaBar;

            var bossBar = (ProgressBarStyle)s.HorizontalProgressBarStyle.Clone();
            bossBar.Background = new SolidBrush(new Color(0, 0, 0, 136));
            bossBar.Filler = new SolidBrush(new Color(221, 102, 34, 204));
            s.HorizontalProgressBarStyles["bossBar"] = bossBar;

            var xpBar = (ProgressBarStyle)s.HorizontalProgressBarStyle.Clone();
            xpBar.Background = new SolidBrush(new Color(0, 0, 0, 136));
            xpBar.Filler = new SolidBrush(new Color(180, 130, 255, 204));
            s.HorizontalProgressBarStyles["xpBar"] = xpBar;
        }

        private static void ApplyPanelStyles(Stylesheet s)
        {

        }
    }
}