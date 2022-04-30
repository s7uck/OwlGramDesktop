using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using muxc = Microsoft.UI.Xaml.Controls;
using OwlDesktop.Views;
using OwlDesktop.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
// Pazzo assurdo

namespace OwlDesktop
{
    public sealed partial class MainPage : Page
    {
        public ApplicationView View { get; private set; }
        public CoreApplicationView CoreView { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();

            View = ApplicationView.GetForCurrentView();
            CoreView = CoreApplication.GetCurrentView();
            ExpandView(TitleBar);
        }

        internal void ExpandView(Grid titleBar = null)
        {
            CoreView.TitleBar.ExtendViewIntoTitleBar = true;
            if (titleBar != null)
                Window.Current.SetTitleBar(titleBar);
        }

        private readonly List<(string Tag, Type Page)> NavbarPages = new List<(string Tag, Type Page)>
        {
            ("allChats", typeof(AllChats)),
            ("calls", typeof(AllCalls)),
            ("contacts", typeof(Contacts)),
            ("archived", typeof(ArchivedChats)),
        };

        private void LoadNavbar(object sender, RoutedEventArgs e)
        {
            NavbarPageContent.Navigated += PageNavigated;

            Navbar.SelectedItem = HomePageNav;
        }

        private void NavbarNavigate(string tag, NavigationTransitionInfo transitionInfo)
        {
            Type NavbarPage = null;
            if (tag == "settings") NavbarPage = typeof(SettingsPage);
            else
            {
                var item = NavbarPages.FirstOrDefault(i => i.Tag.Equals(tag));
                NavbarPage = item.Page;
            }
            var currentPage = NavbarPageContent.CurrentSourcePageType;
            if (!(NavbarPage is null) && !Type.Equals(currentPage, NavbarPage))
                NavbarPageContent.Navigate(NavbarPage, null, transitionInfo);
        }

        private void PageNavigated(object sender, NavigationEventArgs e)
        {
            Navbar.IsBackEnabled = NavbarPageContent.CanGoBack;

            if (NavbarPageContent.SourcePageType == typeof(SettingsPage))
                Navbar.SelectedItem = Navbar.SettingsItem;
            else if (NavbarPageContent.SourcePageType != null)
            {
                var item = NavbarPages.FirstOrDefault(i => i.Page == e.SourcePageType);

                Navbar.SelectedItem = Navbar.MenuItems
                    .OfType<muxc.NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag));
            }
        }

        private void NavbarPageSelected(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs e)
        {
            if (e.IsSettingsSelected)
                NavbarNavigate("settings", e.RecommendedNavigationTransitionInfo);
            else if (e.SelectedItemContainer != null && e.SelectedItemContainer.Tag != null)
            {
                var tag = e.SelectedItemContainer.Tag.ToString();
                NavbarNavigate(tag, e.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavigatingBack(muxc.NavigationView sender, muxc.NavigationViewBackRequestedEventArgs e)
        {
            TryNavigateBack();
        }

        private void TryNavigateBack()
        {
            if (!NavbarPageContent.CanGoBack)
                return;

            if (Navbar.IsPaneOpen && Navbar.DisplayMode != muxc.NavigationViewDisplayMode.Expanded)
                return;

            NavbarPageContent.GoBack();
        }
    }
}
