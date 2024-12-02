using H3;
using H3.Algorithms;

namespace Contract.Services.V1;

public interface IH3Service
{
    H3Index GetH3IndexFromLatLng(double latitude, double longitude);
    IEnumerable<H3Index> GetNeighbours(H3Index h3Index);
    IEnumerable<H3Index> GetNeighboursFromLatLng(double latitude, double longitude);
    IEnumerable<RingCell> GetH3IndexesInRadius(double latitude, double longitude, int radiusInMeters);
    bool IsH3IndexWithinRadius(H3Index h3Index, double latitude, double longitude, int radiusInMeters);

    int GetDistance(H3Index h3IndexStart, H3Index h3IndexEnd);
    bool IsValidH3Index(string h3IndexString);
    bool IsH3IndexesNeighbour(H3Index h3Index, H3Index h3Indexneighbour);

}
