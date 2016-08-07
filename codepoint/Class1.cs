using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace codepoint
{
    public class Class1
    {
        public byte[] codelink(string xml_path, string ori_path)
        {
            FileStream fs = new FileStream(xml_path, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(codepoint.Model.font));
            codepoint.Model.font model = (codepoint.Model.font)serializer.Deserialize(fs);
            List<byte[]> codeList = new List<byte[]>();
            //xmlから各要素を読み込んでバイト配列に変換
            codeList.Add(BitConverter.GetBytes(model.chars.count));
            foreach (codepoint.Model.chr chr in model.chars.chrs)
            {
                codeList.Add(BitConverter.GetBytes(chr.id));
                codeList.Add(BitConverter.GetBytes(chr.x));
                codeList.Add(BitConverter.GetBytes(chr.y));
                codeList.Add(BitConverter.GetBytes(chr.width));
                codeList.Add(BitConverter.GetBytes(chr.height));
                codeList.Add(BitConverter.GetBytes(chr.xoffset));
                codeList.Add(BitConverter.GetBytes(chr.yoffset));
                codeList.Add(BitConverter.GetBytes(chr.xadvance));
                codeList.Add(BitConverter.GetBytes(chr.chnl));
            }
            //末端の処理
            for (int i = 0; i < 7; i++)
            {
                codeList.Add(BitConverter.GetBytes(0));
            }
            fs.Close();

            //パッチを当てる座標ファイルを開く
            FileStream orifs = new FileStream(ori_path, FileMode.Open);
            BinaryReader br = new BinaryReader(orifs);
            byte[] oriHeader = br.ReadBytes(60);
            //画像のW&Hは不要なのでシーク
            orifs.Seek(8, SeekOrigin.Current);
            //フォント名のバイト長を取得
            int namecount = BitConverter.ToInt32(br.ReadBytes(4), 0);
            //フォント名を取得
            byte[] fontname = br.ReadBytes(namecount);
            byte[] space = new byte[] { 0 };
            for (int i = 0; i < 4 - namecount%4; i++)
            {               
                fontname = fontname
                    .Concat(space)
                    .ToArray();
            }
            //oriHeaderに画像のW&H, フォント名のバイト長, フォント名を追加
            byte[] newHeader = oriHeader
                .Concat(BitConverter.GetBytes(model.common.scaleW))
                .Concat(BitConverter.GetBytes(model.common.scaleH))
                .Concat(BitConverter.GetBytes(namecount))
                .Concat(fontname)
                .ToArray();

            byte[] codeArray = codeList
                .SelectMany(a => a)
                .ToArray();

            byte[] codepoint = newHeader
                .Concat(codeArray)
                .ToArray();

            orifs.Close();

            return codepoint;
        }
    }
}
