using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectAutoBuilder.Test.Helper
{
    public class RecursiveItemB
    {
        private readonly RecursiveItemA innerItem;

        public RecursiveItemA RecursiveItemA
        {
            get
            {
                return innerItem;
            }
        }

        public RecursiveItemB(RecursiveItemA innerItem)
        {
            this.innerItem = innerItem;
        }
    }
}
