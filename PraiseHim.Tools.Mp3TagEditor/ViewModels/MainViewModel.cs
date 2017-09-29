using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PraiseHim.Tools.Mp3TagEditor.Models;
using PraiseHim.Tools.Mp3TagEditor.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace PraiseHim.Tools.Mp3TagEditor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private TagInfo _currentTagInfo;
        private IDialogService _dialogService;
        private TitleOptions _selectedTitleOption;

        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            CurrentTagInfo = new TagInfo();
        }

        /// <summary>
        /// Get or set CurrentTagInfo value
        /// </summary>
        public TagInfo CurrentTagInfo
        {
            get { return _currentTagInfo; }
            set { Set(ref _currentTagInfo, value); }
        }

        public ICommand EditTagInfoCommand { get => new RelayCommand(EditTagInfo); }

        private List<string> _allFiles;

        /// <summary>
        /// Get or set AllFiles value
        /// </summary>
        public List<string> AllFiles
        {
            get { return _allFiles; }
            set { Set(ref _allFiles, value); }
        }


        private void EditTagInfo()
        {
            if (AllFiles == null || AllFiles.Count == 0)
            {
                _dialogService.Alert("no mp3 found");
                return;
            }

            if ((int)CurrentTagInfo.TitleOption == 0
                || string.IsNullOrWhiteSpace(CurrentTagInfo.Album)
                || string.IsNullOrWhiteSpace(CurrentTagInfo.Author))
            {
                _dialogService.Alert("please fill in all the info");
                return;
            }

            var folder = Path.GetDirectoryName(AllFiles.First());
            MediaTags mTags = new MediaTags(folder);

            switch (CurrentTagInfo.TitleOption)
            {
                case TitleOptions.FileNameWithoutExtensions:
                    foreach (var file in AllFiles)
                    {
                        mTags.Title = Path.GetFileNameWithoutExtension(file);
                        mTags.AlbumTitle = CurrentTagInfo.Album;
                        mTags.Author = CurrentTagInfo.Author;
                        mTags.Commit(file);
                    }
                    break;
            }

            _dialogService.Info("done");
        }
    }
}