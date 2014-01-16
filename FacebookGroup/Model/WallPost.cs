using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookGroup.Model
{
    public class WallPost
    {
        public int postId { get; private set; }
        public string postText { get; private set; }
        public bool isDisplayed { get; private set; }

        public WallPost(int id, string text, bool isShown)
        {
            this.postId = id;
            this.postText = text;
            this.isDisplayed = isShown;
        }

    }
}
