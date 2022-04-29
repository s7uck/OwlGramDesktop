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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace OwlDesktop.Controls
{
    public partial class ChatCard : UserControl
    {
        public string ChatTitle
        {
            get { return (string)GetValue(ChatTitleProperty); }
            set { SetValue(ChatTitleProperty, value); }
        }
        public static readonly DependencyProperty ChatTitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ChatCard), new PropertyMetadata(0));

        public string ChatPicture
        {
            get { return (string)GetValue(ChatPictureProperty); }
            set { SetValue(ChatPictureProperty, value); }
        }
        public static readonly DependencyProperty ChatPictureProperty =
            DependencyProperty.Register("Picture", typeof(string), typeof(ChatCard), new PropertyMetadata(0));

        public string PreviewText
        {
            get { return (string)GetValue(PreviewTextProperty); }
            set { SetValue(PreviewTextProperty, value); }
        }
        public static readonly DependencyProperty PreviewTextProperty =
            DependencyProperty.Register("PreviewText", typeof(string), typeof(ChatCard), new PropertyMetadata(0));

        public ChatCard()
        {
            this.InitializeComponent();
        }
    }
}
