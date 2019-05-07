using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLayerApp.WEB.Models
{
    
    abstract class AbstractClass
    {
        
        public int FirstStep(int a, int b, int c)
        {
            return a + b + c;
        }
        
        public abstract double SecondStep(int a, int b, int c);

        public int ThirdStep(int a, int b, int c)
        {
            return (a + b + c) * 2;
        }

        public void Calculating(int a, int b, int c)
        {
            this.FirstStep(a, b, c);
            this.SecondStep(a, b, c);
            this.ThirdStep(a, b, c);
        }

    }

    class Formula1 : AbstractClass
    {
        public override double SecondStep(int a, int b, int c)
        {
            return (a + b + c) / 3;
        }
    }

    class Formula2 : AbstractClass
    {
        public override double SecondStep(int a, int b, int c)
        {
            return (a + b + c) / 100;
        }
    }
}