using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraiseHim.Tools.Mp3TagEditor.Services
{
    public interface IDialogService
    {
        void Alert(string message);
        void Info(string message);
    }
}
