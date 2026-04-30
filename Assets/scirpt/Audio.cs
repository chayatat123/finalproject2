using UnityEngine;

public class PlaySoundOnTouch : MonoBehaviour
{
    // ลากไฟล์เสียง (AudioClip) มาใส่ในช่องนี้ที่หน้า Inspector
    public AudioClip soundEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบ Tag ก่อน (ถ้าต้องการ) เช่น if (other.CompareTag("Player"))
        if (soundEffect != null)
        {
            // สร้างเสียงที่ตำแหน่งของวัตถุนี้
            AudioSource.PlayClipAtPoint(soundEffect, transform.position);
        }

        // ลบวัตถุนี้ทิ้งได้เลย โดยที่เสียงยังคงเล่นอยู่จนจบ
        Destroy(gameObject);
    }
}
