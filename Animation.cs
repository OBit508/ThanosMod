using FungleAPI.Utilities.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FungleAPI.Attributes;

namespace ThanosMod
{
    [RegisterTypeInIl2Cpp]
    internal class Animation : MonoBehaviour
    {
        public static void Show(List<Sprite> file, Transform transform)
        {
            Animation animation = new GameObject("Animation").AddComponent<Animation>();
            animation.gameObject.layer = 5;
            animation.transform.SetParent(transform);
            animation.transform.localPosition = Vector3.zero;
            animation.Rend = animation.gameObject.AddComponent<SpriteRenderer>();
            animation.Anim = file;
        }
        public static void Show(List<Sprite> file, Vector2 pos)
        {
            Animation animation = new GameObject("Animation").AddComponent<Animation>();
            animation.gameObject.layer = 5;
            animation.transform.position = pos;
            animation.Rend = animation.gameObject.AddComponent<SpriteRenderer>();
            animation.Anim = file;
        }
        public SpriteRenderer Rend;
        public int sprite;
        public List<Sprite> Anim;
        public void Update()
        {
            sprite++;
            if (sprite == Anim.Count)
            {
                GameObject.Destroy(gameObject);
                return;
            }
            Rend.sprite = Anim[sprite];
            
        }
    }
}
