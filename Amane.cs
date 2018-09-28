using System;
using Logger;
using AWO;

namespace Amane
{
  class Program
  {
    public const string version = "0.1.0";
    static void Main(string[] args)
    {
      Log.info("=========================");
      Log.info($"|    version: {version}     |");
      Log.info("==========================");
      Log.info("天音システムを起動しています...");
      if (!Chunk.ExistChunk(0, 0, 0))
      {
        Log.info("ワールドの中心が存在しません、生成を開始します...");
        if (Chunk.GenerateChunk(0, 0, 0))
        {
          Log.info("ワールドの中心を生成しました");
          Chunk.LoadChunkBlock(0, 0, 0);
        }
        else
        {
          Log.err("ワールドの中心を生成出来ませんでした");
        }
      }
    }
  }
}
