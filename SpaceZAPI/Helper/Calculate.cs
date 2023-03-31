using System;
namespace SpaceZAPI.Helper
{
    public class Calculate
    {
        public double CalculateTimeToOrbit(double orbitRadius, DateTime launchTime)
        {
            // Calculate time-to-orbit in seconds
            double timeToOrbit = (orbitRadius / 3600) + 10;

            // Calculate the launch time in UTC
            DateTime launchTimeUtc = launchTime.ToUniversalTime();

            // Add the time-to-orbit to the launch time
            DateTime timeToOrbitUtc = launchTimeUtc.AddSeconds(timeToOrbit);

            // Calculate the time difference between the current time and the time-to-orbit
            TimeSpan timeDifference = timeToOrbitUtc.Subtract(DateTime.UtcNow);

            // Round off the seconds
            double roundedSeconds = Math.Round(timeDifference.TotalSeconds);

            // Return the rounded time-to-orbit in seconds
            return roundedSeconds;
        }
    }
}

