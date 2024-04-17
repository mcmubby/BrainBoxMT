namespace Application.Pokemons.Exceptions
{
    public class ExistingRecordException : Exception
    {
        public ExistingRecordException() : base("Similar Product record exists! Verify product name") { }
    }
}
