using Entities;
using System;
using System.Collections.Generic;

namespace UI
{
    public class UIInventoryPagerService
    {
        public static readonly int MAX_SLOT_COUNT_PER_PAGE = 40;

        // The list being paged (may be filtered). Always reassigned via UpdateList.
        public List<Item> SourceList;

        public List<List<Item>> Pages;
        public int CurrentPage;

        public UIInventoryPagerService(List<Item> itemList)
        {
            CurrentPage = 0;
            UpdateList(itemList);
        }

        public void UpdateList(List<Item> newList)
        {
            SourceList = newList;
            CurrentPage = 0;

            Pages = new List<List<Item>>();
            for (int i = 0; i < SourceList.Count; i += MAX_SLOT_COUNT_PER_PAGE)
                Pages.Add(SourceList.GetRange(i, Math.Min(MAX_SLOT_COUNT_PER_PAGE, SourceList.Count - i)));

            if (Pages.Count == 0)
                Pages.Add(new List<Item>());
        }

        public List<Item> GetCurrentPage() => Pages[CurrentPage];

        // Page offset in the source list — used by drag service to map slot index back
        // to absolute index in the full list.
        public int GetCurrentPageOffset() => CurrentPage * MAX_SLOT_COUNT_PER_PAGE;

        public void SwitchToPrevious()
        {
            CurrentPage = (CurrentPage == 0) ? Pages.Count - 1 : CurrentPage - 1;
        }

        public void SwitchToNext()
        {
            CurrentPage = (CurrentPage == Pages.Count - 1) ? 0 : CurrentPage + 1;
        }

        public string GetIndicatorToString() => $"{CurrentPage + 1}/{Pages.Count}";
    }
}