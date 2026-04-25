using UnityEngine;

public class HolderTrigger : MonoBehaviour
{
    Holder holder;

    private void Start()
    {
        holder = GetComponentInParent<Holder>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        holder.near = true;
        // Set player's nearHolder to this holder
        collision.GetComponent<Player>().nearHolders.Add(holder);
        // Swap sprite
        holder.SetSprite();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        holder.near = false;
        // Set player's nearHolder to null
        Player player = collision.GetComponent<Player>();
        player.nearHolders.Remove(holder);

        // Swap sprite
        holder.SetSprite();
    }
}
