using UnityEngine;
using System.Collections;

public class StunnedWording : MonoBehaviour {

    float characterHeight;
    SpriteRenderer spriteRendererOfWording;
	// Use this for initialization
	void Start () {
        spriteRendererOfWording = GetComponent<SpriteRenderer>();
        spriteRendererOfWording.enabled = false;
        SpriteRenderer parentSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        Vector2 sprite_size = parentSpriteRenderer.sprite.rect.size;

        Vector2 local_sprite_size = sprite_size / parentSpriteRenderer.sprite.pixelsPerUnit;
        characterHeight = local_sprite_size.y;
    }
	
	// Update is called once per frame
	//void Update () {
	
	//}

    public void setPositionOfWording()
    {
        Vector2 sprite_size = spriteRendererOfWording.sprite.rect.size;

        Vector2 local_sprite_size = sprite_size / spriteRendererOfWording.sprite.pixelsPerUnit;
        float heightOffset = characterHeight / 2 + (local_sprite_size.y / 2);
        gameObject.transform.position = Vector2.zero;
        gameObject.transform.position = new Vector2(0, heightOffset);
    }

    public void showWording()
    {
        spriteRendererOfWording.enabled = true;
    }

    public void hideWording()
    {
        spriteRendererOfWording.enabled = false;
    }
}
