using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.GameSave.IO
{
  public static class ByteOrderInterpreter
  {
    public static long GetNextByteOffset(long offset, ByteOrder byteOrder)
    {
      if(byteOrder == ByteOrder.BigEndian)
        return offset;

      var offsetIsEven = offset % 2 == 0;
      return offsetIsEven
        ? offset + 3
        : offset - 1;
    }
  }
}
