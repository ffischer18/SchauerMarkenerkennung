using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchauerMarkenerkennung.MVVM.ViewModel
{
    public class HomeViewModel
    {
        List<string> testList = new List<string>();
        public HomeViewModel()
        {
            testList.Add("Hallo");
            Test = testList;
        }

        private List<string> Test;

        public List<string> test
        {
            get { return Test; }
            set { Test = value; }
        }
    }
}
