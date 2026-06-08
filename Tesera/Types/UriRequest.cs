namespace Tesera
{
	public abstract class UriRequest
	{
		public virtual string GetUri() => $"https://api.tesera.ru{this.GetType().FullName?.Remove(0, "Tesera.API".Length).Replace('.', '/').Replace('+', '/')}";
	}
}