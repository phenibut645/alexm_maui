using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alexm_maui
{
    public static class AppContext
    {
        public static List<ContentPage> Pages { get; set; } = new List<ContentPage>() { new ValgusfoorPage(), new rgb_page() };
    }
}
