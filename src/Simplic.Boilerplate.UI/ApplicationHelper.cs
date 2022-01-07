using CommonServiceLocator;
using Simplic.Boilerplate.Client;
using Simplic.Configuration;
using Simplic.WebApi.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Boilerplate.UI
{
    public static class ApplicationHelper
    {
        public static void OpenSignalRWindow(Guid appId)
        {
            var guid = Guid.Parse("2bcd0274-fe11-4819-847d-f7262b4ee2f7");


            var window = new ContactWindow();
            window.AddPagingObject(guid);
            window.WindowMode = Framework.UI.WindowMode.Edit;
            window.Show();
        }
    }
}
