using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Core.UI.Controls.Base
{
    public interface IGsValidatable
    {
        bool IsValid { get; }
        string ErrorMessage { get; }
        void Validate();
    }
}
