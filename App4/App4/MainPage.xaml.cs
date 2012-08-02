using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using FlickrNet;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string key;
        string username;
        string key1;
        Flickr flickr;
        int present= 0;
        int total = 0;
        PhotoCollection photos;
        FoundUser obj = new FoundUser();
        public MainPage()
        {
            key1 = "dc519ef5b05d0129e05cf79ce2fad3f3";
            Windows.Storage.ApplicationDataContainer localSettings =
               Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("userName"))
            {
                username = localSettings.Values["userName"].ToString();
            }
           
            flickr = new Flickr(key1);
            this.InitializeComponent();
            try
            {
                obj = flickr.PeopleFindByUserName(username);
                userid.Text =obj.UserId;
                 photos=flickr.PeopleGetPublicPhotos(obj.UserId);
                userid.Text=" "+photos.PerPage;
                userid.Text = flickr.UrlsGetUserPhotos(obj.UserId);
                total = photos.Count;
                if (total > 1)
                {
                    userid.Text = photos.ElementAt(0).SmallUrl;
                    image1.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(image1.BaseUri,
                       photos.ElementAt(0).SmallUrl));
                    present = 0;
                }
                else userid.Text = "You Dont Have any Public Photos";
                //userid.Text = k;
                
                

                
            }
            catch (Exception)
            {
                userid.Text = "Exception catched";

            }
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if (total > 1)
            {
                present = (++present) % total ;
                userid.Text = photos.ElementAt(present).SmallUrl;
                image1.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(image1.BaseUri,
                   photos.ElementAt(present).SmallUrl));
                
            }
            
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            if (total > 1)
            {
                if (--present < 0)
                    present = total - 1;
                userid.Text = photos.ElementAt(present).SmallUrl;
                image1.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(image1.BaseUri,
                   photos.ElementAt(present).SmallUrl));
                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var rootFrame = new Frame();

            if (!rootFrame.Navigate(typeof(login)))
            {
                throw new Exception("Failed to create initial page");
            }

            // Place the frame in the current Window and ensure that it is active
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }
    }
}
