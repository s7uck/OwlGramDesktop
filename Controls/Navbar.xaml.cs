using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;
using muxc = Microsoft.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace OwlDesktop.Controls
{
    public sealed partial class Navbar
    {
        public muxc.NavigationView NavigationView { get; set; }
        public Frame NavbarPageContent { get; set; }
        public List<(string Tag, Type Page)> Pages { get; set; }
        public muxc.NavigationViewItem HomePage { get; set; }
        public Type SettingsPage { get; set; }

        public Navbar(
            muxc.NavigationView nav, Frame navPageContent,
            List<(string Tag, Type Page)> pages,
            muxc.NavigationViewItem homePage = null, Type settingsPage = null
        )
        {
            NavigationView = nav;
            NavbarPageContent = navPageContent;
            Pages = pages;
            HomePage = homePage;
            SettingsPage = settingsPage;

            NavigationView.Loaded += LoadNavbar;
            NavigationView.SelectionChanged += PageSelected;
            NavigationView.BackRequested += NavigatingBack;
        }

        private void LoadNavbar(object sender, RoutedEventArgs e)
        {
            NavbarPageContent.Navigated += PageNavigated;
            if (HomePage != null) NavigationView.SelectedItem = HomePage;
        }

        private void Navigate(string tag, NavigationTransitionInfo transitionInfo)
        {
            Type NavbarPage = null;
            if (SettingsPage != null && tag == "settings") NavbarPage = SettingsPage;
            else
            {
                var item = Pages.FirstOrDefault(i => i.Tag.Equals(tag));
                NavbarPage = item.Page;
            }
            var currentPage = NavbarPageContent.CurrentSourcePageType;
            if (!(NavbarPage is null) && !Type.Equals(currentPage, NavbarPage))
                NavbarPageContent.Navigate(NavbarPage, null, transitionInfo);
        }

        private void PageNavigated(object sender, NavigationEventArgs e)
        {
            NavigationView.IsBackEnabled = NavbarPageContent.CanGoBack;

            if (SettingsPage != null && NavbarPageContent.SourcePageType == SettingsPage)
                NavigationView.SelectedItem = NavigationView.SettingsItem;
            if (NavbarPageContent.SourcePageType != null)
            {
                var item = Pages.FirstOrDefault(i => i.Page == e.SourcePageType);

                NavigationView.SelectedItem = NavigationView.MenuItems
                                              .OfType<muxc.NavigationViewItem>()
                                              .First(n => n.Tag.Equals(item.Tag));
            }
        }

        private void PageSelected(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs e)
        {
            if (SettingsPage != null && e.IsSettingsSelected)
                Navigate("settings", e.RecommendedNavigationTransitionInfo);
            if (e.SelectedItemContainer != null && e.SelectedItemContainer.Tag != null)
            {
                var tag = e.SelectedItemContainer.Tag.ToString();
                Navigate(tag, e.RecommendedNavigationTransitionInfo);
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

            if (NavigationView.IsPaneOpen && NavigationView.DisplayMode != muxc.NavigationViewDisplayMode.Expanded)
                return;

            NavbarPageContent.GoBack();
        }
    }
}
