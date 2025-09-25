using Entities;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{

    public class UIInventoryPagerService
    {

        public static readonly int MAX_SLOT_COUNT_PER_PAGE = 40;

        public List<Item> OriginalItemList;

        public List<List<Item>> Pages;
        public int CurrentPage;

        public UIInventoryPagerService(List<Item> itemList)
        {
            UpdateList(itemList);

            CurrentPage = 0;
        }


        public void UpdateList(List<Item> newList)
        {
            OriginalItemList = newList;

            Pages = new List<List<Item>>();
            for (int i = 0; i < OriginalItemList.Count; i += MAX_SLOT_COUNT_PER_PAGE)
            {
                Pages.Add(OriginalItemList.GetRange(i, Math.Min(MAX_SLOT_COUNT_PER_PAGE, OriginalItemList.Count - i)));
            }
            if (Pages.Count == 0)
            {
                Pages.Add(new List<Item>());
            }
        }


        public void SwitchToPrevious()
        {
            CurrentPage--;

            if (CurrentPage == -1)
            {
                CurrentPage = Pages.Count - 1;
            }
        }

        public void SwitchToNext()
        {
            CurrentPage++;

            if (CurrentPage == Pages.Count)
            {
                CurrentPage = 0;
            }
        }

        public string GetIndicatorToString()
        {
            return (CurrentPage + 1) + "/" + Pages.Count; 
        }
    }
}
