using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using LoadMenu.Menu;

namespace LoadMenu
{
    internal class MenuManager
    {
        internal MenuManager()
        {
            Pages = new List<Page>();
        }

        internal List<Page> Pages { get; private set; }

        internal void Add(IUserInterface module)
        {
            foreach (MenuItem mnu in module.GetUserInterface())
            {
                Page page = Pages.FirstOrDefault(c => c.Text.Equals(mnu.Page,
                                                                    StringComparison.CurrentCultureIgnoreCase));
                if(page == null)
                {
                    page = new Page
                           {
                                   Text = mnu.Page,
                                   Index = mnu.PageIndex
                           };
                    Pages.Add(page);
                }

                Group group = page.Groups.FirstOrDefault(c => c.Text.Equals(mnu.Group,
                                                                            StringComparison.CurrentCultureIgnoreCase));
                if(group == null)
                {
                    group = new Group
                            {
                                    Text = mnu.Group,
                                    Index = mnu.GroupIndex
                            };
                    page.Groups.Add(group);
                }

                List<int> ids = new List<int>();
                if(mnu.Items.Any())
                {
                    foreach (MenuItem menuItem in mnu.Items)
                    {
                        ids.Add(menuItem.Key);
                    }
                }

                //kiem tra quyen o day
                Item item = new Item
                            {
                                    Text = mnu.Text,
                                    Index = mnu.Index,
                                    Image = mnu.Image,
                                    Type = mnu.Type,
                                    Icon = mnu.Icon,
                                    ActionType = mnu.ActionType,
                                    Url = mnu.Url,
                                    Dialog = mnu.Dialog,
                                    Duplicate = mnu.Duplicate,
                                    Popup = mnu.Popup,
                                    FormType = mnu.FormType
                            };
                group.Items.Add(item);
                // child
                if(mnu.Items.Count > 0)
                {
                    // kiem tra quyen o day
                    //foreach (MenuItem menuItem in mnu.Items.Where(c => AppUtilEx.CheckPermission(c.Key)))
                    foreach (MenuItem menuItem in mnu.Items)
                    {
                        Item subItem = new Item
                                       {
                                               Text = menuItem.Text,
                                               Index = menuItem.Index,
                                               Image = menuItem.Image,
                                               Type = menuItem.Type,
                                               Icon = menuItem.Icon,
                                               ActionType = menuItem.ActionType,
                                               Url = menuItem.Url,
                                               Dialog = menuItem.Dialog,
                                               Duplicate = mnu.Duplicate,
                                               Popup = mnu.Popup,
                                               FormType = mnu.FormType
                                       };
                        item.Items.Add(subItem);
                    }
                }
            }
        }

        public void RemomeEmpty()
        {
            for (int i = 0; i < Pages.Count; i++)
            {
                for (int j = 0; j < Pages[i]
                                    .Groups.Count; j++)
                {
                    if(Pages[i]
                       .Groups[j]
                       .Items.Count == 0)
                    {
                        Pages[i]
                                .Groups.RemoveAt(j--);
                    }
                }

                if(Pages[i]
                   .Groups.Count == 0)
                {
                    Pages.RemoveAt(i--);
                }
            }
        }

        public Item GetMenuFromFormType(int type)
        {
            return Pages.SelectMany(c => c.Groups)
                        .SelectMany(c => c.Items)
                        .FirstOrDefault(c => c.FormType == type);
        }

        public Item GetMenuFromFormType2(int type)
        {
            return Pages.SelectMany(c => c.Groups)
                        .SelectMany(c => c.Items)
                        .SelectMany(c => c.Items)
                        .FirstOrDefault(c => c.FormType == type);
        }

        public Type GetTypeFromFormType(int type)
        {
            Item item = GetMenuFromFormType(type);
            if(item != null)
            {
                return item.Type;
            }

            return null;
        }

        public Item GetMenuItemFromFormType(int type)
        {
            Item item = GetMenuFromFormType(type);
            if(item != null)
            {
                return item;
            }

            return null;
        }
    }

    internal class Page
    {
        internal Page()
        {
            Groups = new List<Group>();
        }

        internal string Text { get; set; }

        internal int Index { get; set; }

        internal List<Group> Groups { get; set; }
    }

    internal class Group
    {
        internal Group()
        {
            Items = new List<Item>();
        }

        internal string Text { get; set; }

        internal int Index { get; set; }

        internal List<Item> Items { get; set; }
    }

    internal class Item
    {
        public Item()
        {
            Items = new List<Item>();
            Dialog = false;
            Duplicate = false;
            Popup = false;
            FormType = 0;
        }

        internal string Text { get; set; }

        internal int Index { get; set; }

        internal Type Type { get; set; }

        internal Image Image { get; set; }

        internal Image Icon { get; set; }

        internal int ActionType { get; set; }

        internal int FormType { get; set; }

        internal List<Item> Items { get; set; }

        internal string Url { get; set; }

        public bool Dialog { get; set; }

        public bool Popup { get; set; }

        public bool Duplicate { get; set; }
    }
}
