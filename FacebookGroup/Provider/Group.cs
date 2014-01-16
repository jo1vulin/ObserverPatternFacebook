using FacebookGroup.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookGroup
{
    public class Group : IObservable<WallPost>
    {
        private List<IObserver<WallPost>> observers;
        private List<WallPost> wallPosts;

        public Group()
        {
            observers = new List<IObserver<WallPost>>();
            wallPosts = new List<WallPost>();
        }

        //Has to provide implementation of the subscribe method
        public IDisposable Subscribe(IObserver<WallPost> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                foreach (var item in wallPosts)
                    observer.OnNext(item);
            }
            return new Unsubscribe<WallPost>(observers, observer);
            
        }

        public void handlePost(int id)
        {
            handlePost(id, String.Empty, false);
        }

        public void handlePost(int id, string text, bool isDisplayed)
        {
            var newPost = new WallPost(id, text, isDisplayed);

            if (!wallPosts.Any(p => p.postId == newPost.postId) && isDisplayed != false)
            {
                wallPosts.Add(newPost);
                foreach (var observer in observers)
                    observer.OnNext(newPost);
            }
            else if (isDisplayed == false)
            {
                var postsToRemove = new List<WallPost>();
                foreach (var existingPost in wallPosts)
                {
                    if (newPost.postId == existingPost.postId)
                    {
                        postsToRemove.Add(existingPost);
                        foreach (var observer in observers)
                            observer.OnNext(newPost);
                    }
                }
                foreach (var postToRemove in postsToRemove)
                {
                    wallPosts.Remove(postToRemove);
                }

                postsToRemove.Clear();
            }
        }

        public void LastMessagePost()
        {
            foreach (var observer in observers)
                observer.OnCompleted();

            observers.Clear();
        }

      
    }
}
