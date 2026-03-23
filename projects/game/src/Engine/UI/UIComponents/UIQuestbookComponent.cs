using Myra.Graphics2D.UI;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIQuestbookComponent : UIComponent
    {

        public UIQuestbookComponent()
        {
            SetTemplate(UITemplates.QUESTBOOK);
        }


        public override void Init()
        {
            var quests = new[]
            {
                new { Title = "The Lost Sword",       Status = "In Progress", Reward = "150 Gold",  Desc = "A legendary sword has gone missing from the royal vault..." },
                new { Title = "Dark Forest",          Status = "Not Started", Reward = "80 Gold",   Desc = "Strange creatures have been spotted in the Dark Forest..." },
                new { Title = "The Missing Merchant", Status = "Completed",   Reward = "200 Gold",  Desc = "A merchant traveling from Aldor never arrived at the city..." },
            };

            for (int i = 0; i < quests.Length; i++)
            {
                var index = i;
                var btn = UI.UIManager.UIDesktop.FindById($"quest{i}") as TextButton;
                if (btn == null) continue;

                btn.TouchUp += (s, e) =>
                {
                    var q = quests[index];
                    (UI.UIManager.UIDesktop.FindById("questTitle") as Label).Text = q.Title;
                    (UI.UIManager.UIDesktop.FindById("questStatus") as Label).Text = q.Status;
                    (UI.UIManager.UIDesktop.FindById("questReward") as Label).Text = q.Reward;
                    (UI.UIManager.UIDesktop.FindById("questDescription") as Label).Text = q.Desc;
                };
            }

            //var btnClose = UI.UIManager.UIDesktop.FindById("btnCloseQuestBook") as TextButton;
            //btnClose.TouchUp += (s, e) => Visible = false;

            base.Init();
        }
    }
}
