using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    public Image[] imageComponents; // Canvas üzerindeki Image bileşenlerini tutacak dizi
    public Sprite[] imageSprites; // Resimlerin sprite'larını tutacak dizi

    private int currentComponentIndex = 0; // Hangi Image bileşenine resim atanacağını belirleyen indis
    public SlowTime _SlowTime;
    public Uı _Uı;

    void Start()
    {
        _SlowTime = FindObjectOfType<SlowTime>();
        _Uı = FindObjectOfType<Uı>();
        // Image bileşenlerini diziye atama
        imageComponents = new Image[3];

        // Eğer Image bileşenleri direk olarak bu GameObject'in altındaysa:
         
        for (int i = 0; i < 3; i++)
        {
            imageComponents[i] = transform.GetChild(i).GetComponent<Image>();
        }
         
        // Eğer Image bileşenleri farklı GameObject'lerin altındaysa:
        // Örneğin, Canvas altındaki bir alt GameObject'e Image bileşenleri eklenmişse:
        // imageComponents = GameObject.Find("Canvas/AltGameObject").GetComponentsInChildren<Image>();
    }

    void Update()
    {
        // Klavyeden tuş girişlerini dinleme
        if (Input.anyKeyDown)
        {
            // Klavyeden girilen tuşa göre resim atama işlemi
            if (Input.GetKeyDown(KeyCode.Q))
                SetImage(0);
            else if (Input.GetKeyDown(KeyCode.E))
                SetImage(1);
            else if (Input.GetKeyDown(KeyCode.R))
                SetImage(2);
        }
    }

    // Image bileşenine resim atayan fonksiyon
    void SetImage(int spriteIndex)
    {
        // Daha önce atanmış bir resmi tekrar atamamaya kontrol etme
        for (int i = 0; i < currentComponentIndex; i++)
        {
            if (imageComponents[i].sprite == imageSprites[spriteIndex])
            {
                Debug.Log("Bu resim zaten atanmış!");
                return;
            }
        }

        // İlgili Image bileşenine resmi atama
        imageComponents[currentComponentIndex].sprite = imageSprites[spriteIndex];  

        // Bir sonraki Image bileşenine geçme
        currentComponentIndex++;

        // Eğer son Image bileşenine ulaşıldıysa Invoke ile ClearSprites fonksiyonunu çağır
        if (currentComponentIndex == imageComponents.Length)
        {
            Debug.Log("Son resim atandı");
            Invoke("ClearSprites", 0.01f);
            
        }
    }

    // Image bileşenlerindeki sprite'ları temizleyen fonksiyon
    void ClearSprites()
    {
        for (int i = 0; i < currentComponentIndex; i++)
        {
            imageComponents[i].sprite = null;
            
        }
        currentComponentIndex = 0;
        _SlowTime.CancelSlowMotion();
        _Uı.CanvasClose();
        
        Debug.Log("Tüm Image bileşenlerindeki sprite'lar silindi!");
    }
}
