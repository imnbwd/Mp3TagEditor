using GalaSoft.MvvmLight;

namespace PraiseHim.Tools.Mp3TagEditor.Models
{
    public class TagInfo : ObservableObject
    {
        private string _album;

        private string _author;

        private TitleOptions _titleOption;

        /// <summary>
        /// Get or set Album value
        /// </summary>
        public string Album
        {
            get { return _album; }
            set { Set(ref _album, value); }
        }

        /// <summary>
        /// Get or set Author value
        /// </summary>
        public string Author
        {
            get { return _author; }
            set { Set(ref _author, value); }
        }

        /// <summary>
        /// Get or set TitleOption value
        /// </summary>
        public TitleOptions TitleOption
        {
            get { return _titleOption; }
            set { Set(ref _titleOption, value); }
        }
    }
}