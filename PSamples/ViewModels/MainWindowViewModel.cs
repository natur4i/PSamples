using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using PSamples.Views;

namespace PSamples.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        private IRegionManager _regionManager;
        private IDialogService _dialogService;

        private string _title = "PSamples";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            SystemDateUpdateButton = new DelegateCommand(SystemDateUpdateButtonExecute);
            ShowViewAButton = new DelegateCommand(ShowViewAButtonExecute);
            ShowViewPButton = new DelegateCommand(ShowViewPButtonExecute);
            ShowViewBButton = new DelegateCommand(ShowViewBButtonExecute);
        }

        //SystemDateLabel
        private string _systemDateLabel = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        public string SystemDateLabel
        {
            get { return _systemDateLabel; }
            set { SetProperty(ref _systemDateLabel, value); }
        }

        //SystemDateUpdateButton
        public DelegateCommand SystemDateUpdateButton { get; }

        private void SystemDateUpdateButtonExecute()
        {
            SystemDateLabel = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        //ShowViewAButton
        public DelegateCommand ShowViewAButton { get; }

        public DelegateCommand ShowViewPButton { get; }

        public DelegateCommand ShowViewBButton {  get; }

        private void ShowViewAButtonExecute() {
            _regionManager.RequestNavigate("ContentRegion", nameof(ViewA));
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowViewPButtonExecute()
        {
            var p = new NavigationParameters();
            p.Add(nameof(ViewAViewModel.MyLabel), SystemDateLabel);

            _regionManager.RequestNavigate("ContentRegion", nameof(ViewA), p);
        }

        /// <summary>
        /// ViewB Dialogを開く
        /// </summary>
        private void ShowViewBButtonExecute()
        {
            var p = new DialogParameters();
            p.Add(nameof(ViewBViewModel.ViewBTextBox), SystemDateLabel);
            _dialogService.ShowDialog(nameof(ViewB), p, ViewBClose);
        }

        /// <summary>
        /// ViewBダイアログを閉じる
        /// メソッドはShowDaialogの引数でCALLBACKを指定する
        /// </summary>
        /// <param name="dialogResult"></param>
        private void ViewBClose(IDialogResult dialogResult)
        {
            if(dialogResult.Result == ButtonResult.OK){
                SystemDateLabel = dialogResult.Parameters.GetValue<string>(nameof(ViewBViewModel.ViewBTextBox));
            }
        }
    }
}
