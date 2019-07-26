using System.Collections.Generic;
using LoadMenu.Forms.Page_0;
using LoadMenu.Menu;
using LoadMenu.Properties;

namespace LoadMenu.Pages
{
    public class Page1Interface : IUserInterface
    {
        #region IUserInterface Members

        public IEnumerable<MenuItem> GetUserInterface()
        {
            List<MenuItem> menus = new List<MenuItem>
                                   {
                                           new MenuItem
                                           {
                                                   Page = "Page 1",
                                                   PageIndex = 0,
                                                   Group = "Group 1",
                                                   GroupIndex = 0,
                                                   Text = "Form 1",
                                                   Index = 0,
                                                   Image = Resources.addressbook32,
                                                   Icon = Resources.addressbook32,
                                                   Type = typeof(Form1),
                                                   Key = 1016,
                                                   Dialog = true
                                           },
                                           new MenuItem
                                           {
                                                   Page = "Page 1",
                                                   PageIndex = 0,
                                                   Group = "Group 1",
                                                   GroupIndex = 0,
                                                   Text = "Form 2",
                                                   Index = 1,
                                                   Image = Resources.map32,
                                                   Icon = Resources.map24,
                                                   Type = typeof(Form2),
                                                   Key = 1016
                                           },
                                           new MenuItem
                                           {
                                                   Page = "Page 1",
                                                   PageIndex = 0,
                                                   Group = "Group 2",
                                                   GroupIndex = 1,
                                                   Text = "Form 3",
                                                   Index = 0,
                                                   Image = Resources.multivehicle32,
                                                   Icon = Resources.multivehicle24,
                                                   //Type = typeof(Form3),
                                                   Key = 1016,
                                                   Items = new List<MenuItem>
                                                           {
                                                                   new MenuItem
                                                                   {
                                                                           Text = "Form 4",
                                                                           Index = 0,
                                                                           Image = Resources.delete_route,
                                                                           Icon = Resources.arrow,
                                                                           Type = typeof(Form4),
                                                                           Key = 1012
                                                                   }
                                                           }
                                           }
                                   };
            return menus;
        }

        #endregion
    }
}
