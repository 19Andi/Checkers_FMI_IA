using Checkers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.ViewModels
{
    public class MultipleJumpVM : BaseNotification
    {
        private MultipleJump multipleJump;

        public MultipleJumpVM(MultipleJump multipleJump)
        {
            this.multipleJump = multipleJump;
        }

        public MultipleJump MultipleJump 
        { 
            get 
            {  
                return multipleJump; 
            }
            set 
            {
                multipleJump = value;
                NotifyPropertyChanged();
            }
        }
    }
}
