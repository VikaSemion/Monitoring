using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLayerApp.WEB.Models
{
    public class Facade
    {
        protected Subsystem1 _subsystem1;

        protected Subsystem2 _subsystem2;

        public Facade(Subsystem1 subsystem1, Subsystem2 subsystem2)
        {
            this._subsystem1 = subsystem1;
            this._subsystem2 = subsystem2;
        }

        
        public int Operation(int a, int b, int c)
        {
            int result = 0;
            result += this._subsystem1.operation1(a, b, c);
            result += this._subsystem2.operation1(a, b, c);
            result += this._subsystem1.operationN(a, b, c);
            result += this._subsystem2.operationZ(a, b, c);
            return result;
        }
    }


    public class Subsystem1
    {
        public int operation1(int a, int b, int c)
        {
            return a + b + c; ;
        }

        public int operationN(int a, int b, int c)
        {
            return (a + b + c) * 2;
        }
    }
    
    public class Subsystem2
    {
        public int operation1(int a, int b, int c)
        {
            return (a + b + c) / 3;
        }

        public int operationZ(int a, int b, int c)
        {
            return (a + b + c) / 100;
        }
    }
}