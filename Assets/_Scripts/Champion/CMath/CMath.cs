using UnityEngine;
using System.Collections;
using System;

#region CMath
/// <summary>
/// Useful Helper Functions
/// </summary>
namespace Champion {
    public struct CMath
    {

	    /// <summary>
	    /// Re-maps a number from one range to another.
	    /// </summary>
	    /// <param name="OldMin">Old minimum.</param>
	    /// <param name="OldMax">Old max.</param>
	    /// <param name="NewMin">New minimum.</param>
	    /// <param name="NewMax">New max.</param>
	    /// <param name="OldValue">Old value.</param>
	    public static float Map (float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
	    {
		    float OldRange = (OldMax - OldMin);
		    float NewRange = (NewMax - NewMin);
		    float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
		    return(NewValue);
	    }

	    public static float PHI = 1.6180339887f;//(1 + Mathf.Sqrt(5)) / 2;

	    /// <summary>
	    /// Returns a Given Color's Hex Code
	    /// </summary>
	    /// <returns>The Color in Hex</returns>
	    /// <param name="color">Color</param>
	    /// <param name="addHashtag">Add a Hashtag?</param>
	    public static string ColorToHex(Color32 color, bool addHashtag = true){
		    Int32 r = color.r;
		    Int32 g = color.g;
		    Int32 b = color.b;
		    string s = "";
		    if(addHashtag)
			    s += "#";
		    s += r.ToString ("x2");
		    s += g.ToString ("x2");
		    s += b.ToString ("x2");
		    return s;
	    }
    }
    #endregion

    #region GPS
    /// <summary>
    /// GPS coordinate
    /// </summary>
    public struct GPS
    {
	    public double longitude;
	    public double latitude;

	    public GPS (Vector2 lonlat)
	    {
		    this.longitude = lonlat.x;
		    this.latitude = lonlat.y;
	    }

	    public GPS (double longitude, double latitude)
	    {
		    this.longitude = longitude;
		    this.latitude = latitude;
	    }

	    public override string ToString ()
	    {
		    string s = longitude + " longitude, " + latitude + " latitude";
		    return s;
	    }

	    /// <summary>
	    /// Adds a given distance in meters to the gps coord.
	    /// </summary>
	    /// <returns>The gps coordanate the given distance away from the original gps</returns>
	    /// <param name="ew">distance east-west</param>
	    /// <param name="ns">distance north-south</param>
	    public GPS AddDistanceToLonLat (double ew, double ns)
	    {
		    GPS final = new GPS (); //lonLat
		    final.latitude = latitude + (180 / Mathf.PI) * (ns / 6378137); //lat
		    final.longitude = longitude + (180 / Mathf.PI) * (ew / 6378137) / Mathf.Cos ((float)latitude); //lon
		    return final;
	    }

	    /// <summary>
	    /// Adds a given distance in meters to the gps coord.
	    /// </summary>
	    /// <returns>The distance to lon lat.</returns>
	    /// <param name="distance">distance vector, x = ew, y = ns</param>
	    public GPS AddDistanceToLonLat (Vector2 distance)
	    {
		    return AddDistanceToLonLat (distance.x, distance.y);
	    }

	    /// <summary>
	    /// Get the distance between two GPS coords
	    /// </summary>
	    /// <returns>The distance in meters</returns>
	    /// <param name="a">The alpha component.</param>
	    /// <param name="b">The beta component.</param>
	    public static double DistanceBetween (GPS a, GPS b)
	    {
		    double aRad = Mathf.PI * a.latitude / 180;
		    double bRad = Mathf.PI * b.latitude / 180;
		    double theta = a.longitude - b.longitude;
		    double thetaRad = Mathf.PI * theta / 180; 

		    double dist = Math.Sin (aRad) * Math.Sin (bRad) + Math.Cos (aRad) * Math.Cos (bRad) * Math.Cos (thetaRad);
		    dist = Math.Acos (dist);
		    dist = dist * 180 / Mathf.PI;
		    dist = dist * 60 * 1.1515f;

		    //convert to kilometers
		    dist *= 1.609344f;
		    //convert to meters
		    dist *= 1000;
		    return dist;
	    }

	    /// <summary>
	    /// Distances to a given GPS
	    /// </summary>
	    /// <returns>The distance in Meters</returns>
	    /// <param name="a">The gps to find the distance to</param>
	    public double DistanceTo (GPS a)
	    {
		    return GPS.DistanceBetween (this, a);
	    }
	    /// <summary>
	    /// Returns new GPS with value zero zero
	    /// </summary>
	    /// <value>zero</value>
	    public static GPS Zero { get { return new GPS (0, 0); } }
	    /// <summary>
	    /// Returns a copy of the GPS
	    /// </summary>
	    public GPS Copy ()
	    {
		    GPS copy = new GPS (this.longitude, this.latitude);
		    return copy;
	    }
    }
        #endregion

}