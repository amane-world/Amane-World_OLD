using System;
using System.IO;
using System.Linq;
using Logger;

namespace AWO
{
  class ChunkBlockStructure
  {
    public int id;
    public float pos;
    public int rotX;
    public int rotY;
    public int rotZ;
  }
  class ChunkStructure
  {
    public int x;
    public int y;
    public int z;
    public ChunkBlockStructure[] blocks;
  }
  class Converter
  {
    public static byte[] StringToByte(string text)
    {
      return System.Text.Encoding.ASCII.GetBytes(text);
    }
  }
  class Chunk
  {
    public static bool ExistChunk(int x, int y, int z)
    {
      if (File.Exists($"worlds/master/{x}_{y}_{z}.awo"))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public static bool GenerateChunk(int x, int y, int z)
    {
      string path = $"{Directory.GetCurrentDirectory()}/worlds/master/{x}.{y}.{z}.awo";
      FileStream fs = new FileStream(path, FileMode.Create);
      BinaryWriter bw = new BinaryWriter(fs);
      bw.Write(4);    // <header> length
      bw.Write(8);    // <id> 
      bw.Write(17.4F); // <pos> 16x16x16(float)
      bw.Write(45);   // <rotx> 0~359 
      bw.Write(45);   // <roty> 0~359 
      bw.Write(45);   // <rotz> 0~359 
                      // extra (ex. light on,off)
      bw.Close();
      fs.Close();
      return true;
    }

    public static void LoadChunkBlock(int x, int y, int z)
    {
      string path = $"{Directory.GetCurrentDirectory()}/worlds/master/{x}.{y}.{z}.awo";
      int length;
      int id;
      float pos;
      int rotX;
      int rotY;
      int rotZ;

      using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
      {
        length = reader.ReadInt32();
        id = reader.ReadInt32();
        pos = reader.ReadSingle();
        rotX = reader.ReadInt32();
        rotY = reader.ReadInt32();
        rotZ = reader.ReadInt32();
      }
      // todo 複数ブロックの非同期読み込み
      Log.info(length.ToString());
      Log.info(id.ToString());
      Log.info(pos.ToString());
      Log.info(rotX.ToString());
      Log.info(rotY.ToString());
      Log.info(rotZ.ToString());
    }
  }
}