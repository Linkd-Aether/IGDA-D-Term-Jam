using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI
{
    public class EnemyPath : MonoBehaviour
    {
        // Variables
        private int currentNode = 0;
        
        // Components & References
        private Transform[] nodes;


        private void Awake() {
            nodes = new Transform[transform.childCount];
            
            int childNum = 0;
            foreach (Transform child in transform) {
                nodes[childNum] = child;
                childNum++;
            }
        }

        private void OnDrawGizmos() {
            Awake();
            for (int i = 0; i < nodes.Length; i++) {
                int next = (i + 1) % nodes.Length;
                Gizmos.color = Color.red;
                Gizmos.DrawLine(nodes[next].position, nodes[i].position);
                Gizmos.color = Color.gray;
                Gizmos.DrawSphere(nodes[i].position, .2f);
            }
        }

        public Transform NextNode() {
            if (nodes.Length == 0) return this.transform;
            currentNode = (currentNode + 1) % nodes.Length;
            return nodes[currentNode];
        }

        #region Unity Editor Functionality
            public void AddNode()
            {
                Awake();
                GameObject node = new GameObject();
                node.name = $"Node {nodes.Length}";
                node.transform.position = transform.position;
                node.transform.parent = this.transform;
            }

            public void RemoveNode()
            {
                if (transform.childCount > 0) {
                    DestroyImmediate(transform.GetChild(transform.childCount - 1).gameObject);
                }
            }
        #endregion
    }
}
