using Contract.Services.V1;
using H3;
using H3.Algorithms;
using H3.Extensions;
using H3.Model;

namespace Infrastructure.Services;
public sealed class H3Service : IH3Service
{
    private const int Resolution = 8;
    public H3Service() { }

    /// <summary>
    /// Calculates the distance between two H3 indexes.
    /// </summary>
    /// <param name="h3IndexStart"></param>
    /// <param name="h3IndexEnd"></param>
    /// <returns></returns>
    public int GetDistance(H3Index h3IndexStart, H3Index h3IndexEnd)
        => h3IndexStart.GridDistance(h3IndexEnd);

    /// <summary>
    /// Find RingCells within a given radius
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <param name="radiusInMeters">Radius (Meters) of the LatLng to be found</param>
    /// <returns>Return IEnumerable RingCell include H3Index and Distance.</returns>
    public IEnumerable<RingCell> GetH3IndexesInRadius(double latitude, double longitude, int rangeInMeters)
    {
        var h3Index = GetH3IndexFromLatLng(latitude, longitude);
        int numRings = CalculateNumRings(h3Index, rangeInMeters);
        return h3Index.GridDiskDistances(numRings); // Count of list is f(n) = 6 * numRings + 1
    }

    /// <summary>
    /// Calculate the number of Rings needed to cover the range
    /// </summary>
    /// <param name="h3Index"></param>
    /// <param name="radiusInMeters">Range(Meters) to be found</param>
    /// <returns></returns>
    private static int CalculateNumRings(H3Index h3Index, int rangeInMeters)
    {
        // Get Radius in Meters 
        double r = h3Index.GetRadiusInKm() * 1000;
        double d = 2 * r;

        // Get NumberRings to cover Range
        int numRings = (int)Math.Ceiling((rangeInMeters - r) / d);
        return numRings;
    }

    /// <summary>
    /// Transfer LatLng to H3Index
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <returns></returns>
    public H3Index GetH3IndexFromLatLng(double latitude, double longitude)
    {
        var latLng = new LatLng(latitude, longitude);
        return H3Index.FromLatLng(latLng, Resolution);
    }

    /// <summary>
    /// Find all neighbours of H3Index
    /// </summary>
    /// <param name="h3Index"></param>
    /// <returns></returns>
    public IEnumerable<H3Index> GetNeighbours(H3Index h3Index)
        => h3Index.GetNeighbours();

    /// <summary>
    /// Find all neighbours of LatLng
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <returns></returns>
    public IEnumerable<H3Index> GetNeighboursFromLatLng(double latitude, double longitude)
    {
        var h3Index = GetH3IndexFromLatLng(latitude, longitude);
        return GetNeighbours(h3Index);
    }

    /// <summary>
    /// Checks if two H3 indexes are neighbors.
    /// </summary>
    /// <param name="h3Index"></param>
    /// <param name="h3IndexNeighbour"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool IsH3IndexesNeighbour(H3Index h3Index, H3Index h3IndexNeighbour)
        => h3Index.IsNeighbour(h3IndexNeighbour);

    /// <summary>
    /// Check if H3Index is within the radius of the LatLng?
    /// </summary>
    /// <param name="h3Index">H3Index need to check</param>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <param name="radiusInMeters"></param>
    /// <returns></returns>
    public bool IsH3IndexWithinRadius(H3Index h3Index, double latitude, double longitude, int radiusInMeters)
    {
        var h3IndexesInRadius = GetH3IndexesInRadius(latitude, longitude, radiusInMeters).Select(x => x.Index);
        return h3IndexesInRadius.Contains(h3Index);
    }

    /// <summary>
    /// Checks if a string is a valid H3 index.
    /// </summary>
    /// <param name="h3IndexString">The string to check.</param>
    /// <returns></returns>
    public bool IsValidH3Index(string h3IndexString)
    {
        try
        {
            H3Index h3Index = new H3Index(h3IndexString);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
