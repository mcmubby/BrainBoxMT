namespace Application.Pokemons.Exceptions
{
    public class GenericNotFoundException : Exception
    {
        public GenericNotFoundException(string Name) : base($"{Name} does not exist") { }
    }
}
