﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace mediaplayer
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class video : Page
    {
        public video()
        {
            this.InitializeComponent();
        }

        private MediaSource _mediaSource;
        private MediaPlayer _mediaPlayer ;

        private async void ButtonBase_OnClickAsync(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            openPicker.FileTypeFilter.Add(".mp3");
            openPicker.FileTypeFilter.Add(".mp4");

            StorageFile file = await openPicker.PickSingleFileAsync();
            
            if (file != null)
            {
                // Application now has read/write access to the picked file
                _mediaSource = MediaSource.CreateFromStorageFile(file);
                //简单访问
                if(_mediaPlayer == null)
                    _mediaPlayer = new MediaPlayer();
                
                _mediaPlayer.Source = _mediaSource;
                _mediaPlayer.Play();
                MediaPlayerElement.SetMediaPlayer(_mediaPlayer);
                path.Text = file.FileType;

                //高级访问
                /*var mediaPlaybackItem = new MediaPlaybackItem(_mediaSource);

                _mediaPlayer.Source = mediaPlaybackItem;*/
            }
            else
            {
                path.Text = "no file";
            }
        }
    }
}
