using Myra.Graphics2D.UI;
using Resources;

namespace UI
{
    public class UISettingsComponent : UIComponent
    {
        private VerticalStackPanel _panelGameplay;
        private VerticalStackPanel _panelGraphics;
        private VerticalStackPanel _panelSound;
        private VerticalStackPanel _panelControls;

        public UISettingsComponent()
        {
            SetTemplate(UITemplates.SETTINGS);
        }

        public override void Init()
        {
            // cache panels
            _panelGameplay = UI.UIManager.UIDesktop.FindById("panelGameplay") as VerticalStackPanel;
            _panelGraphics = UI.UIManager.UIDesktop.FindById("panelGraphics") as VerticalStackPanel;
            _panelSound = UI.UIManager.UIDesktop.FindById("panelSound") as VerticalStackPanel;
            _panelControls = UI.UIManager.UIDesktop.FindById("panelControls") as VerticalStackPanel;

            // tabs
            (UI.UIManager.UIDesktop.FindById("tabGameplay") as TextButton).TouchUp += (s, e) => ShowPanel("gameplay");
            (UI.UIManager.UIDesktop.FindById("tabGraphics") as TextButton).TouchUp += (s, e) => ShowPanel("graphics");
            (UI.UIManager.UIDesktop.FindById("tabSound") as TextButton).TouchUp += (s, e) => ShowPanel("sound");
            (UI.UIManager.UIDesktop.FindById("tabControls") as TextButton).TouchUp += (s, e) => ShowPanel("controls");

            // slider labels
            WireSlider("sliderBrightness", "lblBrightness");
            WireSlider("sliderMaster", "lblMaster");
            WireSlider("sliderMusic", "lblMusic");
            WireSlider("sliderSFX", "lblSFX");

            // close
            (UI.UIManager.UIDesktop.FindById("btnCloseSettings") as TextButton).TouchUp
                += (s, e) => UI.UIManager.ExecuteAction("ingame.settings");

            // show gameplay by default
            ShowPanel("gameplay");

            base.Init();
        }

        private void ShowPanel(string name)
        {
            _panelGameplay.Visible = name == "gameplay";
            _panelGraphics.Visible = name == "graphics";
            _panelSound.Visible = name == "sound";
            _panelControls.Visible = name == "controls";
        }

        private void WireSlider(string sliderId, string labelId)
        {
            var slider = UI.UIManager.UIDesktop.FindById(sliderId) as HorizontalSlider;
            var label = UI.UIManager.UIDesktop.FindById(labelId) as Label;
            if (slider == null || label == null) return;
            slider.ValueChanged += (s, e) => label.Text = ((int)slider.Value).ToString();
        }
    }
}