using UnityEngine;
using System.Collections;

public class Wordings : MonoBehaviour {
    public float lifeTime;
    public SpriteRenderer parentSpriteRenderer;
    public PlayerCharacterLogicScript owner;

    SpriteRenderer spriteRenderer;

    float timer = 0f;
    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(parentSpriteRenderer != null)
        {
            Vector2 parent_sprite_size = parentSpriteRenderer.sprite.rect.size;
            Vector2 parent_local_sprite_size = parent_sprite_size / parentSpriteRenderer.sprite.pixelsPerUnit;

            Vector2 sprite_size = parentSpriteRenderer.sprite.rect.size;
            Vector2 local_sprite_size = sprite_size / parentSpriteRenderer.sprite.pixelsPerUnit;
            if (owner.GetPlayerID() == PLAYER.PLAYER_ONE)
                gameObject.transform.position = gameObject.transform.parent.TransformPoint(0, local_sprite_size.y/2 + parent_local_sprite_size.y/2, gameObject.transform.position.z);
            else if (owner.GetPlayerID() == PLAYER.PLAYER_TWO)
                gameObject.transform.position = gameObject.transform.parent.TransformPoint(0, -local_sprite_size.y - parent_local_sprite_size.y / 2, gameObject.transform.position.z);

        }
    }

    public virtual void Update()
    {
        timer += Time.deltaTime;
        if(timer >= lifeTime)
        {
            timer = 0f;
            gameObject.SetActive(false);
        }
    }
    //float characterHeight;
    //public SpriteRenderer spriteRendererOfWording;
    //public Sprite[] wordings;
    //// Use this for initialization
    //void Start()
    //{
    //    if (spriteRendererOfWording == null)
    //    {
    //        spriteRendererOfWording = GetComponent<SpriteRenderer>();
    //        spriteRendererOfWording.enabled = false;
    //    }
    //    SpriteRenderer parentSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
    //    Vector2 sprite_size = parentSpriteRenderer.sprite.rect.size;

    //    Vector2 local_sprite_size = sprite_size / parentSpriteRenderer.sprite.pixelsPerUnit;
    //    characterHeight = local_sprite_size.y;
    //}

    //// Update is called once per frame
    ////void Update () {

    ////}

    //public void changeWording(WORDING_TYPES type,PLAYER playerNumber)
    //{
    //    if(spriteRendererOfWording == null)
    //    {
    //        spriteRendererOfWording = GetComponent<SpriteRenderer>();
    //        spriteRendererOfWording.enabled = false;
    //    }
    //    spriteRendererOfWording.sprite = wordings[(int)type];
    //    //Vector2 sprite_size = spriteRendererOfWording.sprite.rect.size;

    //    //Vector2 local_sprite_size = sprite_size / spriteRendererOfWording.sprite.pixelsPerUnit;
    //    //float heightOffset = characterHeight / 2 + (local_sprite_size.y / 2);
    //    //gameObject.transform.position -= gameObject.transform.position;
    //    //if (playerNumber == PLAYER.PLAYER_ONE)
    //    //    gameObject.transform.localPosition += new Vector3(0, heightOffset);
    //    //else if (playerNumber == PLAYER.PLAYER_TWO)
    //    //    gameObject.transform.position += new Vector3(0, -heightOffset);
    //}

    //public void showWording()
    //{
    //    spriteRendererOfWording.enabled = true;
    //}

    //public void hideWording()
    //{
    //    spriteRendererOfWording.enabled = false;
    //}
}
