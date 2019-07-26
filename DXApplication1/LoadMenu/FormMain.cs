using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraTabbedMdi;
using LoadMenu.Menu;
using LoadMenu.Pages;

namespace LoadMenu
{
    public partial class FormMain : RibbonForm
    {
        MenuManager _manager;

        public FormMain()
        {
            InitializeComponent();
        }

        private void LoadMenu()
        {
            _manager = new MenuManager();

            _manager.Add(new Page1Interface());
            _manager.Add(new Page2Interface());

            _manager.RemomeEmpty();

            foreach (Page item in _manager.Pages.OrderBy(c => c.Index))
            {
                // page
                RibbonPage page = new RibbonPage(item.Text);
                ribbon.DefaultPageCategory.Pages.Add(page);
                // group
                foreach (Group group in item.Groups.OrderBy(c => c.Index))
                {
                    RibbonPageGroup ribbonPageGroup = new RibbonPageGroup(group.Text)
                                                      {
                                                              ShowCaptionButton = false,
                                                              AllowTextClipping = false
                                                      };
                    page.Groups.Add(ribbonPageGroup);
                    // item
                    foreach (Item mnuItem in group.Items.OrderBy(c => c.Index))
                    {
                        BarButtonItem barButtonItem = new BarButtonItem
                                                      {
                                                              Caption = mnuItem.Text,
                                                              Tag = mnuItem
                                                      };
                        barButtonItem.ItemAppearance.Disabled.Options.UseTextOptions = true;
                        barButtonItem.ItemAppearance.Disabled.TextOptions.WordWrap = WordWrap.NoWrap;
                        barButtonItem.ItemAppearance.Hovered.Options.UseTextOptions = true;
                        barButtonItem.ItemAppearance.Hovered.TextOptions.WordWrap = WordWrap.NoWrap;
                        barButtonItem.ItemAppearance.Normal.Options.UseTextOptions = true;
                        barButtonItem.ItemAppearance.Normal.TextOptions.WordWrap = WordWrap.NoWrap;
                        barButtonItem.ItemAppearance.Pressed.Options.UseTextOptions = true;
                        barButtonItem.ItemAppearance.Pressed.TextOptions.WordWrap = WordWrap.NoWrap;
                        if(mnuItem.Items.Count == 0)
                        {
                            barButtonItem.RibbonStyle = RibbonItemStyles.Large;
                            barButtonItem.LargeGlyph = mnuItem.Image;
                        }
                        else
                        {
                            barButtonItem.ButtonStyle = BarButtonStyle.DropDown;
                            barButtonItem.RibbonStyle = RibbonItemStyles.Large;
                            barButtonItem.LargeGlyph = mnuItem.Image;
                            barButtonItem.ActAsDropDown = true;
                            // barButtonItem.DropDownEnabled = true;
                            // popup menu
                            PopupMenu popup = new PopupMenu(barManager1)
                                              {
                                                      Ribbon = ribbon
                                              };
                            foreach (Item child in mnuItem.Items.OrderBy(c => c.Index))
                            {
                                BarButtonItem childButton = new BarButtonItem
                                                            {
                                                                    Caption = child.Text,
                                                                    Tag = child,
                                                                    Glyph = child.Image
                                                            };
                                popup.AddItem(childButton);
                                childButton.ItemClick += barButtonItem_ItemClick;
                            }

                            barButtonItem.DropDownControl = popup;
                        }

                        barButtonItem.ItemClick += barButtonItem_ItemClick;
                        ribbonPageGroup.ItemLinks.Add(barButtonItem);
                    }
                }
            }
        }

        private void barButtonItem_ItemClick(object sender,
                                             ItemClickEventArgs e)
        {
            if(e?.Item?.Tag is Item item)
            {
                if(item.ActionType == ActionType.URL)
                {
                    FormBrowser form = new FormBrowser
                                       {
                                               Text = item.Text,
                                               Url = item.Url,
                                               Tag = item,
                                               MdiParent = this
                                       };
                    form.Show();
                }
                else
                {
                    AddForm(item.Type,
                            item,
                            true);
                }
            }
        }

        internal dynamic AddForm(Type type,
                                 Item item,
                                 bool focus)
        {
            if(type != null)
            {
                if(item.Duplicate)
                {
                    Form form = Activator.CreateInstance(type,
                                                         true) as Form;
                    if(form != null)
                    {
                        form.Tag = item;
                        if(item.Popup == false)
                        {
                            form.MdiParent = this;
                        }

                        if(item.FormType == FormTypes.FORM_CALLCENTER)
                        {
                            form.TopMost = true;
                            form.ShowIcon = false;
                            form.ShowInTaskbar = false;
                            form.FormBorderStyle = FormBorderStyle.None;
                            form.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - form.Width,
                                                      Screen.PrimaryScreen.WorkingArea.Bottom - form.Height);
                        }

                        form.Show();
                        return form;
                    }
                }
                else if(item.Dialog)
                {
                    Form form = Activator.CreateInstance(type,
                                                         true) as Form;
                    if(form != null)
                    {
                        form.ShowDialog();
                    }
                }
                else
                {
                    foreach (XtraMdiTabPage page in xtraTabbedMdiManager1.Pages)
                    {
                        if(page.MdiChild.GetType() == type)
                        {
                            if(focus)
                            {
                                xtraTabbedMdiManager1.SelectedPage = page;
                            }

                            return page.MdiChild;
                        }
                    }

                    Form form = Activator.CreateInstance(type,
                                                         true) as Form;
                    if(form != null)
                    {
                        form.Tag = item;
                        form.MdiParent = this;
                        form.Show();
                        return form;
                    }
                }
            }

            return null;
        }

        private void FormMain_Load(object sender,
                                   EventArgs e)
        {
            LoadMenu();
        }

        private void ribbon_Merge(object sender,
                                  RibbonMergeEventArgs e)
        {
            RibbonPage page = null;
            if(e.MergedChild.PageCategories.Count > 0)
            {
                if(e.MergedChild.PageCategories[0]
                    .Pages.Count > 0)
                {
                    page = e.MergedChild.PageCategories[0]
                            .Pages[0];
                }
            }
            else
            {
                if(e.MergedChild.DefaultPageCategory.Pages.Count > 0)
                {
                    page = e.MergedChild.DefaultPageCategory.Pages[0];
                }
            }

            if(page != null)
            {
                ribbon.SelectedPage = page;
            }

            // merge status bar
            RibbonControl parentRRibbon = sender as RibbonControl;
            RibbonControl childRibbon = e.MergedChild;
            if(childRibbon.StatusBar != null)
            {
                if(parentRRibbon != null)
                {
                    parentRRibbon.StatusBar.MergeStatusBar(childRibbon.StatusBar);
                }
            }
        }

        private void ribbon_UnMerge(object sender,
                                    RibbonMergeEventArgs e)
        {
            RibbonControl parentRRibbon = sender as RibbonControl;
            if(parentRRibbon != null)
            {
                parentRRibbon.StatusBar.UnMergeStatusBar();
            }
        }
    }
}
