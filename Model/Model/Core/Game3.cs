using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Game
    {
        private (int x, int y)[] _memory = new (int x,int y)[0];
        private void AddMemory(int x,int y)
        {
            if (_memory.Length < 12)
            {
                Array.Resize(ref _memory, _memory.Length+1);
                _memory[_memory.Length - 1] = (x,y);
                return;
            }
            Array.Copy(_memory, 1, _memory, 0, _memory.Length - 1);
            _memory[_memory.Length - 1] = (x, y);
        }
        private bool IsStalemate0()
        {
            if (_memory.Length < 12) return false;
            for (int i =0; i < 4;i++)
            {
                for (int j = i+4; j<12;j += 4)
                {
                    if (_memory[i] != _memory[j]) return false;
                }
            }
            return true;
        }
    }
}
