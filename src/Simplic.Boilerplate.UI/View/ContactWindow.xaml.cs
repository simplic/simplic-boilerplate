using Simplic.Framework.UI;
using System;

namespace Simplic.Boilerplate.UI
{
    /// <summary>
    /// Interaction logic for ContactWindow.xaml
    /// </summary>
    public partial class ContactWindow : DefaultRibbonWindow
    {
        public ContactWindow()
        {
            InitializeComponent();

            UseLocking = false;
        }

        public override void OnOpenPage(WindowOpenPageEventArg e)
        {
            var id = (Guid)e.CurrentObject;

            var vm = new ContactViewModel();

            Dispatcher.Invoke(async () =>
            {
                await vm.Initialize();
                await vm.Edit(id);
                DataContext = vm;
            });

            base.OnOpenPage(e);
        }
    }
}
