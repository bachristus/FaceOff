using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FaceOff.GUI
{
    public class PostsListView : MonoBehaviour
    {
        [SerializeField] private IndividualPostView PostPrefab;
        [SerializeField] private ScrollRect ScrollView;
        //private RectTransform rectTransform;

        private void Awake()
        {
           // rectTransform = GetComponent<RectTransform>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowPosts(IEnumerable<Post> posts)
        {           
            foreach (var postView in this.gameObject.GetComponentsInChildren<IndividualPostView>())
            {
                GameObject.Destroy(postView.gameObject);
            }            

            if (posts == null) return;

           // float offset = 0;

            foreach(var post in posts)
            {
                var postView=GameObject.Instantiate<IndividualPostView>(PostPrefab);

                //var postRectTransform = postView.GetComponent<RectTransform>();
                //postRectTransform.parent = rectTransform;

                postView.transform.SetParent(transform, false);

                //postRectTransform.localPosition = new Vector3(0, offset, 0);

                postView.Post = post;

                //offset += postRectTransform.rect.height;
            }
            ScrollView.verticalNormalizedPosition = 1;
        }

    }
}