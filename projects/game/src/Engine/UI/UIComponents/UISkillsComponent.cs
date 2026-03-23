using Myra.Graphics2D.UI;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.TextureAtlases;
using Microsoft.Xna.Framework;
using Resources;
using System.Collections.Generic;
using System;
using Myra.Graphics2D;

namespace UI
{
    public class UISkillsComponent : UIComponent
    {
        // mock skill data
        private class SkillNode
        {
            public string Id;
            public string Name;
            public string Description;
            public int X, Y;
            public bool Unlocked;
            public List<string> Children = new List<string>();
        }

        private readonly List<SkillNode> _nodes = new List<SkillNode>
        {
            // root at bottom
            new SkillNode { Id = "root",    Name = "Warrior",      Description = "Base class",           X = 240, Y = 1100, Unlocked = true,  Children = { "str1", "agi1", "mag1" } },

            // str branch
            new SkillNode { Id = "str1",    Name = "Power Strike", Description = "+10% damage",          X = 80,  Y = 980,  Unlocked = true,  Children = { "str2" } },
            new SkillNode { Id = "str2",    Name = "Cleave",       Description = "Hit all nearby",       X = 80,  Y = 860,  Unlocked = false, Children = { "str3" } },
            new SkillNode { Id = "str3",    Name = "Berserker",    Description = "+30% dmg below 30% HP",X = 80,  Y = 740,  Unlocked = false, Children = { } },

            // agi branch
            new SkillNode { Id = "agi1",    Name = "Swift Feet",   Description = "+10% move speed",      X = 240, Y = 980,  Unlocked = true,  Children = { "agi2" } },
            new SkillNode { Id = "agi2",    Name = "Dodge Roll",   Description = "Iframe on roll",       X = 240, Y = 860,  Unlocked = false, Children = { "agi3" } },
            new SkillNode { Id = "agi3",    Name = "Shadow Step",  Description = "Blink behind enemy",   X = 240, Y = 740,  Unlocked = false, Children = { } },

            // mag branch
            new SkillNode { Id = "mag1",    Name = "Arcane Touch", Description = "+5 magic power",       X = 400, Y = 980,  Unlocked = false, Children = { "mag2" } },
            new SkillNode { Id = "mag2",    Name = "Fireball",     Description = "Ranged fire attack",   X = 400, Y = 860,  Unlocked = false, Children = { "mag3" } },
            new SkillNode { Id = "mag3",    Name = "Meteor",       Description = "Massive AoE strike",   X = 400, Y = 740,  Unlocked = false, Children = { } },

            // cross-branch at top
            new SkillNode { Id = "hybrid1", Name = "War Mage",     Description = "Melee + magic combo",  X = 240, Y = 620,  Unlocked = false, Children = { } },
        };

        public UISkillsComponent()
        {
            SetTemplate(UITemplates.SKILLS);
        }

        public override void Init()
        {
            var tree = UI.UIManager.UIDesktop.FindById("skillsTree") as Panel;
            if (tree != null)
                BuildTree(tree);

            // scroll to bottom after layout
            var scroll = UI.UIManager.UIDesktop.FindById("skillsScroll") as ScrollViewer;
            if (scroll != null)
                scroll.ScrollPosition = new Microsoft.Xna.Framework.Point(0, int.MaxValue);

            var btnClose = UI.UIManager.UIDesktop.FindById("btnCloseSkills") as TextButton;
            if (btnClose != null)
                btnClose.TouchUp += (s, e) => UI.UIManager.ExecuteAction("ingame.skills");

            base.Init();
        }

        private void BuildTree(Panel tree)
        {
            var nodeMap = new Dictionary<string, SkillNode>();
            foreach (var node in _nodes)
                nodeMap[node.Id] = node;

            // draw nodes on top
            foreach (var node in _nodes)
                DrawNode(tree, node);
        }

        private void DrawNode(Panel tree, SkillNode node)
        {
            var color = node.Unlocked
                ? new Color(60, 180, 60, 220)
                : new Color(60, 60, 60, 180);

            var borderColor = node.Unlocked
                ? new Color(100, 220, 100, 255)
                : new Color(100, 100, 100, 200);

            var btn = new ImageButton
            {
                Id = $"skill_{node.Id}",
                Width = 56,
                Height = 56,
                Left = node.X,
                Top = node.Y,
                Background = new SolidBrush(color),
                OverBackground = new SolidBrush(new Color(80, 200, 80, 230)),
                PressedBackground = new SolidBrush(new Color(40, 140, 40, 230)),
                Border = new SolidBrush(borderColor),
                BorderThickness = new Thickness(1)
            };

            // tooltip on hover
            btn.MouseEntered += (s, e) => ShowTooltip(node.Name, node.Description, node.X, node.Y);
            btn.MouseLeft += (s, e) => HideTooltip();

            tree.Widgets.Add(btn);

            // label below icon
            var label = new Label
            {
                Text = node.Name,
                Left = node.X,
                Top = node.Y + 60,
                Width = 80,
                HorizontalAlignment = HorizontalAlignment.Left,
                TextColor = node.Unlocked ? Color.White : new Color(140, 140, 140, 255)
            };

            tree.Widgets.Add(label);
        }

        private void ShowTooltip(string name, string description, int x, int y)
        {
            var panel = UI.UIManager.UIDesktop.FindById("skillTooltipPanel") as Panel;
            var lblName = UI.UIManager.UIDesktop.FindById("skillTooltipName") as Label;
            var lblDesc = UI.UIManager.UIDesktop.FindById("skillTooltipDesc") as Label;
            if (panel == null) return;

            lblName.Text = name;
            lblDesc.Text = description;
            panel.Left = x + 60; // offset right of the icon
            panel.Top = y;
            panel.Visible = true;
        }

        private void HideTooltip()
        {
            var panel = UI.UIManager.UIDesktop.FindById("skillTooltipPanel") as Panel;
            if (panel != null) panel.Visible = false;
        }
    }
}