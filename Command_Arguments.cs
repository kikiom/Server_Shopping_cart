using Server_Shopping_cart.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Shopping_cart.Services
{
    public class Command_Arguments
    {
        private List<object> _arguments;
        public int Count { get { return _arguments.Count; } }

        public Command_Arguments(List<object> argList)
        {
            _arguments = argList;
        }

        public object GetAt(int index)
        {
            return _arguments[index];
        }

        public string AsString(uint index)
        {
            return (string)_arguments[(int)index];
        }

        public UserRole AsRole(uint index)
        {
            return (UserRole)_arguments[(int)index];
        }

        public int AsNumber(uint index)
        {
            if (_arguments[(int)index].ToString().Contains(".") || _arguments[(int)index].ToString().Contains(","))
            {
                throw new Exception("invalid integer");   
            }
            return Convert.ToInt32(_arguments[(int)index]);
        }

        public float AsDecimal(uint index)
        {
            return (float)_arguments[(int)index];
        }

    }
}
