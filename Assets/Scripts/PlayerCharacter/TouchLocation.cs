using UnityEngine;
namespace Character {
    public class TouchLocation {
        public int fingerID;
        public Transform player;

        public TouchLocation(int t, Transform p) {
            fingerID = t;
            player = p;
        }
    }
}