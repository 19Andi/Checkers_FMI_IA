using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    public class MultipleJump : BaseNotification
    {
        private bool buttonChecked;

        public MultipleJump(bool buttonChecked = false)
        {
            this.buttonChecked = buttonChecked;
        }

        public bool ButtonChecked 
        { 
            get 
            {
                return buttonChecked;
            }
            set
            {
                buttonChecked = value;
                NotifyPropertyChanged();
            }
        }
    }
}
