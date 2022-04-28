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
using Windows.ApplicationModel.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
// Pazzo assurdo

namespace OwlGram_Desktop
{
    public sealed partial class MainPage : Page
    {
        public CoreApplicationView View { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();

            this.View = CoreApplication.GetCurrentView();
            ExpandView();
        }

        internal void ExpandView(StackPanel titleBar = null)
        {
            this.View.TitleBar.ExtendViewIntoTitleBar = true;
            if (titleBar != null)
                Window.Current.SetTitleBar(titleBar);
        }
    }
}
