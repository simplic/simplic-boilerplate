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

        public ContactViewModel VM { get; set; }

        protected override void OnClosed(EventArgs e)
        {
            VM.OnClose();
            base.OnClosed(e);
        }

        public override void OnOpenPage(WindowOpenPageEventArg e)
        {
            var id = (Guid)e.CurrentObject;
            VM = new ContactViewModel();

            Dispatcher.Invoke(async () =>
            {
                await VM.Initialize();
                await VM.Edit(id);
                DataContext = VM;
            });

            base.OnOpenPage(e);
        }
    }
}
