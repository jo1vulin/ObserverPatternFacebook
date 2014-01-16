using FacebookGroup.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookGroup.Observer
{
    class Subscriber : IObserver<WallPost>
    {
        private string name;
        private List<WallPost> wallPosts = new List<WallPost>();
        private IDisposable cancellation;
        private string fmt = "{0,-20} {1,5}  {2, 3}";

        public Subscriber(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("The observer must be assigned a name");
            this.name = name;
        }

        public virtual void Subscribe(Group provider)
        {
            cancellation = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            cancellation.Dispose();
            wallPosts.Clear();
        }

        public void OnCompleted()
        {
            wallPosts.Clear();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(WallPost post)
        {
            bool updated = false;

            //Post removed; remove from the message sent to subscriber
            if (post.isDisplayed == false)
            {
                
                var postsToRemove = new List<WallPost>();
                //string postId = String.Format("{0,5}", post.postId);
                foreach (var posts in wallPosts)
                {
                    
                    if (posts.postId == post.postId)
                    {

                        postsToRemove.Add(posts);
                        updated = true;
                    }
                }

                foreach (var postToRemove in postsToRemove)
                    wallPosts.Remove(postToRemove);

                postsToRemove.Clear();

            }
            else
            {
                // Add post if it does not exist in the collection. 
                if (!wallPosts.Contains(post))
                {
                    wallPosts.Add(post);
                    updated = true;
                }
            }

            if (updated)
            {
                
                Console.WriteLine("Post information: {0}", this.name);
                foreach (var postInfo in wallPosts)
                    Console.WriteLine(postInfo.postId + " " + postInfo.postText);

                Console.WriteLine();
            }
        }
    }
}
  

    

