﻿namespace ConsoleApp1.BSG.SystemInformation;

public enum GpuControllerAvailability
{
    NotSet,
    Other,
    Unknown,
    RunningFullPower,
    Warning,
    InTest,
    NotApplicable,
    PowerOff,
    OffLine,
    OffDuty,
    Degraded,
    NotInstalled,
    InstallError,
    PowerSaveUnknown,
    PowerSaveLowPowerMode,
    PowerSaveStandby,
    PowerCycle,
    PowerSaveWarning,
    Paused,
    NotReady,
    NotConfigured,
    Quiesced
}