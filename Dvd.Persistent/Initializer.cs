namespace Library.Persistent
{
	public static class Initializer
	{
		public static void Initialize(this Context context)
		{
			_ = context.Database.EnsureCreated();

			_ = context.Database.EnsureCreated();
		}
	}
}
