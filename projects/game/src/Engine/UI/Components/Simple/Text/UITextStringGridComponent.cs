using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace UI
{
    // Lays out a grid of (label, value) text pairs in N columns.
    // Each cell is a fixed width so columns stay aligned.
    // Usage:
    //   var grid = new UITextStringGridComponent(-1, pos, cellWidth: 120, columns: 3, rowHeight: 14);
    //   grid.AddRow("Body Damage", "5", "danger", "10", "none");   // label + (value, severity) pairs
    public class UITextStringGridComponent : UIComponent
    {
        public float CellWidth;
        public float RowHeight;
        public int Columns;

        private readonly List<UITextStringComponent> _cells = new();
        private Vector2 _pos;
        private int _fontId;

        public UITextStringGridComponent(int id, Vector2 pos, float cellWidth, int columns, float rowHeight, int fontId = 0) : base(id)
        {
            type = UIComponentTypes.TEXT;
            _pos = pos;
            CellWidth = cellWidth;
            Columns = columns;
            RowHeight = rowHeight;
            _fontId = fontId;
        }

        // Add a full row: alternating label / value pairs, one pair per column.
        // labelSeverity  = severity tag name for labels  (e.g. "none", "danger", "mystery")
        // valueSeverities = per-value severity tags (same length as labels list)
        public void AddRow(params (string text, string severity)[] cells)
        {
            int row = _cells.Count / Columns;  // which row index this fills into

            for (int col = 0; col < cells.Length && col < Columns; col++)
            {
                Vector2 cellPos = new Vector2(
                    _pos.X + col * CellWidth,
                    _pos.Y - row * RowHeight
                );

                string tagged = $"<colored_severity=\"{cells[col].severity}\">{cells[col].text}</colored>";
                _cells.Add(new UITextStringComponent(-1, cellPos, tagged, _fontId, Vector2.One, Color.White));
            }
        }

        // Convenience: plain text row with a single severity for all cells
        public void AddRow(string severity, params string[] texts)
        {
            var cells = new (string, string)[texts.Length];
            for (int i = 0; i < texts.Length; i++)
                cells[i] = (texts[i], severity);
            AddRow(cells);
        }

        public void Clear() => _cells.Clear();

        public override void Update()
        {
            foreach (var cell in _cells)
                cell.Update();
        }

        public override void Draw()
        {
            foreach (var cell in _cells)
                cell.Draw();
        }
    }
}