using BenchmarkDotNet.Attributes;
using Contract.Enumerations;

namespace Benchmarks;

public class SwitchAndHashSetBenchMark
{
    private static readonly Dictionary<AppointmentStatus, HashSet<AppointmentStatus>> ValidStatusTransitions = new()
        {
            { AppointmentStatus.Waiting, new HashSet<AppointmentStatus> { AppointmentStatus.Received, AppointmentStatus.Refused } },
            { AppointmentStatus.Received, new HashSet<AppointmentStatus> { AppointmentStatus.Completed } }
        };

    [Params(AppointmentStatus.Waiting, AppointmentStatus.Received)]
    public AppointmentStatus CurrentStatus;

    [Params(AppointmentStatus.Received, AppointmentStatus.Refused, AppointmentStatus.Completed)]
    public AppointmentStatus NewStatus;

    [Benchmark]
    public bool IsValidStatusTransitionWithSwitch()
    {
        return CurrentStatus switch
        {
            AppointmentStatus.Waiting => NewStatus == AppointmentStatus.Received || NewStatus == AppointmentStatus.Refused,
            AppointmentStatus.Received => NewStatus == AppointmentStatus.Completed,
            _ => false
        };
    }

    [Benchmark]
    public bool IsValidStatusTransitionWithHashSet()
    {
        return ValidStatusTransitions.TryGetValue(CurrentStatus, out var validStatuses) && validStatuses.Contains(NewStatus);
    }



    /*===========================Result=============================
  
    | Method                             | CurrentStatus | NewStatus | Mean      | Error     | StdDev    | Median    |
    |----------------------------------- |-------------- |---------- |----------:|----------:|----------:|----------:|
    | IsValidStatusTransitionWithSwitch  | Waiting       | Received  | 0.1688 ns | 0.0623 ns | 0.1838 ns | 0.0986 ns |
    | IsValidStatusTransitionWithHashSet | Waiting       | Received  | 7.0589 ns | 0.1769 ns | 0.3491 ns | 7.0172 ns |
    | IsValidStatusTransitionWithSwitch  | Waiting       | Completed | 0.3625 ns | 0.1076 ns | 0.3172 ns | 0.2788 ns |
    | IsValidStatusTransitionWithHashSet | Waiting       | Completed | 6.4609 ns | 0.1585 ns | 0.1946 ns | 6.4208 ns |
    | IsValidStatusTransitionWithSwitch  | Waiting       | Refused   | 0.0468 ns | 0.0390 ns | 0.1069 ns | 0.0000 ns |
    | IsValidStatusTransitionWithHashSet | Waiting       | Refused   | 7.2235 ns | 0.0896 ns | 0.0748 ns | 7.2269 ns |
    | IsValidStatusTransitionWithSwitch  | Received      | Received  | 0.2886 ns | 0.0800 ns | 0.2359 ns | 0.2720 ns |
    | IsValidStatusTransitionWithHashSet | Received      | Received  | 6.2240 ns | 0.1464 ns | 0.1369 ns | 6.1971 ns |
    | IsValidStatusTransitionWithSwitch  | Received      | Completed | 0.1278 ns | 0.0545 ns | 0.1607 ns | 0.0115 ns |
    | IsValidStatusTransitionWithHashSet | Received      | Completed | 7.4681 ns | 0.1772 ns | 0.2366 ns | 7.4204 ns |
    | IsValidStatusTransitionWithSwitch  | Received      | Refused   | 0.2228 ns | 0.1040 ns | 0.3068 ns | 0.0000 ns |
    | IsValidStatusTransitionWithHashSet | Received      | Refused   | 6.0836 ns | 0.1291 ns | 0.1723 ns | 6.0150 ns |

     */






}
