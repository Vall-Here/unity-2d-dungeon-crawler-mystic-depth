using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gamecontroller : MonoBehaviour
{

    [Header("========[Drop Area Item Settings]========")]
    public Tilemap groundTilemap;
    public Tilemap obstacleTilemap;
    [SerializeField] private Collider2D dropAreaCollider;


    [Header("========[Player Settings]========")]
    [SerializeField] private GameObject player;


    public void swap(InventoryItem item1, InventoryItem inSlot)
        {
            string item1inv = item1.GetInventory();
            string inSLotInv = inSlot.GetInventory();

            int positem1 = item1.GetPosition();
            int posinslotinv = inSlot.GetPosition();
            InventoryController.instance.RemoveItemPos(inSLotInv, inSlot.GetPosition(), inSlot.GetAmount());

            InventoryController.instance.AddItemPos(item1inv, inSlot.GetItemType(), positem1, inSlot.GetAmount());

            InventoryController.instance.AddItemPos(inSLotInv, item1.GetItemType(), posinslotinv, item1.GetAmount());


        }


    public void DropItem(Vector3 pos, InventoryItem item)
        {
            if (dropAreaCollider != null)
            {
                if (IsPositionInsideDropArea(pos))
                {
                    Vector3Int cellPosition = groundTilemap.WorldToCell(pos);
                    bool isGround = groundTilemap.HasTile(cellPosition);
                    bool isBlocked = obstacleTilemap.HasTile(cellPosition);
                    if (isGround && !isBlocked)
                    {
                        Vector3 dropPosition = groundTilemap.GetCellCenterWorld(cellPosition);
                        for (int i = 0; i < item.GetAmount(); i++)
                        {
                            Instantiate(item.GetRelatedGameObject(), dropPosition, Quaternion.identity);
                        }
                    }
                    else
                    {
                        DropAtPlayerPosition(item);
                    }
                }
                else
                {
                    DropAtPlayerPosition(item);
                }
            }
            else
            {
                DropAtPlayerPosition(item);
            }

            // Collider2D[] collidersAtPosition = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y));

            // bool interactedWithLockable = false;

            // foreach (Collider2D collider in collidersAtPosition)
            //     {
            //         LockedObject lockedObject = collider.GetComponent<LockedObject>();
            //         if (lockedObject != null)
            //         {
            //             interactedWithLockable = true;

            //             if (item.GetItemType() == "Key")
            //             {
            //                 Key key = item.GetRelatedGameObject().GetComponent<Key>();
            //                 if (key != null)
            //                 {
            //                     // Coba membuka kunci dengan keyID
            //                     bool unlockSuccess = lockedObject.TryUnlock(key.keyName);

            //                     if (unlockSuccess)
            //                     {
            //                         // Jika berhasil membuka kunci, hapus item dari inventaris
            //                         InventoryController.instance.RemoveItem(item.GetInventory(), item.GetItemType(), item.GetAmount());
            //                         // Hancurkan game object item jika diperlukan
            //                         Destroy(item.GetRelatedGameObject());
            //                     }
            //                     else
            //                     {
            //                         Debug.Log("Kunci tidak cocok.");
            //                     }
            //                 }
            //                 else
            //                 {
            //                     Debug.LogWarning("Item tidak memiliki komponen KeyItem.");
            //                 }
            //             }
            //             else
            //             {
            //                 Debug.Log("Item bukan kunci.");
            //             }
            //             break; // Keluar dari loop setelah berinteraksi dengan lockable object
            //         }
            //     }

            // if (!interactedWithLockable)
            // {
            //     // Jika tidak ada interaksi dengan lockable object, jatuhkan item seperti biasa
            //     for (int i = 0; i < item.GetAmount(); i++)
            //     {
            //         Instantiate(item.GetRelatedGameObject(), pos, Quaternion.identity);
            //     }
            // }
        }
        

    


        private bool IsPositionInsideDropArea(Vector3 position)
        {
            Vector2 position2D = new Vector2(position.x, position.y);
            return dropAreaCollider.OverlapPoint(position2D);
        }
        private void DropAtPlayerPosition(InventoryItem item)
        {
        if (player != null)
        {
            Vector3 playerPos = player.transform.position;

            for (int i = 0; i < item.GetAmount(); i++)
            {
                Instantiate(item.GetRelatedGameObject(), playerPos, Quaternion.identity);
            }
    }
}




}
