using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectAutoBuilder.Test.Helper
{
    public class RecursiveItemA
    {
        private readonly RecursiveItemB innerItem;

        public RecursiveItemB RecursiveItemB
        {
            get
            {
                return innerItem;
            }
        }

        public RecursiveItemA(RecursiveItemB innerItem)
        {
            this.innerItem = innerItem;
        }
    }
}
