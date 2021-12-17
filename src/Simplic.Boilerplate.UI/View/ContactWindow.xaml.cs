using Simplic.Framework.UI;
using Simplic.Studio.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simplic.Boilerplate.UI
{
    //public abstract class BaseContactWindow : ApplicationWindow<Guid, Contact, ContactViewModel, IContactService>, IContactWindow
    //{
    //    public BaseContactWindow(IContactService contactService)
    //        : base(contactService)
    //    {

    //    }
    //}

    /// <summary>
    /// Interaction logic for ContactWindow.xaml
    /// </summary>
    public partial class ContactWindow : UserControl
    {
        public ContactWindow(/*IContactService contactService*/) /*: base(contactService)*/
        {
            InitializeComponent();
        }
    }
}
