using System;
using System.Collections.Generic;
using System.Drawing;

namespace LoadMenu.Menu
{
    public class MenuItem
    {
        public MenuItem()
        {
            ActionType = Menu.ActionType.FORM;
            Items = new List<MenuItem>();
            Key = 0;
            FormType = FormTypes.FORM_NONE;
            Dialog = false;
            Duplicate = false;
            Popup = false;
        }

        public string Group { get; set; }

        public bool Dialog { get; set; }

        public bool Popup { get; set; }

        public bool Duplicate { get; set; }

        public Type Type { get; set; }

        public string Page { get; set; }

        public int GroupIndex { get; set; }

        public int PageIndex { get; set; }

        public string Text { get; set; }

        public int Index { get; set; }

        public Image Image { get; set; }

        public int RibbonType { get; set; }

        public Image Icon { get; set; }

        public int ActionType { get; set; }

        public List<MenuItem> Items { get; set; }

        public string Url { get; set; }

        public int Key { get; set; }

        public int FormType { get; set; }
    }
}
