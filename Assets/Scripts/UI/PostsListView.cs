using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FaceOff.DataObjects;

namespace FaceOff.GUI
{
    public class PostsListView : View
    {
        [SerializeField] private IndividualPostView postPrefab;
        [SerializeField] private RectTransform postsContainer;
        [SerializeField] private ScrollRect scrollView;
            
        public void ShowPosts(IEnumerable<Post> posts)
        {
            ClearContent();

            if (posts == null) return;

            foreach (var post in posts)
            {
                var postView = GameObject.Instantiate<IndividualPostView>(postPrefab);

                postView.transform.SetParent(postsContainer, false);

                postView.Post = post;
            }

            scrollView.verticalNormalizedPosition = 1;
        }

        public override void ClearContent()
        {
            foreach (var postView in this.gameObject.GetComponentsInChildren<IndividualPostView>())
            {
                GameObject.Destroy(postView.gameObject);
            }
        }
    }
}