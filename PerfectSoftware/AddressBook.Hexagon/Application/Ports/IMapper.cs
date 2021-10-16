//By Bart Vertongen copyright 2021.


namespace PS.AddressBook.Hexagon.Application.Ports
{
    /// <summary>
    /// Generic Mapper Interface
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget">The Mapper should have the target type as prefix</typeparam>
    /// <remarks>
    /// A Mapper is not an Adapter.
    /// A Mapper goes in two directions, an Adapter in one.
    /// </remarks>
    public interface IMapper<TSource, TTarget>
    {
        TTarget MapTo(TSource source);

        TSource MapFrom(TTarget target);
    }
}