using System;

public class GameInfo
{
    public int major;
    public int minor;
    public int patch;
    public bool isLinkGPGS;
    public TimeSpan totalPlayTime;

    public GameInfo(int major, int minor, int patch, bool isLinkGPGS, TimeSpan totalPlayTime)
    {
        this.major = major;
        this.minor = minor;
        this.patch = patch;
        this.isLinkGPGS = isLinkGPGS;
        this.totalPlayTime = totalPlayTime;
    }

}
