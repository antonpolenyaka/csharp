using Xamarin.Forms;
using LibVLCSharp.Shared;

namespace TestVLC
{
    public partial class MainPage : ContentPage
    {
        const string VIDEO_URL = "rtsp://184.72.239.149/vod/mp4:BigBuckBunny_175k.mov";
        const string VIDEO_URL1 = "rtsp://sitel:sitel1@192.168.8.134:88/videoMain";
        const string VIDEO_URL2 = "rtsp://sitel:sitel2@192.168.8.144:88/videoMain";
        readonly LibVLC _libvlc;

        public MainPage()
        {
            InitializeComponent();

            // this will load the native libvlc library (if needed, depending on the platform). 
            Core.Initialize();

            // instanciate the main libvlc object
            _libvlc = new LibVLC();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // create mediaplayer objects,
            // attach them to their respective VideoViews
            // create media objects and start playback

            VideoView0.MediaPlayer = new MediaPlayer(_libvlc);
            VideoView0.MediaPlayer.Play(new Media(_libvlc, VIDEO_URL1, FromType.FromLocation));

            VideoView1.MediaPlayer = new MediaPlayer(_libvlc);
            VideoView1.MediaPlayer.Play(new Media(_libvlc, VIDEO_URL2, FromType.FromLocation));

            VideoView2.MediaPlayer = new MediaPlayer(_libvlc);
            VideoView2.MediaPlayer.Play(new Media(_libvlc, VIDEO_URL, FromType.FromLocation));

            VideoView3.MediaPlayer = new MediaPlayer(_libvlc);
            VideoView3.MediaPlayer.Play(new Media(_libvlc, VIDEO_URL, FromType.FromLocation));
        }
    }
}