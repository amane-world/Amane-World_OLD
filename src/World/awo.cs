using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Logger;
using System.Threading.Tasks;

namespace AWO
{
  class ChunkBlockStructure
  {
    public int id;
    public float pos;
    public int rotX;
    public int rotY;
    public int rotZ;
    public float extra;

    public ChunkBlockStructure(int id, float pos, int rotX, int rotY, int rotZ, float extra)
    {
      this.id = id;
      this.pos = pos;
      this.rotX = rotX;
      this.rotY = rotY;
      this.rotZ = rotZ;
      this.extra = extra;
    }
  }

  class ChunkStructure
  {
    public int x;
    public int y;
    public int z;
    public List<ChunkBlockStructure> blocks;

    public ChunkStructure(int x, int y, int z, List<ChunkBlockStructure> blocks)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.blocks = blocks;
    }
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
      bw.Write(4096 - 1); // <header> length

      for (int i = 0; i < 256; i++)
      {
        // Grass Block
        // <id>
        bw.Write(2);
        // <pos> 16x16x16(float)
        bw.Write(Convert.ToSingle(i));
        // <rotx> 0~359
        bw.Write(0);
        // <roty> 0~359
        bw.Write(0);
        // <rotz> 0~359
        bw.Write(0);
        // extra (ex. light on,off)
        bw.Write(0.0F);
      }
      for (int i = 0; i < 3840; i++)
      {
        // Dirt Block
        bw.Write(2);     // <id> 
        bw.Write(Convert.ToSingle(i)); // <pos> 16x16x16(float)
        bw.Write(0);    // <rotx> 0~359 
        bw.Write(0);    // <roty> 0~359 
        bw.Write(0);   // <rotz> 0~359 
        bw.Write(0.0F);  // extra (ex. light on,off)
      }

      bw.Close();
      fs.Close();
      return true;
    }

    public static ChunkStructure LoadChunk(int x, int y, int z)
    {
      return new ChunkStructure(x, y, z, LoadChunkBlocks(x, y, z));
    }

    private static List<ChunkBlockStructure> LoadChunkBlocks(int x, int y, int z)
    {
      string path = $"{Directory.GetCurrentDirectory()}/worlds/master/{x}.{y}.{z}.awo";
      int length = 0;
      List<ChunkBlockStructure> blocks = new List<ChunkBlockStructure>();

      using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
      {
        // header
        length = reader.ReadInt32();
        Log.info(length.ToString());
        // body
        for (int i = 0; i < length; i++)
        {
          blocks.Add(new ChunkBlockStructure(reader.ReadInt32(), reader.ReadSingle(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadSingle()));
        }
      }

      // foreach (ChunkBlockStructure block in blocks)
      // {
      //   Log.info(block.id.ToString());
      //   Log.info(block.pos.ToString());
      //   Log.info(block.rotX.ToString());
      //   Log.info(block.rotY.ToString());
      //   Log.info(block.rotZ.ToString());
      //   Log.info(block.extra.ToString());
      // }

      Log.info("END");

      return blocks;
    }
  }
}