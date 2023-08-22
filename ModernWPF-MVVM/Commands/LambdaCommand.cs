using ModernWPF_MVVM.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernWPF_MVVM.Commands
{
    internal class LambdaCommand : Command
    {
        public readonly Action<object> _Execute;
        public readonly Func<object, bool> _CanExeCute = null;


        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExeCute = null)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExeCute = CanExeCute;
        }

        public override bool CanExecute(object parameter) => _CanExeCute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _Execute(parameter);
    }
}
