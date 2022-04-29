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
    public partial class UserCard : UserControl
    {
        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }
        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(UserCard), new PropertyMetadata(0));

        public string ChatPicture
        {
            get { return (string)GetValue(ProfilePictureProperty); }
            set { SetValue(ProfilePictureProperty, value); }
        }
        public static readonly DependencyProperty ProfilePictureProperty =
            DependencyProperty.Register("Picture", typeof(string), typeof(UserCard), new PropertyMetadata(0));

        public string PreviewText
        {
            get { return (string)GetValue(PhoneNumberProperty); }
            set { SetValue(PhoneNumberProperty, value); }
        }
        public static readonly DependencyProperty PhoneNumberProperty =
            DependencyProperty.Register("PhoneNumber", typeof(string), typeof(UserCard), new PropertyMetadata(0));

        public UserCard()
        {
            this.InitializeComponent();
        }
    }
}
