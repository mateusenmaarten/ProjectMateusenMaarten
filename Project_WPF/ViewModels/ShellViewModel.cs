using Caliburn.Micro;
using Project_WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_WPF.ViewModels
{
    public class ShellViewModel : Screen
    {
        WindowManager wm = new WindowManager();

        public void btnOpenJsonView()
        {
            wm.ShowWindow(new JsonViewModel(), null, null);
        }
        public void btnOpenBoardgameView()
        {
            wm.ShowWindow(new BoardgameViewModel(), null, null);
        }
        public void btnOpenSearchView()
        {
            wm.ShowWindow(new SearchViewModel(), null, null);
        }
        public void btnOpenMemberView()
        {
            wm.ShowWindow(new MemberViewModel(), null, null);
        }
        public void btnOpenSessionView()
        {
            wm.ShowWindow(new SessionViewModel(), null, null);
        }
    }
}
