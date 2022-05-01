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
        private Navbar NavMenu { get; set; }
        public List<(string Tag, Type Page)> NavbarPages { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();

            View = ApplicationView.GetForCurrentView();
            CoreView = CoreApplication.GetCurrentView();
            ExpandView(TitleBar);

            NavbarPages = new List<(string Tag, Type Page)>
            {
                ("allChats", typeof(AllChats)),
                ("calls", typeof(AllCalls)),
                ("contacts", typeof(Contacts)),
                ("archived", typeof(ArchivedChats)),
            };

            NavMenu = new Navbar(
                Menu, MainContent, NavbarPages,
                HomePageNav, typeof(SettingsPage)
            );
        }

        private void ExpandView(Grid titleBar = null)
        {
            CoreView.TitleBar.ExtendViewIntoTitleBar = true;
            if (titleBar != null)
                Window.Current.SetTitleBar(titleBar);
            View.TitleBar.ButtonBackgroundColor = Colors.Transparent;
        }
    }
}
