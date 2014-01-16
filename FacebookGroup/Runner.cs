using FacebookGroup.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookGroup
{
    class Runner
    {

        public static void Main()
        {
            Group provider = new Group();
            Subscriber observer1 = new Subscriber("User: Jovan Vulin");
            Subscriber observer2 = new Subscriber("User: John Doe");

            provider.handlePost(100, "Welcome users!", true);
            observer1.Subscribe(provider);
            observer2.Subscribe(provider);
            provider.handlePost(101, "C# patterns", true);
            //Invalid post, shouldn't be displayed
            provider.handlePost(100, "Welcome users!", true);
            provider.handlePost(100, "Test users!", true);
            provider.handlePost(100);
            provider.handlePost(102, "Observer pattern", true);

            Console.ReadLine();
          
        }

    }
}
