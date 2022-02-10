using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Entities
{
  public class Player
  {
    public string SavedName { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string PlayerNumber { get; private set; }
  }

  public class Appearance
  {
    public int FaceId { get; private set; }
    public SkinColor SkinColor { get ; private set; }
    public EyeColor EyeColor { get; private set; }
    public HairStyle HairStyle { get; private set; }
  }

  public enum SkinColor { One, Two, Three, Four, Five }
  public enum EyeColor { Brown, Blue }
  public enum HairStyle { }
}
