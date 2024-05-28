using Checkers.Commands;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Checkers.ViewModels
{
    public class ButtonControl
    {
        GameLogic gl;
        ICommand newGameCommand;
        ICommand saveCommand;
        ICommand loadCommand;
        ICommand aboutCommand;
        ICommand statisticsCommnand;

        public ButtonControl(GameLogic gl)
        {
            this.gl = gl;
        }
        public ICommand NewGameCommand
        {
            get
            {
                if (newGameCommand == null) 
                {
                    newGameCommand = new NonGenericCommand(gl.NewGameBoard);
                }
                return newGameCommand;
            }
        }
        public ICommand StatisticsCommand
        {
            get
            {
                if (statisticsCommnand == null)
                {
                    statisticsCommnand = new NonGenericCommand(gl.printScore);
                }
                return statisticsCommnand;
            }
        }
        public ICommand SaveGameCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new NonGenericCommand(gl.SaveGameBoard);
                }
                return saveCommand;
            }
        }
        public ICommand OpenGameCommand
        {
            get
            {
                if (loadCommand == null)
                {
                    loadCommand = new NonGenericCommand(gl.OpenGameBoard);
                }
                return loadCommand;
            }
        }
    }

}
