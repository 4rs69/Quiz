using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;


namespace OpenAI
{
    public class AIImageGenerator : MonoBehaviour
    {
        [SerializeField] private InputField _inputField;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _loadingLabel;

        private readonly OpenAIApi _openai = new();

        private void Start()
        {
            _button.onClick.AddListener(SendImageRequest);
        }

        private async void SendImageRequest()
        {
            _image.sprite = null;
            _button.enabled = false;
            _inputField.enabled = false;
            _loadingLabel.SetActive(true);

            var dallEResponse = await _openai.CreateImage(new CreateImageRequest
            {
                Prompt = _inputField.text,
                Size = ImageSize.Size1024
            });

            if (dallEResponse.Data is {Count: > 0})
            {
                using var request = new UnityWebRequest(dallEResponse.Data[0].Url);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Access-Control-Allow-Origin", "*");
                request.SendWebRequest();

                while (!request.isDone) await Task.Yield();

                var sprite = CreateSprite(request);
                _image.sprite = sprite;
            }
            else
            {
                Debug.LogWarning("No image was created from this prompt.");
            }

            _button.enabled = true;
            _inputField.enabled = true;
            _loadingLabel.SetActive(false);
        }

        private Sprite CreateSprite(UnityWebRequest request)
        {
            var texture = new Texture2D(2, 2);
            texture.LoadImage(request.downloadHandler.data);
            var sprite = Sprite.Create(texture, new Rect(0, 0, 1024, 1024), Vector2.zero, 1f);
            return sprite;
        }
    }
}
