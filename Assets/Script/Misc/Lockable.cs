using UnityEngine;

public class LockedObject : MonoBehaviour
{
    public string requiredKeyID; // ID kunci yang dibutuhkan
    private bool isUnlocked = false;

    // Fungsi untuk mencoba membuka kunci
    public bool TryUnlock(string keyID)
    {
        if (isUnlocked)
        {
            Debug.Log("Objek sudah terbuka.");
            return false;
        }

        if (keyID == requiredKeyID)
        {
            isUnlocked = true;
            Unlock();
            return true;
        }
        else
        {
            Debug.Log("Kunci tidak cocok.");
            return false;
        }
    }

    // Fungsi untuk membuka kunci
    private void Unlock()
    {
        Debug.Log("Objek terbuka!");
        // Tambahkan logika untuk membuka pintu, membuka peti, dll.
        // Misalnya, mengaktifkan animasi atau mengubah state
    }
}
