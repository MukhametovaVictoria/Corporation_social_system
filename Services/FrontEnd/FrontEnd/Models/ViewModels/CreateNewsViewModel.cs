using EmojiData;

namespace FrontEnd.Models
{
	public class CreateNewsViewModel
	{
		public NewsViewModel News { get; set; }
		public List<EmojiViewModel> Emoji { get; set; }
    }
}
