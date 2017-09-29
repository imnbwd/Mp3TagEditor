using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using PraiseHim.Tools.Mp3TagEditor.Services;
using System;

namespace PraiseHim.Tools.Mp3TagEditor.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel MainViewModel { get => SimpleIoc.Default.GetInstance<MainViewModel>(Guid.NewGuid().ToString()); }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}