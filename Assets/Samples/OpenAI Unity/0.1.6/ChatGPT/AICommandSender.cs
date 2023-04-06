using UnityEngine;
using UnityEngine.UI;

namespace OpenAI
{
    public class AICommandSender : MonoBehaviour
    {
        [SerializeField] private InputField _inputField;
        [SerializeField] private Button _button;
        [SerializeField] private Text _textArea;

        private readonly OpenAIApi openai = new();

        private string userInput;
        private string Instruction = "Act as a random stranger in a chat room and reply to the questions.\nQ: ";

        private void Start()
        {
            _button.onClick.AddListener(SendReply);
        }

        private async void SendReply()
        {
            userInput = _inputField.text;
            Instruction += $"{userInput}\nA: ";
            
            _textArea.text = "...";
            _inputField.text = "";

            _button.enabled = false;
            _inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
            {
                Prompt = Instruction,
                Model = "text-davinci-003",
                MaxTokens = 128
            });

            if (completionResponse.Choices is {Count: > 0})
            {
                _textArea.text = completionResponse.Choices[0].Text;
                Instruction += $"{completionResponse.Choices[0].Text}\nQ: ";
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            _button.enabled = true;
            _inputField.enabled = true;
        }
    }
}
